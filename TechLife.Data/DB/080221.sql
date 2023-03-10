USE [master]
GO
/****** Object:  Database [tlsolution]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE DATABASE [tlsolution]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tlsolution', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\tlsolution.mdf' , SIZE = 9216KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'tlsolution_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\tlsolution_log.ldf' , SIZE = 17664KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [tlsolution] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tlsolution].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tlsolution] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tlsolution] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tlsolution] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tlsolution] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tlsolution] SET ARITHABORT OFF 
GO
ALTER DATABASE [tlsolution] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tlsolution] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [tlsolution] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tlsolution] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tlsolution] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tlsolution] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tlsolution] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tlsolution] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tlsolution] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tlsolution] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tlsolution] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tlsolution] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tlsolution] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tlsolution] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tlsolution] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tlsolution] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tlsolution] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tlsolution] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tlsolution] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [tlsolution] SET  MULTI_USER 
GO
ALTER DATABASE [tlsolution] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tlsolution] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tlsolution] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tlsolution] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [tlsolution]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 2/8/2021 3:33:01 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BoPhan]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoPhan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenBoPhan] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_BoPhan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BoPhanHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BoPhanHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BoPhanId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[DonViTinhId] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
 CONSTRAINT [PK_BoPhanHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DanhGia]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhGia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HoVaTen] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[SoDienThoai] [nvarchar](max) NULL,
	[SoSao] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
 CONSTRAINT [PK_DanhGia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[LoaiId] [int] NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_DanhMuc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiaPhuong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiaPhuong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenDiaPhuong] [nvarchar](max) NOT NULL,
	[ParentId] [int] NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_DiaPhuong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DichVu]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenDichVu] [nvarchar](max) NOT NULL,
	[LoaiId] [int] NOT NULL,
	[SucChua] [int] NOT NULL,
	[DVT] [nvarchar](max) NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_DichVu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DichVuHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVuHoSo](
	[DichVuId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[QuyMo] [int] NOT NULL,
	[DonViTinhId] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_DichVuHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiemVeSinh]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemVeSinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ViTri] [nvarchar](max) NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_DiemVeSinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonViTinh]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonViTinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_DonViTinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FileUploads]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileUploads](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[FileUrl] [nvarchar](max) NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Id] [int] NOT NULL,
	[IsImage] [bit] NOT NULL,
	[IsStatus] [bit] NOT NULL,
 CONSTRAINT [PK_FileUploads] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HanhTrinh]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HanhTrinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TourId] [int] NOT NULL,
	[NoiDenId] [int] NOT NULL,
	[Ngay] [int] NOT NULL,
	[Gio] [int] NOT NULL,
	[Phut] [int] NOT NULL,
	[ThoiGian] [int] NOT NULL,
	[Mota] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
 CONSTRAINT [PK_HanhTrinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenCongTy] [nvarchar](max) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[LinhVucKinhDoanhId] [int] NOT NULL,
	[HangSao] [int] NOT NULL,
	[SoQuyetDinh] [nvarchar](max) NULL,
	[NgayQuyetDinh] [datetime2](7) NOT NULL,
	[LoaiHinhId] [int] NOT NULL,
	[TongVonDauTuBanDau] [decimal](18, 2) NOT NULL,
	[TongVonDauTuBoSung] [decimal](18, 2) NOT NULL,
	[DienTichMatBang] [decimal](18, 2) NOT NULL,
	[DienTichMatBangXayDung] [decimal](18, 2) NOT NULL,
	[DienTichXayDung] [decimal](18, 2) NOT NULL,
	[SoTang] [int] NOT NULL,
	[SoGiayPhep] [nvarchar](max) NULL,
	[SoLanChuyenChu] [int] NOT NULL,
	[SoNha] [nvarchar](max) NULL,
	[DuongPho] [nvarchar](max) NULL,
	[PhuongXaId] [int] NOT NULL,
	[QuanHuyenId] [int] NOT NULL,
	[TinhThanhId] [int] NOT NULL,
	[SoDienThoai] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[HoTenNguoiDaiDien] [nvarchar](max) NULL,
	[ChucVuNguoiDaiDien] [nvarchar](max) NULL,
	[GioiTinhNguoiDaiDien] [int] NOT NULL,
	[SoDienThoaiNguoiDaiDien] [nvarchar](max) NULL,
	[SoLuongLaoDong] [int] NOT NULL,
	[DoTuoiTBNam] [int] NOT NULL,
	[DoTuoiTBNu] [int] NOT NULL,
	[KhamSucKhoeDinhKy] [bit] NOT NULL,
	[TrangPhucRieng] [bit] NOT NULL,
	[PhongChayNo] [bit] NOT NULL,
	[CNVSMoiTruong] [bit] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[ThoiDiemBatDauKinhDoanh] [datetime2](7) NOT NULL,
	[GioDongCua] [int] NOT NULL,
	[GioMoCua] [int] NOT NULL,
	[SoLDGianTiep] [int] NOT NULL,
	[SoLDNamNgoaiNuoc] [int] NOT NULL,
	[SoLDNamTrongNuoc] [int] NOT NULL,
	[SoLDNuNgoaiNuoc] [int] NOT NULL,
	[SoLDNuTrongNuoc] [int] NOT NULL,
	[SoLDThoiVu] [int] NOT NULL,
	[SoLDThuongXuyen] [int] NOT NULL,
	[SoLDTrucTiep] [int] NOT NULL,
	[TenVietTat] [nvarchar](max) NULL,
	[ViTriTrenBanDo] [nvarchar](max) NULL,
	[DonViCapPhep] [nvarchar](max) NULL,
	[MaSoCapPhep] [nvarchar](max) NULL,
	[NgayCapPhep] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_HoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HuongDanVien]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HuongDanVien](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HoVaTen] [nvarchar](max) NOT NULL,
	[GioiTinh] [bit] NOT NULL,
	[CMND] [nvarchar](max) NULL,
	[NgayCapCMND] [datetime2](7) NOT NULL,
	[NoiCapCMND] [nvarchar](max) NULL,
	[SoDienThoai] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[DiaChi] [nvarchar](max) NULL,
	[HoKhau] [nvarchar](max) NULL,
	[SoTheHDV] [nvarchar](max) NULL,
	[LoaiTheId] [int] NOT NULL,
	[NgayCapThe] [datetime2](7) NOT NULL,
	[NgayHetHan] [datetime2](7) NOT NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_HuongDanVien] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiDichVu]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiDichVu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_LoaiDichVu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiGiuong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiGiuong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_LoaiGiuong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiHinh]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHinh](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[LinhVucKinhDoanhId] [int] NOT NULL,
 CONSTRAINT [PK_LoaiHinh] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiHinhLaoDong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiHinhLaoDong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenLoaiHinh] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_LoaiHinhLaoDong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiPhong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiPhong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_LoaiPhong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoaiPhongHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiPhongHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenHienThi] [nvarchar](max) NULL,
	[LoaiPhongId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[LoaiGiuongId] [int] NOT NULL,
	[SoPhong] [int] NOT NULL,
	[DienTich] [int] NOT NULL,
	[GiaPhong] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_LoaiPhongHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Logs]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[StackTrace] [nvarchar](max) NULL,
	[Time] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
	[ParentId] [int] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MucDoThongThaoNgoaiNgu]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MucDoThongThaoNgoaiNgu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MucDo] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_MucDoThongThaoNgoaiNgu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MucDoTTNNHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MucDoTTNNHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MucDoId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonViTinhId] [int] NOT NULL,
 CONSTRAINT [PK_MucDoTTNNHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NganhDaoTao]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NganhDaoTao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenNganhDaoTao] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_NganhDaoTao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NgoaiNgu]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NgoaiNgu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenNgoaiNgu] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_NgoaiNgu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NgoaiNguHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NgoaiNguHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NgoaiNguId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonViTinhId] [int] NOT NULL,
 CONSTRAINT [PK_NgoaiNguHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Phong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoaiPhongId] [int] NOT NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[SoNguoiLon] [int] NOT NULL,
	[SoTreEm] [int] NOT NULL,
 CONSTRAINT [PK_Phong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuaTrinhHoatDong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuaTrinhHoatDong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HDVId] [int] NOT NULL,
	[HoatDong] [nvarchar](max) NULL,
	[ThoiGian] [nvarchar](max) NULL,
	[KetQua] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuaTrinhHoatDong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuocTich]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuocTich](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenQuocTich] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_QuocTich] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleGroups]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleGroups](
	[GroupId] [int] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_RoleGroups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Description] [nvarchar](200) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ThucDonHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThucDonHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HosoId] [int] NOT NULL,
	[TenThucDon] [nvarchar](max) NULL,
	[DonGia] [decimal](18, 2) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_ThucDonHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TienNghi]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TienNghi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_TienNghi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TienNghiHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TienNghiHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TienNghiId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[IsPhuPhi] [bit] NOT NULL,
	[IsSuDung] [bit] NOT NULL,
 CONSTRAINT [PK_TienNghiHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TinhChatLaoDong]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinhChatLaoDong](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ten] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_TinhChatLaoDong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tours]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tours](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoaiId] [int] NOT NULL,
	[CongTyLuHanhId] [int] NOT NULL,
	[SoNgay] [int] NOT NULL,
	[TenChuyenDi] [nvarchar](max) NOT NULL,
	[MoTaChuyenDi] [nvarchar](max) NULL,
	[Gia] [decimal](18, 2) NOT NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Tours] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Trackings]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trackings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[Action] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Trackings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrinhDo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrinhDo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TenTrinhDo] [nvarchar](max) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
	[IsStatus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_TrinhDo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrinhDoHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrinhDoHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrinhDoId] [int] NOT NULL,
	[HoSoId] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[DonViTinhId] [int] NOT NULL,
 CONSTRAINT [PK_TrinhDoHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[ProviderKey] [nvarchar](max) NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[NormalizedUserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NormalizedEmail] [nvarchar](max) NULL,
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
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Dob] [datetime2](7) NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VeDichVuHoSo]    Script Date: 2/8/2021 3:33:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VeDichVuHoSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HosoId] [int] NOT NULL,
	[TenVe] [nvarchar](max) NULL,
	[GiaVe] [decimal](18, 2) NOT NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_VeDichVuHoSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_BoPhanHoSo_BoPhanId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_BoPhanHoSo_BoPhanId] ON [dbo].[BoPhanHoSo]
(
	[BoPhanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BoPhanHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_BoPhanHoSo_HoSoId] ON [dbo].[BoPhanHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DichVuHoSo_DichVuId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_DichVuHoSo_DichVuId] ON [dbo].[DichVuHoSo]
(
	[DichVuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DichVuHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_DichVuHoSo_HoSoId] ON [dbo].[DichVuHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HanhTrinh_NoiDenId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_HanhTrinh_NoiDenId] ON [dbo].[HanhTrinh]
(
	[NoiDenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HanhTrinh_TourId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_HanhTrinh_TourId] ON [dbo].[HanhTrinh]
(
	[TourId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HoSo_LoaiHinhId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_HoSo_LoaiHinhId] ON [dbo].[HoSo]
(
	[LoaiHinhId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoaiPhongHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoaiPhongHoSo_HoSoId] ON [dbo].[LoaiPhongHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoaiPhongHoSo_LoaiGiuongId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoaiPhongHoSo_LoaiGiuongId] ON [dbo].[LoaiPhongHoSo]
(
	[LoaiGiuongId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoaiPhongHoSo_LoaiPhongId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_LoaiPhongHoSo_LoaiPhongId] ON [dbo].[LoaiPhongHoSo]
(
	[LoaiPhongId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MucDoTTNNHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_MucDoTTNNHoSo_HoSoId] ON [dbo].[MucDoTTNNHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MucDoTTNNHoSo_MucDoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_MucDoTTNNHoSo_MucDoId] ON [dbo].[MucDoTTNNHoSo]
(
	[MucDoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NgoaiNguHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_NgoaiNguHoSo_HoSoId] ON [dbo].[NgoaiNguHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_NgoaiNguHoSo_NgoaiNguId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_NgoaiNguHoSo_NgoaiNguId] ON [dbo].[NgoaiNguHoSo]
(
	[NgoaiNguId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuaTrinhHoatDong_HDVId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_QuaTrinhHoatDong_HDVId] ON [dbo].[QuaTrinhHoatDong]
(
	[HDVId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleGroups_RoleId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleGroups_RoleId] ON [dbo].[RoleGroups]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ThucDonHoSo_HosoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_ThucDonHoSo_HosoId] ON [dbo].[ThucDonHoSo]
(
	[HosoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TienNghiHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_TienNghiHoSo_HoSoId] ON [dbo].[TienNghiHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TienNghiHoSo_TienNghiId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_TienNghiHoSo_TienNghiId] ON [dbo].[TienNghiHoSo]
(
	[TienNghiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrinhDoHoSo_HoSoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_TrinhDoHoSo_HoSoId] ON [dbo].[TrinhDoHoSo]
(
	[HoSoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TrinhDoHoSo_TrinhDoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_TrinhDoHoSo_TrinhDoId] ON [dbo].[TrinhDoHoSo]
(
	[TrinhDoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VeDichVuHoSo_HosoId]    Script Date: 2/8/2021 3:33:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_VeDichVuHoSo_HosoId] ON [dbo].[VeDichVuHoSo]
(
	[HosoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BoPhan] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[BoPhan] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[BoPhanHoSo] ADD  DEFAULT ((0)) FOR [DonViTinhId]
GO
ALTER TABLE [dbo].[BoPhanHoSo] ADD  DEFAULT ((0)) FOR [SoLuong]
GO
ALTER TABLE [dbo].[DanhGia] ADD  DEFAULT ((0)) FOR [SoSao]
GO
ALTER TABLE [dbo].[DanhMuc] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[DanhMuc] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[DiaPhuong] ADD  CONSTRAINT [DF__DiaPhuong__Paren__59063A47]  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[DiaPhuong] ADD  CONSTRAINT [DF__DiaPhuong__IsSta__59FA5E80]  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[DiaPhuong] ADD  CONSTRAINT [DF__DiaPhuong__IsDel__5AEE82B9]  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[DichVu] ADD  DEFAULT ((0)) FOR [SucChua]
GO
ALTER TABLE [dbo].[DichVu] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[DichVu] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[DonViTinh] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[DonViTinh] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[FileUploads] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[HanhTrinh] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [LinhVucKinhDoanhId]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ThoiDiemBatDauKinhDoanh]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [GioDongCua]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [GioMoCua]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDGianTiep]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDNamNgoaiNuoc]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDNamTrongNuoc]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDNuNgoaiNuoc]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDNuTrongNuoc]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDThoiVu]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDThuongXuyen]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ((0)) FOR [SoLDTrucTiep]
GO
ALTER TABLE [dbo].[HoSo] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [NgayCapPhep]
GO
ALTER TABLE [dbo].[HuongDanVien] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[HuongDanVien] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[LoaiDichVu] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[LoaiDichVu] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[LoaiGiuong] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[LoaiGiuong] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[LoaiHinh] ADD  CONSTRAINT [DF__LoaiHinh__IsStat__6A30C649]  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[LoaiHinh] ADD  CONSTRAINT [DF__LoaiHinh__IsDele__6B24EA82]  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[LoaiHinh] ADD  CONSTRAINT [DF__LoaiHinh__LinhVu__0F624AF8]  DEFAULT ((0)) FOR [LinhVucKinhDoanhId]
GO
ALTER TABLE [dbo].[LoaiHinhLaoDong] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[LoaiHinhLaoDong] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[LoaiPhong] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[LoaiPhong] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[LoaiPhong] ADD  DEFAULT (N'') FOR [Ten]
GO
ALTER TABLE [dbo].[Logs] ADD  DEFAULT ('2020-12-28T14:06:08.0338808+07:00') FOR [Time]
GO
ALTER TABLE [dbo].[Menus] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[Menus] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MucDoThongThaoNgoaiNgu] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[MucDoThongThaoNgoaiNgu] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[NganhDaoTao] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[NganhDaoTao] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[NgoaiNgu] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[NgoaiNgu] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Phong] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[Phong] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Phong] ADD  DEFAULT ((0)) FOR [SoNguoiLon]
GO
ALTER TABLE [dbo].[Phong] ADD  DEFAULT ((0)) FOR [SoTreEm]
GO
ALTER TABLE [dbo].[QuocTich] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[QuocTich] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[TienNghi] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[TienNghi] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[TienNghiHoSo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsSuDung]
GO
ALTER TABLE [dbo].[TinhChatLaoDong] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[TinhChatLaoDong] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Tours] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[Tours] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Trackings] ADD  DEFAULT ('2020-12-28T14:06:08.0319657+07:00') FOR [Time]
GO
ALTER TABLE [dbo].[TrinhDo] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsStatus]
GO
ALTER TABLE [dbo].[TrinhDo] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [GroupId]
GO
ALTER TABLE [dbo].[BoPhanHoSo]  WITH CHECK ADD  CONSTRAINT [FK_BoPhanHoSo_BoPhan_BoPhanId] FOREIGN KEY([BoPhanId])
REFERENCES [dbo].[BoPhan] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BoPhanHoSo] CHECK CONSTRAINT [FK_BoPhanHoSo_BoPhan_BoPhanId]
GO
ALTER TABLE [dbo].[BoPhanHoSo]  WITH CHECK ADD  CONSTRAINT [FK_BoPhanHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BoPhanHoSo] CHECK CONSTRAINT [FK_BoPhanHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[DichVuHoSo]  WITH CHECK ADD  CONSTRAINT [FK_DichVuHoSo_DichVu_DichVuId] FOREIGN KEY([DichVuId])
REFERENCES [dbo].[DichVu] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DichVuHoSo] CHECK CONSTRAINT [FK_DichVuHoSo_DichVu_DichVuId]
GO
ALTER TABLE [dbo].[DichVuHoSo]  WITH CHECK ADD  CONSTRAINT [FK_DichVuHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DichVuHoSo] CHECK CONSTRAINT [FK_DichVuHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[HanhTrinh]  WITH CHECK ADD  CONSTRAINT [FK_HanhTrinh_HoSo_NoiDenId] FOREIGN KEY([NoiDenId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HanhTrinh] CHECK CONSTRAINT [FK_HanhTrinh_HoSo_NoiDenId]
GO
ALTER TABLE [dbo].[HanhTrinh]  WITH CHECK ADD  CONSTRAINT [FK_HanhTrinh_Tours_TourId] FOREIGN KEY([TourId])
REFERENCES [dbo].[Tours] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HanhTrinh] CHECK CONSTRAINT [FK_HanhTrinh_Tours_TourId]
GO
ALTER TABLE [dbo].[HoSo]  WITH CHECK ADD  CONSTRAINT [FK_HoSo_LoaiHinh_LoaiHinhId] FOREIGN KEY([LoaiHinhId])
REFERENCES [dbo].[LoaiHinh] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HoSo] CHECK CONSTRAINT [FK_HoSo_LoaiHinh_LoaiHinhId]
GO
ALTER TABLE [dbo].[LoaiPhongHoSo]  WITH CHECK ADD  CONSTRAINT [FK_LoaiPhongHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LoaiPhongHoSo] CHECK CONSTRAINT [FK_LoaiPhongHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[LoaiPhongHoSo]  WITH CHECK ADD  CONSTRAINT [FK_LoaiPhongHoSo_LoaiGiuong_LoaiGiuongId] FOREIGN KEY([LoaiGiuongId])
REFERENCES [dbo].[LoaiGiuong] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LoaiPhongHoSo] CHECK CONSTRAINT [FK_LoaiPhongHoSo_LoaiGiuong_LoaiGiuongId]
GO
ALTER TABLE [dbo].[LoaiPhongHoSo]  WITH CHECK ADD  CONSTRAINT [FK_LoaiPhongHoSo_LoaiPhong_LoaiPhongId] FOREIGN KEY([LoaiPhongId])
REFERENCES [dbo].[LoaiPhong] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LoaiPhongHoSo] CHECK CONSTRAINT [FK_LoaiPhongHoSo_LoaiPhong_LoaiPhongId]
GO
ALTER TABLE [dbo].[MucDoTTNNHoSo]  WITH CHECK ADD  CONSTRAINT [FK_MucDoTTNNHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MucDoTTNNHoSo] CHECK CONSTRAINT [FK_MucDoTTNNHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[MucDoTTNNHoSo]  WITH CHECK ADD  CONSTRAINT [FK_MucDoTTNNHoSo_MucDoThongThaoNgoaiNgu_MucDoId] FOREIGN KEY([MucDoId])
REFERENCES [dbo].[MucDoThongThaoNgoaiNgu] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MucDoTTNNHoSo] CHECK CONSTRAINT [FK_MucDoTTNNHoSo_MucDoThongThaoNgoaiNgu_MucDoId]
GO
ALTER TABLE [dbo].[NgoaiNguHoSo]  WITH CHECK ADD  CONSTRAINT [FK_NgoaiNguHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NgoaiNguHoSo] CHECK CONSTRAINT [FK_NgoaiNguHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[NgoaiNguHoSo]  WITH CHECK ADD  CONSTRAINT [FK_NgoaiNguHoSo_NgoaiNgu_NgoaiNguId] FOREIGN KEY([NgoaiNguId])
REFERENCES [dbo].[NgoaiNgu] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NgoaiNguHoSo] CHECK CONSTRAINT [FK_NgoaiNguHoSo_NgoaiNgu_NgoaiNguId]
GO
ALTER TABLE [dbo].[QuaTrinhHoatDong]  WITH CHECK ADD  CONSTRAINT [FK_QuaTrinhHoatDong_HuongDanVien_HDVId] FOREIGN KEY([HDVId])
REFERENCES [dbo].[HuongDanVien] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuaTrinhHoatDong] CHECK CONSTRAINT [FK_QuaTrinhHoatDong_HuongDanVien_HDVId]
GO
ALTER TABLE [dbo].[RoleGroups]  WITH CHECK ADD  CONSTRAINT [FK_RoleGroups_Groups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleGroups] CHECK CONSTRAINT [FK_RoleGroups_Groups_GroupId]
GO
ALTER TABLE [dbo].[RoleGroups]  WITH CHECK ADD  CONSTRAINT [FK_RoleGroups_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleGroups] CHECK CONSTRAINT [FK_RoleGroups_Roles_RoleId]
GO
ALTER TABLE [dbo].[ThucDonHoSo]  WITH CHECK ADD  CONSTRAINT [FK_ThucDonHoSo_HoSo_HosoId] FOREIGN KEY([HosoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ThucDonHoSo] CHECK CONSTRAINT [FK_ThucDonHoSo_HoSo_HosoId]
GO
ALTER TABLE [dbo].[TienNghiHoSo]  WITH CHECK ADD  CONSTRAINT [FK_TienNghiHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TienNghiHoSo] CHECK CONSTRAINT [FK_TienNghiHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[TienNghiHoSo]  WITH CHECK ADD  CONSTRAINT [FK_TienNghiHoSo_TienNghi_TienNghiId] FOREIGN KEY([TienNghiId])
REFERENCES [dbo].[TienNghi] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TienNghiHoSo] CHECK CONSTRAINT [FK_TienNghiHoSo_TienNghi_TienNghiId]
GO
ALTER TABLE [dbo].[TrinhDoHoSo]  WITH CHECK ADD  CONSTRAINT [FK_TrinhDoHoSo_HoSo_HoSoId] FOREIGN KEY([HoSoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrinhDoHoSo] CHECK CONSTRAINT [FK_TrinhDoHoSo_HoSo_HoSoId]
GO
ALTER TABLE [dbo].[TrinhDoHoSo]  WITH CHECK ADD  CONSTRAINT [FK_TrinhDoHoSo_TrinhDo_TrinhDoId] FOREIGN KEY([TrinhDoId])
REFERENCES [dbo].[TrinhDo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TrinhDoHoSo] CHECK CONSTRAINT [FK_TrinhDoHoSo_TrinhDo_TrinhDoId]
GO
ALTER TABLE [dbo].[VeDichVuHoSo]  WITH CHECK ADD  CONSTRAINT [FK_VeDichVuHoSo_HoSo_HosoId] FOREIGN KEY([HosoId])
REFERENCES [dbo].[HoSo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VeDichVuHoSo] CHECK CONSTRAINT [FK_VeDichVuHoSo_HoSo_HosoId]
GO
USE [master]
GO
ALTER DATABASE [tlsolution] SET  READ_WRITE 
GO
