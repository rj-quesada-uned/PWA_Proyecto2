create database PAW_Proyecto2;
use PAW_Proyecto2;

CREATE TABLE Aviones (
    Id INT PRIMARY KEY IDENTITY(1,1),
	MarcaId INT,
    ModeloId INT,
    NumeroSerie VARCHAR(50) UNIQUE,
    NombreFantasia VARCHAR(100),
    Dimensiones VARCHAR(50),
    DistanciaMaximaCombustible FLOAT,
    FechaIngreso DATETIME,
    TecnicoIngreso VARCHAR(100),
    CantidadExistencia INT,
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
    Id INT PRIMARY KEY IDENTITY(1,1),
    NumeroDespegue VARCHAR(20) UNIQUE,
    FechaHoraInicia DATETIME,
    TecnicoEncargado VARCHAR(100),
    NombreMision VARCHAR(100)
);

CREATE TABLE AvionDespegue (
    AvionId INT,
    DespegueId INT,
    PilotoEncargado VARCHAR(100),
    FOREIGN KEY (AvionId) REFERENCES Aviones(Id),
    FOREIGN KEY (DespegueId) REFERENCES Despegue(Id)
);

CREATE TABLE Aterrizaje (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DespegueId INT,
    FechaHoraRetorno DATETIME,
    AvionPerdido BIT,
    PerdidasHumanas BIT,
    NecesitaRescate BIT,
    FOREIGN KEY (DespegueId) REFERENCES Despegue(Id)
);