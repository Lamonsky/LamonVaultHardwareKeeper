USE [LamonVault_Hardware_Keeper]
GO
/****** Object:  User [lamonvaulthardwarekeeperapiuser]    Script Date: 17.09.2024 20:32:00 ******/
CREATE USER [lamonvaulthardwarekeeperapiuser] FOR LOGIN [lamonvaulthardwarekeeperapiuser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [lamonvaulthardwarekeeperapiuser]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Computer_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Computer_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Computers]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Device_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Device_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Devices]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Hard_Drive_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Hard_Drives]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[License_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Licenses]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Locations]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Logs]    Script Date: 17.09.2024 20:32:00 ******/
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
	[Modified_By] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manufacturers]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Monitor_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Monitor_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Monitors]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Network_Device_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Network_Device_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Network_Devices]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Operating_Systems]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[PageContent]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageContent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_PageContent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phone_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Phone_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Phones]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Positions]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Printer_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Printer_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Printers]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Rack_Cabinet_Models]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Rack_Cabinet_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Rack_Cabinets]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Servers]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[SIM_Cards]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[SIM_Component_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[SIM_Components]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Software]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Statuses]    Script Date: 17.09.2024 20:32:00 ******/
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
SET IDENTITY_INSERT [dbo].[Statuses] ON;
GO
INSERT INTO [dbo].[Statuses] (ID, Name, Comment, Created_At, Created_By, Modified_At, Modified_By)
VALUES (99, 'Wy³¹czone', NULL, GETDATE(), NULL, NULL, NULL);
GO
SET IDENTITY_INSERT [dbo].[Statuses] OFF;
GO
/****** Object:  Table [dbo].[Technicians]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Templates]    Script Date: 17.09.2024 20:32:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[HTMLContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket_Categories]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Ticket_Statuses]    Script Date: 17.09.2024 20:32:00 ******/
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
	[CountToClosed] [bit] NULL,
	[CountToNew] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket_Types]    Script Date: 17.09.2024 20:32:00 ******/
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
/****** Object:  Table [dbo].[Tickets]    Script Date: 17.09.2024 20:32:00 ******/
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
	[Device] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.09.2024 20:32:00 ******/
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
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
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
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Device_Type])
REFERENCES [dbo].[Device_Types] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Manufacturer])
REFERENCES [dbo].[Manufacturers] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Model])
REFERENCES [dbo].[Device_Models] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD  CONSTRAINT [FK__Devices__StatusI__51DA19CB] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Statuses] ([ID])
GO
ALTER TABLE [dbo].[Devices] CHECK CONSTRAINT [FK__Devices__StatusI__51DA19CB]
GO
ALTER TABLE [dbo].[Devices]  WITH CHECK ADD FOREIGN KEY([Users])
REFERENCES [dbo].[Users] ([ID])
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
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([Created_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users1] FOREIGN KEY([Modified_By])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users1]
GO
