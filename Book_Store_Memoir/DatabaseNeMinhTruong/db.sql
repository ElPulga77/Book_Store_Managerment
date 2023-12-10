USE [master]
GO
/****** Object:  Database [Book_Store_Memoir]    Script Date: 10/12/2023 2:20:22 CH ******/
CREATE DATABASE [Book_Store_Memoir]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Book_Store_Memoir', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\Book_Store_Memoir.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Book_Store_Memoir_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\Book_Store_Memoir_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Book_Store_Memoir] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Book_Store_Memoir].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Book_Store_Memoir] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET ARITHABORT OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Book_Store_Memoir] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Book_Store_Memoir] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Book_Store_Memoir] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Book_Store_Memoir] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Book_Store_Memoir] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET RECOVERY FULL 
GO
ALTER DATABASE [Book_Store_Memoir] SET  MULTI_USER 
GO
ALTER DATABASE [Book_Store_Memoir] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Book_Store_Memoir] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Book_Store_Memoir] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Book_Store_Memoir] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Book_Store_Memoir] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Book_Store_Memoir] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Book_Store_Memoir', N'ON'
GO
ALTER DATABASE [Book_Store_Memoir] SET QUERY_STORE = OFF
GO
USE [Book_Store_Memoir]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/12/2023 2:20:23 CH ******/
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
/****** Object:  Table [dbo].[Admins]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Admins] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameAuhor] [nvarchar](max) NULL,
	[Hometown] [nvarchar](255) NULL,
	[Birthday] [date] NULL,
	[Death] [date] NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookAuthors]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAuthors](
	[BookId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_BookAuthors] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[ISBN] [nvarchar](max) NULL,
	[Publication_Date] [datetime2](7) NULL,
	[PageNumber] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Publisher_Id] [int] NULL,
	[Language_Id] [int] NULL,
	[Category_Id] [int] NULL,
	[Image] [nvarchar](max) NULL,
	[Price] [float] NULL,
	[PublisherId] [int] NULL,
	[LanguageId] [int] NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryReceipts]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryReceipts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryDate] [datetime] NULL,
	[Note] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[OrderId] [int] NULL,
	[ShipperId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Language_Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Book_Id] [int] NULL,
	[Order_Id] [int] NULL,
	[TotalAmount] [float] NULL,
	[Quantity] [int] NULL,
	[OrdersId] [int] NULL,
	[BookId] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NULL,
	[CustomerId] [int] NULL,
	[TotalAmount] [float] NULL,
	[ShipperId] [int] NULL,
	[OrderStatusId] [int] NULL,
	[RecieverName] [nvarchar](255) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[OrderDate] [date] NULL,
	[CustomersCustomerId] [int] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Id] [int] NOT NULL,
	[Status] [nvarchar](max) NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptDetails]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NULL,
	[DeliveryReceiptId] [int] NULL,
	[Quantity] [int] NULL,
	[TotalAmount] [float] NULL,
 CONSTRAINT [PK__ReceiptD__3214EC0700288775] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipper]    Script Date: 10/12/2023 2:20:23 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipper](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[BirthDay] [nvarchar](max) NULL,
	[ImageAvt] [nvarchar](max) NULL,
	[CCCD] [nvarchar](max) NULL,
	[Adress] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_BookAuthors_AuthorId]    Script Date: 10/12/2023 2:20:23 CH ******/
CREATE NONCLUSTERED INDEX [IX_BookAuthors_AuthorId] ON [dbo].[BookAuthors]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Books_Category_Id]    Script Date: 10/12/2023 2:20:23 CH ******/
CREATE NONCLUSTERED INDEX [IX_Books_Category_Id] ON [dbo].[Books]
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_OrdersId]    Script Date: 10/12/2023 2:20:23 CH ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_OrdersId] ON [dbo].[OrderDetails]
(
	[OrdersId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_BookId]    Script Date: 10/12/2023 2:20:23 CH ******/
CREATE NONCLUSTERED INDEX [IX_Orders_BookId] ON [dbo].[Orders]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookAuthors]  WITH CHECK ADD  CONSTRAINT [FK_BookAuthors_Authors_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookAuthors] CHECK CONSTRAINT [FK_BookAuthors_Authors_AuthorId]
GO
ALTER TABLE [dbo].[BookAuthors]  WITH CHECK ADD  CONSTRAINT [FK_BookAuthors_Books_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookAuthors] CHECK CONSTRAINT [FK_BookAuthors_Books_BookId]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Categories_Category_Id] FOREIGN KEY([Category_Id])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Categories_Category_Id]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Languages] FOREIGN KEY([Language_Id])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Languages]
GO
ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Books_Publisher] FOREIGN KEY([Publisher_Id])
REFERENCES [dbo].[Publisher] ([Id])
GO
ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Books_Publisher]
GO
ALTER TABLE [dbo].[DeliveryReceipts]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[DeliveryReceipts]  WITH CHECK ADD FOREIGN KEY([ShipperId])
REFERENCES [dbo].[Shipper] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Books] FOREIGN KEY([Book_Id])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Books]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrdersId] FOREIGN KEY([OrdersId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrdersId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderStatus] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[OrderStatus] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_OrderStatus]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Shipper] FOREIGN KEY([ShipperId])
REFERENCES [dbo].[Shipper] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Shipper]
GO
ALTER TABLE [dbo].[ReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK__ReceiptDe__BookI__43D61337] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[ReceiptDetails] CHECK CONSTRAINT [FK__ReceiptDe__BookI__43D61337]
GO
ALTER TABLE [dbo].[ReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK__ReceiptDe__Deliv__44CA3770] FOREIGN KEY([DeliveryReceiptId])
REFERENCES [dbo].[DeliveryReceipts] ([Id])
GO
ALTER TABLE [dbo].[ReceiptDetails] CHECK CONSTRAINT [FK__ReceiptDe__Deliv__44CA3770]
GO
USE [master]
GO
ALTER DATABASE [Book_Store_Memoir] SET  READ_WRITE 
GO
