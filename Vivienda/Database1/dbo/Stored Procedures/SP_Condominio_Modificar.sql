-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Condominio_Modificar
	-- Add the parameters for the stored procedure here
	@Id_Condominio int,
	@Condominio nvarchar(50),
	@Clave nvarchar(5),
	@Colonia nvarchar(50),
	@Estado int,
	@CP int,
	@Municipio nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	update Tbl_Condominio
	set  Condominio = @Condominio, Clave = @Clave,
	Colonia= @Colonia, Id_Estado = @Estado, CP= @CP,
	Municipio = @Municipio
	where Id_Condominio = @Id_Condominio
END