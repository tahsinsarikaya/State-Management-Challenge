USE [master]
GO
/****** Object:  Database [StateManagement]    Script Date: 12/24/2021 5:50:18 PM ******/
CREATE DATABASE [StateManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StateManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\StateManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StateManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\StateManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [StateManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StateManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StateManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StateManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StateManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StateManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StateManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [StateManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StateManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StateManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StateManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StateManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StateManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StateManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StateManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StateManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StateManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StateManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StateManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StateManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StateManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StateManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StateManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StateManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StateManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [StateManagement] SET  MULTI_USER 
GO
ALTER DATABASE [StateManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StateManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StateManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StateManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StateManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StateManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'StateManagement', N'ON'
GO
ALTER DATABASE [StateManagement] SET QUERY_STORE = OFF
GO
USE [StateManagement]
GO
/****** Object:  Table [dbo].[Flow]    Script Date: 12/24/2021 5:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Flow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 12/24/2021 5:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlowId] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Sequence] [int] NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 12/24/2021 5:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlowId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskHistory]    Script Date: 12/24/2021 5:50:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[FlowId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Description] [nvarchar](max) NULL,
	[Created] [datetime] NULL,
	[Updated] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_TaskHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Flow] ON 
GO
INSERT [dbo].[Flow] ([Id], [Name], [Created], [Updated], [IsDeleted]) VALUES (1, N'Flow1', CAST(N'2021-12-23T14:41:19.687' AS DateTime), CAST(N'2021-12-23T14:41:19.687' AS DateTime), 0)
GO
INSERT [dbo].[Flow] ([Id], [Name], [Created], [Updated], [IsDeleted]) VALUES (2, N'Flow2', CAST(N'2021-12-23T15:39:04.610' AS DateTime), CAST(N'2021-12-23T15:39:04.613' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Flow] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 
GO
INSERT [dbo].[State] ([Id], [FlowId], [Name], [Sequence], [Created], [Updated], [IsDeleted]) VALUES (1, 1, N'StateA', 1, CAST(N'2021-12-24T16:19:11.757' AS DateTime), CAST(N'2021-12-24T16:19:11.757' AS DateTime), 0)
GO
INSERT [dbo].[State] ([Id], [FlowId], [Name], [Sequence], [Created], [Updated], [IsDeleted]) VALUES (2, 1, N'StateB', 2, CAST(N'2021-12-24T16:19:42.833' AS DateTime), CAST(N'2021-12-24T16:19:42.833' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[Task] ON 
GO
INSERT [dbo].[Task] ([Id], [FlowId], [StateId], [Name], [Description], [Created], [Updated], [IsDeleted]) VALUES (1, 1, 1, N'Task1', N'Task1 desc', CAST(N'2021-12-24T17:34:51.627' AS DateTime), CAST(N'2021-12-24T17:37:18.240' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskHistory] ON 
GO
INSERT [dbo].[TaskHistory] ([Id], [TaskId], [FlowId], [StateId], [Name], [Description], [Created], [Updated], [IsDeleted]) VALUES (1, 1, 1, 1, N'Task1', N'Test task1', CAST(N'2021-12-24T17:34:54.567' AS DateTime), CAST(N'2021-12-24T17:34:54.567' AS DateTime), 0)
GO
INSERT [dbo].[TaskHistory] ([Id], [TaskId], [FlowId], [StateId], [Name], [Description], [Created], [Updated], [IsDeleted]) VALUES (2, 1, 1, 1, N'Task1', N'Task1 desc', CAST(N'2021-12-24T17:37:24.117' AS DateTime), CAST(N'2021-12-24T17:37:24.117' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[TaskHistory] OFF
GO
ALTER TABLE [dbo].[Flow] ADD  CONSTRAINT [DF_Flow_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[State] ADD  CONSTRAINT [DF_State_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DF_Task_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TaskHistory] ADD  CONSTRAINT [DF_TaskHistory_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
USE [master]
GO
ALTER DATABASE [StateManagement] SET  READ_WRITE 
GO
