USE [51ICD_DB]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 08/26/2016 15:47:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

truncate table dbo.Code_Order
truncate table dbo.Code_Order_Audit
truncate table dbo.Code_Order_Diagnosis
truncate table dbo.Code_Order_Operate
truncate table dbo.Code_Order_UploadedItem


truncate table dbo.User_Attach
truncate table dbo.User_ChangeAuditLog
truncate table dbo.User_Service_Attach
truncate table dbo.User_Service_Claim
truncate table dbo.User_Service_Providerd