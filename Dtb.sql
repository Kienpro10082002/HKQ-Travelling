﻿USE master
GO
CREATE DATABASE [HKQTravel1]
GO
USE [HKQTravel1]
GO
/****** Object:  Table [dbo].[admin_account]    Script Date: 6/4/2023 3:52:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin_account](
	[admin_id] [bigint] IDENTITY(1,1) NOT NULL,
	[admin_user] [varchar](50) NOT NULL,
	[admin_password] [varchar](100) NOT NULL,
	[admin_fulllname] [nvarchar](50) NULL,
	[email] [varchar](150) NULL,
	[status] [int] NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
 CONSTRAINT [PK_admin_account] PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill_flight]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_flight](
	[bill_flight_id] [bigint] IDENTITY(1,1) NOT NULL,
	[total] [decimal](18, 2) NULL,
	[create_date] [datetime] NULL,
	[quantity] [int] NULL,
	[user_id] [bigint] NULL,
	[flight_id] [bigint] NULL,
 CONSTRAINT [PK_bill_flight] PRIMARY KEY CLUSTERED 
(
	[bill_flight_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill_hotel]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_hotel](
	[bill_hotel_id] [bigint] IDENTITY(1,1) NOT NULL,
	[total] [decimal](18, 2) NULL,
	[create_date] [datetime] NULL,
	[quantity] [int] NULL,
	[user_id] [bigint] NULL,
	[hotel_id] [bigint] NULL,
 CONSTRAINT [PK_bill_hotel] PRIMARY KEY CLUSTERED 
(
	[bill_hotel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill_tour]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_tour](
	[bill_tour_id] [bigint] IDENTITY(1,1) NOT NULL,
	[total] [decimal](18, 2) NULL,
	[create_date] [datetime] NULL,
	[quantity] [int] NULL,
	[user_id] [bigint] NULL,
	[tour_id] [bigint] NULL,
 CONSTRAINT [PK_bill_tour] PRIMARY KEY CLUSTERED 
(
	[bill_tour_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[class]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[class](
	[class_id] [int] IDENTITY(1,1) NOT NULL,
	[class_name] [nvarchar](20) NULL,
 CONSTRAINT [PK_class] PRIMARY KEY CLUSTERED 
(
	[class_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[departure_point]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[departure_point](
	[departure_id] [bigint] IDENTITY(1,1) NOT NULL,
	[departure_name] [nvarchar](50) NOT NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_departure_point] PRIMARY KEY CLUSTERED 
(
	[departure_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[destination_point]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[destination_point](
	[destination_id] [bigint] IDENTITY(1,1) NOT NULL,
	[destination_name] [nvarchar](50) NOT NULL,
	[destination_image] [varchar](max) NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_destination_point] PRIMARY KEY CLUSTERED 
(
	[destination_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[discount]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[discount](
	[discount_id] [bigint] IDENTITY(1,1) NOT NULL,
	[discount_month] [date] NULL,
	[percentage] [float] NULL,
	[discount_name] [nvarchar](50) NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_discount] PRIMARY KEY CLUSTERED 
(
	[discount_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flight]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flight](
	[flight_id] [bigint] IDENTITY(1,1) NOT NULL,
	[flight_name] [nvarchar](50) NULL,
	[departure_location] [nvarchar](50) NULL,
	[destination_location] [nvarchar](50) NULL,
	[start_time] [datetime] NULL,
	[return_time] [datetime] NULL,
	[price] [decimal](18, 2) NULL,
	[number_of_passenger] [varchar](5) NULL,
	[class_id] [int] NULL,
	[flight_brand_id] [int] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_flight] PRIMARY KEY CLUSTERED 
(
	[flight_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flight_brand]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flight_brand](
	[flight_brand_id] [int] IDENTITY(1,1) NOT NULL,
	[flight_brand_name] [varchar](50) NULL,
	[flight_brand_image] [nvarchar](max) NULL,
 CONSTRAINT [PK_flight_brand] PRIMARY KEY CLUSTERED 
(
	[flight_brand_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hotel]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hotel](
	[hotel_id] [bigint] IDENTITY(1,1) NOT NULL,
	[hotel_name] [nvarchar](50) NOT NULL,
	[location] [nvarchar](50) NULL,
	[number_room] [int] NULL,
	[price] [decimal](18, 2) NULL,
	[ranking] [int] NULL,
	[image] [varchar](max) NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_hotel] PRIMARY KEY CLUSTERED 
(
	[hotel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tour]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tour](
	[tour_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tour_name] [nvarchar](200) NULL,
	[price] [decimal](18, 2) NULL,
	[departure_time] [datetime] NULL,
	[return_time] [datetime] NULL,
	[status] [int] NULL,
	[destination_id] [bigint] NULL,
	[departure_id] [bigint] NULL,
	[discount_id] [bigint] NULL,
	[tour_type_id] [bigint] NULL,
	[create_date] [datetime] NULL,
	[update_create] [datetime] NULL,
 CONSTRAINT [PK_tour] PRIMARY KEY CLUSTERED 
(
	[tour_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tour_type]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tour_type](
	[tour_type_id] [bigint] IDENTITY(1,1) NOT NULL,
	[tour_type_name] [nvarchar](50) NOT NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_tour_type] PRIMARY KEY CLUSTERED 
(
	[tour_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_account]    Script Date: 6/4/2023 3:52:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_account](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[user_password] [varchar](50) NOT NULL,
	[user_fullname] [nvarchar](50) NOT NULL,
	[phone_number] [varchar](20) NULL,
	[email] [varchar](100) NULL,
	[birthday] [datetime] NULL,
	[address] [nvarchar](150) NULL,
	[sex] [varchar](5) NULL,
	[user_image] [varchar](max) NULL,
	[create_date] [datetime] NULL,
	[update_date] [datetime] NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_user_account] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[admin_account] ON 

INSERT [dbo].[admin_account] ([admin_id], [admin_user], [admin_password], [admin_fulllname], [email], [status], [create_date], [update_date]) VALUES (1, N'quang', N'1234', N'Ngô Minh Quang', N'quangngo1310@gmail.com', 1, NULL, CAST(N'2023-06-04T14:11:05.187' AS DateTime))
INSERT [dbo].[admin_account] ([admin_id], [admin_user], [admin_password], [admin_fulllname], [email], [status], [create_date], [update_date]) VALUES (3, N'quang1310', N'123', N'Ngô Minh Quang', N'quangngo7821@gmail.com', 1, CAST(N'2023-06-04T14:04:55.937' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[admin_account] OFF
GO
SET IDENTITY_INSERT [dbo].[bill_flight] ON 

INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (1, CAST(9000000.00 AS Decimal(18, 2)), CAST(N'2023-05-26T02:51:53.377' AS DateTime), 3, 1, 8)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (2, CAST(6000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T19:21:49.407' AS DateTime), 2, 1, 9)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (3, NULL, CAST(N'2023-05-27T19:22:16.113' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (4, NULL, CAST(N'2023-05-27T19:23:42.943' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (5, CAST(6000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T19:24:14.520' AS DateTime), 2, 1, 10)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (6, CAST(3000000.00 AS Decimal(18, 2)), CAST(N'2023-06-03T02:08:23.863' AS DateTime), 1, 3, 10)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (10006, CAST(9000000.00 AS Decimal(18, 2)), CAST(N'2023-06-03T13:05:55.713' AS DateTime), 3, 3, 1)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (20006, CAST(3000000.00 AS Decimal(18, 2)), CAST(N'2023-06-04T15:12:42.283' AS DateTime), 1, 10007, 10)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (20007, CAST(3000000.00 AS Decimal(18, 2)), CAST(N'2023-06-04T15:18:52.023' AS DateTime), 1, 10007, 9)
INSERT [dbo].[bill_flight] ([bill_flight_id], [total], [create_date], [quantity], [user_id], [flight_id]) VALUES (20008, CAST(3000000.00 AS Decimal(18, 2)), CAST(N'2023-06-04T15:20:41.203' AS DateTime), 1, 10007, 7)
SET IDENTITY_INSERT [dbo].[bill_flight] OFF
GO
SET IDENTITY_INSERT [dbo].[bill_hotel] ON 

INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (1, NULL, CAST(N'2023-05-26T02:07:13.627' AS DateTime), 1, 1, 3)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (2, NULL, CAST(N'2023-05-26T02:09:55.197' AS DateTime), 1, 1, 3)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (3, NULL, CAST(N'2023-05-26T02:12:36.223' AS DateTime), 1, 1, 2)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (4, NULL, CAST(N'2023-05-26T02:14:52.773' AS DateTime), 1, 1, 4)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (5, CAST(900000.00 AS Decimal(18, 2)), CAST(N'2023-05-26T02:17:45.860' AS DateTime), 1, 1, 5)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (6, CAST(4500000.00 AS Decimal(18, 2)), CAST(N'2023-05-26T02:18:34.173' AS DateTime), 5, 1, 5)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (7, CAST(1800000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T03:05:38.507' AS DateTime), 2, 1, 5)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (8, CAST(6000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T03:08:43.047' AS DateTime), 2, 1, 1)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (9, CAST(1400000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T03:13:31.983' AS DateTime), 2, 1, 4)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (10, NULL, CAST(N'2023-05-27T03:14:24.113' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (11, NULL, CAST(N'2023-05-27T03:15:18.527' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (12, NULL, CAST(N'2023-05-27T03:16:38.477' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (13, CAST(900000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T03:17:19.867' AS DateTime), 1, 1, 5)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (14, CAST(1400000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T19:11:45.067' AS DateTime), 2, 1, 6)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (15, CAST(900000.00 AS Decimal(18, 2)), CAST(N'2023-06-03T02:06:49.770' AS DateTime), 1, 3, 5)
INSERT [dbo].[bill_hotel] ([bill_hotel_id], [total], [create_date], [quantity], [user_id], [hotel_id]) VALUES (10015, CAST(700000.00 AS Decimal(18, 2)), CAST(N'2023-06-04T15:19:11.383' AS DateTime), 1, 10007, 6)
SET IDENTITY_INSERT [dbo].[bill_hotel] OFF
GO
SET IDENTITY_INSERT [dbo].[bill_tour] ON 

INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (4, CAST(16000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T02:51:44.620' AS DateTime), 2, 1, 5)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (5, CAST(8000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T02:53:32.243' AS DateTime), 1, 1, 5)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (6, CAST(8000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T02:54:58.940' AS DateTime), 1, 1, 5)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (7, CAST(8000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T15:52:42.237' AS DateTime), 1, 1, 5)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (8, NULL, CAST(N'2023-05-27T15:53:14.327' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (9, CAST(7990000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T15:55:21.080' AS DateTime), 1, 1, 9)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (10, NULL, CAST(N'2023-05-27T15:56:22.937' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (11, NULL, CAST(N'2023-05-27T15:56:34.043' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (12, CAST(15980000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T15:58:43.153' AS DateTime), 2, 1, 9)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (13, CAST(5490000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T16:00:55.677' AS DateTime), 1, 1, 8)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (14, CAST(8000000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T16:03:54.760' AS DateTime), 1, 1, 5)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (15, NULL, CAST(N'2023-05-27T16:05:31.090' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (16, NULL, CAST(N'2023-05-27T16:05:41.267' AS DateTime), NULL, 1, NULL)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (17, CAST(5490000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T16:07:22.793' AS DateTime), 1, 1, 8)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (18, CAST(5490000.00 AS Decimal(18, 2)), CAST(N'2023-05-27T16:09:10.840' AS DateTime), 1, 1, 8)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (19, CAST(7990000.00 AS Decimal(18, 2)), CAST(N'2023-06-02T02:54:48.110' AS DateTime), 1, 3, 9)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (10019, CAST(7990000.00 AS Decimal(18, 2)), CAST(N'2023-06-03T02:13:27.837' AS DateTime), 1, 3, 9)
INSERT [dbo].[bill_tour] ([bill_tour_id], [total], [create_date], [quantity], [user_id], [tour_id]) VALUES (20019, CAST(7990000.00 AS Decimal(18, 2)), CAST(N'2023-06-03T13:02:58.313' AS DateTime), 1, 3, 9)
SET IDENTITY_INSERT [dbo].[bill_tour] OFF
GO
SET IDENTITY_INSERT [dbo].[class] ON 

INSERT [dbo].[class] ([class_id], [class_name]) VALUES (1, N'First')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (2, N'Prenium')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (3, N'Business')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (4, N'Economy')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (5, N'Monster')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (6, N'Alience')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (7, N'Non-people')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (8, N'Animal')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (9, N'Superman')
INSERT [dbo].[class] ([class_id], [class_name]) VALUES (10, N'Batman')
SET IDENTITY_INSERT [dbo].[class] OFF
GO
SET IDENTITY_INSERT [dbo].[departure_point] ON 

INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (1, N'HoChiMinh City', 1)
INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (2, N'DaLat', 1)
INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (3, N'CanTho', 1)
INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (4, N'DaNang', 1)
INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (5, N'NoiBai', 1)
INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (6, N'HaiPhong', 1)
INSERT [dbo].[departure_point] ([departure_id], [departure_name], [status]) VALUES (7, N'CanTho', 1)
SET IDENTITY_INSERT [dbo].[departure_point] OFF
GO
SET IDENTITY_INSERT [dbo].[destination_point] ON 

INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (1, N'Đà Lạt', N'~/Assets/User/img_slider/dalat.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (2, N'Đà Nẵng', N'~/Assets/User/img_slider/danang.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (3, N'Phú Quốc', N'~/Assets/User/img_slider/phuquoc.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (4, N'Hội An', N'~/Assets/User/img_slider/hoian.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (5, N'Sapa', N'~/Assets/User/img_slider/sapa.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (6, N'Paris', N'~/Assets/User/img_slider/paris.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (7, N'Dubai', N'~/Assets/User/img_slider/dubai.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (8, N'New York', N'~/Assets/User/img_slider/ny.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (9, N'Los Angeles', N'~/Assets/User/img_slider/la.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (10, N'Korea', N'~/Assets/User/img_slider/kr.jpg', 1)
INSERT [dbo].[destination_point] ([destination_id], [destination_name], [destination_image], [status]) VALUES (11, N'Côn Đảo', N'~/Assets/User/img_slider/condao.jpg', 1)
SET IDENTITY_INSERT [dbo].[destination_point] OFF
GO
SET IDENTITY_INSERT [dbo].[discount] ON 

INSERT [dbo].[discount] ([discount_id], [discount_month], [percentage], [discount_name], [create_date], [update_date], [status]) VALUES (1, CAST(N'2023-05-31' AS Date), 0.32, N'HKQ-T6(Welcom June)', NULL, NULL, 1)
INSERT [dbo].[discount] ([discount_id], [discount_month], [percentage], [discount_name], [create_date], [update_date], [status]) VALUES (2, CAST(N'2023-05-31' AS Date), 0.25, N'Sacombank tung ưu đãi (Welcom June)', NULL, NULL, 1)
INSERT [dbo].[discount] ([discount_id], [discount_month], [percentage], [discount_name], [create_date], [update_date], [status]) VALUES (3, CAST(N'2023-06-17' AS Date), 12, N'HKQ 7 (Welcom July)', NULL, CAST(N'2023-06-04T14:19:35.103' AS DateTime), 1)
INSERT [dbo].[discount] ([discount_id], [discount_month], [percentage], [discount_name], [create_date], [update_date], [status]) VALUES (4, CAST(N'2023-07-01' AS Date), 32, N'(Welcom July)', NULL, CAST(N'2023-06-04T14:19:20.267' AS DateTime), 1)
INSERT [dbo].[discount] ([discount_id], [discount_month], [percentage], [discount_name], [create_date], [update_date], [status]) VALUES (5, CAST(N'2023-07-01' AS Date), 26, N'test', CAST(N'2023-06-04T14:19:51.837' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[discount] OFF
GO
SET IDENTITY_INSERT [dbo].[flight] ON 

INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (1, N'Bamboo Airways - BAV1034', N'DaLat', N'HoChiMinh City', CAST(N'2023-05-31T18:23:00.000' AS DateTime), CAST(N'2023-06-03T18:23:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'01A', 2, 1, 2)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (2, N'Bamboo Airways - BAV1310', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'02A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (3, N'Bamboo Airways - BAV2804', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'03A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (4, N'Bamboo Airways - BAV9312', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'04A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (5, N'Bamboo Airways - BAV1876', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'05A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (6, N'Bamboo Airways - BAV1011', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'06A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (7, N'Bamboo Airways - BAV1233', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'07A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (8, N'Bamboo Airways - BAV9985', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'08A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (9, N'Bamboo Airways - BAV1745', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'09A', 4, 3, 1)
INSERT [dbo].[flight] ([flight_id], [flight_name], [departure_location], [destination_location], [start_time], [return_time], [price], [number_of_passenger], [class_id], [flight_brand_id], [status]) VALUES (10, N'Bamboo Airways - BAV5681', N'HoChiMinh City', N'DaLat', CAST(N'2023-06-06T18:00:00.000' AS DateTime), CAST(N'2023-06-09T22:00:00.000' AS DateTime), CAST(3000000.00 AS Decimal(18, 2)), N'10A', 4, 3, 1)
SET IDENTITY_INSERT [dbo].[flight] OFF
GO
SET IDENTITY_INSERT [dbo].[flight_brand] ON 

INSERT [dbo].[flight_brand] ([flight_brand_id], [flight_brand_name], [flight_brand_image]) VALUES (1, N'Vietjet Air', N'~/Assets/User/img_flight/vietjet_air.jpg')
INSERT [dbo].[flight_brand] ([flight_brand_id], [flight_brand_name], [flight_brand_image]) VALUES (2, N'Pacific Airlines', N'~/Assets/User/img_flight/pacific_airlines.jpg')
INSERT [dbo].[flight_brand] ([flight_brand_id], [flight_brand_name], [flight_brand_image]) VALUES (3, N'Bamboo Airways', N'~/Assets/User/img_flight/bamboo-airways.png')
SET IDENTITY_INSERT [dbo].[flight_brand] OFF
GO
SET IDENTITY_INSERT [dbo].[hotel] ON 

INSERT [dbo].[hotel] ([hotel_id], [hotel_name], [location], [number_room], [price], [ranking], [image], [status]) VALUES (1, N'Ana Mandara Villas Dalat Resort & Spa', N'DaLat', 306, CAST(3000000.00 AS Decimal(18, 2)), 5, N'~/Assets/User/img_hotel/Ana_Mandara_Villas_Dalat_Resort_&_Spa.jpg', 1)
INSERT [dbo].[hotel] ([hotel_id], [hotel_name], [location], [number_room], [price], [ranking], [image], [status]) VALUES (2, N'Le Recit Boutique Hotel de Dalat', N'DaLat', 706, CAST(1500000.00 AS Decimal(18, 2)), 5, N'~/Assets/User/img_hotel/Le_Recit_Boutique_Hotel_de_Dalat.jpg', 1)
INSERT [dbo].[hotel] ([hotel_id], [hotel_name], [location], [number_room], [price], [ranking], [image], [status]) VALUES (3, N'Dalat Wonder Resort', N'DaLat', 302, CAST(2300000.00 AS Decimal(18, 2)), 5, N'~/Assets/User/img_hotel/Dalat_Wonder_Resort.jpg', 1)
INSERT [dbo].[hotel] ([hotel_id], [hotel_name], [location], [number_room], [price], [ranking], [image], [status]) VALUES (4, N'Khách sạn The Grace Dalat', N'DaLat', 106, CAST(700000.00 AS Decimal(18, 2)), 5, N'~/Assets/User/img_hotel/The_Grace_Dalat.jpg', 1)
INSERT [dbo].[hotel] ([hotel_id], [hotel_name], [location], [number_room], [price], [ranking], [image], [status]) VALUES (5, N'LATA Hotel & Apartments', N'DaLat', 507, CAST(900000.00 AS Decimal(18, 2)), 5, N'~/Assets/User/img_hotel/LATA_Hotel_&_Apartments.jpg', 1)
INSERT [dbo].[hotel] ([hotel_id], [hotel_name], [location], [number_room], [price], [ranking], [image], [status]) VALUES (6, N'Khách sạn The Secret Côn Đảo(The Secret Con Dao)', N'ConDao', 593, CAST(700000.00 AS Decimal(18, 2)), 5, N'~/Assets/User/img_hotel/The_Secret_Con_Dao(The_Secret_Con_Dao).jpg', 2)
SET IDENTITY_INSERT [dbo].[hotel] OFF
GO
SET IDENTITY_INSERT [dbo].[tour] ON 

INSERT [dbo].[tour] ([tour_id], [tour_name], [price], [departure_time], [return_time], [status], [destination_id], [departure_id], [discount_id], [tour_type_id], [create_date], [update_create]) VALUES (5, N'Đà Lạt - Thác Bobla - KDL Cao Nguyên Hoa - Trang Trại rau và hoa Vạn Thành', CAST(8000000.00 AS Decimal(18, 2)), CAST(N'2023-05-31T16:32:00.000' AS DateTime), CAST(N'2023-06-03T16:32:00.000' AS DateTime), 1, 1, 1, NULL, 4, NULL, CAST(N'2023-05-28T16:32:45.440' AS DateTime))
INSERT [dbo].[tour] ([tour_id], [tour_name], [price], [departure_time], [return_time], [status], [destination_id], [departure_id], [discount_id], [tour_type_id], [create_date], [update_create]) VALUES (6, N'Phú Quốc - Kỳ Nghỉ Đẳng Cấp Tại Thiên Đường Biển Đảo', CAST(6500000.00 AS Decimal(18, 2)), CAST(N'2023-06-12T12:00:00.000' AS DateTime), CAST(N'2023-06-15T20:00:00.000' AS DateTime), 1, 3, 1, NULL, 1, NULL, NULL)
INSERT [dbo].[tour] ([tour_id], [tour_name], [price], [departure_time], [return_time], [status], [destination_id], [departure_id], [discount_id], [tour_type_id], [create_date], [update_create]) VALUES (8, N'Đà Nẵng - Hội An - Bà Nà - Cầu Vàng - Huế - Trải nghiệm đi thuyền dạo Sông Hoài & Tặng show Ký Ức Hội An | Kích cầu Du lịch', CAST(5490000.00 AS Decimal(18, 2)), CAST(N'2023-06-12T12:00:00.000' AS DateTime), CAST(N'2023-06-15T20:00:00.000' AS DateTime), 1, 4, 1, NULL, 1, NULL, NULL)
INSERT [dbo].[tour] ([tour_id], [tour_name], [price], [departure_time], [return_time], [status], [destination_id], [departure_id], [discount_id], [tour_type_id], [create_date], [update_create]) VALUES (9, N'Đà Nẵng - Huế - Đầm Lập An - La Vang - Động Phong Nha & Thiên Đường - KDL Bà Nà - Cầu Vàng -Sơn Trà - Hội An - Đà Nẵng', CAST(7990000.00 AS Decimal(18, 2)), CAST(N'2023-06-12T12:00:00.000' AS DateTime), CAST(N'2023-06-15T20:00:00.000' AS DateTime), 1, 2, 1, 3, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tour] OFF
GO
SET IDENTITY_INSERT [dbo].[tour_type] ON 

INSERT [dbo].[tour_type] ([tour_type_id], [tour_type_name], [status]) VALUES (1, N'High-class', 1)
INSERT [dbo].[tour_type] ([tour_type_id], [tour_type_name], [status]) VALUES (2, N'Standard', 1)
INSERT [dbo].[tour_type] ([tour_type_id], [tour_type_name], [status]) VALUES (3, N'Save', 1)
INSERT [dbo].[tour_type] ([tour_type_id], [tour_type_name], [status]) VALUES (4, N'Good prices', 1)
INSERT [dbo].[tour_type] ([tour_type_id], [tour_type_name], [status]) VALUES (5, N'Ultra Save', 1)
SET IDENTITY_INSERT [dbo].[tour_type] OFF
GO
SET IDENTITY_INSERT [dbo].[user_account] ON 

INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (1, N'quang', N'202cb962ac59075b964b07152d234b70', N'NGÔ MINH QUANG', N'0981494344', N'quangngo1310@gmail.com', NULL, N'13, D5, Ward 25, Binh Thanh District, HCMC', N'Nam', N'~/Assets/User/img_account/R.jpg', CAST(N'2023-04-06T18:25:53.813' AS DateTime), CAST(N'2023-06-04T14:28:31.770' AS DateTime), 1)
INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (2, N'kienpro10082002', N'4f204529de367c1e6f1181bb3a54f8f4', N'Đoàn Trung Kiên', N'0344156156', N'doantrungkien10082002@gmail.com', NULL, N'13 D5 25 Ward Binh Thanh District Ho Chi Minh city', N'Nam', N'~/Assets/User/img_account/OIP.jpg', CAST(N'2023-05-27T20:08:30.160' AS DateTime), CAST(N'2023-06-04T14:28:24.393' AS DateTime), 1)
INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (3, N'quang1310', N'e87aa9487ff70b92559eb1bca3140fd1', N'Ngô Minh Quang', N'0981494344', N'quangngo7821@gmail.com', CAST(N'2002-10-13T00:00:00.000' AS DateTime), N'13 D5 Street 25 Ward Binh Thanh District Ho Chi Minh City', N'Nam', NULL, CAST(N'2023-06-02T01:58:06.853' AS DateTime), NULL, NULL)
INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (10004, N'Quang7821', N'212ab61ac7c73b39c46cd934fb67204c', N'Ngô Minh Quang', N'0981494344', N'quangngo7821@gmail.com.vn', CAST(N'2002-10-13T00:00:00.000' AS DateTime), N'Hutech Univercity', N'Nam', N'~/Assets/User/img_account/R-1.jpg', CAST(N'2023-06-04T14:28:09.507' AS DateTime), NULL, 1)
INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (10005, N'quang99', N'202cb962ac59075b964b07152d234b70', N'Quang Minh', N'0981494344', N'quangngo7821@gmail.com', CAST(N'2023-06-04T00:00:00.000' AS DateTime), N'Hutech Univercity', N'Nam', N'~/Assets/User/img_account/R-2.jpg', CAST(N'2023-06-04T14:50:28.273' AS DateTime), NULL, 1)
INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (10006, N'quang98', N'e87aa9487ff70b92559eb1bca3140fd1', N'Ngô Minh Quang', N'0981494344', N'quangngo7821@gmail.com.vn', CAST(N'2023-06-04T00:00:00.000' AS DateTime), N'Hutech Univercity', N'Nam', N'~/Assets/User/img_account/R-3.jpg', CAST(N'2023-06-04T14:52:25.300' AS DateTime), NULL, 1)
INSERT [dbo].[user_account] ([user_id], [user_name], [user_password], [user_fullname], [phone_number], [email], [birthday], [address], [sex], [user_image], [create_date], [update_date], [status]) VALUES (10007, N'quang78213', N'a655ee930ef168140d07b525774f95f8', N'Ngô Minh Quang', N'0981494344', N'quangngo78213@gmail.com.vn', CAST(N'2002-10-13T00:00:00.000' AS DateTime), N'13 D5 Street 25 Ward Binh Thanh District Ho Chi Minh City', N'Nam', NULL, CAST(N'2023-06-04T15:01:38.180' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[user_account] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__admin_ac__E88D8FF14420EDAB]    Script Date: 6/4/2023 3:52:38 PM ******/
ALTER TABLE [dbo].[admin_account] ADD UNIQUE NONCLUSTERED 
(
	[admin_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__user_acc__7C9273C4D91DF2AD]    Script Date: 6/4/2023 3:52:38 PM ******/
ALTER TABLE [dbo].[user_account] ADD UNIQUE NONCLUSTERED 
(
	[user_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bill_tour] ADD  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[user_account] ADD  DEFAULT (getdate()) FOR [create_date]
GO
ALTER TABLE [dbo].[bill_flight]  WITH CHECK ADD  CONSTRAINT [Relationship13] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_account] ([user_id])
GO
ALTER TABLE [dbo].[bill_flight] CHECK CONSTRAINT [Relationship13]
GO
ALTER TABLE [dbo].[bill_flight]  WITH CHECK ADD  CONSTRAINT [Relationship22] FOREIGN KEY([flight_id])
REFERENCES [dbo].[flight] ([flight_id])
GO
ALTER TABLE [dbo].[bill_flight] CHECK CONSTRAINT [Relationship22]
GO
ALTER TABLE [dbo].[bill_hotel]  WITH CHECK ADD  CONSTRAINT [Relationship12] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_account] ([user_id])
GO
ALTER TABLE [dbo].[bill_hotel] CHECK CONSTRAINT [Relationship12]
GO
ALTER TABLE [dbo].[bill_hotel]  WITH CHECK ADD  CONSTRAINT [Relationship21] FOREIGN KEY([hotel_id])
REFERENCES [dbo].[hotel] ([hotel_id])
GO
ALTER TABLE [dbo].[bill_hotel] CHECK CONSTRAINT [Relationship21]
GO
ALTER TABLE [dbo].[bill_tour]  WITH CHECK ADD  CONSTRAINT [Relationship11] FOREIGN KEY([user_id])
REFERENCES [dbo].[user_account] ([user_id])
GO
ALTER TABLE [dbo].[bill_tour] CHECK CONSTRAINT [Relationship11]
GO
ALTER TABLE [dbo].[bill_tour]  WITH CHECK ADD  CONSTRAINT [Relationship20] FOREIGN KEY([tour_id])
REFERENCES [dbo].[tour] ([tour_id])
GO
ALTER TABLE [dbo].[bill_tour] CHECK CONSTRAINT [Relationship20]
GO
ALTER TABLE [dbo].[flight]  WITH CHECK ADD  CONSTRAINT [Relationship23] FOREIGN KEY([class_id])
REFERENCES [dbo].[class] ([class_id])
GO
ALTER TABLE [dbo].[flight] CHECK CONSTRAINT [Relationship23]
GO
ALTER TABLE [dbo].[flight]  WITH CHECK ADD  CONSTRAINT [Relationship24] FOREIGN KEY([flight_brand_id])
REFERENCES [dbo].[flight_brand] ([flight_brand_id])
GO
ALTER TABLE [dbo].[flight] CHECK CONSTRAINT [Relationship24]
GO
ALTER TABLE [dbo].[tour]  WITH CHECK ADD  CONSTRAINT [Relationship4] FOREIGN KEY([destination_id])
REFERENCES [dbo].[destination_point] ([destination_id])
GO
ALTER TABLE [dbo].[tour] CHECK CONSTRAINT [Relationship4]
GO
ALTER TABLE [dbo].[tour]  WITH CHECK ADD  CONSTRAINT [Relationship5] FOREIGN KEY([departure_id])
REFERENCES [dbo].[departure_point] ([departure_id])
GO
ALTER TABLE [dbo].[tour] CHECK CONSTRAINT [Relationship5]
GO
ALTER TABLE [dbo].[tour]  WITH CHECK ADD  CONSTRAINT [Relationship6] FOREIGN KEY([discount_id])
REFERENCES [dbo].[discount] ([discount_id])
GO
ALTER TABLE [dbo].[tour] CHECK CONSTRAINT [Relationship6]
GO
ALTER TABLE [dbo].[tour]  WITH CHECK ADD  CONSTRAINT [Relationship7] FOREIGN KEY([tour_type_id])
REFERENCES [dbo].[tour_type] ([tour_type_id])
GO
ALTER TABLE [dbo].[tour] CHECK CONSTRAINT [Relationship7]
GO