CREATE TABLE [dbo].[Files](
	[FileId] [uniqueidentifier] NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Users]
GO
ALTER TABLE [dbo].[Files] ADD  CONSTRAINT [DF_Files_FileId]  DEFAULT (newid()) FOR [FileId]
GO
ALTER TABLE [dbo].[Files] ADD  CONSTRAINT [DF_Files_UploadDate]  DEFAULT (getdate()) FOR [UploadDate]