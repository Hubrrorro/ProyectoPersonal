CREATE TABLE [dbo].[Tbl_Vivienda] (
    [Id_Vivienda]     INT           IDENTITY (1, 1) NOT NULL,
    [Vivienda]        NVARCHAR (50) NOT NULL,
    [Id_TipoVivienda] INT           NOT NULL,
    [Calle]           NVARCHAR (50) NOT NULL,
    [Lote]            NVARCHAR (5)  NOT NULL,
    [NumExt]          NVARCHAR (5)  NOT NULL,
    [NumInt]          NVARCHAR (5)  NULL,
    [Id_Condominio]   INT           NOT NULL,
    [Activo]          BIT           DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Vivienda] ASC),
    FOREIGN KEY ([Id_Condominio]) REFERENCES [dbo].[Tbl_Condominio] ([Id_Condominio]),
    FOREIGN KEY ([Id_TipoVivienda]) REFERENCES [dbo].[Tbl_TipoVivienda] ([Id_TipoVivienda])
);

