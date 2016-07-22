/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](max) NULL,
	[LastModtifyTime] [datetime] NULL,
	[LastModityUserID] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User_Service_Provider]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Service_Provider]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User_Service_Provider](
	[User_ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[ServiceID] [int] NULL,
	[CertificationStatus] [int] NULL,
	[ApplyTime] [datetime] NULL,
	[PlatformAuditTime] [datetime] NULL,
	[PlatformAuditUserID] [int] NULL,
	[PlatformAbendTime] [datetime] NULL,
	[PlatformAbendUserID] [datetime] NULL,
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
END
GO
/****** Object:  Table [dbo].[User_Service_Attach]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Service_Attach]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User_Service_Attach](
	[UserServiceAttachID] [int] IDENTITY(1,1) NOT NULL,
	[User_ServiceID] [int] NULL,
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
END
GO
/****** Object:  Table [dbo].[User_ChangeAuditLog]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_ChangeAuditLog]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'UserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'UserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'OriginValue'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'原来的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OriginValue'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'NewValue'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作后的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'NewValue'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'OperateTarget'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作对象' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OperateTarget'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'OperateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OperateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'Operatedatetime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'Operatedatetime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'OperateType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OperateType'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'OpertateDescription'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作的整体描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'OpertateDescription'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_ChangeAuditLog', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_ChangeAuditLog', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[User_Attach]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User_Attach]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User_Attach](
	[UserAttachID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'User_Attach', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1：删除 其他未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_Attach', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
/****** Object:  Table [dbo].[Sec_Message]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sec_Message]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Sec_Message', N'COLUMN',N'SecurityMessage_ID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短信发送记录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'SecurityMessage_ID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Sec_Message', N'COLUMN',N'UserPhoneNumber'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送的手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'UserPhoneNumber'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Sec_Message', N'COLUMN',N'UserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'UserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Sec_Message', N'COLUMN',N'SourceIP'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送短信时，用户的请求地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'SourceIP'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Sec_Message', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间 发送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Sec_Message', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sec_Message', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[Sec_IP]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sec_IP]') AND type in (N'U'))
BEGIN
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
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ORG_SubOrganization]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORG_SubOrganization]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ORG_Service_UploadItem]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORG_Service_UploadItem]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ORG_Service_Provider]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORG_Service_Provider]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ORG_Service_Config]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORG_Service_Config]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[ORG_Attach]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ORG_Attach]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'ORG_Attach', N'COLUMN',N'ORGAttachURL'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL长度在HTTP中没有限制，但是浏览器和Web服务器会有最大长度字符数限制，中文编码后长度会变长，此处先限制到250' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ORG_Attach', @level2type=N'COLUMN',@level2name=N'ORGAttachURL'
GO
/****** Object:  UserDefinedFunction [dbo].[fun_getPY]    Script Date: 07/21/2016 16:14:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_getPY]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'create function [dbo].[fun_getPY](@str nvarchar(4000)) 
returns nvarchar(4000) 
as 
begin 
declare @word nchar(1),@PY nvarchar(4000) 
set @PY='''' 
while len(@str)>0 
begin 
set @word=left(@str,1) 
--如果非汉字字符，返回原字符 
set @PY=@PY+(case when unicode(@word) between 19968 and 19968+20901 
then (select top 1 PY from ( 
select ''A'' as PY,N''骜'' as word 
union all select ''B'',N''簿'' 
union all select ''C'',N''错'' 
union all select ''D'',N''鵽'' 
union all select ''E'',N''樲'' 
union all select ''F'',N''鳆'' 
union all select ''G'',N''腂'' 
union all select ''H'',N''夻'' 
union all select ''J'',N''攈'' 
union all select ''K'',N''穒'' 
union all select ''L'',N''鱳'' 
union all select ''M'',N''旀'' 
union all select ''N'',N''桛'' 
union all select ''O'',N''沤'' 
union all select ''P'',N''曝'' 
union all select ''Q'',N''囕'' 
union all select ''R'',N''鶸'' 
union all select ''S'',N''蜶'' 
union all select ''T'',N''箨'' 
union all select ''W'',N''鹜'' 
union all select ''X'',N''鑂'' 
union all select ''Y'',N''韵'' 
union all select ''Z'',N''咗'' 
) T 
where word>=@word collate Chinese_PRC_CS_AS_KS_WS 
order by PY ASC) else '''' end) 
set @str=right(@str,len(@str)-1) 
end 
return @PY 
end ' 
END
GO
/****** Object:  Table [dbo].[Dic_UploadItem]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_UploadItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dic_UploadItem](
	[UploadItemID] [int] IDENTITY(1,1) NOT NULL,
	[UploadItemName] [nvarchar](300) NULL,
	[UploadItemDescription] [nvarchar](500) NULL,
	[PinyinShort] [nvarchar](300) NULL,
	[ParentID] [int] NULL,
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'UploadItemName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'UploadItemDescription'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传项目描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemDescription'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'ParentID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级目录ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'ParentID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'UploadItemIndex'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同层级下的该项目的索引' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemIndex'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'LastModifyUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_UploadItem', N'COLUMN',N'LastModifyStamp'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_UploadItem', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Dic_Service_UploadItem]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_Service_UploadItem]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_UploadItem', N'COLUMN',N'Service_UploadItemID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务需要上传项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_UploadItem', @level2type=N'COLUMN',@level2name=N'Service_UploadItemID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_UploadItem', N'COLUMN',N'ServiceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_UploadItem', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_UploadItem', N'COLUMN',N'UploadItemID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'需要上传项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_UploadItem', @level2type=N'COLUMN',@level2name=N'UploadItemID'
GO
/****** Object:  Table [dbo].[Dic_Service_Menu]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_Service_Menu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dic_Service_Menu](
	[Service_Menu_ID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NULL,
	[MenuID] [int] NULL,
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Menu', N'COLUMN',N'Service_Menu_ID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务菜单配置ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Menu', @level2type=N'COLUMN',@level2name=N'Service_Menu_ID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Menu', N'COLUMN',N'ServiceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Menu', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Menu', N'COLUMN',N'MenuID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Menu', @level2type=N'COLUMN',@level2name=N'MenuID'
GO
/****** Object:  Table [dbo].[Dic_Service_Claim]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_Service_Claim]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'ServiceClaimID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务限制ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ServiceClaimID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'ServiceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'ClaimType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制项目' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ClaimType'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'ClaimOperateType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制操作符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ClaimOperateType'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'ClaimValue'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'限制值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'ClaimValue'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'LastModifyUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service_Claim', N'COLUMN',N'LastModifyStamp'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service_Claim', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Dic_Service]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_Service]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dic_Service](
	[ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](300) NULL,
	[Description] [nvarchar](500) NULL,
	[ServiceType] [int] NULL,
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'ServiceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务ID主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'ServiceID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'ServiceName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'ServiceName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'Description'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'Description'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'LastModifyUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Service', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Service', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
/****** Object:  Table [dbo].[Dic_Organization]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_Organization]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'OrganizationID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'OrganizationID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'OrganizationCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构代码_全国组织结构代码管理中心可查询到的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'OrganizationCode'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'OrganizationName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构名称_全国组织结构代码管理中心可查询到的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'OrganizationName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'ShortName_CN'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构中文简写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'ShortName_CN'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'ShortName_EN'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构英语简写' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'ShortName_EN'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'RegisterCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构登记证号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'RegisterCode'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'ProvinceID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织结构所在省份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'ProvinceID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'CityID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构所在城市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CityID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'AreaID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构所在区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'AreaID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'DetailAddress1'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构详细地址，主机构主要存储组织结构注册地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'DetailAddress1'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'DetailAddress2'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'组织机构有分区，但是病案室没有分开的情况，存储多个地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'DetailAddress2'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'DetailAddress3'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展字段，存储多个组织机构地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'DetailAddress3'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'LastModifyID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'LastModifyID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Organization', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Organization', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
/****** Object:  Table [dbo].[Dic_Menu]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dic_Menu]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'MenuID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目录主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'MenuID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'DisplayName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'DisplayName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'AreaName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所在分区目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'AreaName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'ControllerName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'控制器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'ControllerName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'ActionName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'ActionName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'MenuIndex'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同级目录下的排序值（从小到大 小的在前在上）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'MenuIndex'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Dic_Menu', N'COLUMN',N'ParentMenuID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级MenuID null或者0 是第一级目录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Dic_Menu', @level2type=N'COLUMN',@level2name=N'ParentMenuID'
GO
/****** Object:  Table [dbo].[Code_Order_Operate]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Code_Order_Operate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Code_Order_Operate](
	[CodeOrderOperateID] [int] NOT NULL,
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
END
GO
/****** Object:  Table [dbo].[Code_Order_Item]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Code_Order_Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Code_Order_Item](
	[CodeOrderItemID] [int] IDENTITY(1,1) NOT NULL,
	[CodeOrderID] [int] NULL,
	[UploadItemID] [int] NULL,
	[GIndex] [int] NULL,
	[ContentType] [nvarchar](max) NULL,
	[AttachURL] [nvarchar](max) NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [nvarchar](128) NULL,
	[LastModifyTime] [datetime] NULL,
	[LastModifyUserID] [nvarchar](128) NULL,
	[DeleteFlag] [int] NULL,
	[LastModifyStamp] [timestamp] NULL,
 CONSTRAINT [PK_Code_Builder_Item] PRIMARY KEY CLUSTERED 
(
	[CodeOrderItemID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'CodeOrderItemID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单上传的资料ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'CodeOrderItemID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'CodeOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'CodeOrderID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'UploadItemID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传的项目ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'UploadItemID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'GIndex'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该资料在该项目下的排序（一个项目中可能上传了多张图片资料）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'GIndex'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'LastModifyUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1：删除  其他：未删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order_Item', N'COLUMN',N'LastModifyStamp'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间戳  并发控制更新的版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order_Item', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[Code_Order_Diagnosis]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Code_Order_Diagnosis]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Code_Order_Diagnosis](
	[CodeResultID] [int] IDENTITY(1,1) NOT NULL,
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
	[CodeResultID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Code_Order_Audit]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Code_Order_Audit]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[Code_Order]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Code_Order]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Code_Order](
	[CodeOrderID] [int] IDENTITY(1,1) NOT NULL,
	[PlatformOrderCode] [varchar](15) NULL,
	[CaseNum] [nvarchar](max) NULL,
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
	[CodeOrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'CodeOrderID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'CodeOrderID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'PlatformOrderCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码订单号:规则' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'PlatformOrderCode'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'CaseNum'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'病案号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'CaseNum'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'OrderStatus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'OrderStatus'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'ORGID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单需求方的组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'ORGID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'ORGSubID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单需求方分区组织ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'ORGSubID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'Createtime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'Createtime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'订单创建人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'LastModifyUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记 1 删除 其他 未删' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Code_Order', N'COLUMN',N'LastModifyStamp'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新版本号，单库并发控制字段' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Code_Order', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[BaseDic_Province]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseDic_Province]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[BaseDic_ICD_Version]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseDic_ICD_Version]') AND type in (N'U'))
BEGIN
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
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'ICD_VersionID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ICD版本ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'ICD_VersionID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'ICD_VersionName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ICD版本号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'ICD_VersionName'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'ICD_Description'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ICD版本描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'ICD_Description'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'CreateTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'CreateUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'CreateUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'LastModifyTime'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'LastModifyTime'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'LastModifyUserID'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'LastModifyUserID'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'DeleteFlag'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'删除标记' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'DeleteFlag'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BaseDic_ICD_Version', N'COLUMN',N'LastModifyStamp'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BaseDic_ICD_Version', @level2type=N'COLUMN',@level2name=N'LastModifyStamp'
GO
/****** Object:  Table [dbo].[BaseDic_ICD_Repository]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseDic_ICD_Repository]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[BaseDic_Code]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseDic_Code]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BaseDic_Code](
	[Code] [nvarchar](7) NOT NULL,
	[LastModiftTime] [datetime] NULL,
 CONSTRAINT [PK_BaseDic_Code] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BaseDic_City]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseDic_City]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[BaseDic_Area]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseDic_Area]') AND type in (N'U'))
BEGIN
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
END
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[IDCardNo] [nvarchar](max) NULL,
	[RealName] [nvarchar](max) NULL,
	[BankCardNO] [nvarchar](max) NULL,
	[CertificationFlag] [int] NULL,
	[ORGID] [int] NULL,
	[SubORGID] [int] NULL,
	[IsFirstLogin] [int] NULL,
	[LastLoginIP] [nvarchar](max) NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateTime] [datetime] NULL,
	[CreateUserID] [int] NULL,
	[LastModtifyTime] [datetime] NULL,
	[LastModityUserID] [int] NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 07/21/2016 16:14:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Default [DF_BaseDic_ICD_Repository_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BaseDic_ICD_Repository_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseDic_ICD_Repository]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BaseDic_ICD_Repository_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BaseDic_ICD_Repository] ADD  CONSTRAINT [DF_BaseDic_ICD_Repository_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_BaseDic_ICD_Repository_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BaseDic_ICD_Repository_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseDic_ICD_Repository]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BaseDic_ICD_Repository_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BaseDic_ICD_Repository] ADD  CONSTRAINT [DF_BaseDic_ICD_Repository_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_BaseDic_ICD_Version_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BaseDic_ICD_Version_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseDic_ICD_Version]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BaseDic_ICD_Version_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BaseDic_ICD_Version] ADD  CONSTRAINT [DF_BaseDic_ICD_Version_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_BaseDic_ICD_Version_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BaseDic_ICD_Version_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[BaseDic_ICD_Version]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BaseDic_ICD_Version_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BaseDic_ICD_Version] ADD  CONSTRAINT [DF_BaseDic_ICD_Version_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Code_Builder_Createtime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Code_Builder_Createtime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Code_Order]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Code_Builder_Createtime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Code_Order] ADD  CONSTRAINT [DF_Code_Builder_Createtime]  DEFAULT (getdate()) FOR [Createtime]
END


End
GO
/****** Object:  Default [DF_Code_Builder_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Code_Builder_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Code_Order]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Code_Builder_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Code_Order] ADD  CONSTRAINT [DF_Code_Builder_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Code_Builder_Item_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Code_Builder_Item_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Code_Order_Item]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Code_Builder_Item_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Code_Order_Item] ADD  CONSTRAINT [DF_Code_Builder_Item_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Code_Builder_Item_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Code_Builder_Item_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Code_Order_Item]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Code_Builder_Item_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Code_Order_Item] ADD  CONSTRAINT [DF_Code_Builder_Item_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_Menu_Createtime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Menu_Createtime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Menu]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Menu_Createtime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Menu] ADD  CONSTRAINT [DF_Dic_Menu_Createtime]  DEFAULT (getdate()) FOR [Createtime]
END


End
GO
/****** Object:  Default [DF_Dic_Menu_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Menu_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Menu]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Menu_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Menu] ADD  CONSTRAINT [DF_Dic_Menu_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_Organization_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Organization_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Organization]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Organization_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Organization] ADD  CONSTRAINT [DF_Dic_Organization_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Dic_Organization_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Organization_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Organization]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Organization_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Organization] ADD  CONSTRAINT [DF_Dic_Organization_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service] ADD  CONSTRAINT [DF_Dic_Service_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service] ADD  CONSTRAINT [DF_Dic_Service_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_Claim_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_Claim_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service_Claim]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_Claim_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service_Claim] ADD  CONSTRAINT [DF_Dic_Service_Claim_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_Claim_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_Claim_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service_Claim]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_Claim_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service_Claim] ADD  CONSTRAINT [DF_Dic_Service_Claim_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_Menu_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_Menu_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service_Menu]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_Menu_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service_Menu] ADD  CONSTRAINT [DF_Dic_Service_Menu_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_Menu_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_Menu_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service_Menu]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_Menu_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service_Menu] ADD  CONSTRAINT [DF_Dic_Service_Menu_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_UploadItem_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_UploadItem_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service_UploadItem]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_UploadItem_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service_UploadItem] ADD  CONSTRAINT [DF_Dic_Service_UploadItem_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Dic_Service_UploadItem_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_Service_UploadItem_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_Service_UploadItem]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_Service_UploadItem_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_Service_UploadItem] ADD  CONSTRAINT [DF_Dic_Service_UploadItem_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Dic_UploadItem_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_UploadItem_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_UploadItem]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_UploadItem_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_UploadItem] ADD  CONSTRAINT [DF_Dic_UploadItem_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Dic_UploadItem_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Dic_UploadItem_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Dic_UploadItem]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Dic_UploadItem_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dic_UploadItem] ADD  CONSTRAINT [DF_Dic_UploadItem_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_ORG_Attach_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Attach_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Attach]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Attach_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Attach] ADD  CONSTRAINT [DF_ORG_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_ORG_Attach_LastModityTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Attach_LastModityTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Attach]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Attach_LastModityTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Attach] ADD  CONSTRAINT [DF_ORG_Attach_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
END


End
GO
/****** Object:  Default [DF_ORG_Service_Config_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Service_Config_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Service_Config]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Service_Config_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Service_Config] ADD  CONSTRAINT [DF_ORG_Service_Config_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_ORG_Service_Config_LastModityTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Service_Config_LastModityTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Service_Config]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Service_Config_LastModityTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Service_Config] ADD  CONSTRAINT [DF_ORG_Service_Config_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
END


End
GO
/****** Object:  Default [DF_ORG_Service_Provider_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Service_Provider_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Service_Provider]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Service_Provider_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Service_Provider] ADD  CONSTRAINT [DF_ORG_Service_Provider_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_ORG_Service_Provider_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Service_Provider_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Service_Provider]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Service_Provider_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Service_Provider] ADD  CONSTRAINT [DF_ORG_Service_Provider_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_ORG_Service_UploadItem_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Service_UploadItem_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Service_UploadItem]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Service_UploadItem_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Service_UploadItem] ADD  CONSTRAINT [DF_ORG_Service_UploadItem_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_ORG_Service_UploadItem_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_Service_UploadItem_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_Service_UploadItem]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_Service_UploadItem_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_Service_UploadItem] ADD  CONSTRAINT [DF_ORG_Service_UploadItem_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_ORG_SubOrganization_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_SubOrganization_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_SubOrganization]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_SubOrganization_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_SubOrganization] ADD  CONSTRAINT [DF_ORG_SubOrganization_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_ORG_SubOrganization_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ORG_SubOrganization_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[ORG_SubOrganization]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ORG_SubOrganization_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ORG_SubOrganization] ADD  CONSTRAINT [DF_ORG_SubOrganization_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Sec_IP_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Sec_IP_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sec_IP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sec_IP_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sec_IP] ADD  CONSTRAINT [DF_Sec_IP_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Sec_IP_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Sec_IP_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sec_IP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sec_IP_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sec_IP] ADD  CONSTRAINT [DF_Sec_IP_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_Sec_Message_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Sec_Message_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sec_Message]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sec_Message_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sec_Message] ADD  CONSTRAINT [DF_Sec_Message_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_Sec_Message_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Sec_Message_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[Sec_Message]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Sec_Message_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Sec_Message] ADD  CONSTRAINT [DF_Sec_Message_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_User_Attach_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Attach_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_Attach]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Attach_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_Attach] ADD  CONSTRAINT [DF_User_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_User_Attach_LastModityTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Attach_LastModityTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_Attach]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Attach_LastModityTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_Attach] ADD  CONSTRAINT [DF_User_Attach_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
END


End
GO
/****** Object:  Default [DF_User_ChangeAuditLog_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_ChangeAuditLog_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_ChangeAuditLog]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_ChangeAuditLog_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_ChangeAuditLog] ADD  CONSTRAINT [DF_User_ChangeAuditLog_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_User_ChangeAuditLog_LastModifyTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_ChangeAuditLog_LastModifyTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_ChangeAuditLog]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_ChangeAuditLog_LastModifyTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_ChangeAuditLog] ADD  CONSTRAINT [DF_User_ChangeAuditLog_LastModifyTime]  DEFAULT (getdate()) FOR [LastModifyTime]
END


End
GO
/****** Object:  Default [DF_User_Service_Attach_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Service_Attach_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_Service_Attach]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Service_Attach_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_Service_Attach] ADD  CONSTRAINT [DF_User_Service_Attach_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_User_Service_Attach_LastModityTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Service_Attach_LastModityTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_Service_Attach]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Service_Attach_LastModityTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_Service_Attach] ADD  CONSTRAINT [DF_User_Service_Attach_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
END


End
GO
/****** Object:  Default [DF_User_Service_Provider_CreateTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Service_Provider_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_Service_Provider]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Service_Provider_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_Service_Provider] ADD  CONSTRAINT [DF_User_Service_Provider_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_User_Service_Provider_LastModityTime]    Script Date: 07/21/2016 16:14:25 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_User_Service_Provider_LastModityTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[User_Service_Provider]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_User_Service_Provider_LastModityTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[User_Service_Provider] ADD  CONSTRAINT [DF_User_Service_Provider_LastModityTime]  DEFAULT (getdate()) FOR [LastModityTime]
END


End
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]    Script Date: 07/21/2016 16:14:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]    Script Date: 07/21/2016 16:14:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]    Script Date: 07/21/2016 16:14:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]    Script Date: 07/21/2016 16:14:25 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
