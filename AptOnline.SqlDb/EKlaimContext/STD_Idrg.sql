CREATE TABLE STD_Idrg(
    IdrgId VARCHAR(10) NOT NULL CONSTRAINT DF_STD_Idrg_IdrgId DEFAULT(''),
    Code2 VARCHAR(10) NULL CONSTRAINT DF_STD_Idrg_Code2 DEFAULT(''),
    IdrgName VARCHAR(255) NOT NULL CONSTRAINT DF_STD_Idrg_IdrgName DEFAULT(''),
    StdSystem VARCHAR(20) NOT NULL CONSTRAINT DF_STD_Idrg_StdSystem DEFAULT(''),
    ValidCode BIT NOT NULL CONSTRAINT DF_STD_Idrg_ValidCode DEFAULT(0),
    Accpdx VARCHAR(1) NOT NULL CONSTRAINT DF_STD_Idrg_Accpdx DEFAULT(''),
    Asterisk BIT NOT NULL CONSTRAINT DF_STD_Idrg_Asterisk DEFAULT(0),
    Im BIT NOT NULL CONSTRAINT DF_STD_Idrg_Im DEFAULT(0),
    
    CONSTRAINT PK_STD_Idrg PRIMARY KEY CLUSTERED(IdrgId, Im)
)
GO

-- script-update data doble/salah dari kem-kes
-- UPDATE STD_Idrg
-- SET IdrgId = '56.870'
-- WHERE Code2 = '56870';
-- 
-- UPDATE STD_Idrg
-- SET IdrgId = 'S22.010'
-- WHERE Code2 = 'S22.010';
-- 
-- UPDATE STD_Idrg
-- SET IdrgId = '22.200'
-- WHERE Code2 = '22200';
-- 
-- UPDATE STD_Idrg
-- SET IdrgId = '22.20'
-- WHERE Code2 = '2220'