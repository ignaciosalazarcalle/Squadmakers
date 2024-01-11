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
       CONCAT('Chiste temática ', tematica.Nombre),
       CONCAT('Chiste temática ', tematica.Nombre, ' para el Usuario ', usuario.Nombre),
       usuario.Nombre,
       usuario.Id
FROM dbo.Usuarios usuario
    CROSS APPLY
(
    SELECT TOP 3
           ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS RowNum
    FROM master.dbo.spt_values
) AS ChistesGenerados
    CROSS JOIN dbo.Tematicas tematica;

INSERT INTO [dbo].[ChistesTematicas]
(
    [ChisteId],
    [TematicaId]
)
SELECT chiste.Id AS ChisteId, tematica.TematicaId
FROM dbo.Chistes chiste
JOIN (
    SELECT Id TematicaId, Nombre AS TematicaNombre
    FROM dbo.Tematicas tematica
) tematica ON chiste.Titulo LIKE '%' + tematica.TematicaNombre + '%'
