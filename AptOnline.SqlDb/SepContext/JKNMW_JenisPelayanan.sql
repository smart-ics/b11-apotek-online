CREATE TABLE JKNMW_JenisPelayanan(
    JenisPelayananId VARCHAR(1) NOT NULL CONSTRAINT DF_JKNMW_JenisPelayanan_JenisPelayananId DEFAULT(''),
    JenisPelayananName VARCHAR(15) NOT NULL CONSTRAINT DF_JKNMW_JenisPelayanan_JenisPelayananName DEFAULT(''),
    
    CONSTRAINT PK_JKNMW_JenisPelayanan PRIMARY KEY CLUSTERED (JenisPelayananId)
)

