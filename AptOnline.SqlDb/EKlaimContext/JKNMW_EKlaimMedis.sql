CREATE TABLE JKNMW_EKlaimMedis(
    EKlaimId VARCHAR(26) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_EKlaimId DEFAULT(''),
    
    AdlSubAcuteScore INT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_AdlSubAcuteScore DEFAULT(0),
    AdlChronicScore INT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_AdlChronicScore DEFAULT(0),

    IcuFlag INT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IcuFlag DEFAULT(0),
    IcuLos INT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IcuDays DEFAULT(0),
    IcuDescription VARCHAR(100) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IcuDescription DEFAULT(''),
    
    Covid19StatusId VARCHAR(1) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_Covid19StatusId DEFAULT(''),
    Covid19StatusName VARCHAR(20) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_Covid19StatusDescription DEFAULT(''),
    
    Covid19TipeNoKartuId VARCHAR(10) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_Covid19TipeNoKartuId DEFAULT(''),
    Covid19TipeNoKartuName VARCHAR(35) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_Covid19TipeNoKartuName DEFAULT(''),
    
    IsPemulasaranJenazah BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsPemulasaranJenazah DEFAULT(0),
    IsKantongJenazah BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsKantongJenazah DEFAULT(0),
    IsPetiJenazah BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsPetiJenazah DEFAULT(0),
    IsPlastikErat BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsPlastikErat DEFAULT(0),
    IsDesinfeksiJenazah BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsDesinfeksiJenazah DEFAULT(0),
    IsMobilJenazah BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsMobillJenazah DEFAULT(0),
    IsDesinfektanMobilJenazah BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsDesinfektanMobilJenazah DEFAULT(0),
    
    IsIsoman BIT NOT NULL CONSTRAINT JKNMW_EKlaimMedis_IsIsoman DEFAULT(0),
    Episodes VARCHAR(30) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_Episodes DEFAULT(''),
    AksesNaat VARCHAR(1) NOT NULL CONSTRAINT JKNMW_EKlaimMedis_AksesNaat DEFAULT(0),
    
    DializerUsageId VARCHAR(1) NOT NULL CONSTRAINT JKNMW_DializerUsageId DEFAULT(''),
    DializerUsageName VARCHAR(20) NOT NULL CONSTRAINT JKNMW_DializerUsageName DEFAULT(''),
    JumKantongDarah INT NOT NULL CONSTRAINT JKNMW_JumKantongDarah DEFAULT(0),
    AlteplaseIndikator BIT NOT NULL CONSTRAINT JKNMW_AlteplaseIndikator DEFAULT(0),

    Sistole INT NOT NULL CONSTRAINT JKNMW_Sistole DEFAULT(0),
    Diastole INT NOT NULL CONSTRAINT JKNMW_Diastole DEFAULT(0),
    BodyWeight DECIMAL(18,2) NOT NULL CONSTRAINT JKNMW_BodyWeight DEFAULT(0),
    
    TbIndikatorId VARCHAR(1) NOT NULL CONSTRAINT JKNMW_TbIndikatorId DEFAULT(''),
    TbIndikatorName VARCHAR(20) NOT NULL CONSTRAINT JKNMW_TbIndikatorName DEFAULT(''),

    CONSTRAINT JKNMW_EKlaimMedis_pk PRIMARY KEY CLUSTERED (EKlaimId)
)