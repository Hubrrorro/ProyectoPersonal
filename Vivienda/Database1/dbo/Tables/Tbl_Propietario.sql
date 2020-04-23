CREATE TABLE [dbo].[Tbl_Propietario] (
    [Id_Propietario] INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]         NVARCHAR (50)  NOT NULL,
    [ApePaterno]     NVARCHAR (50)  NOT NULL,
    [ApeMaterno]     NVARCHAR (50)  NOT NULL,
    [Correo]         NVARCHAR (200) NULL,
    [Tel1]           NVARCHAR (10)  NULL,
    [Tel2]           NVARCHAR (10)  NULL,
    [Cel1]           NVARCHAR (10)  NULL,
    [Cel2]           NVARCHAR (10)  NULL,
    [Activo]         BIT            DEFAULT ((1)) NOT NULL,
    [Id_Vivienda]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Propietario] ASC),
    FOREIGN KEY ([Id_Vivienda]) REFERENCES [dbo].[Tbl_Vivienda] ([Id_Vivienda])
);

