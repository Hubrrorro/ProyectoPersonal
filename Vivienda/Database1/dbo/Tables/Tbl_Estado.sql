CREATE TABLE [dbo].[Tbl_Estado] (
    [Id_Estado] INT           NOT NULL,
    [Estado]    NVARCHAR (50) NOT NULL,
    [Activo]    BIT           NOT NULL,
    CONSTRAINT [PK_Tbl_Estado] PRIMARY KEY CLUSTERED ([Id_Estado] ASC)
);

