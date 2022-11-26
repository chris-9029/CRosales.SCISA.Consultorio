USE [master]
GO
/****** Object:  Database [SCISAConsultorio]    Script Date: 11/25/2022 7:12:20 PM ******/
CREATE DATABASE [SCISAConsultorio]
 CONTAINMENT = NONE
GO
ALTER DATABASE [SCISAConsultorio] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SCISAConsultorio].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SCISAConsultorio] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET ARITHABORT OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SCISAConsultorio] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SCISAConsultorio] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SCISAConsultorio] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SCISAConsultorio] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SCISAConsultorio] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET RECOVERY FULL 
GO
ALTER DATABASE [SCISAConsultorio] SET  MULTI_USER 
GO
ALTER DATABASE [SCISAConsultorio] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SCISAConsultorio] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SCISAConsultorio] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SCISAConsultorio] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SCISAConsultorio', N'ON'
GO
USE [SCISAConsultorio]
GO
/****** Object:  StoredProcedure [dbo].[DoctorAdd]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DoctorAdd]
(
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Cedula VARCHAR(10),
@Foto VARCHAR(MAX)
)AS
INSERT INTO Doctor (Nombre, ApellidoPaterno, ApellidoMaterno, Cedula, Foto) VALUES
(@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Cedula, @Foto)

GO
/****** Object:  StoredProcedure [dbo].[DoctorDelete]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DoctorDelete]
@IdDoctor INT
AS
DELETE FROM Doctor 
WHERE IdDoctor = @IdDoctor

GO
/****** Object:  StoredProcedure [dbo].[DoctorGetAll]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DoctorGetAll]
as
SELECT IdDoctor, Nombre, ApellidoPaterno, ApellidoMaterno, Cedula, Foto FROM Doctor

GO
/****** Object:  StoredProcedure [dbo].[DoctorGetById]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DoctorGetById]
@IdDoctor INT
as
SELECT IdDoctor, Nombre, ApellidoPaterno, ApellidoMaterno, Cedula, Foto FROM Doctor
WHERE IdDoctor = @IdDoctor

GO
/****** Object:  StoredProcedure [dbo].[DoctorUpdate]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DoctorUpdate]
(
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Cedula VARCHAR(10),
@Foto VARCHAR(MAX),
@IdDoctor INT
)AS
UPDATE Doctor SET 
Nombre = @Nombre, 
ApellidoPaterno = @ApellidoPaterno, 
ApellidoMaterno = @ApellidoMaterno,
Cedula = @Cedula,
Foto = @Foto
WHERE IdDoctor = @IdDoctor

GO
/****** Object:  StoredProcedure [dbo].[PacienteAdd]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PacienteAdd]
(
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento DATE,
@Peso DECIMAL,
@Altura DECIMAL,
@Foto VARCHAR(MAX)
)AS
INSERT INTO Paciente (Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, Peso, Altura, Foto)VALUES
(@Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @Peso, @Altura, @Foto)

GO
/****** Object:  StoredProcedure [dbo].[PacienteDelete]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PacienteDelete] 
@IdPaciente INT
AS
DELETE FROM Paciente
WHERE IdPaciente = @IdPaciente

GO
/****** Object:  StoredProcedure [dbo].[PacienteGetAll]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PacienteGetAll]
AS
SELECT IdPaciente, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, Peso, Altura, Foto FROM Paciente

GO
/****** Object:  StoredProcedure [dbo].[PacienteGetById]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PacienteGetById]
@IdPaciente INT 
AS
SELECT IdPaciente, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento, Peso, Altura, Foto FROM Paciente
WHERE IdPaciente = @IdPaciente

GO
/****** Object:  StoredProcedure [dbo].[PacienteUpdate]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PacienteUpdate]
(@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento DATE,
@Peso DECIMAL,
@Altura DECIMAL,
@Foto VARCHAR(MAX),
@IdPaciente INT
)
AS
UPDATE Paciente SET 
Nombre = @Nombre,
ApellidoPaterno = @ApellidoPaterno,
ApellidoMaterno = @ApellidoMaterno,
FechaNacimiento = @FechaNacimiento, 
Peso = @Peso, 
Altura = @Altura, 
Foto = @Foto 
WHERE IdPaciente = @IdPaciente

GO
/****** Object:  Table [dbo].[CITA]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CITA](
	[IdCita] [int] IDENTITY(1,1) NOT NULL,
	[IdDoctor] [int] NULL,
	[IdPaciente] [int] NULL,
	[Detalle] [varchar](250) NULL,
	[Fecha] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCita] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Doctor](
	[IdDoctor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[ApellidoPaterno] [varchar](50) NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Cedula] [varchar](10) NULL,
	[Foto] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDoctor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Paciente]    Script Date: 11/25/2022 7:12:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paciente](
	[IdPaciente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[ApellidoPaterno] [varchar](50) NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[FechaNacimiento] [date] NULL,
	[Peso] [decimal](18, 0) NULL,
	[Altura] [decimal](18, 0) NULL,
	[Foto] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPaciente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CITA] ON 

INSERT [dbo].[CITA] ([IdCita], [IdDoctor], [IdPaciente], [Detalle], [Fecha]) VALUES (1, 1, 1, N'Fiebre y dolor de cabeza', CAST(0x0000AE1000107BEC AS DateTime))
SET IDENTITY_INSERT [dbo].[CITA] OFF
SET IDENTITY_INSERT [dbo].[Doctor] ON 

INSERT [dbo].[Doctor] ([IdDoctor], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [Cedula], [Foto]) VALUES (1, N'Gregory', N'House', N'Martinez', N'12654789', N'')
SET IDENTITY_INSERT [dbo].[Doctor] OFF
SET IDENTITY_INSERT [dbo].[Paciente] ON 

INSERT [dbo].[Paciente] ([IdPaciente], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [FechaNacimiento], [Peso], [Altura], [Foto]) VALUES (1, N'Pedro', N'Gonzales', N'Huerta', CAST(0xBA220B00 AS Date), CAST(61 AS Decimal(18, 0)), CAST(2 AS Decimal(18, 0)), N'')
SET IDENTITY_INSERT [dbo].[Paciente] OFF
ALTER TABLE [dbo].[CITA]  WITH CHECK ADD FOREIGN KEY([IdDoctor])
REFERENCES [dbo].[Doctor] ([IdDoctor])
GO
ALTER TABLE [dbo].[CITA]  WITH CHECK ADD FOREIGN KEY([IdPaciente])
REFERENCES [dbo].[Paciente] ([IdPaciente])
GO
USE [master]
GO
ALTER DATABASE [SCISAConsultorio] SET  READ_WRITE 
GO
