USE Squadmakers;
GO

INSERT INTO [dbo].[Chistes]
(
    [Id],
    [Titulo],
    [Cuerpo],
    [Autor],
    [UsuarioId]
)
SELECT NEWID(),
       CONCAT('Chiste temática ', Tematica.Nombre),
       CONCAT('Chiste temática ', Tematica.Nombre, ' para el Usuario ', usuario.Nombre),
	   usuario.Nombre,
       usuario.Id
FROM dbo.Usuarios usuario
    CROSS JOIN dbo.Tematicas Tematica;

INSERT INTO [dbo].[ChistesTematicas]
(
    [ChisteId],
    [TematicaId]
)
SELECT chiste.Id,
       Tematica.id
FROM dbo.Chistes chiste
    CROSS JOIN dbo.Tematicas Tematica;