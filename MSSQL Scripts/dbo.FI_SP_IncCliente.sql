﻿/*
	20/11/2020
	Hélio Barbosa
	DROP PROCEDURE FI_SP_IncCliente ;

*/

CREATE PROC FI_SP_IncCliente
	@CPF           VARCHAR (11),
    @NOME          VARCHAR (50) ,
    @SOBRENOME     VARCHAR (255),
    @NACIONALIDADE VARCHAR (50) ,
    @CEP           VARCHAR (9)  ,
    @ESTADO        VARCHAR (2)  ,
    @CIDADE        VARCHAR (50) ,
    @LOGRADOURO    VARCHAR (500),
    @EMAIL         VARCHAR (2079)
AS
BEGIN

    IF (SELECT COUNT(*) FROM CLIENTES WHERE CPF=@CPF ) <= 0 
		INSERT INTO CLIENTES 
		(
		CPF,
		NOME,
		SOBRENOME,
		NACIONALIDADE,
		CEP,
		ESTADO,
		CIDADE,
		LOGRADOURO,
		EMAIL
		) 
		VALUES 
		(
		@CPF,
		@NOME,
		@SOBRENOME,
		@NACIONALIDADE,
		@CEP,
		@ESTADO,
		@CIDADE,
		@LOGRADOURO,
		@EMAIL
	 );
END
