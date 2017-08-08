USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[WebChat_User]    Script Date: 03/20/2017 15:28:46 ******/
IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebChat_User]') AND type in (N'U'))
begin
CREATE TABLE [dbo].[WebChat_User](
	[Openid] [varchar](50) NOT NULL,
	[Unionid] [nvarchar](100) NULL,
	[Appid] [nvarchar](100) NULL,
	[UserID] [nvarchar](128) NULL,
	[City] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[Province] [nvarchar](100) NULL,
	[NikceName] [nvarchar](100) NULL,
	[Gender] [int] NULL,
	[AvatarUrl] [nvarchar](300) NULL,
	[LastLoginTime] [datetime] NULL,
 CONSTRAINT [PK_WebChat_User_1] PRIMARY KEY CLUSTERED 
(
	[Openid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

print('表[WebChat_User]创建成功')

end
else
begin
print('表[WebChat_User]已经存在')
end
go
SET ANSI_PADDING OFF
GO

USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[WebChat_UserICDVersion]    Script Date: 03/21/2017 11:06:44 ******/
IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebChat_UserICDVersion]') AND type in (N'U'))
begin

CREATE TABLE [dbo].[WebChat_UserICDVersion](
	[UserICDVersionID] [int] IDENTITY(1,1) NOT NULL,
	[Openid] [varchar](50) NOT NULL,
	[ICD_Type] [int] NOT NULL,
	[ICD_VersionID] [int] NOT NULL,
 CONSTRAINT [PK_WebChat_UserICDVersion_1] PRIMARY KEY CLUSTERED 
(
	[UserICDVersionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
)  ON [PRIMARY]

print('表[WebChat_UserICDVersion]创建成功')

end
else
begin
print('表[WebChat_UserICDVersion]已经存在')
end
go
SET ANSI_PADDING OFF
GO



USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[WebChat_UserSearchLog]    Script Date: 04/10/2017 10:08:18 ******/
IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebChat_UserSearchLog]') AND type in (N'U'))
begin

CREATE TABLE [dbo].[WebChat_UserSearchLog](
	[LogID] [varchar](32) NOT NULL,
	[Openid] [varchar](50) NULL,
	[SearchFilter] [nvarchar](100) NULL,
	[SearchTime] [datetime] NULL,
	[ICDVersionID] [int] NULL,
	[ICDVersionType] [int] NULL,
 CONSTRAINT [PK_WebChat_UserSearchLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

print('表[WebChat_UserSearchLog]创建成功')

end
else
begin
print('表[WebChat_UserSearchLog]已经存在')
end
go
SET ANSI_PADDING OFF
GO


/****** Table [dbo].[WebChat_UserSearchLog] 添加 SegmentResult列  Script Date: 04/10/2017 17:43:18 ******/
if not exists(select 1 from syscolumns where id=object_id('WebChat_UserSearchLog') and name='SegmentResult')
begin
alter table WebChat_UserSearchLog add SegmentResult nvarchar(200) null
print('添加列')
end
go


/****** Table [dbo].[Code_Order_Diagnosis] 添加 ClinicalDiagnosis列  Script Date: 07/27/2017 18:43:18 ******/
if not exists(select 1 from syscolumns where id=object_id('Code_Order_Diagnosis') and name='ClinicalDiagnosis')
begin
alter table Code_Order_Diagnosis add ClinicalDiagnosis nvarchar(200) null
print('添加列')
end
go


/****** Table [dbo].[Code_Order_Operate] 添加 ClinicalOperate列  Script Date: 07/27/2017 18:43:18 ******/
if not exists(select 1 from syscolumns where id=object_id('Code_Order_Operate') and name='ClinicalOperate')
begin
alter table Code_Order_Operate add ClinicalOperate nvarchar(200) null
print('添加列')
end
go



USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[ORG_Service_Assign]    Script Date: 08/01/2017 10:08:18 ******/
IF not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORG_Service_Assign]') AND type in (N'U'))
begin

CREATE TABLE [dbo].[ORG_Service_Assign](
	[ORG_Service_AssignedID] [int] IDENTITY(1,1) NOT NULL,
	[ORG_ServiceID] [int] NULL,
	[UserID] [nvarchar](128) NULL,
	[Enable] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModtifyTime] [datetime] NULL,
	[LastModityUserID] [nvarchar](128) NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_ORG_Service_Assign] PRIMARY KEY CLUSTERED 
(
	[ORG_Service_AssignedID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[ORG_Service_Assign] ADD  CONSTRAINT [DF_ORG_Service_Assign_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]

ALTER TABLE [dbo].[ORG_Service_Assign] ADD  CONSTRAINT [DF_ORG_Service_Assign_LastModtifyTime]  DEFAULT (getdate()) FOR [LastModtifyTime]

print('表[ORG_Service_Assign]创建成功')

end
else
begin
print('表[ORG_Service_Assign]已经存在')
end
go
SET ANSI_PADDING OFF
GO



if not exists(select 1 from syscolumns where id=object_id('ORG_Service_Assign') and name='ORG_ID')
begin
alter table ORG_Service_Assign add ORG_ID int  null
print('添加列ORG_Service_Assign.ORG_ID')
end
go
