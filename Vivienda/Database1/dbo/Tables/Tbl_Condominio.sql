CREATE TABLE [dbo].[Tbl_Condominio] (
    [Id_Condominio] INT           IDENTITY (1, 1) NOT NULL,
    [Condominio]    NVARCHAR (50) NOT NULL,
    [Clave]         NVARCHAR (5)  NOT NULL,
    [Colonia]       NVARCHAR (50) NOT NULL,
    [Id_Estado]     INT           NOT NULL,
    [CP]            INT           NOT NULL,
    [Municipio]     NVARCHAR (50) NOT NULL,
    [Activo]        BIT           DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([Id_Condominio] ASC),
    FOREIGN KEY ([Id_Estado]) REFERENCES [dbo].[Tbl_Estado] ([Id_Estado])
);

