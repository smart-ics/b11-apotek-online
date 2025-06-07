CREATE TABLE [dbo].[APTOL_ResepMidwareItem]
(
    ResepMidwareId	VARCHAR(26) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_ResepMidwareId DEFAULT '',  
    NoUrut INT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_NoUrut DEFAULT 0, 
    IsRacik BIT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_IsRacik DEFAULT 0,
    RacikId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_RacikId DEFAULT '',
    BarangId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_BarangId DEFAULT '',
    BarangName VARCHAR(50) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_BarangName DEFAULT '',
    DphoId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_DphoId DEFAULT '',
    DphoName VARCHAR(50) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_DphoName DEFAULT '',
    Signa1 INT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_Signa1 DEFAULT 0, 
    Signa2 DECIMAL(18,2) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_Signa2 DEFAULT 0, 
    Permintaan INT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_Permintaan DEFAULT 0,
    Jho INT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_Jho DEFAULT 0,
    Jumlah INT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_Jumlah DEFAULT 0,
    Note VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidwareItem_Note DEFAULT '',
    IsUploaded BIT NOT NULL CONSTRAINT APTOL_ResepMidwareItem_IsUploaded DEFAULT 0

    CONSTRAINT PK_APTOL_ResepMidwareItem PRIMARY KEY (ResepMidwareId, NoUrut)
)
GO

CREATE UNIQUE INDEX UQ_APTOL_ResepMidwareItem_BarangId
    ON APTOL_ResepMidwareItem (ResepMidwareId, BarangId)
GO

