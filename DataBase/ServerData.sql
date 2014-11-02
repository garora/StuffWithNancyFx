CREATE DATABASE [crudwepapi]
GO

USE [crudwepapi]
GO

/****** Object:  Table [dbo].[ServerData]    Script Date: 11/2/2014 9:48:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServerData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitialDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[OrderNumber] [int] NULL,
	[IsDirty] [bit] NULL,
	[IP] [nvarchar](15) NOT NULL,
	[Type] [int] NULL,
	[RecordIdentifier] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_InitialDate]  DEFAULT (getdate()) FOR [InitialDate]
GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_EndDate]  DEFAULT (getdate()) FOR [EndDate]
GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_OrderNumber]  DEFAULT ((1)) FOR [OrderNumber]
GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_IsDirty]  DEFAULT ((0)) FOR [IsDirty]
GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_IP]  DEFAULT (N'127.0.0.1') FOR [IP]
GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_Type]  DEFAULT ((1)) FOR [Type]
GO

ALTER TABLE [dbo].[ServerData] ADD  CONSTRAINT [DF_ServerData_RecordIdentifier]  DEFAULT ((1)) FOR [RecordIdentifier]
GO


CREATE DATABASE [crudwepapi]
GO


INSERT INTO [dbo].[ServerData]
           ([InitialDate]
           ,[EndDate]
           ,[OrderNumber]
           ,[IsDirty]
           ,[IP]
           ,[Type]
           ,[RecordIdentifier])
     VALUES
           (GETDATE()
           ,GETDATE()
           ,1
           ,1
           ,'127.0.0.1'
           ,1
           ,1)
GO

