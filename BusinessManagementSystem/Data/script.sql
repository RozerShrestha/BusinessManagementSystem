USE [BusinessManagement]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientName] [varchar](255) NOT NULL,
	[ClientPhoneNumber] [varchar](20) NOT NULL,
	[AppointmentDate] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
	[ReferalId] [int] NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[TotalHours] [float] NOT NULL,
	[Status] [varchar](100) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[Allergies] [varchar](500) NOT NULL,
	[ConsentFormSigned] [bit] NOT NULL,
	[FollowUpRequired] [bit] NOT NULL,
	[InkColorPreferance] [varchar](150) NULL,
	[MedicalConditions] [varchar](500) NOT NULL,
	[PainToleranceLevel] [varchar](150) NOT NULL,
	[Placement] [varchar](150) NULL,
	[SessionNumber] [int] NOT NULL,
	[TattooDesign] [varchar](500) NULL,
	[guid] [uniqueidentifier] NOT NULL,
	[IsForeigner] [bit] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicConfigurations]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicConfigurations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailAlias] [varchar](100) NOT NULL,
	[Email] [varchar](max) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[HostName] [varchar](100) NOT NULL,
	[Port] [int] NOT NULL,
	[ApplicationTitle] [varchar](250) NOT NULL,
	[EmployerName] [varchar](100) NOT NULL,
	[EmployerEmailAddress] [varchar](100) NOT NULL,
	[EmployerAddress] [varchar](100) NOT NULL,
	[CreatedBy] [varchar](150) NULL,
	[UpdatedBy] [varchar](150) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[DreadLockPrice] [float] NOT NULL,
	[PiercingPrice] [float] NOT NULL,
	[TattooPrice] [float] NOT NULL,
 CONSTRAINT [PK_BasicConfigurations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuRoles]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuRoles](
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[UpdatedBy] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MenuRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Parent] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Url] [varchar](255) NOT NULL,
	[Sort] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[Icon] [varchar](150) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AppointmentId] [int] NOT NULL,
	[Deposit] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[DiscountInHour] [float] NOT NULL,
	[TotalCost] [float] NOT NULL,
	[PaymentToStudio] [float] NOT NULL,
	[PaymentToArtist] [float] NOT NULL,
	[PaymentMethod] [nvarchar](max) NOT NULL,
	[PaymentSettlement] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[TipAmount] [float] NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Referals]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](255) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[Commission] [int] NOT NULL,
	[ReferalAppointDate] [date] NOT NULL,
 CONSTRAINT [PK_Referals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tips]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tips](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipAmount] [float] NOT NULL,
	[TipAmountForUsers] [float] NOT NULL,
	[TipAssignedToUser] [int] NOT NULL,
	[AppointmentId] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[TipSettlement] [bit] NOT NULL,
 CONSTRAINT [PK_Tips] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/18/2024 2:09:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](150) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[FullName] [varchar](255) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [varchar](10) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[HashPassword] [varchar](255) NOT NULL,
	[Salt] [varchar](255) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[Status] [bit] NOT NULL,
	[Occupation] [nvarchar](max) NOT NULL,
	[FirstPasswordReset] [bit] NOT NULL,
	[FacebookLink] [varchar](255) NULL,
	[InstagramLink] [varchar](255) NULL,
	[ProfilePictureLink] [varchar](255) NULL,
	[TiktokLink] [varchar](255) NULL,
	[Notes] [varchar](255) NULL,
	[Skills] [varchar](255) NULL,
	[Percentage] [int] NOT NULL,
	[DefaultTips] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([Id], [ClientName], [ClientPhoneNumber], [AppointmentDate], [UserId], [ReferalId], [Category], [TotalHours], [Status], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Allergies], [ConsentFormSigned], [FollowUpRequired], [InkColorPreferance], [MedicalConditions], [PainToleranceLevel], [Placement], [SessionNumber], [TattooDesign], [guid], [IsForeigner]) VALUES (1024, N'Hira Shrestha', N'9841223848', CAST(N'2024-11-17T19:12:00.0000000' AS DateTime2), 8, 1, N'Tattoo', 5, N'Completed', N'dinesh', N'dinesh', CAST(N'2024-11-17T19:14:00.6687940' AS DateTime2), CAST(N'2024-11-18T02:23:11.3680341' AS DateTime2), N'No', 1, 0, N'B/W', N'No', N'No', N'Hand', 1, N'Flower', N'692b20be-5423-4e9c-ad57-b0d1bad39e57', 1)
