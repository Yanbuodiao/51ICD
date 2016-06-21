﻿USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[User_Service_Provider]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Service_Provider](
	[User_ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[ServiceID] [int] NULL,
	[CertificationStatus] [int] NULL,
	[ApplyTime] [datetime] NULL,
	[PlatformAuditTime] [datetime] NULL,
	[PlatformAuditUserID] [int] NULL,
	[PlatformAbendTime] [datetime] NULL,
	[PlatformAbendUserID] [datetime] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [int] NULL,
	[LastModityTime] [datetime] NULL,
	[LastModifyUserID] [int] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_User_Service] PRIMARY KEY CLUSTERED 
(
	[User_ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Service_Attach]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Service_Attach](
	[UserServiceAttachID] [int] IDENTITY(1,1) NOT NULL,
	[User_ServiceID] [int] NULL,
	[AttachType] [int] NULL,
	[AttachURL] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModityTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_User_Service_Attach] PRIMARY KEY CLUSTERED 
(
	[UserServiceAttachID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Attach]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Attach](
	[UserAttachID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[AttachType] [int] NULL,
	[AttachURL] [nvarchar](max) NULL,
	[AttachDecription] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModityTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_User_Attach] PRIMARY KEY CLUSTERED 
(
	[UserAttachID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1：删除 其他未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Attach', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
/****** Object:  Table [dbo].[ORG_SubOrganization]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_SubOrganization](
	[SubORGID] [int] IDENTITY(1,1) NOT NULL,
	[SubORGCode] [int] NULL,
	[SubORGName] [nvarchar](300) NULL,
	[SubORGAddress] [nvarchar](300) NULL,
	[ORGID] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_ORG_SubOrganization] PRIMARY KEY CLUSTERED 
(
	[SubORGID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORG_Service_UploadItem]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Service_UploadItem](
	[ORG_Service_UploadItemID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[ORGID] [int] NULL,
	[Sub_ORGID] [int] NULL,
	[UploadItemID] [int] NULL,
	[Required] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_ORG_Service_Require_Config] PRIMARY KEY CLUSTERED 
(
	[ORG_Service_UploadItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORG_Service_Provider]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Service_Provider](
	[ORG_ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ORGID] [int] NULL,
	[ServiceID] [int] NULL,
	[CertificationStatus] [int] NULL,
	[ApplyTime] [datetime] NULL,
	[PlatformAuditTime] [datetime] NULL,
	[PlatformAuditUserID] [int] NULL,
	[PlatformAbendTime] [datetime] NULL,
	[PlatformAbendUserID] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [int] NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [int] NULL,
	[DeleteFlag] [int] NULL,
 CONSTRAINT [PK_ORG_Service_Provider] PRIMARY KEY CLUSTERED 
(
	[ORG_ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORG_Service_Config]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Service_Config](
	[ORG_ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ORGID] [int] NULL,
	[ServiceID] [int] NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ServiceAuditStatus] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [int] NULL,
	[LastModityTime] [datetime] NULL,
	[LastModityUserID] [int] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_ORG_Organization_Require] PRIMARY KEY CLUSTERED 
(
	[ORG_ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORG_Attach]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Attach](
	[ORGAttachID] [int] IDENTITY(1,1) NOT NULL,
	[ORGID] [int] NULL,
	[ORGAttachType] [int] NULL,
	[ORGAttachURL] [nvarchar](max) NULL,
	[AttachDescription] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [int] NULL,
	[LastModityTime] [datetime] NULL,
	[LastModifyUserID] [int] NULL,
	[DeleteFlag] [int] NULL,
 CONSTRAINT [PK_ORG_Attach] PRIMARY KEY CLUSTERED 
(
	[ORGAttachID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL长度在HTTP中没有限制，但是浏览器和Web服务器会有最大长度字符数限制，中文编码后长度会变长，此处先限制到250' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ORG_Attach', @level2type=N'COLUMN',@level2name=N'ORGAttachURL'
GO
/****** Object:  UserDefinedFunction [dbo].[fun_getPY]    Script Date: 06/21/2016 16:00:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[fun_getPY](@str nvarchar(4000)) 
returns nvarchar(4000) 
as 
begin 
declare @word nchar(1),@PY nvarchar(4000) 
set @PY='' 
while len(@str)>0 
begin 
set @word=left(@str,1) 
--如果非汉字字符，返回原字符 
set @PY=@PY+(case when unicode(@word) between 19968 and 19968+20901 
then (select top 1 PY from ( 
select 'A' as PY,N'骜' as word 
union all select 'B',N'簿' 
union all select 'C',N'错' 
union all select 'D',N'鵽' 
union all select 'E',N'樲' 
union all select 'F',N'鳆' 
union all select 'G',N'腂' 
union all select 'H',N'夻' 
union all select 'J',N'攈' 
union all select 'K',N'穒' 
union all select 'L',N'鱳' 
union all select 'M',N'旀' 
union all select 'N',N'桛' 
union all select 'O',N'沤' 
union all select 'P',N'曝' 
union all select 'Q',N'囕' 
union all select 'R',N'鶸' 
union all select 'S',N'蜶' 

union all select 'W',N'鹜' 
union all select 'X',N'鑂' 
union all select 'Y',N'韵' 
union all select 'Z',N'咗' 
) T 
where word>=@word collate Chinese_PRC_CS_AS_KS_WS 
order by PY ASC) else '' end) 
set @str=right(@str,len(@str)-1) 
end 
return @PY 
end
GO
/****** Object:  Table [dbo].[Dic_UploadItem]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_UploadItem](
	[UploadItemID] [int] IDENTITY(1,1) NOT NULL,
	[UploadItemName] [nvarchar](300) NULL,
	[UploadItemDescription] [nvarchar](500) NULL,
	[PinyinShort] [nvarchar](300) NULL,
	[UploadItemLevel] [int] NULL,
	[UploadItemIndex] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_UploadItem] PRIMARY KEY CLUSTERED 
(
	[UploadItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目所在层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同层级下的该项目的索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Dic_Service_UploadItem]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service_UploadItem](
	[Service_UploadItemID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[UploadItemID] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Service_UploadItem] PRIMARY KEY CLUSTERED 
(
	[Service_UploadItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务需要上传项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_UploadItem', @level2type=N'COLUMN',@level2name=N'Service_UploadItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_UploadItem', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需要上传项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemID'
GO
/****** Object:  Table [dbo].[Dic_Service_Claim]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service_Claim](
	[ServiceClaimID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[ClaimType] [int] NULL,
	[ClaimOperateType] [int] NULL,
	[ClaimValue] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [nvarchar](128) NULL,
 CONSTRAINT [PK_Dic_Service_Claim] PRIMARY KEY CLUSTERED 
(
	[ServiceClaimID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务限制ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ServiceClaimID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制项目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ClaimType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制操作符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ClaimOperateType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ClaimValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Dic_Service]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service](
	[ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](300) NULL,
	[Description] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'ServiceName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
/****** Object:  Table [dbo].[Dic_Organization]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Organization](
	[OrganizationID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationCode] [nvarchar](300) NULL,
	[OrganizationName] [nvarchar](300) NULL,
	[ShortName_CN] [nvarchar](200) NULL,
	[ShortName_EN] [nvarchar](200) NULL,
	[RegisterCode] [nvarchar](300) NULL,
	[ProvinceID] [int] NULL,
	[CityID] [int] NULL,
	[AreaID] [int] NULL,
	[DetailAddress1] [nvarchar](300) NULL,
	[DetailAddress2] [nvarchar](300) NULL,
	[DetailAddress3] [nvarchar](300) NULL,
	[CertificationFlag] [int] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Organization] PRIMARY KEY CLUSTERED 
(
	[OrganizationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'OrganizationID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构代码_全国组织结构代码管理中心可查询到的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'OrganizationCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构名称_全国组织结构代码管理中心可查询到的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'OrganizationName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构中文简写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'ShortName_CN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构英语简写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'ShortName_EN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构登记证号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'RegisterCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构所在省份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'ProvinceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构所在城市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CityID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构所在区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构详细地址，主机构主要存储组织结构注册地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'DetailAddress1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构有分区，但是病案室没有分开的情况，存储多个地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'DetailAddress2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展字段，存储多个组织机构地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'DetailAddress3'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'LastModifyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[Dic_Menu]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Menu](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](300) NULL,
	[AreaName] [nvarchar](300) NULL,
	[ControllerName] [nvarchar](300) NULL,
	[ActionName] [nvarchar](300) NULL,
	[ParentMenuID] [int] NULL,
	[RoleControl] [int] NULL,
	[Createtime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Code_Builder_Item]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Code_Builder_Item](
	[BuilderCodeItemID] [int] IDENTITY(1,1) NOT NULL,
	[BuilderCodeID] [int] NULL,
	[UploadItemID] [int] NULL,
	[GIndex] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder_Item] PRIMARY KEY CLUSTERED 
(
	[BuilderCodeItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单上传的资料ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'BuilderCodeItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'BuilderCodeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传的项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'UploadItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该资料在该项目下的排序（一个项目中可能上传了多张图片资料）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'GIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1：删除  其他：未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间戳  并发控制更新的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder_Item', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Code_Builder]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Code_Builder](
	[BuilderCodeID] [int] IDENTITY(1,1) NOT NULL,
	[PlatformOrderCode] [varchar](15) NULL,
	[OrderStatus] [int] NULL,
	[ORGID] [int] NULL,
	[ORGSubID] [int] NULL,
	[Createtime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder] PRIMARY KEY CLUSTERED 
(
	[BuilderCodeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'BuilderCodeID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'PlatformOrderCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'OrderStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单需求方的组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'ORGID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单需求方分区组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'ORGSubID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'Createtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1 删除 其他 未删' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新版本号，单库并发控制字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Builder', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[BaseDic_Province]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_Province](
	[ProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceCode] [nvarchar](50) NULL,
	[ProvinceName] [nvarchar](200) NULL,
	[PinyinShort] [nvarchar](200) NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Province] PRIMARY KEY CLUSTERED 
(
	[ProvinceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_ICD_Version]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_ICD_Version](
	[ICD_VersionID] [int] IDENTITY(1,1) NOT NULL,
	[ICD_VersionName] [nvarchar](300) NULL,
	[ICD_Description] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_ICD_Version] PRIMARY KEY CLUSTERED 
(
	[ICD_VersionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ICD版本ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'ICD_VersionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ICD版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'ICD_VersionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ICD版本描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'ICD_Description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[BaseDic_ICD_Repository]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_ICD_Repository](
	[ICDID] [int] IDENTITY(1,1) NOT NULL,
	[ICD_Code] [nvarchar](300) NULL,
	[ICD_Name] [nvarchar](300) NULL,
	[ICD_VersionID] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_ICD_Repository] PRIMARY KEY CLUSTERED 
(
	[ICDID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_City]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_City](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[CityCode] [nvarchar](50) NULL,
	[CityName] [nvarchar](200) NULL,
	[ProvinceCode] [nvarchar](50) NULL,
	[PinyinShort] [nvarchar](200) NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_Area]    Script Date: 06/21/2016 16:00:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_Area](
	[AreaID] [int] IDENTITY(1,1) NOT NULL,
	[AreaCode] [nvarchar](50) NULL,
	[AreaName] [nvarchar](200) NULL,
	[CityCode] [nvarchar](50) NULL,
	[PinyinShort] [nvarchar](200) NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Area] PRIMARY KEY CLUSTERED 
(
	[AreaID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Dic_Menu_Createtime]    Script Date: 06/21/2016 16:00:39 ******/
ALTER TABLE [dbo].[Dic_Menu] ADD  CONSTRAINT [DF_Dic_Menu_Createtime]  DEFAULT (getdate()) FOR [Createtime]
GO
/****** Object:  Default [DF_Dic_Menu_LastModifyTime]    Script Date: 06/21/2016 16:00:39 ******/
ALTER TABLE [dbo].[Dic_Menu] ADD  CONSTRAINT [DF_Dic_Menu_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
