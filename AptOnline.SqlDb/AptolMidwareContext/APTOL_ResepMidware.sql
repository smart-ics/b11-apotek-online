CREATE TABLE [dbo].[APTOL_ResepMidware]
(
    --  metadata
    ResepMidwareId	VARCHAR(13) NOT NULL CONSTRAINT APTOL_ResepMidware_ResepMidwareId DEFAULT '',
    ResepMidwareDate DATETIME NOT NULL CONSTRAINT APTOL_ResepMidware_ResepMidwareDate DEFAULT '3000-01-01',
    BridgeState VARCHAR(10) NOT NULL CONSTRAINT APTOL_ResepMidware_ResepMidwareState DEFAULT '', -- CREATED, SYNCED, UPLOADED
    CreateTimestamp DATETIME NOT NULL CONSTRAINT APTOL_ResepMidware_CreateTimestamp DEFAULT '3000-01-01',
    SyncTimestamp DATETIME NOT NULL CONSTRAINT APTOL_ResepMidware_SyncTimestamp DEFAULT '3000-01-01',
    UploadTimestamp DATETIME NOT NULL CONSTRAINT APTOL_ResepMidware_UploadTimestamp DEFAULT '3000-01-01',

    -- Billing-EMR Related
    ChartId VARCHAR(14) NOT NULL CONSTRAINT APTOL_ResepMidware_ChartId DEFAULT '',
    ResepRsId VARCHAR(10) NOT NULL CONSTRAINT APTOL_ResepMidware_ResepRsId DEFAULT '',
    RegId VARCHAR(10) NOT NULL CONSTRAINT APTOL_ResepMidware_RegId DEFAULT '',
    PasienId VARCHAR(15) NOT NULL CONSTRAINT APTOL_ResepMidware_PasienId DEFAULT '',
    PasienName VARCHAR(60) NOT NULL CONSTRAINT APTOL_ResepMidware_PasienName DEFAULT '',

    -- SEP Related
    SepId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_SepId DEFAULT '',
    SepDate DATETIME NOT NULL CONSTRAINT APTOL_ResepMidware_SepDate DEFAULT '3000-01-01',
    NoPeserta VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_NoPeserta DEFAULT '',
    FaskesId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_FaskesId DEFAULT '',
    FaskesName VARCHAR(100) NOT NULL CONSTRAINT APTOL_ResepMidware_FaskesAsal DEFAULT '',
    PoliBpjsId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_PoliBpjsId DEFAULT '',
    PoliBpjsNama VARCHAR(100) NOT NULL CONSTRAINT APTOL_ResepMidware_PoliBpjsNama DEFAULT '',
    DokterId VARCHAR(20) NOT NULL CONSTRAINT APTOL_ResepMidware_DokterId DEFAULT '',
    DokterNama VARCHAR(100) NOT NULL CONSTRAINT APTOL_ResepMidware_DokterNama DEFAULT '',

    -- APTOL Related
    ReffId VARCHAR(30) NOT NULL CONSTRAINT APTOL_ResepMidware_ReffId DEFAULT '',
    JenisObatId VARCHAR(2) NOT NULL CONSTRAINT APTOL_ResepMidware_JenisObatId DEFAULT '',
    Iterasi INT NOT NULL CONSTRAINT APTOL_ResepMidware_Iterasi DEFAULT 0,

    CONSTRAINT PK_APTOL_ResepMidware_ResepMidwareId PRIMARY KEY CLUSTERED(ResepMidwareId)
)
