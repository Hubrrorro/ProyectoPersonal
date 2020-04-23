-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Condominio_Agregar
	-- Add the parameters for the stored procedure here
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

	insert into Tbl_Condominio(Condominio,Clave, Colonia, Id_Estado, CP, Municipio)
    values(@Condominio,@Clave, @Colonia, @Estado, @CP, @Municipio)
END