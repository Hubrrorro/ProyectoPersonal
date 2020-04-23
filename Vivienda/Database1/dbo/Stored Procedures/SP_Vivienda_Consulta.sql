-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Vivienda_Consulta]
	-- Add the parameters for the stored procedure here
	@Vivienda nvarchar(50) = '',
	@Id_Vivienda int= -1,
	@Activo int =- 1,
	@Id_Condominio int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @QRYCOMMAND NVARCHAR(1000);
    SET @QRYCOMMAND = N'select v.Id_Vivienda,upper(c.Clave) +''-'' + upper(tv.Clave) +''-'' + NumExt as Vivienda,v.Id_TipoVivienda,Calle,Lote,NumExt,v.Id_Condominio,c.Condominio,
	Cast(v.Activo as int) as Activo, Cast(ISNULL(p.Activo, 0) as int) as ActivoPropietario
from Tbl_Vivienda v inner join Tbl_Condominio c on c.Id_Condominio = v.Id_Condominio 
inner join Tbl_TipoVivienda tv on tv.Id_TipoVivienda = v.Id_TipoVivienda
left join Tbl_Propietario p on v.Id_Vivienda = p.Id_Vivienda
where 1=1 '
	IF (@Vivienda != '')
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND Upper(Vivienda) like ''%'' + Upper(@Vivienda) +''%''  ';
	END
	IF (@Id_Condominio != -1)
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND  v.Id_Condominio= @Id_Condominio ';
	END
	IF (@Id_Vivienda != -1)
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND v.Id_Vivienda =   @Id_Vivienda ';
	END
	IF (@Activo != -1)
	BEGIN
		SET @QRYCOMMAND =@QRYCOMMAND + ' AND v.Activo = @Activo ';
	END
	print @QRYCOMMAND;
	EXECUTE sp_executesql @QRYCOMMAND, N'@Vivienda nvarchar(50),	@Id_Vivienda int,@Activo int, @Id_Condominio int'
									,@Vivienda,@Id_Vivienda,@Activo,@Id_Condominio
									
						 
END