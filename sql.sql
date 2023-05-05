USE [Clients]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 5/5/2023 11:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[clientid] [int] IDENTITY(1,1) NOT NULL,
	[level] [int] NOT NULL,
	[personid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[clientid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 5/5/2023 11:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[personid] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[dni] [int] NOT NULL,
	[birthdate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[personid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD FOREIGN KEY([personid])
REFERENCES [dbo].[Person] ([personid])
GO
/****** Object:  StoredProcedure [dbo].[AddClient]    Script Date: 5/5/2023 11:16:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--				EXEC AddClient @name = 'Juan', @lastname = 'Pérez', @dni = 12345678, @birthdate = '1990-01-01', @level = 1;


CREATE PROCEDURE [dbo].[AddClient]
    @name		NVARCHAR(50),
    @lastname	NVARCHAR(50),
    @dni		INT,
    @birthdate	DATETIME,
    @level		INT
AS
BEGIN
    DECLARE @personid INT;

    -- Insertar en la tabla Personas
    INSERT INTO Person (name, lastname, dni, birthdate)
    VALUES (@name, @lastname, @dni, @birthdate);

    -- Obtener el id de la persona recién insertada
    SET @personid = SCOPE_IDENTITY();

    -- Insertar en la tabla Clientes
    INSERT INTO Client (level, personid)
    VALUES (@level, @personid);
END
GO
