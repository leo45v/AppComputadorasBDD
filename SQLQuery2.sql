CREATE TABLE Color(
 IdColor SMALLINT NOT NULL,
 ColorRgb NVARCHAR(12) NOT NULL,
 Nombre VARCHAR(30) NOT NULL,
 PRIMARY KEY CLUSTERED ([IdColor] ASC),
);
GO

CREATE TABLE ProductoColor(
IdProducto UNIQUEIDENTIFIER NOT NULL,
IdColor SMALLINT NOT NULL,
CONSTRAINT [FK_ProductoColor_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Producto] ([IdProducto]) ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT [FK_ProductoColor_Color] FOREIGN KEY ([IdColor]) REFERENCES [dbo].[Color] ([IdColor]) ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT [uniqueKeyProductColor] UNIQUE([IdProducto], [IdColor])
);
GO

ALTER TABLE ProductoColor
  ADD CONSTRAINT uniqueKeyProductColor UNIQUE(IdProducto, IdColor);

ALTER TABLE Gabinete DROP COLUMN Color;
GO
ALTER TABLE Monitor DROP COLUMN Color;
GO

INSERT INTO Color(IdColor, ColorRgb, Nombre)
VALUES(1, '255,0,0', 'Rojo'),
      (2, '0,255,0', 'Verde'),
      (3, '0,0,255', 'Azul'),
      (4, '255,255,255', 'Blanco'),
      (5, '0,0,0', 'Negro')
GO