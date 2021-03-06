USE [airline]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 6/12/2017 4:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[flights]    Script Date: 6/12/2017 4:45:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[departure_time] [varchar](50) NULL,
	[departure_city] [varchar](50) NULL,
	[arrival_city] [varchar](50) NULL,
	[status] [varchar](50) NULL
) ON [PRIMARY]

GO
