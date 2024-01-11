USE Squadmakers;
GO

SELECT chiste.Id,
       chiste.Titulo,
       chiste.Cuerpo,
       chiste.Autor
FROM dbo.Chistes chiste
    INNER JOIN dbo.Usuarios usuario
        ON usuario.Id = chiste.UsuarioId
WHERE usuario.Nombre = 'Manolito';