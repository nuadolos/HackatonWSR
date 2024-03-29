USE [master]
GO
/****** Object:  Database [HackatonWSR]    Script Date: 02.08.2023 13:35:12 ******/
CREATE DATABASE [HackatonWSR]
GO
USE [HackatonWSR]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberId] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[TimeActivity] [time](7) NOT NULL,
	[JuryId] [int] NOT NULL,
 CONSTRAINT [PK_Activity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityDocument]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityDocument](
	[ActivityId] [int] NOT NULL,
	[DocumentId] [int] NOT NULL,
	[Nothing] [nvarchar](5) NULL,
 CONSTRAINT [PK_ActivityDocument] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC,
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityParticipant]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityParticipant](
	[ActivityId] [int] NOT NULL,
	[ParticipantId] [int] NOT NULL,
	[Nothing] [nvarchar](5) NULL,
 CONSTRAINT [PK_ActivityParticipant] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC,
	[ParticipantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direction]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Direction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Resource] [varbinary](max) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[DirectionId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[DateEvent] [date] NOT NULL,
	[StartEvent] [time](7) NOT NULL,
	[EndEvent] [time](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Logo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventActivity]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventActivity](
	[EventId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
	[Nothing] [nvarchar](5) NULL,
 CONSTRAINT [PK_EventActivity] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02.08.2023 13:35:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Middlename] [nvarchar](50) NOT NULL,
	[GenderId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[EventId] [int] NULL,
	[Birthday] [date] NULL,
	[CountryId] [int] NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [NumberId], [Password]) VALUES (6, 1001, N'Dols#22')
INSERT [dbo].[Account] ([Id], [NumberId], [Password]) VALUES (7, 1002, N'folp$64')
INSERT [dbo].[Account] ([Id], [NumberId], [Password]) VALUES (8, 1003, N'gpgw$55')
INSERT [dbo].[Account] ([Id], [NumberId], [Password]) VALUES (11, 1004, N'Wops#21')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Activity] ON 

INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (8, N'Сбор участников', CAST(N'10:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (9, N'Презентация. Часть 1', CAST(N'11:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (10, N'Ответы на вопросы', CAST(N'13:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (11, N'Презентация. Часть 2', CAST(N'15:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (12, N'Ответы на вопросы', CAST(N'17:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (13, N'Представление ИИ', CAST(N'18:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (14, N'Сбор участников', CAST(N'10:00:00' AS Time), 1004)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (15, N'Презентация. Часть 1', CAST(N'11:45:00' AS Time), 1004)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (16, N'Ответы на вопросы', CAST(N'13:30:00' AS Time), 1004)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (17, N'Презентация. Часть 2', CAST(N'15:15:00' AS Time), 1004)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (18, N'Ответы на вопросы', CAST(N'17:00:00' AS Time), 1004)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (19, N'Представление ИИ', CAST(N'18:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (42, N'Вступление', CAST(N'10:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (43, N'Обновленная ОС IOS', CAST(N'11:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (44, N'Технология Mac', CAST(N'13:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (45, N'Представление нового IPhone 14', CAST(N'15:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (46, N'Оценка производительности', CAST(N'17:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (47, N'Оптимизация', CAST(N'18:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (48, N'Вступление', CAST(N'09:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (49, N'Представление', CAST(N'10:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (50, N'Общественный показ', CAST(N'12:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (51, N'Демонстрация функций. Часть 1', CAST(N'14:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (52, N'Демонстрация функций. Часть 1', CAST(N'16:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (53, N'Демонстрация функций. Часть 1', CAST(N'17:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (54, N'Робот в деятельности полиции', CAST(N'19:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (55, N'Эксплуатация', CAST(N'21:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (56, N'Вступление разработчиков', CAST(N'00:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (57, N'Представление новых фитч', CAST(N'01:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (58, N'Исправление багов', CAST(N'03:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (59, N'Презентация про ASP.NET. Часть 1', CAST(N'05:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (60, N'Презентация про ASP.NET. Часть 2', CAST(N'07:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (61, N'Презентация про ASP.NET. Часть 3', CAST(N'08:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (62, N'Презентация про ASP.NET. Часть 4', CAST(N'10:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (63, N'Подробное знакомство с ASP.NET. Часть 1', CAST(N'12:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (64, N'Обед', CAST(N'14:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (65, N'Подробное знакомство с ASP.NET. Часть 2', CAST(N'15:45:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (66, N'Подробное знакомство с ASP.NET. Часть 3', CAST(N'17:30:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (67, N'Ужин', CAST(N'19:15:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (68, N'Подведение итогов', CAST(N'21:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (74, N'Основы тестирования', CAST(N'12:00:00' AS Time), 1005)
INSERT [dbo].[Activity] ([Id], [Title], [TimeActivity], [JuryId]) VALUES (75, N'Практическое задание', CAST(N'13:45:00' AS Time), 1005)
SET IDENTITY_INSERT [dbo].[Activity] OFF
GO
INSERT [dbo].[ActivityDocument] ([ActivityId], [DocumentId], [Nothing]) VALUES (16, 1, NULL)
INSERT [dbo].[ActivityDocument] ([ActivityId], [DocumentId], [Nothing]) VALUES (19, 2, NULL)
GO
INSERT [dbo].[ActivityParticipant] ([ActivityId], [ParticipantId], [Nothing]) VALUES (18, 1002, NULL)
INSERT [dbo].[ActivityParticipant] ([ActivityId], [ParticipantId], [Nothing]) VALUES (18, 1003, NULL)
GO
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([Id], [Name]) VALUES (1, N'Москва')
INSERT [dbo].[City] ([Id], [Name]) VALUES (2, N'Санкт-Петербург')
INSERT [dbo].[City] ([Id], [Name]) VALUES (3, N'Екатеринбург')
INSERT [dbo].[City] ([Id], [Name]) VALUES (4, N'Новосибирск')
INSERT [dbo].[City] ([Id], [Name]) VALUES (5, N'Владивосток')
INSERT [dbo].[City] ([Id], [Name]) VALUES (6, N'Хабаровск')
INSERT [dbo].[City] ([Id], [Name]) VALUES (7, N'Киев')
INSERT [dbo].[City] ([Id], [Name]) VALUES (8, N'Одесса')
INSERT [dbo].[City] ([Id], [Name]) VALUES (9, N'Сочи')
INSERT [dbo].[City] ([Id], [Name]) VALUES (10, N'Краснодар')
INSERT [dbo].[City] ([Id], [Name]) VALUES (13, N'Минск')
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'Абхазия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (2, N'Беларусь')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (3, N'Венгрия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (4, N'Греция')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (5, N'Дания')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (6, N'Египет')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (7, N'Зимбабве')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (8, N'Италия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (9, N'Кипр')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (10, N'Латвия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (11, N'Мальдивы')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (12, N'Норвегия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (13, N'Польша')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (14, N'Румыния')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (15, N'Сербия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (16, N'Турция')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (17, N'Украина')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (18, N'Франция')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (19, N'Хорватия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (20, N'Чехия')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (21, N'Швеция')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (22, N'Эстония')
INSERT [dbo].[Country] ([Id], [Name]) VALUES (23, N'Япония')
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Direction] ON 

INSERT [dbo].[Direction] ([Id], [Name]) VALUES (1, N'Программирование')
INSERT [dbo].[Direction] ([Id], [Name]) VALUES (2, N'Робототехника')
INSERT [dbo].[Direction] ([Id], [Name]) VALUES (3, N'Искусственный интеллект')
INSERT [dbo].[Direction] ([Id], [Name]) VALUES (4, N'3D Моделирование')
INSERT [dbo].[Direction] ([Id], [Name]) VALUES (7, N'Тестирование')
SET IDENTITY_INSERT [dbo].[Direction] OFF
GO
SET IDENTITY_INSERT [dbo].[Document] ON 

INSERT [dbo].[Document] ([Id], [Name], [Resource]) VALUES (1, N'Презентация.pptx', NULL)
INSERT [dbo].[Document] ([Id], [Name], [Resource]) VALUES (2, N'ИС Алиэкспресс.docx', NULL)
SET IDENTITY_INSERT [dbo].[Document] OFF
GO
SET IDENTITY_INSERT [dbo].[Event] ON 

INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (1, N'Разработка ПО', 1, 1, CAST(N'2021-10-15' AS Date), CAST(N'12:00:00' AS Time), CAST(N'20:00:00' AS Time), N'Новое программное обеспечение', NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (2, N'Презентация "Маруся"', 3, 4, CAST(N'2021-10-20' AS Date), CAST(N'11:00:00' AS Time), CAST(N'21:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (3, N'Создание управляемой машины', 2, 6, CAST(N'2021-10-25' AS Date), CAST(N'09:00:00' AS Time), CAST(N'19:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (4, N'Новая эра изделий', 4, 2, CAST(N'2021-10-30' AS Date), CAST(N'10:00:00' AS Time), CAST(N'22:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (5, N'Visual Studio 2022', 1, 7, CAST(N'2021-11-12' AS Date), CAST(N'12:00:00' AS Time), CAST(N'22:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (9, N'Новое оружие против ленивых', 3, 3, CAST(N'0001-01-01' AS Date), CAST(N'10:00:00' AS Time), CAST(N'22:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (11, N'Новые технологии Apple', 1, 1, CAST(N'2021-11-29' AS Date), CAST(N'10:00:00' AS Time), CAST(N'21:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (12, N'Полицейский робот', 2, 1, CAST(N'2021-11-29' AS Date), CAST(N'09:00:00' AS Time), CAST(N'23:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (13, N'ASP.NET. Преимущества технологии', 1, 4, CAST(N'2021-12-01' AS Date), CAST(N'00:00:00' AS Time), CAST(N'23:00:00' AS Time), NULL, NULL)
INSERT [dbo].[Event] ([Id], [Title], [DirectionId], [CityId], [DateEvent], [StartEvent], [EndEvent], [Description], [Logo]) VALUES (17, N'Тестирование серверов', 7, 13, CAST(N'2021-12-01' AS Date), CAST(N'12:00:00' AS Time), CAST(N'17:00:00' AS Time), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (5, 14, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (5, 15, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (5, 16, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (5, 17, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (5, 18, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (5, 19, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (9, 8, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (9, 9, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (9, 10, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (9, 11, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (9, 12, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (9, 13, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (11, 42, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (11, 43, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (11, 44, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (11, 45, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (11, 46, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (11, 47, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 48, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 49, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 50, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 51, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 52, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 53, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 54, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (12, 55, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 56, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 57, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 58, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 59, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 60, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 61, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 62, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 63, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 64, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 65, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 66, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 67, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (13, 68, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (17, 74, NULL)
INSERT [dbo].[EventActivity] ([EventId], [ActivityId], [Nothing]) VALUES (17, 75, NULL)
GO
SET IDENTITY_INSERT [dbo].[Gender] ON 

INSERT [dbo].[Gender] ([Id], [Name]) VALUES (1, N'Мужской')
INSERT [dbo].[Gender] ([Id], [Name]) VALUES (2, N'Женский')
SET IDENTITY_INSERT [dbo].[Gender] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Name]) VALUES (1, N'Participant')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (2, N'Organizer')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (3, N'Moderator')
INSERT [dbo].[Role] ([Id], [Name]) VALUES (4, N'Jury')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1001, N'Рубцов', N'Владимир', N'Андреевич', 1, 2, NULL, CAST(N'1994-08-02' AS Date), 4, N'+7(952)455-93-57', N'rva@gmail.com')
INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1002, N'Носорогов', N'Павел', N'Олегович', 1, 1, NULL, CAST(N'1991-12-25' AS Date), 3, N'+7(922)785-59-88', N'npo@gmail.com')
INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1003, N'Тильсон', N'Александр', N'Алексеевич', 1, 1, NULL, CAST(N'2000-06-14' AS Date), 7, N'+7(967)794-00-83', N'taa@gmail.com')
INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1004, N'Тактова', N'Виктория', N'Александровна', 2, 3, NULL, CAST(N'2001-01-18' AS Date), 22, N'+7(901)425-11-23', N'tva@gmail.com')
INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1005, N'Коволева', N'Екатерина', N'Петровна', 2, 4, NULL, CAST(N'1998-11-28' AS Date), 17, N'+7(902)892-74-55', N'kep@gmail.com')
INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1007, N'Интеракова', N'Светлана', N'Андреевна', 2, 4, NULL, CAST(N'1998-08-30' AS Date), 13, N'+7(912)822-75-55', N'isa@gmail.com')
INSERT [dbo].[Users] ([Id], [Surname], [Name], [Middlename], [GenderId], [RoleId], [EventId], [Birthday], [CountryId], [Phone], [Email]) VALUES (1008, N'Букина', N'Александра', N'Владимировна', 2, 4, NULL, CAST(N'1995-12-18' AS Date), 6, N'+7(922)772-44-68', N'bav@gmail.com')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Users] FOREIGN KEY([NumberId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Users]
GO
ALTER TABLE [dbo].[Activity]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Users] FOREIGN KEY([JuryId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Activity] CHECK CONSTRAINT [FK_Activity_Users]
GO
ALTER TABLE [dbo].[ActivityDocument]  WITH CHECK ADD  CONSTRAINT [FK_ActivityDocument_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActivityDocument] CHECK CONSTRAINT [FK_ActivityDocument_Activity]
GO
ALTER TABLE [dbo].[ActivityDocument]  WITH CHECK ADD  CONSTRAINT [FK_ActivityDocument_Document] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Document] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActivityDocument] CHECK CONSTRAINT [FK_ActivityDocument_Document]
GO
ALTER TABLE [dbo].[ActivityParticipant]  WITH CHECK ADD  CONSTRAINT [FK_ActivityParticipant_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ActivityParticipant] CHECK CONSTRAINT [FK_ActivityParticipant_Activity]
GO
ALTER TABLE [dbo].[ActivityParticipant]  WITH CHECK ADD  CONSTRAINT [FK_ActivityParticipant_Users] FOREIGN KEY([ParticipantId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ActivityParticipant] CHECK CONSTRAINT [FK_ActivityParticipant_Users]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_City]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Direction] FOREIGN KEY([DirectionId])
REFERENCES [dbo].[Direction] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Direction]
GO
ALTER TABLE [dbo].[EventActivity]  WITH CHECK ADD  CONSTRAINT [FK_EventActivity_Activity] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EventActivity] CHECK CONSTRAINT [FK_EventActivity_Activity]
GO
ALTER TABLE [dbo].[EventActivity]  WITH CHECK ADD  CONSTRAINT [FK_EventActivity_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EventActivity] CHECK CONSTRAINT [FK_EventActivity_Event]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Country]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Event1] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Event1]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Gender] FOREIGN KEY([GenderId])
REFERENCES [dbo].[Gender] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Gender]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
USE [master]
GO
ALTER DATABASE [HackatonWSR] SET  READ_WRITE 
GO
