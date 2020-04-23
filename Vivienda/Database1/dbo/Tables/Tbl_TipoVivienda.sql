CREATE TABLE [dbo].[Tbl_TipoVivienda] (
    [Id_TipoVivienda] INT           IDENTITY (1, 1) NOT NULL,
    [TipoVivienda]    NVARCHAR (50) NOT NULL,
    [Clave]           NVARCHAR (5)  NOT NULL,
    [Activo]          BIT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id_TipoVivienda] ASC)
);

