-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Condominio_Consulta]
	-- Add the parameters for the stored procedure here
	@Condominio nvarchar(50) = '',
	@Id_Condominio int= 0,
	@Activo int =- 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @QRYCOMMAND NVARCHAR(1000);
    SET @QRYCOMMAND = N'Select Id_Condominio,Condominio,Clave,Colonia,Id_Estado,CP,Municipio,CAST(Activo AS INT) AS ACTIVO from Tbl_Condominio where 1= 1'
	IF (@Condominio != '')
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND Condominio = ''%'' + @Condominio +''%''';
	END
	IF (@Id_Condominio != -1)
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND Id_Condominio = @Id_Condominio  ';
	END
	IF (@Activo != -1)
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND Activo = @Activo ';
	END
	print @QRYCOMMAND
	EXECUTE sp_executesql @QRYCOMMAND, N'@Condominio nvarchar(50),@Id_Condominio int,@Activo int'
									, @Condominio,@Id_Condominio,@Activo

									
END