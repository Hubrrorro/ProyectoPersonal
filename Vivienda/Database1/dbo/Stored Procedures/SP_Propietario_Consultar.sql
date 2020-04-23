-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Propietario_Consultar]
	-- Add the parameters for the stored procedure here
	@Id_Vivienda int = -1,
	@Id_Propietario int = -1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select ISNULL(p.Id_Propietario,0) Id_Propietario,ISNULL(p.Nombre,'') Nombre, ISNULL(p.ApePaterno,'') ApePaterno, ISNULL(p.ApeMaterno,'') ApeMaterno,
	ISNULL(p.Correo,'') Correo,ISNULL(p.Tel1,'') Tel1, ISNULL(p.Tel2,'') Tel2,ISNULL(p.Cel1,'') Cel1,ISNULL(p.Cel2,'') Cel2,
	CAST(ISNULL(p.Activo,1) as int) as Activo, v.Id_Vivienda,
	c.Condominio, upper(c.Clave) +'-' + upper(tv.Clave) +'-'+ NumExt as Vivienda, NumExt, NumInt
	from 
	 Tbl_Vivienda v 
	inner join Tbl_TipoVivienda tv on tv.Id_TipoVivienda = v.Id_TipoVivienda
	inner join Tbl_Condominio c on c.Id_Condominio = v.Id_Condominio
	left join Tbl_Propietario p on p.Id_Vivienda = v.Id_Vivienda 
	where (@Id_Vivienda = -1 or v.Id_Vivienda = @Id_Vivienda)
	and (@Id_Propietario = -1 or p.Id_Propietario = @Id_Propietario)
END