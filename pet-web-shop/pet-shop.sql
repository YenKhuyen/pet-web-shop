USE [master]
GO
/****** Object:  Database [pet-shop]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE DATABASE [pet-shop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'pet-shop', FILENAME = N'E:\Program Files\SQL\MSSQL16.MSSQLSERVER01\MSSQL\DATA\pet-shop.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'pet-shop_log', FILENAME = N'E:\Program Files\SQL\MSSQL16.MSSQLSERVER01\MSSQL\DATA\pet-shop_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [pet-shop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pet-shop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pet-shop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pet-shop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pet-shop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pet-shop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pet-shop] SET ARITHABORT OFF 
GO
ALTER DATABASE [pet-shop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [pet-shop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pet-shop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pet-shop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pet-shop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pet-shop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pet-shop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pet-shop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pet-shop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pet-shop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [pet-shop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pet-shop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pet-shop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pet-shop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pet-shop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pet-shop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pet-shop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pet-shop] SET RECOVERY FULL 
GO
ALTER DATABASE [pet-shop] SET  MULTI_USER 
GO
ALTER DATABASE [pet-shop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pet-shop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pet-shop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pet-shop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pet-shop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [pet-shop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'pet-shop', N'ON'
GO
ALTER DATABASE [pet-shop] SET QUERY_STORE = ON
GO
ALTER DATABASE [pet-shop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [pet-shop]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_about]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_about](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contents] [nvarchar](max) NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
 CONSTRAINT [PK_dbo.tb_about] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_account]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[role] [int] NOT NULL,
	[status] [int] NOT NULL,
	[first_name] [nvarchar](225) NOT NULL,
	[last_name] [nvarchar](225) NOT NULL,
	[address] [nvarchar](225) NULL,
	[date_of_birth] [datetime] NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[avatar] [image] NULL,
	[phone_number] [nvarchar](50) NULL,
	[gender] [int] NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
 CONSTRAINT [PK_dbo.tb_account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_cart]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
	[status] [int] NOT NULL,
	[price] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tb_cart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_category]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[description] [nvarchar](max) NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
 CONSTRAINT [PK_dbo.tb_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_image_product]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_image_product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[image] [nvarchar](500) NOT NULL,
	[isDefault] [bit] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
 CONSTRAINT [PK_dbo.tb_image_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_order]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](max) NOT NULL,
	[customer_name] [nvarchar](max) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NOT NULL,
	[total_price] [decimal](18, 2) NOT NULL,
	[status] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tb_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_order_detail]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_order_detail](
	[order_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[quantity] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
 CONSTRAINT [PK_dbo.tb_order_detail] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_post]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_post](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[image] [varbinary](max) NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[sub_title] [nvarchar](max) NOT NULL,
	[user_id] [int] NOT NULL,
	[contents] [nvarchar](max) NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
 CONSTRAINT [PK_dbo.tb_post] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_product]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](max) NOT NULL,
	[category_id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[price] [int] NOT NULL,
	[sale] [int] NULL,
	[sold_count] [int] NULL,
	[quantity] [int] NOT NULL,
	[status] [int] NULL,
	[detail] [nvarchar](max) NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
	[isHot] [bit] NOT NULL,
	[isNew] [bit] NOT NULL,
	[isSale] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.tb_product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_review]    Script Date: 5/5/2024 11:47:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[comment] [nvarchar](max) NULL,
	[user_id] [int] NOT NULL,
	[created] [datetime] NOT NULL,
	[modified] [datetime] NULL,
	[post_id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.tb_review] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_product_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_product_id] ON [dbo].[tb_cart]
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_user_id] ON [dbo].[tb_cart]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_product_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_product_id] ON [dbo].[tb_image_product]
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_user_id] ON [dbo].[tb_order]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_order_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_order_id] ON [dbo].[tb_order_detail]
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_product_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_product_id] ON [dbo].[tb_order_detail]
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_user_id] ON [dbo].[tb_post]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_category_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_category_id] ON [dbo].[tb_product]
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_post_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_post_id] ON [dbo].[tb_review]
(
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_user_id]    Script Date: 5/5/2024 11:47:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_user_id] ON [dbo].[tb_review]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tb_about] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [created]
GO
ALTER TABLE [dbo].[tb_cart] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[tb_cart] ADD  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[tb_post] ADD  DEFAULT ('') FOR [contents]
GO
ALTER TABLE [dbo].[tb_post] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [created]
GO
ALTER TABLE [dbo].[tb_review] ADD  DEFAULT ((0)) FOR [post_id]
GO
ALTER TABLE [dbo].[tb_cart]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_cart_dbo.tb_account_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[tb_account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_cart] CHECK CONSTRAINT [FK_dbo.tb_cart_dbo.tb_account_user_id]
GO
ALTER TABLE [dbo].[tb_cart]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_cart_dbo.tb_product_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[tb_product] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_cart] CHECK CONSTRAINT [FK_dbo.tb_cart_dbo.tb_product_product_id]
GO
ALTER TABLE [dbo].[tb_image_product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_image_product_dbo.tb_product_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[tb_product] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_image_product] CHECK CONSTRAINT [FK_dbo.tb_image_product_dbo.tb_product_product_id]
GO
ALTER TABLE [dbo].[tb_order]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_order_dbo.tb_account_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[tb_account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_order] CHECK CONSTRAINT [FK_dbo.tb_order_dbo.tb_account_user_id]
GO
ALTER TABLE [dbo].[tb_order_detail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_order_detail_dbo.tb_order_order_id] FOREIGN KEY([order_id])
REFERENCES [dbo].[tb_order] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_order_detail] CHECK CONSTRAINT [FK_dbo.tb_order_detail_dbo.tb_order_order_id]
GO
ALTER TABLE [dbo].[tb_order_detail]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_order_detail_dbo.tb_product_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[tb_product] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_order_detail] CHECK CONSTRAINT [FK_dbo.tb_order_detail_dbo.tb_product_product_id]
GO
ALTER TABLE [dbo].[tb_post]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_post_dbo.tb_account_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[tb_account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_post] CHECK CONSTRAINT [FK_dbo.tb_post_dbo.tb_account_user_id]
GO
ALTER TABLE [dbo].[tb_product]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_product_dbo.tb_category_category_id] FOREIGN KEY([category_id])
REFERENCES [dbo].[tb_category] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_product] CHECK CONSTRAINT [FK_dbo.tb_product_dbo.tb_category_category_id]
GO
ALTER TABLE [dbo].[tb_review]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_review_dbo.tb_account_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[tb_account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tb_review] CHECK CONSTRAINT [FK_dbo.tb_review_dbo.tb_account_user_id]
GO
ALTER TABLE [dbo].[tb_review]  WITH CHECK ADD  CONSTRAINT [FK_dbo.tb_review_dbo.tb_post_post_id] FOREIGN KEY([post_id])
REFERENCES [dbo].[tb_post] ([id])
GO
ALTER TABLE [dbo].[tb_review] CHECK CONSTRAINT [FK_dbo.tb_review_dbo.tb_post_post_id]
GO
USE [master]
GO
ALTER DATABASE [pet-shop] SET  READ_WRITE 
GO
