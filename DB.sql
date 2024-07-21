USE [master]
GO
/****** Object:  Database [ProjectPRN211_HuongNT7_G6]    Script Date: 7/21/2024 1:52:04 AM ******/
CREATE DATABASE [ProjectPRN211_HuongNT7_G6]

GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectPRN211_HuongNT7_G6].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProjectPRN211_HuongNT7_G6]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](30) NOT NULL,
	[wallet] [money] NOT NULL,
	[role_id] [int] NOT NULL,
	[address] [nvarchar](1000) NOT NULL,
	[phone_number] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[account_id] [int] NOT NULL,
	[medicine_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC,
	[medicine_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicine]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
	[country_id] [int] NULL,
	[expired_date] [date] NULL,
	[image] [nvarchar](max) NULL,
	[descript] [nvarchar](4000) NULL,
	[min_age] [int] NULL,
	[category_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[quantity] [int] NULL,
	[price] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [int] NOT NULL,
	[total_money] [money] NOT NULL,
	[created_date] [date] NOT NULL,
	[address] [nvarchar](max) NULL,
	[phone_number] [varchar](10) NULL,
	[customer_name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[order_id] [int] NOT NULL,
	[medicine_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [money] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 7/21/2024 1:52:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([id], [username], [password], [wallet], [role_id], [address], [phone_number]) VALUES (1, N'lehieu', N'123456', 4000.0000, 2, N'Hanoi', N'1111112223')
GO
INSERT [dbo].[Account] ([id], [username], [password], [wallet], [role_id], [address], [phone_number]) VALUES (2, N'toru', N'123456', 2000.0000, 2, N'Japan', N'012346688')
GO
INSERT [dbo].[Account] ([id], [username], [password], [wallet], [role_id], [address], [phone_number]) VALUES (3, N'admin', N'admin', 2000.0000, 1, N'Japan', N'012346688')
GO
INSERT [dbo].[Account] ([id], [username], [password], [wallet], [role_id], [address], [phone_number]) VALUES (4, N'long', N'123456', 0.0000, 2, N'Ha Noi', N'0123456789')
GO
INSERT [dbo].[Account] ([id], [username], [password], [wallet], [role_id], [address], [phone_number]) VALUES (5, N'lehieu1', N'123456', 0.0000, 2, N'gdfgdf', N'234342344')
GO
INSERT [dbo].[Account] ([id], [username], [password], [wallet], [role_id], [address], [phone_number]) VALUES (6, N'lehieu123', N'abcdef1', 0.0000, 2, N'abcdef', N'8594308545')
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
INSERT [dbo].[Cart] ([account_id], [medicine_id], [quantity], [price]) VALUES (3, 2, 7, 84.0000)
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (1, N'Vitamins - Minerals')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (2, N'Heart')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (3, N'Digest')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (4, N'Flu - Cough - Fever')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (5, N'Skin Care')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (6, N'Functional foods')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (7, N'Osteoarthritis')
GO
INSERT [dbo].[Category] ([id], [name]) VALUES (8, N'Allergy')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (1, N'USA')
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (2, N'Canada')
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (3, N'Sweden')
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (4, N'Netherlands')
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (5, N'China')
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (6, N'India')
GO
INSERT [dbo].[Country] ([id], [name]) VALUES (7, N'England')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Medicine] ON 
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (1, N'Siro Fer Multi Hatro', 2, CAST(N'2026-03-03' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/DSC_08324_453bb032ed.jpg', N'Fer Multi Hatro syrup supplements iron and B vitamins for the body. Supports increased blood formation ability, helping reduce the risk of iron deficiency anemia. Supports health improvement.', 8, 1, 2, 200, 6.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (2, N'Dacolfort', 3, CAST(N'2028-04-04' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00029275_dacolfort_500mg_danapha_3x10_6954_6062_large_fdad157540.jpg', N'Dacolfort is produced by Danapha Pharmaceutical, with main ingredients Diosminn and Hesperidin, indicated to treat symptoms related to venous and lymphatic insufficiency (heavy legs, pain, uncomfortable legs in the morning). ). Treatment of functional signs related to acute hemorrhoids.', 40, 2, 5, 390, 12.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (3, N'Telmisartan', 4, CAST(N'2025-05-05' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/IMG_3890_6c0c5b5b5d.jpg', N'The drug is produced by TV.PHARM Pharmaceutical Joint Stock Company. The drug''s main ingredient is telmisartan, used to treat hypertension. It can be used alone or in combination with other antihypertensive drugs.', 50, 2, 5, 4, 4.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (4, N'Soki Miter', 1, CAST(N'2025-03-05' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00501624_soki_miter_20_goi_7086_62fb_large_b763f7ad9b.jpg', N'Soki Miter helps support digestion, reduce bloating, indigestion, and constipation.', 3, 3, 2, 50, 9.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (5, N'Tussiday OPC', 7, CAST(N'2024-03-05' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00032610_vien_tri_ho_tussiday_opc_10x10_2392_6189_large_4e859234f9.jpg', N'Tussiday cough medicine is a product of Opc Pharmaceuticals made from natural medicinal herbs containing Eucalyptol, ash essential oil, and ginger essential oil to treat coughs, sore throats, runny noses, and flu.', 12, 4, 4, 5, 9.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (6, N'Optimax Immunity', 6, CAST(N'2026-03-12' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00033702_vien_uong_tang_suc_khang_optimax_immunity_booster_vid_fighter_20v_4698_62ae_large_b9fe136dbf.jpg', N'Optimax Immunity Booster Vid-Fighter helps supplement vitamin C, vitamin D, zinc and manganese to the body, helping to increase resistance.', 12, 4, 3, 4, 6.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (7, N'Acetab Extra Agimexpharm', 6, CAST(N'2029-12-07' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00033094_acetab_extra_agimexpharm_10x10_8890_61c9_large_198442652a.jpg', N'Acetab Extra is a product of Agimexpharm Pharmaceutical Joint Stock Company containing the main ingredients Paracetamol and Caffeine used to treat flu symptoms: Headache, fever, sore throat, muscle aches.', 16, 4, 5, 17, 9.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (8, N'Liversoft', 1, CAST(N'2023-12-03' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/DSC_09561_8f72c0c4a0.jpg', N'Liversoft supports liver protection, enhances liver function, supports liver detoxification, and helps limit the harmful effects of alcohol and chemicals that affect the liver.', 18, 6, 4, 9, 14.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (9, N'Telfor DHG', 2, CAST(N'2024-11-04' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00021755_telfor_120mg_dhg_2x10_5243_5d9d_large_c01f32be12.jpg', N'Telfor 120Mg DHG 2X10 is produced by Hau Giang Pharmaceutical Company, the main active ingredient is fexofenadine HCl, used in cases of allergic rhinitis or chronic spontaneous urticaria.', 18, 8, 5, 100, 2.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (10, N'Glotadol ', 7, CAST(N'2023-11-16' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/375x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00030829_glotadol_150mg_abbott_glomed_20_goi_x_2g_9308_617b_large_c6d88b5169.JPG', N'Glotadol 150mg package contains paracetamol active ingredient, produced by Glomed Pharmaceutical Joint Stock Company. Glotadol 150mg is antipyretic and reduces pain due to the flu or common cold, headache, sore throat, teething, vaccinated, tonsillectomy.', 1, 6, 1, 100, 5.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (11, N'Cledomox', 1, CAST(N'2030-03-03' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00502567_cledomox_625mg_tenamyd_2x7_1547_63e3_large_61024b768d.jpg', N'CLEDOMOX is a product of Medopharm with pharmaceutical ingredients including amoxicillin trihydrate BP and potassium clavulanate. This is a drug for patients treating bacterial infections caused by sensitive bacteria, some diseases such as tonsillitis, sinusitis, otitis media; Exacection of chronic bronchitis, hydrogen pneumonia and bronchitis. Genital urinary tract infections such as cystitis, urethritis, nephritis - pyelonephritis; boils, abscesses, cellular inflammation, wound infections; Bone and joint infections like boneitis', 18, 1, 2, 2000, 30.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (12, N'Quinine Sulphate', 2, CAST(N'2028-04-04' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00006232_d11693f658.jpg', N'Quinin Sulfate 250 mg of Mekophar Pharmaceutical - Pharmaceutical Joint Stock Company, the main ingredient containing Quinin Sulfate, is a drug used to treat malaria.', 12, 3, 4, 999, 254.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (13, N'Acetab Extra Agimexpharm', 4, CAST(N'2025-05-05' AS Date), N'https://cdn.nhathuoclongchau.com.vn/unsafe/636x0/filters:quality(90)/https://cms-prod.s3-sgn09.fptcloud.com/00033094_acetab_extra_agimexpharm_10x10_8890_61c9_large_198442652a.jpg', N'Acetab Extra is a product of Agimexpharm Pharmaceutical Joint Stock Company containing the main ingredient Paracetamol and caffeine used to treat the symptoms of the flu: headache, fever, sore throat, muscle aches.', 12, 3, 3, 40, 17.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (14, N'ádadasd', 4, CAST(N'2024-07-25' AS Date), N'kk', N'kkkkkkkk', 2, 1, 3, 0, 6.0000)
GO
INSERT [dbo].[Medicine] ([id], [name], [country_id], [expired_date], [image], [descript], [min_age], [category_id], [type_id], [quantity], [price]) VALUES (15, N'Siro Fer Multi Hatro222', 2, CAST(N'2024-07-27' AS Date), N'kk', N'kkkkkkkk', 2222, 1, 1, 22, 22.0000)
GO
SET IDENTITY_INSERT [dbo].[Medicine] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (1, 1, 22.0000, CAST(N'2023-11-11' AS Date), N'Hanoi', N'012345678', N'cr7')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (2, 1, 24.0000, CAST(N'2023-11-11' AS Date), N'Hanoi', N'012345678', N'long')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (3, 4, 12.0000, CAST(N'2023-11-11' AS Date), N'Thach That', N'0123456789', N'Kien')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (4, 1, 73.0000, CAST(N'2023-11-27' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (5, 1, 25.0000, CAST(N'2024-07-15' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (6, 1, 88.0000, CAST(N'2024-07-18' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (7, 1, 15.0000, CAST(N'2024-07-21' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (8, 1, 12.0000, CAST(N'2024-07-21' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (9, 1, 12.0000, CAST(N'2024-07-21' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (10, 1, 4.0000, CAST(N'2024-07-21' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (11, 1, 27.0000, CAST(N'2024-07-21' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
INSERT [dbo].[Order] ([id], [account_id], [total_money], [created_date], [address], [phone_number], [customer_name]) VALUES (12, 1, 4.0000, CAST(N'2024-07-21' AS Date), N'Hanoi', N'012345678', N'lehieu')
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (1, 1, 2, 10.0000, 1)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (1, 2, 1, 12.0000, 2)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (2, 2, 2, 24.0000, 3)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (3, 2, 1, 12.0000, 4)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (4, 1, 5, 25.0000, 5)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (4, 6, 8, 48.0000, 6)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (5, 3, 4, 16.0000, 7)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (5, 5, 1, 9.0000, 8)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (6, 1, 8, 40.0000, 9)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (6, 2, 4, 48.0000, 10)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (7, 1, 3, 15.0000, 11)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (8, 2, 1, 12.0000, 12)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (9, 2, 1, 12.0000, 13)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (10, 3, 1, 4.0000, 14)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (11, 5, 3, 27.0000, 15)
GO
INSERT [dbo].[OrderDetails] ([order_id], [medicine_id], [quantity], [price], [id]) VALUES (12, 3, 1, 4.0000, 16)
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([id], [name]) VALUES (1, N'Admin                                             ')
GO
INSERT [dbo].[Role] ([id], [name]) VALUES (2, N'User                                              ')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 
GO
INSERT [dbo].[Type] ([id], [name]) VALUES (1, N'Injection                                         ')
GO
INSERT [dbo].[Type] ([id], [name]) VALUES (2, N'Solution                                          ')
GO
INSERT [dbo].[Type] ([id], [name]) VALUES (3, N'Effervescent tablet                               ')
GO
INSERT [dbo].[Type] ([id], [name]) VALUES (4, N'Powder                                            ')
GO
INSERT [dbo].[Type] ([id], [name]) VALUES (5, N'Tablet                                            ')
GO
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([medicine_id])
REFERENCES [dbo].[Medicine] ([id])
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD FOREIGN KEY([country_id])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Medicine]  WITH CHECK ADD FOREIGN KEY([type_id])
REFERENCES [dbo].[Type] ([id])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([id])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([medicine_id])
REFERENCES [dbo].[Medicine] ([id])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([id])
GO
USE [master]
GO
ALTER DATABASE [ProjectPRN211_HuongNT7_G6] SET  READ_WRITE 
GO
