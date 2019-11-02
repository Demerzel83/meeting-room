USE [master]
GO
/****** Object:  Database [MeetingRooms]    Script Date: 03/10/2019 19:56:59 ******/
CREATE DATABASE [MeetingRooms]
 CONTAINMENT = NONE
 ON  PRIMARY
( NAME = N'MeetingRooms', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\MeetingRooms.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON
( NAME = N'MeetingRooms_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\MeetingRooms_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MeetingRooms] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeetingRooms].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeetingRooms] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MeetingRooms] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MeetingRooms] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MeetingRooms] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MeetingRooms] SET ARITHABORT OFF
GO
ALTER DATABASE [MeetingRooms] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MeetingRooms] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MeetingRooms] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MeetingRooms] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MeetingRooms] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MeetingRooms] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MeetingRooms] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MeetingRooms] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MeetingRooms] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MeetingRooms] SET  DISABLE_BROKER
GO
ALTER DATABASE [MeetingRooms] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MeetingRooms] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MeetingRooms] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MeetingRooms] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MeetingRooms] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MeetingRooms] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MeetingRooms] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [MeetingRooms] SET RECOVERY FULL
GO
ALTER DATABASE [MeetingRooms] SET  MULTI_USER
GO
ALTER DATABASE [MeetingRooms] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MeetingRooms] SET DB_CHAINING OFF
GO
ALTER DATABASE [MeetingRooms] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF )
GO
ALTER DATABASE [MeetingRooms] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO
ALTER DATABASE [MeetingRooms] SET DELAYED_DURABILITY = DISABLED
GO
EXEC sys.sp_db_vardecimal_storage_format N'MeetingRooms', N'ON'
GO
ALTER DATABASE [MeetingRooms] SET QUERY_STORE = OFF
GO
USE [MeetingRooms]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [MeetingRooms]
GO
/****** Object:  Table [dbo].[MeetingRooms]    Script Date: 03/10/2019 19:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingRooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeetingRoom] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 03/10/2019 19:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MeetingRoomId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[From] [datetime2](7) NOT NULL,
	[to] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Reservations] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 03/10/2019 19:56:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NULL,
	[Surname] [nvarchar](512) NULL,
	[Email] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_MeetingRooms] FOREIGN KEY([MeetingRoomId])
REFERENCES [dbo].[MeetingRooms] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_MeetingRooms]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Users]
GO
USE [master]
GO
ALTER DATABASE [MeetingRooms] SET  READ_WRITE
GO
