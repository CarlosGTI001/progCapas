SELECT * FROM usuario

CREATE PROC actualizarEstatus
	@estatus nvarchar(20),
	@usuario nvarchar(255)
AS
	UPDATE usuario 
	SET estatus = @estatus
	WHERE usuario = @usuario;
GO

