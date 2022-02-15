CREATE TABLE [dbo].[Policy]
(
	[Id] UNIQUEIDENTIFIER NOT NULL , 
    [Name] NVARCHAR(20) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL, 
    [IsDeleted] BIT NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL, 
    [ModifiedBy] UNIQUEIDENTIFIER NULL, 
    [ModifiedDate] DATETIME2 NULL, 
    CONSTRAINT [PK_Policy_Id] PRIMARY KEY NONCLUSTERED ([Id]) 
)

GO

CREATE UNIQUE CLUSTERED INDEX [IX_Policy_Name] ON [dbo].[Policy] ([Name])
