-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Nivel1_C2_Agregar]
	-- Add the parameters for the stored procedure here
	@desc nvarchar(50),
	@desc2 nvarchar(50),
	@tabla nvarchar(50),
	@descValor nvarchar(300),
	@descValor2 nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @QRYCOMMAND nvarchar(1000);
	set @QRYCOMMAND  = 'INSERT INTO ' + ltrim(rtrim(@tabla)) + '(' + ltrim(rtrim(@desc)) + ',' + ltrim(rtrim(@desc2)) + ',ACTIVO)' ;
	set @QRYCOMMAND  = @QRYCOMMAND + ' VALUES(ltrim(rtrim(@descValor)),ltrim(rtrim(@descValor2)),1)';
	PRINT @QRYCOMMAND
	EXECUTE sp_executesql @QRYCOMMAND, N'@descValor nvarchar(300),@descValor2 nvarchar(50)', @descValor, @descValor2
END