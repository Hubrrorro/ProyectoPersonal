-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Nivel1_C2_Modificar]
	-- Add the parameters for the stored procedure here
	@id nvarchar(50),
	@desc nvarchar(50),
	@desc2 nvarchar(50),
	@tabla nvarchar(50),
	@idValor int,
	@descValor nvarchar(300),
	@descValor2 nvarchar(50),
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
	set @QRYCOMMAND  = @QRYCOMMAND + ltrim(rtrim(@desc2)) + '= ltrim(rtrim(@descValor2)), ';
	set @QRYCOMMAND  = @QRYCOMMAND + ' ACTIVO = @estatusValor ';
	set @QRYCOMMAND  = @QRYCOMMAND + ' where '+ ltrim(rtrim(@id)) + '= @idValor ';
	
	EXECUTE sp_executesql @QRYCOMMAND, N'@idValor int ,@descValor nvarchar(300),@descValor2 nvarchar(50), @estatusValor int', 
	@idValor ,@descValor,@descValor2, @estatusValor 
END