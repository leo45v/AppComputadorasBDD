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

ALTER TABLE Producto ALTER COLUMN Imagen NVARCHAR(MAX) NOT NULL;

INSERT INTO Color(IdColor, ColorRgb, Nombre)
VALUES(1, '255,0,0', 'Rojo'),
      (2, '0,255,0', 'Verde'),
      (3, '0,0,255', 'Azul'),
      (4, '255,255,255', 'Blanco'),
      (5, '0,0,0', 'Negro')
GO


ALTER TABLE Procesador 
ADD Socket INT NOT NULL DEFAULT 0;
GO
ALTER TABLE PlacaBase
ALTER COLUMN SoporteProcesador INT NOT NULL;
GO

CREATE TABLE SocketProcesador(
	IdSocket INT NOT NULL,
	NombreSocket NVARCHAR(20) NOT NULL,
	Descripcion NVARCHAR(100),
	PRIMARY KEY CLUSTERED ([IdSocket] ASC)
);
GO

INSERT INTO SocketProcesador(IdSocket, NombreSocket, Descripcion)
VALUES (0,'Ninguno', 'ERROR'),
       (1, 'Socket_604_2002','Intel Xeon Año 2002'),
       (102, 'LGA_1155_2011','Intel 2da Generación Año 2011'),
       (103, 'LGA_1155_2012','Intel 3ra Generación Año 2012'),
       (104, 'LGA_1150_2013','Intel 4ta Generación Año 2013'),
       (105, 'LGA_1150_2014','Intel 5ta Generación Año 2014'),
       (106, 'LGA_1151_2015','Intel 6ta Generación Año 2015'),
       (107, 'LGA_1151_2016','Intel 7ma Generación Año 2016'),
       (108, 'LGA_1151_2017','Intel 8va Generación Año 2017'),
       (109, 'LGA_1151_2018','Intel 9na Generación Año 2018'),
       (110, 'LGA_1200_2020','Intel 10ma Generación Año 2020'),
       (299, 'AM4', 'ZEN, ZEN+, ZEN2, ZEN 3'),
       (300, 'AM4_A320','AMD SOPORTE ZEN, ZEN+, ZEN 2 (ALGUNOS), No Overclock, Año 2017'),
       (301, 'AM4_B350','AMD SOPORTE ZEN, ZEN+, ZEN 2 (ALGUNOS), Año 2017'),
       (302, 'AM4_X370','AMD SOPORTE ZEN, ZEN+, ZEN 2 (ALGUNOS), Año 2017'),
       (303, 'AM4_B450','AMD SOPORTE ZEN, ZEN+, ZEN 2, ZEN 3 (ALGUNOS), Año 2018'),
       (304, 'AM4_X470','AMD SOPORTE ZEN, ZEN+, ZEN 2, ZEN 3 (ALGUNOS), Año 2018'),
       (305, 'AM4_A520','AMD SOPORTE ZEN 2, ZEN 3, No Overclock, Año 2020'),
       (306, 'AM4_B550','AMD SOPORTE ZEN 2, ZEN 3, Año 2020'),
       (307, 'AM4_X570','AMD SOPORTE ZEN+, ZEN 2, ZEN 3, Año 2019')
GO

ALTER TABLE Procesador 
ADD 
CONSTRAINT [FK_Procesador_SocketProcesador] FOREIGN KEY ([Socket]) REFERENCES [dbo].[SocketProcesador] ([IdSocket]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE PlacaBase 
ADD
CONSTRAINT [FK_PlacaBase_SocketProcesador] FOREIGN KEY ([SoporteProcesador]) REFERENCES [dbo].[SocketProcesador] ([IdSocket]) ON DELETE CASCADE ON UPDATE CASCADE
GO

ALTER TABLE TarjetaGrafica
ADD Consumo INT NOT NULL DEFAULT 0;
GO


CREATE TABLE Ratio(
	IdRatio TINYINT NOT NULL,
	NombreRatio VARCHAR(20) NOT NULL,
	PRIMARY KEY CLUSTERED ([IdRatio] ASC),
)
GO
CREATE TABLE Resolucion(
	IdResolucion TINYINT NOT NULL,
	NombreResolucion VARCHAR(20) NOT NULL,
	PRIMARY KEY CLUSTERED ([IdResolucion] ASC),
)
GO

INSERT INTO Ratio (IdRatio, NombreRatio)
VALUES (1, '4:3'),
	   (2, '16:9'),
	   (3, '16:10'),
	   (4, '21:9'),
	   (5, '47:20')
GO

INSERT INTO Resolucion(IdResolucion, NombreResolucion)
VALUES (1, '1280x720'),
	   (2, '1280x800'),
	   (3, '1600x900'),
	   (4, '1920x1080'),
	   (5, '1920x1200'),
	   (6, '2560x1440'),
	   (7, '2560x1600'),
	   (8, '3840x2160')
GO


EXEC sp_rename 'Monitor.Resolucion', 'IdResolucion', 'COLUMN';
GO
EXEC sp_rename 'Monitor.Ratio', 'IdRatio', 'COLUMN';
GO

ALTER TABLE Monitor ALTER COLUMN IdResolucion
TINYINT NOT NULL;
GO
ALTER TABLE Monitor ADD 
CONSTRAINT [FK_Monitor_Resolucion] FOREIGN KEY ([IdResolucion]) REFERENCES [dbo].[Resolucion] ([IdResolucion]) ON DELETE CASCADE ON UPDATE CASCADE
GO
ALTER TABLE Monitor ALTER COLUMN IdRatio
TINYINT NOT NULL;
GO
ALTER TABLE Monitor ADD 
CONSTRAINT [FK_Monitor_Ratio] FOREIGN KEY ([IdRatio]) REFERENCES [dbo].[Ratio] ([IdRatio]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- AGREGAR 2020-27-11

ALTER TABLE Fuente ALTER COLUMN Certificacion INT NOT NULL;
GO
