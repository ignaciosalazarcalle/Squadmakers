USE Squadmakers;
GO

SELECT chiste.Id,
       chiste.Titulo,
       chiste.Cuerpo,
       chiste.Autor
FROM dbo.Chistes chiste
    INNER JOIN dbo.ChistesTematicas chisteTematica
        ON chisteTematica.ChisteId = chiste.Id
    INNER JOIN dbo.Tematicas tematica
        ON tematica.Id = chisteTematica.TematicaId
WHERE tematica.Nombre = 'Humor negro';