USE [Orenjs]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nick] [nvarchar](max) NULL,
	[NameSurname] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Role] [int] NULL,
	[CommentID] [int] NULL,
	[LikeID] [int] NULL,
	[ArticleID] [int] NULL,
	[AddedDate] [datetime2](7) NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[isAllPropsFilled] [bit] NOT NULL,
	[AboutMe] [nvarchar](max) NULL,
	[isArtist] [bit] NOT NULL,
	[Seen] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AppUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ArtComments]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArtComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArticleID] [int] NOT NULL,
	[CommentID] [int] NOT NULL,
	[AddedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.ArtComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Articles]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Desc] [nvarchar](max) NULL,
	[CommentID] [int] NULL,
	[LikeID] [int] NULL,
	[AddedDate] [datetime2](7) NULL,
	[ImagePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Articles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ArticleID] [int] NULL,
	[Text] [nvarchar](max) NULL,
	[AddedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Likes]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Likes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArticleID] [int] NOT NULL,
	[AppUserId] [int] NOT NULL,
	[AddedDate] [datetime2](7) NULL,
	[LikerName] [nvarchar](max) NULL,
	[LastSeen] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.Likes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 03.10.2018 22:57:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ReceivederId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[AddedDate] [datetime2](7) NULL,
	[LastCheck] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[AppUser] ADD  DEFAULT ((0)) FOR [isAllPropsFilled]
GO
ALTER TABLE [dbo].[AppUser] ADD  DEFAULT ((0)) FOR [isArtist]
GO
ALTER TABLE [dbo].[AppUser] ADD  DEFAULT ((0)) FOR [Seen]
GO
ALTER TABLE [dbo].[ArtComments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ArtComments_dbo.Articles_ArticleID] FOREIGN KEY([ArticleID])
REFERENCES [dbo].[Articles] ([Id])
GO
ALTER TABLE [dbo].[ArtComments] CHECK CONSTRAINT [FK_dbo.ArtComments_dbo.Articles_ArticleID]
GO
ALTER TABLE [dbo].[ArtComments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ArtComments_dbo.Comments_CommentID] FOREIGN KEY([CommentID])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[ArtComments] CHECK CONSTRAINT [FK_dbo.ArtComments_dbo.Comments_CommentID]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Articles_dbo.AppUser_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_dbo.Articles_dbo.AppUser_UserID]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comments_dbo.AppUser_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_dbo.Comments_dbo.AppUser_UserID]
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Likes_dbo.AppUser_AppUserId] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Likes] CHECK CONSTRAINT [FK_dbo.Likes_dbo.AppUser_AppUserId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.AppUser_ReceivederId] FOREIGN KEY([ReceivederId])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.AppUser_ReceivederId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.AppUser_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AppUser] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.AppUser_SenderId]
GO
