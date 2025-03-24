CREATE TABLE [dbo].[APTOL_ResepMidware]
(
    ResepMidwareId	VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_ResepMidwareId DEFAULT '',  
    ReffId VARCHAR(30) NOT NULL CONSTRAINT APTOL_ResepMidware_ReffId DEFAULT '', 
    ResepMidwareDate DateTime NOT NULL CONSTRAINT APTOL_ResepMidware_ResepMidwareDate DEFAULT '3000-01-01',
    EntryDate DateTime NOT NULL CONSTRAINT APTOL_ResepMidware_EntryDate DEFAULT '3000-01-01',

    SepId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_SepId DEFAULT '',
    SepDate DATETIME NOT NULL CONSTRAINT APTOL_ResepMidware_SepDate DEFAULT '3000-01-01',
    NoPesertaSep VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_NoPesertaSep DEFAULT '',
    FaskesId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_FaskesId DEFAULT '',
    FaskesName VARCHAR(100) NOT NULL CONSTRAINT APTOL_ResepMidware_FaskesAsal DEFAULT '',
    PoliBpjsId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_PoliBpjsId DEFAULT '',
    PoliBpjsNama VARCHAR(100) NOT NULL CONSTRAINT APTOL_ResepMidware_PoliBpjsNama DEFAULT '',
    NoPeserta VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_NoPeserta DEFAULT '',

    JenisObatId VARCHAR(2) NOT NULL CONSTRAINT APTOL_ResepMidware_JenisObatId DEFAULT '',
    DokterId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_DokterId DEFAULT '',
    DokterNama VARCHAR(100) NOT NULL CONSTRAINT APTOL_ResepMidware_DokterNama DEFAULT '',
    Iterasi INT NOT NULL CONSTRAINT APTOL_ResepMidware_Iterasi DEFAULT 0,

    CONSTRAINT PK_APTOL_ResepMidware_ResepMidwareId PRIMARY KEY CLUSTERED(ResepMidwareId)
)
