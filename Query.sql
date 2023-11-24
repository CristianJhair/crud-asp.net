CREATE DATABASE BDEmpresa

USE BDEmpresa

CREATE TABLE Empresa (
codigoEmpresa int identity(1,1) primary key,
ruc varchar(11),
razonSocial varchar(50),
fechaFundacion date,
estado bit
)

CREATE TABLE Cargo (
codigoCargo int identity(1,1) primary key,
descripcion varchar(100),
fechaCreacion date,
flagModificado bit
)

CREATE TABLE Personal (
codigoEmpleado int identity(1,1) primary key,
numeroDocumento varchar(8),
nombres varchar(50),
apellidoPaterno varchar(50),
apellidoMaterno varchar(50),
fechaNacimiento date,
fechaIngreso date,
codigoEmpresa INT FOREIGN KEY REFERENCES Empresa(codigoEmpresa),
codigoCargo INT FOREIGN KEY REFERENCES Cargo(codigoCargo)
)

ALTER TABLE Empresa
ADD CONSTRAINT UQ_ruc UNIQUE (ruc);

ALTER TABLE Personal
ADD CONSTRAINT UQ_numeroDocumento UNIQUE (numeroDocumento);

--INSERTS

--1 empresa
INSERT INTO Empresa (ruc, razonSocial, fechaFundacion, estado)
VALUES ('12345678901', 'Bembos', '2022-01-01', 1);

--3 cargos
INSERT INTO Cargo (descripcion, fechaCreacion, flagModificado)
VALUES ('Cientifico de datos', '2023-01-01', 0);
INSERT INTO Cargo (descripcion, fechaCreacion, flagModificado)
VALUES ('Desarrollador', '2023-02-01', 0);
INSERT INTO Cargo (descripcion, fechaCreacion, flagModificado)
VALUES ('Arquitecto de Software', '2023-03-01', 0);

--5 personal
INSERT INTO Personal (numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, codigoEmpresa, codigoCargo)
VALUES ('12345678', 'Juan', 'Lopez', 'Perez', '1990-01-01', '2023-01-01', 1, 1);
INSERT INTO Personal (numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, codigoEmpresa, codigoCargo)
VALUES ('56785678', 'Alberto', 'Duarte', 'Ramirez', '1995-01-01', '2023-03-01', 1, 1);
INSERT INTO Personal (numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, codigoEmpresa, codigoCargo)
VALUES ('11112222', 'Daniela', 'Nunez', 'Lozada', '1998-05-01', '2023-05-01', 1, 2);
INSERT INTO Personal (numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, codigoEmpresa, codigoCargo)
VALUES ('22223333', 'Sonia', 'Villa', 'Gutierrez', '1989-04-01', '2023-06-01', 1, 2);
INSERT INTO Personal (numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, fechaNacimiento, fechaIngreso, codigoEmpresa, codigoCargo)
VALUES ('44445555', 'Samuel', 'Sosa', 'Avila', '1990-09-05', '2023-05-01', 1, 3);

--Ejercicio e)
CREATE FUNCTION dbo.ObtenerEdad (@fechaNacimiento DATE)
RETURNS INT
AS
BEGIN
    DECLARE @edad INT

    SELECT @edad = DATEDIFF(YEAR, @fechaNacimiento, GETDATE())

    IF DATEADD(YEAR, @edad, @fechaNacimiento) > GETDATE()
    BEGIN
        SET @edad = @edad - 1
    END

    RETURN @edad
END;

--Prueba
SELECT dbo.ObtenerEdad('1990-01-01') AS Edad;

