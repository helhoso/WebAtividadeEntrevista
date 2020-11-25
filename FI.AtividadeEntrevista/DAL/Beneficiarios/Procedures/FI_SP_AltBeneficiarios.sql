CREATE PROC FI_SP_AltBeneficiarios
	@Id            BIGINT ,
    @NOME          VARCHAR (50) ,
    /* @CPF           VARCHAR (14), */
	@IdCliente     BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS
	SET 
		NOME = @NOME, 
		/* CPf = @CPf, */
		IdCliente = @IdCliente 
	WHERE Id = @Id
END