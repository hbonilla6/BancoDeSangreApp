-- ===============================================================
-- 1. TABLA: EntidadesSalud
-- ===============================================================
-- Representa hospitales, clínicas o sedes donde opera el sistema.
-- Permite que el sistema sea multi-sede (varios bancos bajo una sola instancia).
CREATE TABLE EntidadesSalud (
    IdEntidad INT IDENTITY(1,1) PRIMARY KEY,            -- Clave primaria autoincremental
    Nombre NVARCHAR(150) NOT NULL,                      -- Nombre oficial del hospital o clínica
    Codigo NVARCHAR(20) UNIQUE NOT NULL,                -- Código interno único (ej. HSS01)
    Direccion NVARCHAR(250),                            -- Dirección física
    Telefono NVARCHAR(20),                              -- Teléfono de contacto
    Correo NVARCHAR(100),                               -- Correo institucional
    Activo BIT NOT NULL DEFAULT 1,                      -- Estado lógico (1=activo, 0=inactivo)
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME() -- Fecha y hora de registro
);
GO

-- ===============================================================
-- 2. TABLA: Roles
-- ===============================================================
-- Contiene los distintos roles del sistema (Administrador, Operador, Técnico, etc.)
-- Permite agregar roles nuevos sin modificar código.
CREATE TABLE Roles (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,                -- Identificador único del rol
    Nombre NVARCHAR(50) UNIQUE NOT NULL,                -- Nombre del rol
    Descripcion NVARCHAR(200),                          -- Descripción o propósito del rol
    NivelAcceso INT NOT NULL DEFAULT 1,                 -- Nivel de acceso jerárquico (1=básico, 5=administrador)
    FechaCreacion DATETIME2 DEFAULT SYSDATETIME()       -- Fecha de creación del rol
);
GO

-- ===============================================================
-- 3. TABLA: Usuarios
-- ===============================================================
-- Contiene los usuarios del sistema (administrativos, técnicos, etc.)
-- Asociados a una entidad (hospital o clínica).
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,            -- Identificador único del usuario
    IdEntidad INT NOT NULL,                             -- Relación con la entidad de salud
    Usuario NVARCHAR(50) NOT NULL UNIQUE,               -- Nombre de usuario para login
    ClaveHash NVARCHAR(255) NOT NULL,                   -- Contraseña en formato hash seguro
    NombreCompleto NVARCHAR(120) NOT NULL,              -- Nombre real del usuario
    Correo NVARCHAR(100),                               -- Correo electrónico institucional
    Telefono NVARCHAR(20),                              -- Teléfono de contacto
    Activo BIT NOT NULL DEFAULT 1,                      -- Estado (1=activo)
    UltimoAcceso DATETIME2 NULL,                        -- Última vez que ingresó al sistema
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(), -- Fecha de alta
    FechaModificacion DATETIME2 NULL,                   -- Fecha de última modificación
    CONSTRAINT FK_Usuarios_Entidad FOREIGN KEY (IdEntidad)
        REFERENCES EntidadesSalud(IdEntidad)            -- Enlace con la entidad de salud
        ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- ===============================================================
-- 4. TABLA: UsuarioRoles
-- ===============================================================
-- Relación N:N entre Usuarios y Roles.
-- Permite asignar múltiples roles a un mismo usuario.
CREATE TABLE UsuarioRoles (
    IdUsuario INT NOT NULL,                             -- Usuario asignado
    IdRol INT NOT NULL,                                 -- Rol asignado
    PRIMARY KEY (IdUsuario, IdRol),                     -- Evita duplicados del mismo par
    CONSTRAINT FK_UsuarioRoles_Usuario FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(IdUsuario)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_UsuarioRoles_Rol FOREIGN KEY (IdRol)
        REFERENCES Roles(IdRol)
        ON UPDATE CASCADE ON DELETE CASCADE
);
GO

-- ===============================================================
-- 5. TABLA: TiposSangre
-- ===============================================================
-- Catálogo estático de tipos de sangre, usado por donantes y donaciones.
CREATE TABLE TiposSangre (
    IdTipoSangre INT IDENTITY(1,1) PRIMARY KEY,         -- Identificador del tipo
    Codigo NVARCHAR(5) UNIQUE NOT NULL,                 -- Código estándar (ej. 'A+', 'O-')
    Descripcion NVARCHAR(50),                           -- Texto descriptivo
    FechaCreacion DATETIME2 DEFAULT SYSDATETIME()       -- Fecha de creación del registro
);
GO

