-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Propietario_Modificar]
	-- Add the parameters for the stored procedure here
	@Nombre nvarchar(50),
	@ApePaterno nvarchar(50),
	@ApeMaterno nvarchar(50),
	@Correo nvarchar(200),
	@Tel1 nvarchar(10),
	@Tel2 nvarchar(10),
	@Cel1 nvarchar(10),
	@Cel2 nvarchar(10),
	@Activo int,
	@Id_Vivienda int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @existe int

	select @existe = count(*) 
	from Tbl_Propietario where Id_Vivienda = @Id_Vivienda;
	if (@existe > 0)
	begin
		update Tbl_Propietario
		set Nombre = @Nombre, ApePaterno = @ApePaterno, ApeMaterno = @ApeMaterno, Correo = @Correo, Tel1 = @Tel1, Tel2 = @Tel2, 
		Cel1 = @Cel1, Cel2= @Cel2, Activo = @Activo
		where Id_Vivienda = @Id_Vivienda;
	end
    else
	begin
		insert into Tbl_Propietario(Nombre,ApePaterno, ApeMaterno,Correo,Tel1, Tel2, Cel1, Cel2, Activo, Id_Vivienda)
		values (@Nombre,@ApePaterno, @ApeMaterno,@Correo,@Tel1, @Tel2, @Cel1, @Cel2, 1, @Id_Vivienda)
	end

END