USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[User_Service_Provider]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Service_Provider](
	[User_ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[ServiceID] [int] NULL,
	[CertificationStatus] [int] NULL,
	[ApplyTime] [datetime] NULL,
	[PlatformAuditTime] [datetime] NULL,
	[PlatformAuditUserID] [nvarchar](128) NULL,
	[PlatformAbendTime] [datetime] NULL,
	[PlatformAbendUserID] [nvarchar](128) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModityTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_User_Service] PRIMARY KEY CLUSTERED 
(
	[User_ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Service_Claim]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Service_Claim](
	[User_Service_ClaimID] [int] IDENTITY(1,1) NOT NULL,
	[User_ServiceID] [int] NULL,
	[ClaimType] [int] NULL,
	[ClaimOperateType] [int] NULL,
	[ClaimValue] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_User_Service_Claim] PRIMARY KEY CLUSTERED 
(
	[User_Service_ClaimID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_Service_Attach]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Service_Attach](
	[UserServiceAttachID] [int] IDENTITY(1,1) NOT NULL,
	[User_ServiceID] [int] NULL,
	[FileName] [nvarchar](max) NULL,
	[AttachType] [int] NULL,
	[ContentType] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[User_ChangeAuditLog]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_ChangeAuditLog](
	[UserChangeID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[OriginValue] [nvarchar](500) NULL,
	[NewValue] [nvarchar](500) NULL,
	[OperateTarget] [int] NULL,
	[OperateUserID] [nvarchar](128) NULL,
	[Operatedatetime] [datetime] NULL,
	[OperateType] [int] NULL,
	[OpertateDescription] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_User_ChangeAuditLog] PRIMARY KEY CLUSTERED 
(
	[UserChangeID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原来的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OriginValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作后的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'NewValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OperateTarget'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OperateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'Operatedatetime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OperateType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作的整体描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OpertateDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[User_Attach]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Attach](
	[UserAttachID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[FileName] [nvarchar](max) NULL,
	[AttachType] [int] NULL,
	[ContentType] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[Sec_Message]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sec_Message](
	[SecurityMessage_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserPhoneNumber] [nvarchar](max) NULL,
	[UserID] [nvarchar](128) NULL,
	[SourceIP] [varchar](100) NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_Sec_Message] PRIMARY KEY CLUSTERED 
(
	[SecurityMessage_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短信发送记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'SecurityMessage_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送的手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'UserPhoneNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'UserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送短信时，用户的请求地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'SourceIP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间 发送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[Sec_IP]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sec_IP](
	[SecurityIP_ID] [int] IDENTITY(1,1) NOT NULL,
	[SourceIP] [varchar](100) NULL,
	[CreateTime] [datetime] NULL,
	[LastModifyTime] [datetime] NULL,
 CONSTRAINT [PK_Sec_IP] PRIMARY KEY CLUSTERED 
(
	[SecurityIP_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ORG_SubOrganization]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_SubOrganization](
	[SubORGID] [int] IDENTITY(1,1) NOT NULL,
	[SubORGCode] [int] NULL,
	[SubORGName] [nvarchar](300) NULL,
	[SubORGAddress] [nvarchar](300) NULL,
	[ORGID] [int] NULL,
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
/****** Object:  Table [dbo].[ORG_Service_Provider]    Script Date: 11/24/2016 13:11:59 ******/
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
	[CreateTime] [datetime] NULL,
	[CreateUserID] [int] NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [int] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_ORG_Service_Provider] PRIMARY KEY CLUSTERED 
(
	[ORG_ServiceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORG_Service_Item]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Service_Item](
	[ORG_Service_ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[ORGID] [int] NULL,
	[Sub_ORGID] [int] NULL,
	[ItemID] [int] NULL,
	[Required] [bit] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_ORG_Service_Require_Config] PRIMARY KEY CLUSTERED 
(
	[ORG_Service_ItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORG_Service_Config]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORG_Service_Config](
	[ORG_ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ORGID] [int] NULL,
	[ServiceID] [int] NULL,
	[AuthCode] [nvarchar](300) NULL,
	[BeginTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[ServiceAuditStatus] [int] NULL,
	[Diagnosis_ICD_VersionID] [int] NULL,
	[Operation_ICD_VersionID] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码服务的诊断ICD版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ORG_Service_Config', @level2type=N'COLUMN',@level2name=N'Diagnosis_ICD_VersionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手术操作版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ORG_Service_Config', @level2type=N'COLUMN',@level2name=N'Operation_ICD_VersionID'
GO
/****** Object:  Table [dbo].[ORG_Attach]    Script Date: 11/24/2016 13:11:59 ******/
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
/****** Object:  Table [dbo].[Order_Interface]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Interface](
	[RequestID] [bigint] IDENTITY(1,1) NOT NULL,
	[AuthCode] [nvarchar](300) NULL,
	[TicketID] [nvarchar](300) NULL,
	[CodeOrderID] [int] NULL,
	[Version] [nvarchar](100) NULL,
	[RequestTime] [datetime] NULL,
	[GetRequestTime] [datetime] NULL,
	[RequestIp] [nchar](100) NULL,
	[InterfaceName] [nvarchar](200) NULL,
	[SyncResponseCode] [nchar](50) NULL,
	[SyncResponseStr] [nvarchar](150) NULL,
	[SyncResponseTime] [datetime] NULL,
	[NotifyCode] [nchar](50) NULL,
	[NotiryStr] [nvarchar](150) NULL,
	[NotifyContent] [nvarchar](500) NULL,
 CONSTRAINT [PK_Order_Interface] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dic_Service_Menu]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service_Menu](
	[Service_Menu_ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[MenuID] [int] NULL,
	[ServiceType] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Service_Menu] PRIMARY KEY CLUSTERED 
(
	[Service_Menu_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务菜单配置ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Menu', @level2type=N'COLUMN',@level2name=N'Service_Menu_ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Menu', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Menu', @level2type=N'COLUMN',@level2name=N'MenuID'
GO
/****** Object:  Table [dbo].[Dic_Service_Item]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service_Item](
	[Service_UploadItemID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[ItemID] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务需要上传项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Item', @level2type=N'COLUMN',@level2name=N'Service_UploadItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Item', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需要上传项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Item', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
/****** Object:  Table [dbo].[Dic_Service_Claim]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service_Claim](
	[ServiceClaimID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[ClaimType] [int] NULL,
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
/****** Object:  Table [dbo].[Dic_Service_Attach]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Service_Attach](
	[Dic_Service_AttachID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[AttachType] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_Service_Attach] PRIMARY KEY CLUSTERED 
(
	[Dic_Service_AttachID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dic_Service]    Script Date: 11/24/2016 13:11:59 ******/
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
/****** Object:  Table [dbo].[Dic_Organization]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Organization](
	[OrganizationID] [int] NOT NULL,
	[AuthorizeCode] [nvarchar](300) NOT NULL,
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
	[EncryPubKey] [nvarchar](500) NULL,
	[EncryType] [int] NULL,
	[EncryKey] [nvarchar](500) NULL,
	[SignPriKey] [nvarchar](500) NULL,
	[SignType] [int] NULL,
	[SignKey] [nvarchar](500) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加密类型 1：DES 2：AES 3：RSA 4：先DES 再RSA 5：先AES 再RSA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'EncryType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'加密key的名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'EncryKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签名时使用的私钥所在路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'SignPriKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签名类型 1：MD5+Salt 2:先MD5+Salt 在RSA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'SignType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签名key的名称 ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'SignKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'LastModifyID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[Dic_Menu]    Script Date: 11/24/2016 13:11:59 ******/
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
	[MenuIndex] [int] NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目录主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'MenuID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在分区目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'AreaName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'ControllerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同级目录下的排序值（从小到大 小的在前在上）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'MenuIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级MenuID null或者0 是第一级目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'ParentMenuID'
GO
/****** Object:  Table [dbo].[Dic_Item]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dic_Item](
	[ItemID] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](300) NULL,
	[ItemDescription] [nvarchar](500) NULL,
	[PinyinShort] [nvarchar](300) NULL,
	[ParentID] [int] NULL,
	[ItemIndex] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_UploadItem] PRIMARY KEY CLUSTERED 
(
	[ItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'ItemName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'ItemDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级目录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同层级下的该项目的索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'ItemIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Item', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Code_Order_UploadedItem]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Code_Order_UploadedItem](
	[CodeOrderUploadedItemID] [int] IDENTITY(1,1) NOT NULL,
	[CodeOrderID] [int] NULL,
	[ItemID] [int] NULL,
	[GIndex] [int] NULL,
	[ContentType] [nvarchar](max) NULL,
	[AttachURL] [nvarchar](max) NULL,
	[AttachName] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder_Item] PRIMARY KEY CLUSTERED 
(
	[CodeOrderUploadedItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单上传的资料ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'CodeOrderUploadedItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'CodeOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传的项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'ItemID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该资料在该项目下的排序（一个项目中可能上传了多张图片资料）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'GIndex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件上传和下载时通过http协议时的文件类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'ContentType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件保存后的URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'AttachURL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1：删除  其他：未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间戳  并发控制更新的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_UploadedItem', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Code_Order_Operate]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Code_Order_Operate](
	[CodeOrderOperateID] [int] IDENTITY(1,1) NOT NULL,
	[CodeOrderID] [int] NULL,
	[ICDCode] [nvarchar](max) NULL,
	[ICDContent] [nvarchar](max) NULL,
	[OperateTime] [datetime] NULL,
	[OperateLevel] [nvarchar](max) NULL,
	[Opetator] [nvarchar](max) NULL,
	[Assistant1] [nvarchar](max) NULL,
	[Assistant2] [nvarchar](max) NULL,
	[Anesthesia] [nvarchar](max) NULL,
	[Anesthesiologist] [nvarchar](max) NULL,
	[OperateIndex] [int] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder_Operate] PRIMARY KEY CLUSTERED 
(
	[CodeOrderOperateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Code_Order_Diagnosis]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Code_Order_Diagnosis](
	[CodeDiagnosisResultID] [int] IDENTITY(1,1) NOT NULL,
	[CodeOrderID] [int] NULL,
	[DiagnosisIndex] [int] NULL,
	[ICD_Code] [nvarchar](500) NULL,
	[ICD_Content] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Coder_Builder_Diagnosis] PRIMARY KEY CLUSTERED 
(
	[CodeDiagnosisResultID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Code_Order_Audit]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Code_Order_Audit](
	[CodeOrderAuditID] [int] IDENTITY(1,1) NOT NULL,
	[CodeOrderID] [int] NULL,
	[OriginValue] [nvarchar](500) NULL,
	[NewValue] [nvarchar](500) NULL,
	[OperateTarget] [int] NULL,
	[OperateUserID] [nvarchar](128) NULL,
	[OperateDateTime] [datetime] NULL,
	[OperateType] [int] NULL,
	[OperateDescription] [nvarchar](500) NULL,
	[CreateTime] [datetime] NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder_Audit] PRIMARY KEY CLUSTERED 
(
	[CodeOrderAuditID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Code_Order]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Code_Order](
	[CodeOrderID] [int] IDENTITY(1,1) NOT NULL,
	[PlatformOrderCode] [varchar](15) NULL,
	[CaseNum] [nvarchar](200) NULL,
	[OutTime] [datetime] NULL,
	[AdmissionTimes] [int] NULL,
	[MedicalRecordPath] [nvarchar](500) NULL,
	[OrderStatus] [int] NULL,
	[AUTHCode] [nvarchar](300) NULL,
	[ORGID] [int] NULL,
	[ORGSubID] [int] NULL,
	[Diagnosis_ICD_VersionID] [int] NULL,
	[Operation_ICD_VersionID] [int] NULL,
	[PickedTime] [datetime] NULL,
	[PickedUserID] [nvarchar](128) NULL,
	[ServiceID] [int] NULL,
	[OrderType] [int] NULL,
	[Createtime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder] PRIMARY KEY CLUSTERED 
(
	[CodeOrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'CodeOrderID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单号:规则' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'PlatformOrderCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病案号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'CaseNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'OrderStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单需求方的组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'ORGID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单需求方分区组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'ORGSubID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'冗余诊断编码库版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'Diagnosis_ICD_VersionID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'被领单时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'PickedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'领单人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'PickedUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单所属服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单类别  null or 0：图片上传的订单  1：接口上传订单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'OrderType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'Createtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1 删除 其他 未删' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新版本号，单库并发控制字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[BaseDic_Province]    Script Date: 11/24/2016 13:11:59 ******/
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
/****** Object:  Table [dbo].[BaseDic_ICD_Version]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_ICD_Version](
	[ICD_VersionID] [int] IDENTITY(1,1) NOT NULL,
	[ICD_VersionName] [nvarchar](300) NULL,
	[ICD_Description] [nvarchar](300) NULL,
	[ICD_Type] [int] NULL,
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
/****** Object:  Table [dbo].[BaseDic_ICD_Operate_Repository]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_ICD_Operate_Repository](
	[ICDID] [int] IDENTITY(1,1) NOT NULL,
	[ICD_Code] [nvarchar](300) NULL,
	[ICD_Name] [nvarchar](300) NULL,
	[ICD_VersionID] [int] NULL,
	[ICD_Description] [nvarchar](500) NULL,
	[PinyinShort] [nvarchar](300) NULL,
	[Property] [nvarchar](300) NULL,
	[InputOption] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_BaseDic_ICD_Operate_Repository] PRIMARY KEY CLUSTERED 
(
	[ICDID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_ICD_Diagnosis_Repository]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_ICD_Diagnosis_Repository](
	[ICDID] [int] IDENTITY(1,1) NOT NULL,
	[ICD_Code] [nvarchar](300) NULL,
	[ICD_Name] [nvarchar](300) NULL,
	[ICD_VersionID] [int] NULL,
	[ICD_Description] [nvarchar](500) NULL,
	[PinyinShort] [nvarchar](300) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Dic_ICD_Diagnosis_Repository] PRIMARY KEY CLUSTERED 
(
	[ICDID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_Code_History]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_Code_History](
	[CodeHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[CodeNum] [nvarchar](20) NULL,
	[CreateDateTime] [datetime] NULL,
 CONSTRAINT [PK_BaseDic_Code_History] PRIMARY KEY CLUSTERED 
(
	[CodeHistoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_Code]    Script Date: 11/24/2016 13:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseDic_Code](
	[Code] [nvarchar](7) NOT NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_BaseDic_Code] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaseDic_City]    Script Date: 11/24/2016 13:11:59 ******/
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
/****** Object:  Table [dbo].[BaseDic_Area]    Script Date: 11/24/2016 13:11:59 ******/
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
/****** Object:  Default [DF_BaseDic_ICD_Repository_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[BaseDic_ICD_Diagnosis_Repository] ADD  CONSTRAINT [DF_BaseDic_ICD_Repository_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_BaseDic_ICD_Repository_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[BaseDic_ICD_Diagnosis_Repository] ADD  CONSTRAINT [DF_BaseDic_ICD_Repository_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_BaseDic_ICD_Version_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[BaseDic_ICD_Version] ADD  CONSTRAINT [DF_BaseDic_ICD_Version_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_BaseDic_ICD_Version_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[BaseDic_ICD_Version] ADD  CONSTRAINT [DF_BaseDic_ICD_Version_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Code_Builder_Createtime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Code_Order] ADD  CONSTRAINT [DF_Code_Builder_Createtime]  DEFAULT (getdate()) FOR [Createtime]
GO
/****** Object:  Default [DF_Code_Builder_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Code_Order] ADD  CONSTRAINT [DF_Code_Builder_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Code_Builder_Item_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Code_Order_UploadedItem] ADD  CONSTRAINT [DF_Code_Builder_Item_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Code_Builder_Item_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Code_Order_UploadedItem] ADD  CONSTRAINT [DF_Code_Builder_Item_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_UploadItem_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Item] ADD  CONSTRAINT [DF_Dic_UploadItem_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_UploadItem_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Item] ADD  CONSTRAINT [DF_Dic_UploadItem_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Menu_Createtime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Menu] ADD  CONSTRAINT [DF_Dic_Menu_Createtime]  DEFAULT (getdate()) FOR [Createtime]
GO
/****** Object:  Default [DF_Dic_Menu_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Menu] ADD  CONSTRAINT [DF_Dic_Menu_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Organization_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Organization] ADD  CONSTRAINT [DF_Dic_Organization_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_Organization_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Organization] ADD  CONSTRAINT [DF_Dic_Organization_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Service_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service] ADD  CONSTRAINT [DF_Dic_Service_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_Service_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service] ADD  CONSTRAINT [DF_Dic_Service_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Service_Attach_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Attach] ADD  CONSTRAINT [DF_Dic_Service_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_Service_Attach_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Attach] ADD  CONSTRAINT [DF_Dic_Service_Attach_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Service_Claim_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Claim] ADD  CONSTRAINT [DF_Dic_Service_Claim_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_Service_Claim_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Claim] ADD  CONSTRAINT [DF_Dic_Service_Claim_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Service_UploadItem_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Item] ADD  CONSTRAINT [DF_Dic_Service_UploadItem_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_Service_UploadItem_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Item] ADD  CONSTRAINT [DF_Dic_Service_UploadItem_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Dic_Service_Menu_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Menu] ADD  CONSTRAINT [DF_Dic_Service_Menu_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Dic_Service_Menu_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Dic_Service_Menu] ADD  CONSTRAINT [DF_Dic_Service_Menu_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_ORG_Attach_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Attach] ADD  CONSTRAINT [DF_ORG_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ORG_Attach_LastModityTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Attach] ADD  CONSTRAINT [DF_ORG_Attach_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
GO
/****** Object:  Default [DF_ORG_Service_Config_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Service_Config] ADD  CONSTRAINT [DF_ORG_Service_Config_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ORG_Service_Config_LastModityTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Service_Config] ADD  CONSTRAINT [DF_ORG_Service_Config_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
GO
/****** Object:  Default [DF_ORG_Service_UploadItem_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Service_Item] ADD  CONSTRAINT [DF_ORG_Service_UploadItem_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ORG_Service_UploadItem_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Service_Item] ADD  CONSTRAINT [DF_ORG_Service_UploadItem_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_ORG_Service_Provider_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Service_Provider] ADD  CONSTRAINT [DF_ORG_Service_Provider_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ORG_Service_Provider_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_Service_Provider] ADD  CONSTRAINT [DF_ORG_Service_Provider_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_ORG_SubOrganization_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_SubOrganization] ADD  CONSTRAINT [DF_ORG_SubOrganization_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_ORG_SubOrganization_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[ORG_SubOrganization] ADD  CONSTRAINT [DF_ORG_SubOrganization_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Sec_IP_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Sec_IP] ADD  CONSTRAINT [DF_Sec_IP_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Sec_IP_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Sec_IP] ADD  CONSTRAINT [DF_Sec_IP_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_Sec_Message_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Sec_Message] ADD  CONSTRAINT [DF_Sec_Message_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_Sec_Message_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[Sec_Message] ADD  CONSTRAINT [DF_Sec_Message_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_User_Attach_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Attach] ADD  CONSTRAINT [DF_User_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_Attach_LastModityTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Attach] ADD  CONSTRAINT [DF_User_Attach_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
GO
/****** Object:  Default [DF_User_ChangeAuditLog_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_ChangeAuditLog] ADD  CONSTRAINT [DF_User_ChangeAuditLog_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_ChangeAuditLog_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_ChangeAuditLog] ADD  CONSTRAINT [DF_User_ChangeAuditLog_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_User_Service_Attach_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Service_Attach] ADD  CONSTRAINT [DF_User_Service_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_Service_Attach_LastModityTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Service_Attach] ADD  CONSTRAINT [DF_User_Service_Attach_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
GO
/****** Object:  Default [DF_User_Service_Claim_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Service_Claim] ADD  CONSTRAINT [DF_User_Service_Claim_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_Service_Claim_LastModifyTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Service_Claim] ADD  CONSTRAINT [DF_User_Service_Claim_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
GO
/****** Object:  Default [DF_User_Service_Provider_CreateTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Service_Provider] ADD  CONSTRAINT [DF_User_Service_Provider_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF_User_Service_Provider_LastModityTime]    Script Date: 11/24/2016 13:11:59 ******/
ALTER TABLE [dbo].[User_Service_Provider] ADD  CONSTRAINT [DF_User_Service_Provider_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
GO
