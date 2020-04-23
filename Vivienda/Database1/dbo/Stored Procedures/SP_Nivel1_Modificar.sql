-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Nivel1_Modificar]
	-- Add the parameters for the stored procedure here
	@id nvarchar(50),
	@desc nvarchar(50),
	@tabla nvarchar(50),
	@idValor int,
	@descValor nvarchar(300),
	@estatusValor int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @QRYCOMMAND nvarchar(1000);
	set @QRYCOMMAND  = 'UPDATE ' + ltrim(rtrim(@tabla)) + ' SET ';
	set @QRYCOMMAND  = @QRYCOMMAND + ltrim(rtrim(@desc)) + '= ltrim(rtrim(@descValor)), ';
	set @QRYCOMMAND  = @QRYCOMMAND + ' ACTIVO = @estatusValor ';
	set @QRYCOMMAND  = @QRYCOMMAND + ' where '+ ltrim(rtrim(@id)) + '= @estatusValor ';
	
	EXECUTE sp_executesql @QRYCOMMAND, N'@idValor int ,@descValor nvarchar(300), @estatusValor int', 
	@idValor ,@descValor, @estatusValor 
END