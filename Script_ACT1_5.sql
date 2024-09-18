CREATE DATABASE ACT1_5
GO
USE ACT1_5
SET DATEFORMAT dmy
GO
CREATE TABLE ARTICULOS (
id_articulo int IDENTITY(1, 1) not null,
descripcion varchar(50) not null,
estado bit not null
CONSTRAINT PK_ARTICULOS PRIMARY KEY (id_articulo)
)

CREATE TABLE TIPOS_PAGOS (
id_tipo_pago int IDENTITY(1, 1) not null,
descripcion varchar(50) not null
CONSTRAINT PK_TIPOS_PAGOS PRIMARY KEY (id_tipo_pago)
)

CREATE TABLE FACTURAS (
id_factura int IDENTITY(1, 1) not null,
fecha_factura datetime not null,
id_tipo_pago int not null,
cliente varchar(50)not null
CONSTRAINT PK_FACTURAS PRIMARY KEY (id_factura),
CONSTRAINT FK_FACTURAS_TIPOS_PAGOS FOREIGN KEY (id_tipo_pago) REFERENCES TIPOS_PAGOS (id_tipo_pago) 
)

CREATE TABLE DETALLES_FACTURAS (
id_detalle_factura int IDENTITY(1, 1) not null,
id_factura int not null, 
id_articulo int not null,
cantidad int not null,
precio_unidad float not null
CONSTRAINT PK_DETALLES_FACTURAS PRIMARY KEY (id_detalle_factura),
CONSTRAINT FK_DETALLES_FACTURAS_FACTURAS FOREIGN KEY (id_factura) REFERENCES FACTURAS (id_factura),
CONSTRAINT FK_DETALLES_FACTURAS_ARTICULOS FOREIGN KEY (id_articulo) REFERENCES ARTICULOS (id_articulo)
)

GO

CREATE PROCEDURE SP_DELETE
	@id int
AS
	BEGIN
		UPDATE ARTICULOS SET estado = 0 WHERE id_articulo = @id
	END

GO

CREATE PROCEDURE SP_SAVE
	@descripcion varchar(50)
AS
	BEGIN
		INSERT INTO ARTICULOS(descripcion, estado) VALUES (@descripcion, 1) 
	END

GO

CREATE PROCEDURE SP_GET_ALL
AS
	BEGIN
		SELECT id_articulo, descripcion, estado FROM ARTICULOS
	END

GO

CREATE PROCEDURE SP_GET_BY_ID
	@id int
AS
	BEGIN
		SELECT id_articulo, descripcion, estado FROM ARTICULOS WHERE id_articulo = @id
	END

GO

CREATE PROCEDURE SP_EDIT
	@id int, @descripcion varchar(50)
AS
	BEGIN
		UPDATE ARTICULOS SET descripcion = @descripcion WHERE id_articulo = @id
	END

GO

CREATE PROCEDURE SP_SAVE_TP
	@descripcion varchar(50)
AS
	BEGIN
		INSERT INTO TIPOS_PAGOS(descripcion) VALUES (@descripcion) 
	END

GO

CREATE PROCEDURE SP_GET_ALL_TP
AS
	BEGIN
		SELECT id_tipo_pago, descripcion FROM TIPOS_PAGOS
	END

GO

CREATE PROCEDURE SP_GET_BY_ID_TP
	@id int
AS
	BEGIN
		SELECT id_tipo_pago, descripcion FROM TIPOS_PAGOS WHERE id_tipo_pago = @id
	END

GO

CREATE PROCEDURE SP_INSERT_FACTURA
	@id_tipo_pago int,
	@cliente varchar(50),
	@id_factura int output
AS
	BEGIN 
		INSERT INTO FACTURAS(fecha_factura, id_tipo_pago, cliente) values (GETDATE(), @id_tipo_pago, @cliente)
		SET @id_factura = SCOPE_IDENTITY()
	END

GO

CREATE PROCEDURE SP_INSERT_DETALLE_FACTURA
	@id_factura int,
	@id_articulo int, 
	@cantidad int, 
	@precio_unidad float
AS
	BEGIN
		INSERT INTO DETALLES_FACTURAS(id_factura, id_articulo, cantidad, precio_unidad) VALUES(@id_factura, @id_articulo, @cantidad, @precio_unidad)
	END

GO

CREATE PROCEDURE SP_GET_DETALLE_FACTURA
	@id int
AS
	BEGIN
		SELECT DF.id_detalle_factura, A.id_articulo, A.descripcion, cantidad, precio_unidad
		FROM DETALLES_FACTURAS DF 
		JOIN ARTICULOS A ON A.id_articulo = df.id_articulo
		WHERE id_factura = @id
	END

GO

CREATE PROCEDURE SP_GET_FACTURA
	@id int
AS
	BEGIN
		SELECT F.fecha_factura, TP.id_tipo_pago, TP.descripcion, cliente
		FROM FACTURAS F 
		JOIN TIPOS_PAGOS TP ON F.id_tipo_pago = TP.id_tipo_pago
		WHERE F.id_factura = @id
	END

GO

CREATE PROCEDURE SP_GET_ALL_FACTURAS
AS
	BEGIN 
		SELECT * FROM FACTURAS
	END	

GO

--INSERTSS

INSERT INTO TIPOS_PAGOS(descripcion) values ('CREDITO')
INSERT INTO TIPOS_PAGOS(descripcion) values ('EFECTIVO')

INSERT INTO ARTICULOS(descripcion, estado) values ('MILANESA', 1)
INSERT INTO ARTICULOS(descripcion, estado) values ('HAMBURGUESA', 1)
INSERT INTO ARTICULOS(descripcion, estado) values ('MEDALLON.POLLO', 1)
