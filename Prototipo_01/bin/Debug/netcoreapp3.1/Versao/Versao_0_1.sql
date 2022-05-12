--VERSAO 0.1
IF NOT EXISTS (SELECT 1 FROM Sys.Objects WHERE OBJECT_ID = OBJECT_ID('[dbo].[Versao]')) 
BEGIN
	CREATE TABLE [dbo].[Versao] (
		Versao VARCHAR(10) NOT NULL DEFAULT '',
		AtualizandoPara VARCHAR(10) NOT NULL DEFAULT ''
	);
END
GO

IF NOT EXISTS(SELECT Versao FROM [dbo].[Versao])
	INSERT INTO [dbo].[Versao] (Versao, AtualizandoPara) VALUES ('0.0', '0.1')
GO

--Início do script

IF NOT EXISTS (SELECT 1 FROM Sys.Objects WHERE OBJECT_ID = OBJECT_ID('[dbo].[Tecnicos]'))
BEGIN
	CREATE TABLE [dbo].[Tecnicos](
		Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
		Nome VARCHAR(100) NOT NULL DEFAULT ''
		PRIMARY KEY (Id)
	);
END
GO

IF NOT EXISTS (SELECT 1 FROM Sys.Objects WHERE OBJECT_ID = OBJECT_ID('[dbo].[Chamados]'))
BEGIN 
	CREATE TABLE [dbo].[Chamados](
		Id UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
		Descricao VARCHAR(100) NOT NULL DEFAULT '',
		Status VARCHAR(100) NOT NULL,
		DataCriacao DATETIME NOT NULL DEFAULT GETDATE(),
		PRIMARY KEY(Id)
	)
END
GO

IF NOT EXISTS(SELECT 1 FROM Sys.Objects WHERE OBJECT_ID = OBJECT_ID('[dbo].[Atribuicoes]'))
BEGIN
	CREATE TABLE [dbo].[Atribuicoes](
		IdTecnico UNIQUEIDENTIFIER NOT NULL,
		IdChamado UNIQUEIDENTIFIER NOT NULL,
		DataInicio DATETIME NOT NULL DEFAULT GETDATE(),
		DataFim DATETIME NULL,
		Ativa BIT NOT NULL DEFAULT 0,
		FOREIGN KEY(IdTecnico) REFERENCES Tecnicos (Id),
		FOREIGN KEY (IdChamado) REFERENCES Chamados (Id),
		CONSTRAINT PK_Atribuicoes PRIMARY KEY (IdTecnico, IdChamado)
	);
END
GO

IF NOT EXISTS(SELECT 1 FROM SysColumns WHERE Name = 'Detalhes' AND OBJECT_NAME(Id) = 'Chamados')
	ALTER TABLE Chamados ADD Detalhes VARCHAR(Max);
GO
--Fim do script

IF EXISTS (SELECT 1 FROM [dbo].[Versao] WHERE Versao = '0.0' AND AtualizandoPara = '0.1')
	UPDATE [dbo].[Versao] SET Versao = '0.1', AtualizandoPara = '';
GO