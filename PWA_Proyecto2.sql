create database PAW_Proyecto2;
use PAW_Proyecto2;

CREATE TABLE MarcasAviones (
    IdMarca INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100)
);

CREATE TABLE ModelosAviones (
    ModeloId INT PRIMARY KEY IDENTITY(1,1),
    MarcaId INT,
    Nombre VARCHAR(100)
    FOREIGN KEY (MarcaId) REFERENCES MarcasAviones(IdMarca)
);

INSERT INTO MarcasAviones (Nombre)
VALUES
	('Boeing'),
	('Airbus'),
	('Embraer'),
	('Bombardier'),
	('Lockheed Martin'),
	('McDonnell Douglas'),
	('ATR'),
	('Fokker'),
	('Saab'),
	('Antonov');

INSERT INTO ModelosAviones (MarcaId, Nombre)
VALUES 
    (1, '737'),
    (1, '747'),
    (1, '777'),
    (2, 'A320'),
    (2, 'A330'),
    (2, 'A380'),
    (3, 'E190'),
    (3, 'E195'),
    (3, 'E175'),
    (4, 'CRJ900'),
    (4, 'Global 7500'),
    (4, 'Challenger 350'),
	(5, 'C-130 Hercules'),
	(5, 'F-35 Lightning II'),
	(5, 'P-3 Orion'),
	(6, 'MD-11'),
	(6, 'MD-80'),
	(6, 'AV-8B Harrier II'),
	(7, 'ATR 72'),
	(7, 'ATR 42'),
	(7, 'ATR 42-600'),
	(8, 'Fokker 100'),
	(8, 'Fokker 70'),
	(8, 'Fokker 50'),
	(9, 'Saab 340'),
	(9, 'Saab 2000'),
	(9, 'Saab JAS 39 Gripen'),
	(10, 'An-225 Mriya'),
	(10, 'An-124 Ruslan'),
	(10, 'An-22 Antei');

CREATE TABLE Aviones (
    AvionId INT PRIMARY KEY IDENTITY(1,1),
	MarcaId INT,
    ModeloId INT,
    NumeroSerie VARCHAR(50) UNIQUE,
    NombreFantasia VARCHAR(100),
    Dimensiones VARCHAR(50),
    DistanciaMaximaCombustible FLOAT,
    FechaIngreso DATETIME,
    TecnicoIngreso VARCHAR(100),
    PilotoEncargado VARCHAR(100),
    CantidadExistencia INT,
	FOREIGN KEY (MarcaId) REFERENCES MarcasAviones(IdMarca),
    FOREIGN KEY (ModeloId) REFERENCES ModelosAviones(ModeloId)
);

CREATE TABLE RetiroAviones (
    RetiroId INT PRIMARY KEY IDENTITY(1,1),
    NumeroSerie VARCHAR(50),
    TecnicoRetiro VARCHAR(100),
    FechaRetiro DATETIME,
    MotivoRetiro VARCHAR(255),
    FOREIGN KEY (NumeroSerie) REFERENCES Aviones(NumeroSerie)
);

CREATE TABLE Despegue (
    NumeroDespegue VARCHAR(20) PRIMARY KEY,
    FechaHoraInicia DATETIME,
    TecnicoEncargado VARCHAR(100),
    NombreMision VARCHAR(100),
    NombrePiloto VARCHAR(255),
	AvionId int
);

CREATE TABLE Aterrizaje (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DespegueId VARCHAR(20),
    FechaHoraRetorno DATETIME,
    AvionPerdido BIT,
    PerdidasHumanas BIT,
    NecesitaRescate BIT,
    FOREIGN KEY (DespegueId) REFERENCES Despegue(NumeroDespegue)
);