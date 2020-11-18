ALTER TABLE Producto 
ADD Eliminado BIT NOT NULL DEFAULT 0;
GO

CREATE TABLE ProductoTipo(
IdProducto UNIQUEIDENTIFIER NOT NULL,
Tipo NVARCHAR (50)    NOT NULL,
PRIMARY KEY CLUSTERED ([IdProducto] ASC),
CONSTRAINT [FK_ProductoTipo_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([IdProducto]) ON DELETE CASCADE ON UPDATE CASCADE
);
GO
CREATE TRIGGER InsertTipoProductoAlmacenamiento
ON Almacenamiento
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Almacenamiento')
END
GO





CREATE TRIGGER InsertTipoProductoFuente
ON Fuente
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Fuente')
END
GO
CREATE TRIGGER InsertTipoProductoGabinete
ON Gabinete
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Gabinete')
END
GO
CREATE TRIGGER InsertTipoProductoMonitor
ON Monitor
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Monitor')
END
GO
CREATE TRIGGER InsertTipoProductoPlacaBase
ON PlacaBase
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Mother Board')
END
GO
CREATE TRIGGER InsertTipoProductoProcesador
ON Procesador
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Procesador')
END
GO
CREATE TRIGGER InsertTipoProductoRam
ON Ram
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Ram')
END
GO
CREATE TRIGGER InsertTipoProductoTarjetaGrafica
ON TarjetaGrafica
AFTER INSERT
AS
BEGIN
	DECLARE @ID as UNIQUEIDENTIFIER;
	SET @ID = (SELECT IdProducto FROM inserted);
	INSERT INTO ProductoTipo (IdProducto, Tipo)
	VALUES ( @ID ,'Trajeta Grafica')
END
GO