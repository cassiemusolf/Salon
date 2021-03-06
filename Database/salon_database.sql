USE [hair_salon]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 2/24/2017 5:06:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[phone] [varchar](15) NULL,
	[stylists_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stylists]    Script Date: 2/24/2017 5:06:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[phone] [varchar](15) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[clients] ON 

INSERT [dbo].[clients] ([id], [name], [phone], [stylists_id]) VALUES (1, N'Tanya', N'Tanya', 13)
INSERT [dbo].[clients] ([id], [name], [phone], [stylists_id]) VALUES (2, N'Mackenzie', N'234-239-1029', 14)
SET IDENTITY_INSERT [dbo].[clients] OFF
SET IDENTITY_INSERT [dbo].[stylists] ON 

INSERT [dbo].[stylists] ([id], [name], [phone]) VALUES (13, N'Cassie', N'205-123-4544')
INSERT [dbo].[stylists] ([id], [name], [phone]) VALUES (14, N'Matt', N'234-592-3493')
INSERT [dbo].[stylists] ([id], [name], [phone]) VALUES (15, N'Stacey', N'123-958-0182')
SET IDENTITY_INSERT [dbo].[stylists] OFF