-- ===============================================================
-- 6. TABLA: Donantes
-- ===============================================================
-- Registra personas que donan sangre.
CREATE TABLE Donantes (
    IdDonante INT IDENTITY(1,1) PRIMARY KEY,            -- Identificador único
    IdEntidad INT NOT NULL,                             -- Entidad a la que pertenece el registro
    Nombre NVARCHAR(100) NOT NULL,                      -- Nombre completo
    Documento NVARCHAR(20) UNIQUE NOT NULL,             -- DUI u otro identificador único
    IdTipoSangre INT NOT NULL,                          -- Relación al tipo de sangre
    Genero CHAR(1) CHECK (Genero IN ('M','F')),         -- Sexo del donante
    FechaNacimiento DATE,                               -- Fecha de nacimiento
    Telefono NVARCHAR(20),                              -- Teléfono
    Correo NVARCHAR(100),                               -- Correo electrónico
    Direccion NVARCHAR(200),                            -- Dirección
    Activo BIT NOT NULL DEFAULT 1,                      -- Estado (activo/inactivo)
    FechaRegistro DATETIME2 NOT NULL DEFAULT SYSDATETIME(), -- Fecha de registro
    CreadoPor INT NULL,                                 -- Usuario que lo registró
    CONSTRAINT FK_Donantes_Entidad FOREIGN KEY (IdEntidad)
    REFERENCES EntidadesSalud(IdEntidad)
    ON UPDATE CASCADE ON DELETE NO ACTION,
    CONSTRAINT FK_Donantes_TipoSangre FOREIGN KEY (IdTipoSangre)
        REFERENCES TiposSangre(IdTipoSangre)
        ON UPDATE CASCADE ON DELETE NO ACTION,
    CONSTRAINT FK_Donantes_CreadoPor FOREIGN KEY (CreadoPor)
    REFERENCES Usuarios(IdUsuario)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- ===============================================================
-- 7. TABLA: CentrosRecoleccion
-- ===============================================================
-- Registra los lugares o campañas donde se recolecta sangre.
CREATE TABLE CentrosRecoleccion (
    IdCentro INT IDENTITY(1,1) PRIMARY KEY,             -- Identificador del centro
    IdEntidad INT NOT NULL,                             -- Entidad a la que pertenece
    Nombre NVARCHAR(100) NOT NULL,                      -- Nombre del centro o campaña
    Direccion NVARCHAR(200),                            -- Ubicación o dirección
    Responsable NVARCHAR(100),                          -- Persona encargada
    Telefono NVARCHAR(20),                              -- Teléfono de contacto
    Activo BIT NOT NULL DEFAULT 1,                      -- Estado activo/inactivo
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSDATETIME(), -- Fecha de registro
    CreadoPor INT NULL,                                 -- Usuario que lo creó
    CONSTRAINT FK_Centros_Entidad FOREIGN KEY (IdEntidad)
        REFERENCES EntidadesSalud(IdEntidad)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_Centros_CreadoPor FOREIGN KEY (CreadoPor)
    REFERENCES Usuarios(IdUsuario)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- ===============================================================
-- 8. TABLA: Donaciones
-- ===============================================================
-- Contiene cada registro de donación de sangre realizada.
CREATE TABLE Donaciones (
    IdDonacion INT IDENTITY(1,1) PRIMARY KEY,           -- Identificador de donación
    IdDonante INT NOT NULL,                             -- Donante que realizó la donación
    IdCentro INT NULL,                                  -- Centro donde se realizó
    IdTipoSangre INT NOT NULL,                          -- Tipo de sangre donada
    CantidadML INT NOT NULL CHECK (CantidadML > 0),     -- Cantidad donada (mililitros)
    FechaRecoleccion DATE NOT NULL,                     -- Fecha de extracción
    FechaCaducidad DATE NOT NULL,                       -- Fecha límite de uso
    Estado NVARCHAR(20) DEFAULT 'Disponible' CHECK (Estado IN ('Disponible','Reservada','Asignada','Vencida','Inactiva')),
    Observaciones NVARCHAR(250),                        -- Comentarios adicionales
    FechaCreacion DATETIME2 DEFAULT SYSDATETIME(),      -- Fecha de registro
    CreadoPor INT NULL,                                 -- Usuario responsable
    CONSTRAINT FK_Donaciones_Donante FOREIGN KEY (IdDonante)
        REFERENCES Donantes(IdDonante)
        ON UPDATE CASCADE ON DELETE NO ACTION,

    CONSTRAINT FK_Donaciones_Centro FOREIGN KEY (IdCentro)
        REFERENCES CentrosRecoleccion(IdCentro)
        ON UPDATE NO ACTION ON DELETE NO ACTION,

    CONSTRAINT FK_Donaciones_TipoSangre FOREIGN KEY (IdTipoSangre)
    REFERENCES TiposSangre(IdTipoSangre)
    ON UPDATE NO ACTION ON DELETE NO ACTION,

    CONSTRAINT FK_Donaciones_CreadoPor FOREIGN KEY (CreadoPor)
        REFERENCES Usuarios(IdUsuario)
        ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- ===============================================================
-- 9. TABLA: SolicitudesMedicas
-- ===============================================================
-- Representa solicitudes de unidades sanguíneas por parte de centros o médicos.
CREATE TABLE SolicitudesMedicas (
    IdSolicitud INT IDENTITY(1,1) PRIMARY KEY,          -- Identificador único
    IdEntidad INT NOT NULL,                             -- Entidad que gestiona la solicitud
    Solicitante NVARCHAR(150) NOT NULL,                 -- Nombre del médico o área solicitante
    IdTipoSangre INT NOT NULL,                          -- Tipo de sangre requerido
    CantidadSolicitada INT NOT NULL CHECK (CantidadSolicitada > 0),
    Prioridad NVARCHAR(20) NOT NULL CHECK (Prioridad IN ('Emergencia','Programada','Normal')),
    Estado NVARCHAR(20) DEFAULT 'Pendiente' CHECK (Estado IN ('Pendiente','Aprobada','Rechazada','Atendida')),
    Observaciones NVARCHAR(250),                        -- Comentarios adicionales
    FechaSolicitud DATETIME2 DEFAULT SYSDATETIME(),     -- Fecha de creación
    FechaAtencion DATETIME2 NULL,                       -- Fecha de atención de la solicitud
    CreadoPor INT NULL,                                 -- Usuario que creó la solicitud
    CONSTRAINT FK_Solicitudes_Entidad FOREIGN KEY (IdEntidad)
        REFERENCES EntidadesSalud(IdEntidad)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_Solicitudes_TipoSangre FOREIGN KEY (IdTipoSangre)
        REFERENCES TiposSangre(IdTipoSangre)
        ON UPDATE CASCADE ON DELETE NO ACTION,
    CONSTRAINT FK_Solicitudes_CreadoPor FOREIGN KEY (CreadoPor)
    REFERENCES Usuarios(IdUsuario)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);
GO

-- ===============================================================
-- 10. TABLA: SolicitudesDonaciones
-- ===============================================================
-- Relación N:N entre Solicitudes y Donaciones.
-- Permite asignar varias unidades a una solicitud.
CREATE TABLE SolicitudesDonaciones (
    IdSolicitud INT NOT NULL,                           -- Solicitud médica
    IdDonacion INT NOT NULL,                            -- Donación asignada
    FechaAsignacion DATETIME2 DEFAULT SYSDATETIME(),    -- Fecha de asignación
    PRIMARY KEY (IdSolicitud, IdDonacion),              -- Evita duplicados
    CONSTRAINT FK_SolicitudesDonaciones_Solicitud FOREIGN KEY (IdSolicitud)
        REFERENCES SolicitudesMedicas(IdSolicitud)
        ON UPDATE NO ACTION ON DELETE CASCADE,  -- si se borra la solicitud, se borra su relación

    CONSTRAINT FK_SolicitudesDonaciones_Donacion FOREIGN KEY (IdDonacion)
        REFERENCES Donaciones(IdDonacion)
        ON UPDATE NO ACTION ON DELETE NO ACTION  -- sin cascada para romper el ciclo
);
GO

-- ===============================================================
-- 11. TABLA: InventarioSangre
-- ===============================================================
-- Controla la cantidad disponible de sangre por tipo y por entidad.
CREATE TABLE InventarioSangre (
    IdEntidad INT NOT NULL,                             -- Entidad (hospital o sede)
    IdTipoSangre INT NOT NULL,                          -- Tipo de sangre
    CantidadUnidades INT NOT NULL DEFAULT 0 CHECK (CantidadUnidades >= 0),
    UltimaActualizacion DATETIME2 DEFAULT SYSDATETIME(),-- Última vez que se actualizó
    PRIMARY KEY (IdEntidad, IdTipoSangre),              -- Clave compuesta
    CONSTRAINT FK_Inventario_Entidad FOREIGN KEY (IdEntidad)
        REFERENCES EntidadesSalud(IdEntidad)
        ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT FK_Inventario_TipoSangre FOREIGN KEY (IdTipoSangre)
        REFERENCES TiposSangre(IdTipoSangre)
        ON UPDATE CASCADE ON DELETE NO ACTION
);
GO

-- ===============================================================
-- 12. TABLA: BitacoraAuditoria
-- ===============================================================
-- Registra cada operación importante del sistema para trazabilidad.
CREATE TABLE BitacoraAuditoria (
    IdAuditoria BIGINT IDENTITY(1,1) PRIMARY KEY,       -- Identificador único
    IdUsuario INT NULL,                                 -- Usuario que ejecutó la acción
    Entidad NVARCHAR(100),                              -- Nombre de la tabla afectada
    Operacion NVARCHAR(50),                             -- Tipo de acción (INSERT, UPDATE, DELETE, LOGIN)
    ClavePrimaria NVARCHAR(100),                        -- Valor clave del registro afectado
    ValorAnterior NVARCHAR(MAX),                        -- Datos previos
    ValorNuevo NVARCHAR(MAX),                           -- Datos nuevos
    FechaAccion DATETIME2 DEFAULT SYSDATETIME(),        -- Fecha y hora de la acción
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(IdUsuario)
        ON UPDATE CASCADE ON DELETE SET NULL
);
GO