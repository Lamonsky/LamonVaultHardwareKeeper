USE [master]
GO
/****** Object:  Database [LamonVault_Hardware_Keeper]    Script Date: 14.04.2024 11:42:07 ******/
CREATE DATABASE [LamonVault_Hardware_Keeper]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LamonVault_Hardware_Keeper', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LamonVault_Hardware_Keeper.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LamonVault_Hardware_Keeper_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\LamonVault_Hardware_Keeper_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LamonVault_Hardware_Keeper].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ARITHABORT OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET RECOVERY FULL 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET  MULTI_USER 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'LamonVault_Hardware_Keeper', N'ON'
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET QUERY_STORE = ON
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LamonVault_Hardware_Keeper]
GO
/****** Object:  User [lamonvaulthardwarekeeperapiuser]    Script Date: 14.04.2024 11:42:07 ******/
CREATE USER [lamonvaulthardwarekeeperapiuser] FOR LOGIN [lamonvaulthardwarekeeperapiuser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Computer_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Computer_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Computer_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Computer_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Computers]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Computers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[StatusID] [int] NULL,
	[Computer_Type] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Processor] [varchar](255) NULL,
	[RAM] [varchar](255) NULL,
	[Disk] [varchar](255) NULL,
	[Graphics_Card] [varchar](255) NULL,
	[Operating_System] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[StatusID] [int] NULL,
	[LocationID] [int] NULL,
	[Contract_Type] [int] NULL,
	[Start_Date] [datetime] NULL,
	[Contract_Duration] [int] NULL,
	[Account_Number] [varchar](255) NULL,
	[Payment_Terms] [int] NULL,
	[Is_Renewable] [bit] NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Device_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Device_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Device_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Device_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Devices]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[StatusID] [int] NULL,
	[Device_Type] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Users_ID] [int] NULL,
	[Group_Type_ID] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Status] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
 CONSTRAINT [PK__Groups_T__3214EC27EEB7E0AD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hard_Drive_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hard_Drive_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hard_Drives]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hard_Drives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Capacity] [int] NULL,
	[Server] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Knowledge_Base]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Knowledge_Base](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Category] [int] NULL,
	[Subject] [varchar](max) NULL,
	[Content] [varchar](max) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Knowledge_Base_Categories]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Knowledge_Base_Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[License_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[License_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Software] [int] NULL,
	[Name] [varchar](255) NULL,
	[License_Type] [int] NULL,
	[Publisher] [int] NULL,
	[StatusID] [int] NULL,
	[LocationID] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Expiry_Date] [datetime] NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
	[Postal_Code] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[Country] [varchar](255) NULL,
	[Building_Number] [varchar](255) NULL,
	[Room_Number] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Users] [int] NULL,
	[LogDate] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitor_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitor_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitor_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitor_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitors]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[StatusID] [int] NULL,
	[Monitor_Type] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Network_Device_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Network_Device_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Network_Device_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Network_Device_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Network_Devices]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Network_Devices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[StatusID] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Device_Type] [int] NULL,
	[Rack] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operating_Systems]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operating_Systems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[StatusID] [int] NULL,
	[Phone_Type] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[SIM_Card1] [int] NULL,
	[SIM_Card2] [int] NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Printer_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Printer_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Printer_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Printer_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Printers]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Printers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[StatusID] [int] NULL,
	[Printer_Type] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[IP_Address] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Priority] [varchar](255) NULL,
	[Code] [varchar](255) NULL,
	[StatusID] [int] NULL,
	[Project_Type] [int] NULL,
	[Users] [int] NULL,
	[Planned_Start_Date] [datetime] NULL,
	[Planned_End_Date] [datetime] NULL,
	[Actual_Start_Date] [datetime] NULL,
	[Actual_End_Date] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[Comment] [varchar](max) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
 CONSTRAINT [PK__Projects__3214EC271F1E24AD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rack_Cabinet_Models]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rack_Cabinet_Models](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rack_Cabinet_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rack_Cabinet_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rack_Cabinets]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rack_Cabinets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusID] [int] NULL,
	[LocationID] [int] NULL,
	[Cabinet_Type] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Height] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servers]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[StatusID] [int] NULL,
	[Manufacturer] [int] NULL,
	[Model] [int] NULL,
	[Processor] [varchar](255) NULL,
	[RAM] [varchar](255) NULL,
	[Operating_System] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SIM_Cards]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIM_Cards](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PIN_Code] [varchar](255) NULL,
	[PUK_Code] [varchar](255) NULL,
	[Component] [int] NULL,
	[Serial_Number] [varchar](255) NULL,
	[Inventory_Number] [varchar](255) NULL,
	[Phone_Number] [varchar](255) NULL,
	[StatusID] [int] NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SIM_Component_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIM_Component_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SIM_Components]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIM_Components](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Manufacturer] [int] NULL,
	[Type] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Software]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Software](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[Publisher] [int] NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
 CONSTRAINT [PK__Statuses__3214EC2747E298F4] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Technicians]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Technicians](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Users_ID] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket_Categories]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket_Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket_Statuses]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket_Statuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket_Types]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket_Types](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Comment] [varchar](255) NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Type] [int] NULL,
	[Category] [int] NULL,
	[StatusID] [int] NULL,
	[LocationID] [int] NULL,
	[Owner] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
	[User] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Usersname] [varchar](255) NULL,
	[Last_Name] [varchar](255) NULL,
	[First_Name] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[LocationID] [int] NULL,
	[Is_Active] [bit] NULL,
	[Email] [varchar](255) NULL,
	[Phone1] [varchar](255) NULL,
	[Phone2] [varchar](255) NULL,
	[Internal_Number] [varchar](255) NULL,
	[Position] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 14.04.2024 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[Administrative_Number] [varchar](255) NULL,
	[Phone] [varchar](255) NULL,
	[Fax] [varchar](255) NULL,
	[Website] [varchar](255) NULL,
	[Email] [varchar](255) NULL,
	[Address] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[Postal_Code] [varchar](255) NULL,
	[Country] [varchar](255) NULL,
	[Is_Active] [bit] NULL,
	[Users] [int] NULL,
	[Created_At] [datetime] NULL,
	[Created_By] [int] NULL,
	[Modified_At] [datetime] NULL,
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Computer_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Computer_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Computer_Models]  WITH CHECK ADD  CONSTRAINT [FK_Computer_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Computer_Models] CHECK CONSTRAINT [FK_Computer_Models_Statuses]
GO
ALTER TABLE [dbo].[Computer_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Computer_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Computer_Types]  WITH CHECK ADD  CONSTRAINT [FK_Computer_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Computer_Types] CHECK CONSTRAINT [FK_Computer_Types_Statuses]
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Computer_Type])
REFERENCES [dbo].[Computer_Types] ([ID])
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD  CONSTRAINT [FK__Computers__Locat__5C57A83E] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Computers] CHECK CONSTRAINT [FK__Computers__Locat__5C57A83E]
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Computer_Models] ([ID])
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Operating_System])
REFERENCES [dbo].[Operating_Systems] ([ID])
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD  CONSTRAINT [FK__Computers__Statu__5D4BCC77] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Computers] CHECK CONSTRAINT [FK__Computers__Statu__5D4BCC77]
GO
ALTER TABLE [dbo].[Computers]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contract_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contract_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contract_Types]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Types_Contract_Types] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Contract_Types] CHECK CONSTRAINT [FK_Contract_Types_Contract_Types]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD FOREIGN KEY([Contract_Type])
REFERENCES [dbo].[Contract_Types] ([ID])
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK__Contracts__Statu__742F31CF] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK__Contracts__Statu__742F31CF]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Users] FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Users]
GO
ALTER TABLE [dbo].[Device_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Device_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Device_Models]  WITH CHECK ADD  CONSTRAINT [FK_Device_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Device_Models] CHECK CONSTRAINT [FK_Device_Models_Statuses]
GO
ALTER TABLE [dbo].[Device_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Device_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Device_Types]  WITH CHECK ADD  CONSTRAINT [FK_Device_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Device_Types] CHECK CONSTRAINT [FK_Device_Types_Statuses]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK__Devices__StatusI__0B47A151] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK__Devices__StatusI__0B47A151]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK__Devices__StatusI__44B528D7] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK__Devices__StatusI__44B528D7]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK__Devices__StatusI__51DA19CB] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK__Devices__StatusI__51DA19CB]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK__Groups__Group_Ty__5B638405] FOREIGN KEY([Group_Type_ID])
REFERENCES [dbo].[Groups_Types] ([ID])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK__Groups__Group_Ty__5B638405]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD FOREIGN KEY([Users_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Groups_Types]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Groups_Types] CHECK CONSTRAINT [FK_Groups_Types_Statuses]
GO
ALTER TABLE [dbo].[Hard_Drive_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drive_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drive_Models]  WITH CHECK ADD  CONSTRAINT [FK_Hard_Drive_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drive_Models] CHECK CONSTRAINT [FK_Hard_Drive_Models_Statuses]
GO
ALTER TABLE [dbo].[Hard_Drives]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drives]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drives]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Hard_Drive_Models] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drives]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drives]  WITH CHECK ADD FOREIGN KEY([Server])
REFERENCES [dbo].[Servers] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drives]  WITH CHECK ADD  CONSTRAINT [FK_Hard_Drives_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Hard_Drives] CHECK CONSTRAINT [FK_Hard_Drives_Statuses]
GO
ALTER TABLE [dbo].[Knowledge_Base]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [dbo].[Knowledge_Base_Categories] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base]  WITH CHECK ADD  CONSTRAINT [FK_Knowledge_Base_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base] CHECK CONSTRAINT [FK_Knowledge_Base_Statuses]
GO
ALTER TABLE [dbo].[Knowledge_Base]  WITH CHECK ADD  CONSTRAINT [FK_Knowledge_Base_Users] FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base] CHECK CONSTRAINT [FK_Knowledge_Base_Users]
GO
ALTER TABLE [dbo].[Knowledge_Base_Categories]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base_Categories]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base_Categories]  WITH CHECK ADD  CONSTRAINT [FK_Knowledge_Base_Categories_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Knowledge_Base_Categories] CHECK CONSTRAINT [FK_Knowledge_Base_Categories_Statuses]
GO
ALTER TABLE [dbo].[License_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[License_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[License_Types]  WITH CHECK ADD  CONSTRAINT [FK_License_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[License_Types] CHECK CONSTRAINT [FK_License_Types_Statuses]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([License_Type])
REFERENCES [dbo].[License_Types] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([License_Type])
REFERENCES [dbo].[License_Types] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([License_Type])
REFERENCES [dbo].[License_Types] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Publisher])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Publisher])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Publisher])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Software])
REFERENCES [dbo].[Software] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Software])
REFERENCES [dbo].[Software] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Software])
REFERENCES [dbo].[Software] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK__Licenses__Status__0682EC34] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK__Licenses__Status__0682EC34]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK__Licenses__Status__3FF073BA] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK__Licenses__Status__3FF073BA]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK__Licenses__Status__4D1564AE] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK__Licenses__Status__4D1564AE]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Locations]  WITH CHECK ADD  CONSTRAINT [FK_Locations_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Locations] CHECK CONSTRAINT [FK_Locations_Statuses]
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Logs]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Manufacturers]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Manufacturers]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Manufacturers]  WITH CHECK ADD  CONSTRAINT [FK_Manufacturers_Manufacturers] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Manufacturers] CHECK CONSTRAINT [FK_Manufacturers_Manufacturers]
GO
ALTER TABLE [dbo].[Monitor_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitor_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitor_Models]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Monitor_Models] CHECK CONSTRAINT [FK_Monitor_Models_Statuses]
GO
ALTER TABLE [dbo].[Monitor_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitor_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitor_Types]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Monitor_Types] CHECK CONSTRAINT [FK_Monitor_Types_Statuses]
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Monitor_Models] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Monitor_Models] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Monitor_Models] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Monitor_Type])
REFERENCES [dbo].[Monitor_Types] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Monitor_Type])
REFERENCES [dbo].[Monitor_Types] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Monitor_Type])
REFERENCES [dbo].[Monitor_Types] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD  CONSTRAINT [FK__Monitors__Status__131DCD43] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Monitors] CHECK CONSTRAINT [FK__Monitors__Status__131DCD43]
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD  CONSTRAINT [FK__Monitors__Status__2042BE37] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Monitors] CHECK CONSTRAINT [FK__Monitors__Status__2042BE37]
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD  CONSTRAINT [FK__Monitors__Status__59B045BD] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Monitors] CHECK CONSTRAINT [FK__Monitors__Status__59B045BD]
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Monitors]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Models]  WITH CHECK ADD  CONSTRAINT [FK_Network_Device_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Models] CHECK CONSTRAINT [FK_Network_Device_Models_Statuses]
GO
ALTER TABLE [dbo].[Network_Device_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Types]  WITH CHECK ADD  CONSTRAINT [FK_Network_Device_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Network_Device_Types] CHECK CONSTRAINT [FK_Network_Device_Types_Statuses]
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Network_Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Network_Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Network_Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Network_Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Network_Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Network_Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD  CONSTRAINT [FK__Network_D__Statu__1F83A428] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices] CHECK CONSTRAINT [FK__Network_D__Statu__1F83A428]
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD  CONSTRAINT [FK__Network_D__Statu__2CA8951C] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices] CHECK CONSTRAINT [FK__Network_D__Statu__2CA8951C]
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD  CONSTRAINT [FK__Network_D__Statu__66161CA2] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices] CHECK CONSTRAINT [FK__Network_D__Statu__66161CA2]
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Rack])
REFERENCES [dbo].[Rack_Cabinets] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Rack])
REFERENCES [dbo].[Rack_Cabinets] ([ID])
GO
ALTER TABLE [dbo].[Network_Devices]  WITH CHECK ADD FOREIGN KEY([Rack])
REFERENCES [dbo].[Rack_Cabinets] ([ID])
GO
ALTER TABLE [dbo].[Operating_Systems]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Operating_Systems]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Operating_Systems]  WITH CHECK ADD  CONSTRAINT [FK_Operating_Systems_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Operating_Systems] CHECK CONSTRAINT [FK_Operating_Systems_Statuses]
GO
ALTER TABLE [dbo].[Phone_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phone_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phone_Models]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Phone_Models] CHECK CONSTRAINT [FK_Phone_Models_Statuses]
GO
ALTER TABLE [dbo].[Phone_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phone_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phone_Types]  WITH CHECK ADD  CONSTRAINT [FK_Phone_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Phone_Types] CHECK CONSTRAINT [FK_Phone_Types_Statuses]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Phone_Models] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Phone_Models] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Phone_Models] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Phone_Type])
REFERENCES [dbo].[Phone_Types] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Phone_Type])
REFERENCES [dbo].[Phone_Types] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Phone_Type])
REFERENCES [dbo].[Phone_Types] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([SIM_Card1])
REFERENCES [dbo].[SIM_Cards] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([SIM_Card2])
REFERENCES [dbo].[SIM_Cards] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([SIM_Card1])
REFERENCES [dbo].[SIM_Cards] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([SIM_Card2])
REFERENCES [dbo].[SIM_Cards] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([SIM_Card1])
REFERENCES [dbo].[SIM_Cards] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([SIM_Card2])
REFERENCES [dbo].[SIM_Cards] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK__Phones__StatusID__2FBA0BF1] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK__Phones__StatusID__2FBA0BF1]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK__Phones__StatusID__3CDEFCE5] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK__Phones__StatusID__3CDEFCE5]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK__Phones__StatusID__764C846B] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK__Phones__StatusID__764C846B]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Positions]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Positions]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Positions]  WITH CHECK ADD  CONSTRAINT [FK_Positions_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Positions] CHECK CONSTRAINT [FK_Positions_Statuses]
GO
ALTER TABLE [dbo].[Printer_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printer_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printer_Models]  WITH CHECK ADD  CONSTRAINT [FK_Printer_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Printer_Models] CHECK CONSTRAINT [FK_Printer_Models_Statuses]
GO
ALTER TABLE [dbo].[Printer_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printer_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printer_Types]  WITH CHECK ADD  CONSTRAINT [FK_Printer_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Printer_Types] CHECK CONSTRAINT [FK_Printer_Types_Statuses]
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Printer_Models] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Printer_Models] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Printer_Models] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Printer_Type])
REFERENCES [dbo].[Printer_Types] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Printer_Type])
REFERENCES [dbo].[Printer_Types] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Printer_Type])
REFERENCES [dbo].[Printer_Types] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD  CONSTRAINT [FK__Printers__Status__2818EA29] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Printers] CHECK CONSTRAINT [FK__Printers__Status__2818EA29]
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD  CONSTRAINT [FK__Printers__Status__353DDB1D] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Printers] CHECK CONSTRAINT [FK__Printers__Status__353DDB1D]
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD  CONSTRAINT [FK__Printers__Status__6EAB62A3] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Printers] CHECK CONSTRAINT [FK__Printers__Status__6EAB62A3]
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Printers]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Project_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Project_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Project_Types]  WITH CHECK ADD  CONSTRAINT [FK_Project_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Project_Types] CHECK CONSTRAINT [FK_Project_Types_Statuses]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK__Projects__Create__0DEF03D2] FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK__Projects__Create__0DEF03D2]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK__Projects__Modifi__0EE3280B] FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK__Projects__Modifi__0EE3280B]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK__Projects__Projec__0CFADF99] FOREIGN KEY([Project_Type])
REFERENCES [dbo].[Project_Types] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK__Projects__Projec__0CFADF99]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK__Projects__Users__0C06BB60] FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK__Projects__Users__0C06BB60]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Statuses] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Statuses]
GO
ALTER TABLE [dbo].[Rack_Cabinet_Models]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinet_Models]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinet_Models]  WITH CHECK ADD  CONSTRAINT [FK_Rack_Cabinet_Models_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinet_Models] CHECK CONSTRAINT [FK_Rack_Cabinet_Models_Statuses]
GO
ALTER TABLE [dbo].[Rack_Cabinet_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinet_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinet_Types]  WITH CHECK ADD  CONSTRAINT [FK_Rack_Cabinet_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinet_Types] CHECK CONSTRAINT [FK_Rack_Cabinet_Types_Statuses]
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([Cabinet_Type])
REFERENCES [dbo].[Rack_Cabinet_Types] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Rack_Cabinet_Models] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD  CONSTRAINT [FK__Rack_Cabi__Statu__04659998] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Rack_Cabinets] CHECK CONSTRAINT [FK__Rack_Cabi__Statu__04659998]
GO
ALTER TABLE [dbo].[Rack_Cabinets]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Computer_Models] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([Operating_System])
REFERENCES [dbo].[Operating_Systems] ([ID])
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD  CONSTRAINT [FK__Servers__StatusI__78F3E6EC] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Servers] CHECK CONSTRAINT [FK__Servers__StatusI__78F3E6EC]
GO
ALTER TABLE [dbo].[Servers]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Component])
REFERENCES [dbo].[SIM_Components] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Component])
REFERENCES [dbo].[SIM_Components] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Component])
REFERENCES [dbo].[SIM_Components] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD  CONSTRAINT [FK__SIM_Cards__Statu__3943762B] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards] CHECK CONSTRAINT [FK__SIM_Cards__Statu__3943762B]
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD  CONSTRAINT [FK__SIM_Cards__Statu__4668671F] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards] CHECK CONSTRAINT [FK__SIM_Cards__Statu__4668671F]
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD  CONSTRAINT [FK__SIM_Cards__Statu__7FD5EEA5] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards] CHECK CONSTRAINT [FK__SIM_Cards__Statu__7FD5EEA5]
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Cards]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Component_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Component_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Component_Types]  WITH CHECK ADD  CONSTRAINT [FK_SIM_Component_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[SIM_Component_Types] CHECK CONSTRAINT [FK_SIM_Component_Types_Statuses]
GO
ALTER TABLE [dbo].[SIM_Components]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Components]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SIM_Components]  WITH CHECK ADD  CONSTRAINT [FK_SIM_Components_Manufacturers] FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[SIM_Components] CHECK CONSTRAINT [FK_SIM_Components_Manufacturers]
GO
ALTER TABLE [dbo].[SIM_Components]  WITH CHECK ADD  CONSTRAINT [FK_SIM_Components_SIM_Component_Types] FOREIGN KEY([Type])
REFERENCES [dbo].[SIM_Component_Types] ([ID])
GO
ALTER TABLE [dbo].[SIM_Components] CHECK CONSTRAINT [FK_SIM_Components_SIM_Component_Types]
GO
ALTER TABLE [dbo].[SIM_Components]  WITH CHECK ADD  CONSTRAINT [FK_SIM_Components_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[SIM_Components] CHECK CONSTRAINT [FK_SIM_Components_Statuses]
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Publisher])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Publisher])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Publisher])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Software]  WITH CHECK ADD  CONSTRAINT [FK_Software_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Software] CHECK CONSTRAINT [FK_Software_Statuses]
GO
ALTER TABLE [dbo].[Statuses]  WITH CHECK ADD  CONSTRAINT [FK__Statuses__Create__1C3D2329] FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Statuses] CHECK CONSTRAINT [FK__Statuses__Create__1C3D2329]
GO
ALTER TABLE [dbo].[Statuses]  WITH CHECK ADD  CONSTRAINT [FK__Statuses__Modifi__1D314762] FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Statuses] CHECK CONSTRAINT [FK__Statuses__Modifi__1D314762]
GO
ALTER TABLE [dbo].[Technicians]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Technicians]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Technicians]  WITH CHECK ADD FOREIGN KEY([Users_ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Technicians]  WITH CHECK ADD  CONSTRAINT [FK_Technicians_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Technicians] CHECK CONSTRAINT [FK_Technicians_Statuses]
GO
ALTER TABLE [dbo].[Ticket_Categories]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Categories]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Categories]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Categories_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Categories] CHECK CONSTRAINT [FK_Ticket_Categories_Statuses]
GO
ALTER TABLE [dbo].[Ticket_Statuses]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Statuses]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Statuses]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Statuses_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Statuses] CHECK CONSTRAINT [FK_Ticket_Statuses_Statuses]
GO
ALTER TABLE [dbo].[Ticket_Types]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Types]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Types]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Types_Statuses] FOREIGN KEY([Status])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Ticket_Types] CHECK CONSTRAINT [FK_Ticket_Types_Statuses]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [dbo].[Ticket_Categories] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([Owner])
REFERENCES [dbo].[Technicians] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[Ticket_Statuses] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([Type])
REFERENCES [dbo].[Ticket_Types] ([ID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Users] FOREIGN KEY([User])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([ID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Position])
REFERENCES [dbo].[Positions] ([ID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Position])
REFERENCES [dbo].[Positions] ([ID])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([Position])
REFERENCES [dbo].[Positions] ([ID])
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Vendors]  WITH CHECK ADD  CONSTRAINT [FK_Vendors_Users] FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Vendors] CHECK CONSTRAINT [FK_Vendors_Users]
GO
USE [master]
GO
ALTER DATABASE [LamonVault_Hardware_Keeper] SET  READ_WRITE 
GO