INSERT [dbo].[Appointments] ([Id], [ClientName], [ClientPhoneNumber], [AppointmentDate], [UserId], [ReferalId], [Category], [TotalHours], [Status], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Allergies], [ConsentFormSigned], [FollowUpRequired], [InkColorPreferance], [MedicalConditions], [PainToleranceLevel], [Placement], [SessionNumber], [TattooDesign], [guid], [IsForeigner]) VALUES (1026, N'Ganga Bista', N'9818112737', CAST(N'2024-11-17T23:50:00.0000000' AS DateTime2), 8, 1, N'Tattoo', 8, N'Completed', N'dinesh', N'dinesh', CAST(N'2024-11-17T23:51:30.9997522' AS DateTime2), CAST(N'2024-11-18T02:26:33.2100255' AS DateTime2), N'No', 1, 0, N'B/W', N'No', N'No', N'Hand', 1, N'Trisul', N'f997fa3f-7b84-442e-82e6-026747915e66', 1)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
GO
SET IDENTITY_INSERT [dbo].[BasicConfigurations] ON 

INSERT [dbo].[BasicConfigurations] ([Id], [EmailAlias], [Email], [Password], [HostName], [Port], [ApplicationTitle], [EmployerName], [EmployerEmailAddress], [EmployerAddress], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [DreadLockPrice], [PiercingPrice], [TattooPrice]) VALUES (1, N'Email alias', N'employer@gmail.com', N'Not Required', N'hostname', 25, N'Application Title', N'Employer Name', N'EmployerEmail@gmail.com', N'EMployer Address', N'rozer.shrestha', N'rozer.shrestha', CAST(N'2024-11-07T22:59:18.1791542' AS DateTime2), CAST(N'2024-11-07T22:59:18.1791542' AS DateTime2), 3000, 2000, 6000)
SET IDENTITY_INSERT [dbo].[BasicConfigurations] OFF
GO
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 1, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 2, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 3, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 5, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 6, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 7, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 8, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 10, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 11, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 12, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 13, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 14, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 15, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 16, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 17, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (1, 18, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 6, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 7, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 10, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 11, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 12, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 13, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 14, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 15, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 16, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 17, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (10, 18, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 7, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 10, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 11, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 13, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 14, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 17, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (11, 18, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (20, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (20, 6, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (20, 7, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (21, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (21, 7, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (30, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (30, 6, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (30, 7, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (31, 4, NULL, N'', NULL, N'')
INSERT [dbo].[MenuRoles] ([RoleId], [MenuId], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (31, 7, NULL, N'', NULL, N'')
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (1, 0, N'Configurations', N'#', 1, 1, N'fas fa-cogs', N'System', N'rozer', CAST(N'2024-11-01T18:05:21.5740134' AS DateTime2), CAST(N'2024-11-09T16:51:45.7654390' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (2, 1, N'Basic Configuration', N'/BasicConfiguration', 1, 1, N'bi bi-gear', N'System', N'rozer', CAST(N'2024-11-01T18:05:21.5740132' AS DateTime2), CAST(N'2024-11-09T16:53:07.5933471' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (3, 1, N'Menu', N'/Menu', 2, 1, N'bi bi-menu-app', N'System', N'rozer', CAST(N'2024-11-01T18:05:21.5740113' AS DateTime2), CAST(N'2024-11-09T16:53:18.6194629' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (4, 0, N'Users', N'#', 2, 1, N'far fa-user', N'System', N'rozer.shrestha', CAST(N'2024-11-01T18:05:21.5740135' AS DateTime2), CAST(N'2024-11-02T14:35:10.7736437' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (5, 4, N'Create User', N'/Users/Create', 1, 1, N'fa', N'System', N'rozer.shrestha', CAST(N'2024-11-01T18:05:21.5740137' AS DateTime2), CAST(N'2024-11-01T18:27:00.9233810' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (6, 4, N'All Users', N'/Users/Index', 2, 1, N'fa', N'System', N'rozer.shrestha', CAST(N'2024-11-01T18:05:21.5740139' AS DateTime2), CAST(N'2024-11-01T18:27:52.7903866' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (7, 4, N'My Profile', N'/Users/MyProfile', 3, 1, N'fa', N'System', N'rozer', CAST(N'2024-11-01T18:05:21.5740140' AS DateTime2), CAST(N'2024-11-11T01:30:59.6251172' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (8, 1, N'Role', N'/Role', 3, 1, N'fa', N'rozer.shrestha', N'rozer.shrestha', CAST(N'2024-11-01T18:06:03.0037090' AS DateTime2), CAST(N'2024-11-01T18:06:17.8991807' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (10, 0, N'Appointment', N'#', 3, 1, N'fas fa-pencil-ruler', N'rozer.shrestha', NULL, CAST(N'2024-11-02T14:34:45.5572615' AS DateTime2), NULL)
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (11, 10, N'New Appointment', N'/Appointment/Create', 1, 1, N'fa', N'rozer.shrestha', NULL, CAST(N'2024-11-02T14:36:39.3180022' AS DateTime2), NULL)
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (12, 10, N'All Appointment', N'/Appointment', 2, 1, N'fa', N'rozer.shrestha', NULL, CAST(N'2024-11-02T14:37:55.8536942' AS DateTime2), NULL)
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (13, 10, N'My Appointments', N'/Appointment/MyAppointments', 3, 1, N'fa', N'rozer.shrestha', N'rozer', CAST(N'2024-11-02T14:38:56.9982657' AS DateTime2), CAST(N'2024-11-09T18:22:28.2704413' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (14, 0, N'Payments', N'#', 4, 1, N'fas fa-hand-holding-usd', N'rozer', N'rozer', CAST(N'2024-11-15T23:05:57.6217127' AS DateTime2), CAST(N'2024-11-15T23:06:31.5379042' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (15, 14, N'All Payments', N'/Payment/Index', 1, 1, N'fa', N'rozer', N'rozer', CAST(N'2024-11-15T23:08:01.5821443' AS DateTime2), CAST(N'2024-11-15T23:09:39.8452692' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (16, 14, N'All Tips', N'/Tip/Index', 3, 1, N'fa', N'rozer', N'rozer', CAST(N'2024-11-15T23:09:14.8685164' AS DateTime2), CAST(N'2024-11-15T23:11:14.8179879' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (17, 14, N'My Payments', N'/Payment/MyPayments', 2, 1, N'fa', N'rozer', N'rozer', CAST(N'2024-11-15T23:10:47.6128916' AS DateTime2), CAST(N'2024-11-15T23:11:05.5762410' AS DateTime2))
INSERT [dbo].[Menus] ([Id], [Parent], [Name], [Url], [Sort], [Status], [Icon], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (18, 14, N'My Tips', N'/Tip/MyTips', 4, 1, N'fa', N'rozer', N'rozer', CAST(N'2024-11-15T23:12:01.7909172' AS DateTime2), CAST(N'2024-11-15T23:12:10.0049715' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Menus] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [UserId], [AppointmentId], [Deposit], [Discount], [DiscountInHour], [TotalCost], [PaymentToStudio], [PaymentToArtist], [PaymentMethod], [PaymentSettlement], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipAmount]) VALUES (1, 8, 1024, 1000, 0, 1, 47000, 24000, 24000, N'BankQR', 0, N'dinesh', N'dinesh', CAST(N'2024-11-17T19:14:00.6690762' AS DateTime2), CAST(N'2024-11-18T02:23:11.3680412' AS DateTime2), 6000)
INSERT [dbo].[Payments] ([Id], [UserId], [AppointmentId], [Deposit], [Discount], [DiscountInHour], [TotalCost], [PaymentToStudio], [PaymentToArtist], [PaymentMethod], [PaymentSettlement], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipAmount]) VALUES (3, 8, 1026, 2000, 1000, 1, 81000, 41500, 41500, N'CardPayment', 0, N'dinesh', N'dinesh', CAST(N'2024-11-17T23:51:30.9998958' AS DateTime2), CAST(N'2024-11-18T02:26:33.2104295' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Referals] ON 

INSERT [dbo].[Referals] ([Id], [FullName], [Status], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Commission], [ReferalAppointDate]) VALUES (1, N'No Referal', 1, N'system', N'system', CAST(N'2024-10-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-10-10T00:00:00.0000000' AS DateTime2), 5, CAST(N'2024-10-10' AS Date))
SET IDENTITY_INSERT [dbo].[Referals] OFF
GO
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (1, N'superadmin', N'System', N'System', CAST(N'2024-11-01T18:05:21.2502303' AS DateTime2), NULL)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (10, N'admin_tattoo', N'System', N'System', CAST(N'2024-11-01T18:05:21.2503088' AS DateTime2), NULL)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (11, N'employee_tattoo', N'System', N'System', CAST(N'2024-11-01T18:05:21.2503094' AS DateTime2), NULL)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (20, N'admin_kaffe', N'System', N'System', CAST(N'2024-11-01T18:05:21.2503091' AS DateTime2), NULL)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (21, N'employee_kaffe', N'System', N'System', CAST(N'2024-11-01T18:05:21.2503096' AS DateTime2), NULL)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (30, N'admin_apartment', N'System', N'System', CAST(N'2024-11-01T18:05:21.2503092' AS DateTime2), NULL)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt]) VALUES (31, N'employee_apartment', N'System', N'System', CAST(N'2024-11-01T18:05:21.2503097' AS DateTime2), NULL)
GO
SET IDENTITY_INSERT [dbo].[Tips] ON 

INSERT [dbo].[Tips] ([Id], [TipAmount], [TipAmountForUsers], [TipAssignedToUser], [AppointmentId], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipSettlement]) VALUES (16, 6000, 2000, 9, 1024, N'dinesh', NULL, CAST(N'2024-11-17T19:14:00.6690802' AS DateTime2), NULL, 0)
INSERT [dbo].[Tips] ([Id], [TipAmount], [TipAmountForUsers], [TipAssignedToUser], [AppointmentId], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipSettlement]) VALUES (17, 6000, 2000, 12, 1024, N'dinesh', NULL, CAST(N'2024-11-17T19:14:00.6690807' AS DateTime2), NULL, 0)
INSERT [dbo].[Tips] ([Id], [TipAmount], [TipAmountForUsers], [TipAssignedToUser], [AppointmentId], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipSettlement]) VALUES (18, 6000, 2000, 8, 1024, N'dinesh', NULL, CAST(N'2024-11-17T19:14:00.6690831' AS DateTime2), NULL, 0)
INSERT [dbo].[Tips] ([Id], [TipAmount], [TipAmountForUsers], [TipAssignedToUser], [AppointmentId], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipSettlement]) VALUES (19, 15000, 5000, 9, 1026, N'dinesh', NULL, CAST(N'2024-11-18T02:26:33.2100051' AS DateTime2), NULL, 0)
INSERT [dbo].[Tips] ([Id], [TipAmount], [TipAmountForUsers], [TipAssignedToUser], [AppointmentId], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipSettlement]) VALUES (20, 15000, 5000, 12, 1026, N'dinesh', NULL, CAST(N'2024-11-18T02:26:33.2100250' AS DateTime2), NULL, 0)
INSERT [dbo].[Tips] ([Id], [TipAmount], [TipAmountForUsers], [TipAssignedToUser], [AppointmentId], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [TipSettlement]) VALUES (21, 15000, 5000, 8, 1026, N'dinesh', NULL, CAST(N'2024-11-18T02:26:33.2100251' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Tips] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (6, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (8, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (9, 1)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (12, 10)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (10, 11)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (11, 11)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (15, 20)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (13, 21)
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (14, 21)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (6, N'167f4cad-840d-4ac1-9900-2b28f4f44053', N'rozer', N'rozer.shrestha611@gmail.com', N'Rozer Shrestha', CAST(N'1991-03-01' AS Date), N'male', N'Bhimsensthan', N'9818136462', N'vNWsg9wG82FOVlKYm4fJNkTSysuUuGoeuCNL/oYbwn4=', N'x9MC6J+9dMJ06q0OP/T4/w==', N'rozer.shrestha', N'rozer', CAST(N'2024-11-07T23:31:50.6193400' AS DateTime2), CAST(N'2024-11-09T16:40:52.1721100' AS DateTime2), 1, N'ChiefOperatingOfficer', 0, N'https://www.facebook.com/kabir.mhzrn?locale=hi_IN', N'https://www.instagram.com/laddustra/?hl=en', N'image\ProfilePicture\man-7796384_640.jpg', NULL, N'Here goes the note of laddu', N'Here goes the skill of laddu', 0, 0)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (8, N'07507b39-f807-4305-b75a-46f82fa93537', N'dinesh', N'dinesh.maharjan@gmail.com', N'Dinesh Maharjan', CAST(N'1991-11-06' AS Date), N'male', N'Bhimsensthan', N'9899998787', N'DSEWbg80aB5pstuYCldpoqaNk2pglUAwgq6ZpLb7P18=', N'Ysujwl4gu4h//BGaUcw0cw==', N'rozer', N'dinesh', CAST(N'2024-11-09T13:17:22.2433717' AS DateTime2), CAST(N'2024-11-18T03:01:00.7437231' AS DateTime2), 1, N'TattooArtist', 0, N'https://www.youtube.com/', N'https://www.youtube.com/', N'image\ProfilePicture\Screenshot 2024-11-09 131527.png', N'https://www.youtube.com/', N'Here goes the Notes of Dinesh', N'Here goes the skills of Dinesh', 0, 0)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (9, N'9395ed0f-78a3-4c01-8bfb-c0c9364bd6d2', N'prajina', N'prajina.shrestha@gmail.com', N'Prajina Shrestha', CAST(N'1992-11-09' AS Date), N'female', N'Bhimsensthan', N'9851293945', N'gZZeS7Ld6bUV1eAKFkZ4IP7NZU53lSwaJVSHVvIpoeE=', N'q6vafugRumDx1ux4KO3wog==', N'rozer', N'rozer', CAST(N'2024-11-09T16:34:19.8359464' AS DateTime2), CAST(N'2024-11-15T14:50:58.8378844' AS DateTime2), 1, N'Manager', 0, N'fb', N'ins', N'image\ProfilePicture\black-girl-aesthetic-pfp-1.jpg', N'tiik', N'Here goes the notes of Prajina', N'Here goes the skills of Prajina', 0, 1)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (10, N'f9057089-12ab-4cd7-9098-56ec7ff0a8e7', N'sanik', N'sanik@gmail.com', N'Sanik Shahi', CAST(N'1997-11-06' AS Date), N'male', N'Jochehen', N'9833848483', N'Ta7jzEPmNwWoKP4r2WE5RNQJPJFpZ1WpseFommzJ2/c=', N'nfDqJnavRFHrwtVuITm0QQ==', N'rozer', NULL, CAST(N'2024-11-09T16:36:46.1808373' AS DateTime2), NULL, 1, N'TattooArtist', 0, N'fb', N'inst', N'image\ProfilePicture\sanik.jpg', N'tik', N'Here goes the note of Sanik', N'Here goes the skills of Sanik', 50, 0)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (11, N'994150c1-04fe-40f5-9c27-cfdff897e868', N'laddu', N'laddu@gmail.com', N'Laddu Maharjan', CAST(N'1995-11-05' AS Date), N'male', N'Patan', N'9898787876', N'59QhoyLWjGvkJYHD7+qf19pyfAhuMFvDJcFFhvRZIP8=', N'2xAz5WGumM2F62ndq2UXng==', N'rozer', N'rozer', CAST(N'2024-11-09T16:38:03.7224129' AS DateTime2), CAST(N'2024-11-17T01:10:29.3948758' AS DateTime2), 1, N'TattooArtist', 0, N'fb', N'inst', N'image\ProfilePicture\Screenshot 2024-11-09 131527.png', N'tik', N'Here goes the notes of Laddu', N'Here goes the skills of Laddu', 50, 0)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (12, N'012b055c-a99e-4131-89dc-1c50daa059a9', N'juju', N'juju@gmail.com', N'Juju Tamang', CAST(N'1995-11-06' AS Date), N'male', N'Dhapasi', N'9898787676', N'I3gWbCSm4IvaL5k97LSKGxKh3Q0wwSxUGDPVrxWuTnQ=', N'0l9nVXGBfX8bpG84xCdGjg==', N'rozer', N'rozer', CAST(N'2024-11-09T16:39:54.5476373' AS DateTime2), CAST(N'2024-11-15T14:51:50.4029459' AS DateTime2), 1, N'TattooArtist', 0, N'fb', N'inst', N'image\ProfilePicture\images.jpg', N'tik', N'Here goes the notes of Juju', N'Here geos the skills of Juju', 50, 1)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (13, N'f18d549c-0ecb-4a87-8f21-2985bd373e7c', N'mendo', N'mendo@gmail.com', N'Mendo Gurung', CAST(N'1998-11-06' AS Date), N'female', N'Hattiban', N'9898765432', N'CVq4McKle51Au7RMXcQ9nYV3R8VxzdHCFmrNZSEauQw=', N'nr9ioTNu3L4lCrwUBbZceA==', N'rozer', N'rozer', CAST(N'2024-11-09T16:42:50.6210516' AS DateTime2), CAST(N'2024-11-09T16:45:32.7021509' AS DateTime2), 1, N'Barista', 0, N'fb', N'inst', N'image\ProfilePicture\black-girl-aesthetic-pfp-1.jpg', N'tik', N'Here goes the notes of Mendo', N'Here goes the skills of Mendo', 0, 0)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (14, N'3dbf3c84-360d-411a-b4c5-9cbf00e26e01', N'nabin', N'nabin@gmail.com', N'Nabin Lama', CAST(N'1999-11-07' AS Date), N'male', N'Thamel', N'9890987654', N'TAUNCiaZ5PSTOqtyr1p4evtNxfYi/D395U31gdjz5B0=', N'hdY+H0PjRJKFSNG41xuKOg==', N'rozer', NULL, CAST(N'2024-11-09T16:45:26.0319320' AS DateTime2), NULL, 1, N'Barista', 0, N'fb', N'inst', N'image\ProfilePicture\images (1).jpg', N'tik', N'Here goes the notes of Nabin', N'Here goes the skills of Nabin', 0, 0)
INSERT [dbo].[Users] ([Id], [Guid], [UserName], [Email], [FullName], [DateOfBirth], [Gender], [Address], [PhoneNumber], [HashPassword], [Salt], [CreatedBy], [UpdatedBy], [CreatedAt], [UpdatedAt], [Status], [Occupation], [FirstPasswordReset], [FacebookLink], [InstagramLink], [ProfilePictureLink], [TiktokLink], [Notes], [Skills], [Percentage], [DefaultTips]) VALUES (15, N'ebeeff34-10b7-4982-acdc-ae647db1abdd', N'amrit', N'amrit@gmail.com', N'Amrit Khadka', CAST(N'1998-11-06' AS Date), N'male', N'Bauddha', N'9841771650', N'etvDFwjNfuOW59oOLj6VRch8BnLfp/kS2hxgBhY7M04=', N'0eYYd7IXkmBiEYsNNwyE1w==', N'rozer', NULL, CAST(N'2024-11-09T16:50:48.7062257' AS DateTime2), NULL, 1, N'Manager', 0, N'fb', N'inst', N'image\ProfilePicture\360_F_608557356_ELcD2pwQO9pduTRL30umabzgJoQn5fnd.jpg', N'tik', N'Here goes the notes of Amrit', N'Here goes the skills of Amrit', 0, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ('') FOR [Allergies]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [ConsentFormSigned]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [FollowUpRequired]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ('') FOR [MedicalConditions]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ('') FOR [PainToleranceLevel]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ((0)) FOR [SessionNumber]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [guid]
GO
ALTER TABLE [dbo].[Appointments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsForeigner]
GO
ALTER TABLE [dbo].[BasicConfigurations] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [DreadLockPrice]
GO
ALTER TABLE [dbo].[BasicConfigurations] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [PiercingPrice]
GO
ALTER TABLE [dbo].[BasicConfigurations] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [TattooPrice]
GO
ALTER TABLE [dbo].[MenuRoles] ADD  DEFAULT (N'') FOR [CreatedBy]
GO
ALTER TABLE [dbo].[MenuRoles] ADD  DEFAULT (N'') FOR [UpdatedBy]
GO
ALTER TABLE [dbo].[Payments] ADD  DEFAULT ((0.0000000000000000e+000)) FOR [TipAmount]
GO
ALTER TABLE [dbo].[Referals] ADD  DEFAULT ((0)) FOR [Commission]
GO
ALTER TABLE [dbo].[Referals] ADD  DEFAULT ('0001-01-01') FOR [ReferalAppointDate]
GO
ALTER TABLE [dbo].[Tips] ADD  DEFAULT (CONVERT([bit],(0))) FOR [TipSettlement]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [Occupation]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [FirstPasswordReset]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [Percentage]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [DefaultTips]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Referals_ReferalId] FOREIGN KEY([ReferalId])
REFERENCES [dbo].[Referals] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Referals_ReferalId]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Users_UserId]
GO
ALTER TABLE [dbo].[MenuRoles]  WITH CHECK ADD  CONSTRAINT [FK_MenuRoles_Menus_MenuId] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menus] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MenuRoles] CHECK CONSTRAINT [FK_MenuRoles_Menus_MenuId]
GO
ALTER TABLE [dbo].[MenuRoles]  WITH CHECK ADD  CONSTRAINT [FK_MenuRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MenuRoles] CHECK CONSTRAINT [FK_MenuRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Appointments_AppointmentId] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Appointments_AppointmentId]
GO
ALTER TABLE [dbo].[Tips]  WITH CHECK ADD  CONSTRAINT [FK_Tips_Appointments_AppointmentId] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tips] CHECK CONSTRAINT [FK_Tips_Appointments_AppointmentId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
