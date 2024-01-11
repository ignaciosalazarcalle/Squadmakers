USE [Squadmakers];
GO

IF EXISTS
(
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'ChistesTematicas'
)
BEGIN
    DROP TABLE [dbo].[ChistesTematicas];
END;

IF EXISTS
(
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Chistes'
)
BEGIN
    DROP TABLE [dbo].[Chistes];
END;

IF EXISTS
(
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Tematicas'
)
BEGIN
    DROP TABLE [dbo].[Tematicas];
END;

IF EXISTS
(
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_NAME = 'Usuarios'
)
BEGIN
    DROP TABLE [dbo].[Usuarios];
END;

CREATE TABLE [dbo].[Chistes]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [Titulo] [NVARCHAR](255) NOT NULL,
    [Cuerpo] [NVARCHAR](MAX) NOT NULL,
    [Autor] [NVARCHAR](255) NOT NULL,
    [UsuarioId] [UNIQUEIDENTIFIER] NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
          ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
         ) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
GO

CREATE TABLE [dbo].[ChistesTematicas]
(
    [ChisteId] [UNIQUEIDENTIFIER] NOT NULL,
    [TematicaId] [INT] NOT NULL,
    PRIMARY KEY CLUSTERED (
                              [ChisteId] ASC,
                              [TematicaId] ASC
                          )
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
          ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
         ) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [dbo].[Tematicas]
(
    [Id] [INT] NOT NULL,
    [Nombre] [NVARCHAR](255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
          ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
         ) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [dbo].[Usuarios]
(
    [Id] [UNIQUEIDENTIFIER] NOT NULL,
    [Nombre] [NVARCHAR](255) NOT NULL,
    [Contraseña] [NVARCHAR](255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
          ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
         ) ON [PRIMARY]
) ON [PRIMARY];
GO
ALTER TABLE [dbo].[Chistes] WITH CHECK
ADD
    FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id]);
GO
ALTER TABLE [dbo].[ChistesTematicas] WITH CHECK
ADD
    FOREIGN KEY ([ChisteId]) REFERENCES [dbo].[Chistes] ([Id]);
GO
ALTER TABLE [dbo].[ChistesTematicas] WITH CHECK
ADD
    FOREIGN KEY ([TematicaId]) REFERENCES [dbo].[Tematicas] ([Id]);
GO
