using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BancoDeSangreApp.Components
{
    /// <summary>
    /// Componente reutilizable para mostrar overlay de carga sobre cualquier control o formulario
    /// </summary>
    public class LoadingOverlay : Panel
    {
        private Timer animationTimer;
        private int rotationAngle = 0;
        private Label lblMessage;
        private Panel spinnerPanel;

        public string Message
        {
            get => lblMessage.Text;
            set => lblMessage.Text = value;
        }

        public LoadingOverlay()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Configuración del overlay principal
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(180, 30, 30, 30); // Semi-transparente oscuro
            this.Visible = false;

            // Panel contenedor central
            Panel containerPanel = new Panel
            {
                Size = new Size(200, 150),
                BackColor = Color.White,
                Location = new Point(
                    (this.Width - 200) / 2,
                    (this.Height - 150) / 2
                ),
                Anchor = AnchorStyles.None
            };
            containerPanel.Paint += ContainerPanel_Paint;

            // Panel para el spinner
            spinnerPanel = new Panel
            {
                Size = new Size(60, 60),
                Location = new Point(70, 30),
                BackColor = Color.Transparent
            };
            spinnerPanel.Paint += SpinnerPanel_Paint;

            // Label para el mensaje
            lblMessage = new Label
            {
                Text = "Cargando...",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 60),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Size = new Size(180, 30),
                Location = new Point(10, 100)
            };

            containerPanel.Controls.Add(spinnerPanel);
            containerPanel.Controls.Add(lblMessage);
            this.Controls.Add(containerPanel);

            // Centrar al cambiar tamaño
            this.Resize += (s, e) =>
            {
                containerPanel.Location = new Point(
                    (this.Width - containerPanel.Width) / 2,
                    (this.Height - containerPanel.Height) / 2
                );
            };

            // Timer para animación
            animationTimer = new Timer { Interval = 30 };
            animationTimer.Tick += (s, e) =>
            {
                rotationAngle = (rotationAngle + 10) % 360;
                spinnerPanel.Invalidate();
            };
        }

        private void ContainerPanel_Paint(object sender, PaintEventArgs e)
        {
            // Bordes redondeados
            using (GraphicsPath path = GetRoundedRectangle(
                ((Panel)sender).ClientRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                ((Panel)sender).Region = new Region(path);

                using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private void SpinnerPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TranslateTransform(30, 30); // Centro del panel
            e.Graphics.RotateTransform(rotationAngle);

            // Dibujar spinner circular
            int numCircles = 12;
            for (int i = 0; i < numCircles; i++)
            {
                float angle = i * (360f / numCircles);
                float alpha = 255 - (i * (255f / numCircles));

                using (SolidBrush brush = new SolidBrush(
                    Color.FromArgb((int)alpha, 33, 150, 243)))
                {
                    float x = (float)(20 * Math.Cos(angle * Math.PI / 180)) - 3;
                    float y = (float)(20 * Math.Sin(angle * Math.PI / 180)) - 3;
                    e.Graphics.FillEllipse(brush, x, y, 6, 6);
                }
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // Esquina superior izquierda
            path.AddArc(arc, 180, 90);

            // Esquina superior derecha
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Esquina inferior derecha
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Esquina inferior izquierda
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Muestra el overlay de carga
        /// </summary>
        public void Show(string message = "Cargando...")
        {
            Message = message;
            this.Visible = true;
            this.BringToFront();
            animationTimer.Start();
        }

        /// <summary>
        /// Oculta el overlay de carga
        /// </summary>
        public new void Hide()
        {
            animationTimer.Stop();
            this.Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Métodos de extensión para facilitar el uso del LoadingOverlay
    /// </summary>
    public static class LoadingOverlayExtensions
    {
        private static LoadingOverlay GetOrCreateOverlay(Control control)
        {
            // Buscar si ya existe un overlay
            foreach (Control c in control.Controls)
            {
                if (c is LoadingOverlay overlay)
                {
                    return overlay;
                }
            }

            // Crear nuevo overlay
            LoadingOverlay newOverlay = new LoadingOverlay();
            control.Controls.Add(newOverlay);
            newOverlay.BringToFront();
            return newOverlay;
        }

        /// <summary>
        /// Muestra el overlay de carga en el control
        /// </summary>
        public static void ShowLoading(this Control control, string message = "Cargando...")
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => ShowLoading(control, message)));
                return;
            }

            GetOrCreateOverlay(control).Show(message);
        }

        /// <summary>
        /// Oculta el overlay de carga del control
        /// </summary>
        public static void HideLoading(this Control control)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new Action(() => HideLoading(control)));
                return;
            }

            foreach (Control c in control.Controls)
            {
                if (c is LoadingOverlay overlay)
                {
                    overlay.Hide();
                    return;
                }
            }
        }
    }
}