USE [Squadmakers];
GO

INSERT INTO [dbo].[Usuarios]
(
    [Id],
    [Nombre],
    [Contraseņa]
)
VALUES
(NEWID(), 'Manolito', 'Manolito'),
(NEWID(), 'Pepe', 'Pepe'),
(NEWID(), 'Isabel', 'Isabel'),
(NEWID(), 'Pedro', 'Pedro');