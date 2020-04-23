-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Nivel1_Consultar]
	-- Add the parameters for the stored procedure here
	@id nvarchar(50),
	@desc nvarchar(50),
	@tabla nvarchar(50),
	@idValor int = -1,
	@descValor nvarchar(300) = '',
	@estatusValor int= -1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @QRYCOMMAND nvarchar(1000);
	set @QRYCOMMAND  = 'select ' + ltrim(rtrim(@id)) + ' as ID, ';
	set @QRYCOMMAND  = @QRYCOMMAND + ltrim(rtrim(@desc)) + ' AS DESCRIPCION,CAST(ACTIVO AS INT) AS ACTIVO from ';
	set @QRYCOMMAND  = @QRYCOMMAND + ltrim(rtrim(@tabla));
	set @QRYCOMMAND  = @QRYCOMMAND + ' where 1=1 '
	if (@idValor != -1)
	begin
		set @QRYCOMMAND  = @QRYCOMMAND + ' and ' + ltrim(rtrim(@id)) + '= @idValor';
	end
	if (Ltrim(rtrim(@descValor)) != '')
	begin
		set @QRYCOMMAND  = @QRYCOMMAND + ' and ' + UPPER(ltrim(rtrim(@desc))) + ' LIKE ''%'' + UPPER(ltrim(rtrim(@descValor))) + ''%''';
	end
	if (Ltrim(rtrim(@estatusValor)) != -1)
	begin
		set @QRYCOMMAND  = @QRYCOMMAND + ' and Activo= @estatusValor';
	end
	print @QRYCOMMAND;
	EXECUTE sp_executesql @QRYCOMMAND, N'@idValor int ,@descValor nvarchar(300), @estatusValor int', 
	@idValor ,@descValor, @estatusValor 
END