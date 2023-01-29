/****** Script do comando SelectTopNRows de SSMS  ******/
CREATE TABLE Cargo(
    CargoId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    Descricao VARCHAR(255) NOT NULL 
);

CREATE TABLE Funcionario(
    FuncionarioId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    NomeDoFuncionario VARCHAR(255) NOT NULL,
    Cpf VARCHAR(11) NOT NULL,
    NascimentoFuncionario DATE NOT NULL,
    DataDeAdmissao DATE NOT NULL,
    CelularFuncionario VARCHAR(11) NOT NULL,
    EmailFuncionario VARCHAR(55) NOT NULL,
    CargoId INT FOREIGN KEY  REFERENCES Cargo(CargoId)

);

CREATE TABLE Liderancas(
    LiderancaId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    FuncionarioId INT FOREIGN KEY REFERENCES Funcionario(FuncionarioId),
    DescricaoEquipe VARCHAR(155) NOT NULL
);

CREATE TABLE Equipes(
    EquipeId INT PRIMARY KEY NOT NULL IDENTITY(1,1),
    LiderancaId INT FOREIGN KEY REFERENCES Liderancas(LiderancaId),
    FuncionarioId INT FOREIGN KEY REFERENCES Funcionario(FuncionarioId)
);

CREATE TABLE Ponto(
    PontoId BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1),
    DataHorarioPonto DATETIME NOT NULL,
    Justificativa VARCHAR(255),
    FuncionarioId INT FOREIGN KEY REFERENCES Funcionario(FuncionarioId)
);