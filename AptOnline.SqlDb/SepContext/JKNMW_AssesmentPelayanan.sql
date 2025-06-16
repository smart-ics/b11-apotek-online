CREATE TABLE JKNMW_AssesmentPelayanan(
    AssesmentPelayananId VARCHAR(1) NOT NULL CONSTRAINT DF_JKNMW_AssesmentPelayanan_AssesmentPelayananId DEFAULT(''),
    AssesmentPelayananName VARCHAR(65) NOT NULL CONSTRAINT DF_JKNMW_AssesmentPelayanan_AssesmentPelayananName DEFAULT(''),
    
    CONSTRAINT PK_JKNMW_AssesmentPelayanan PRIMARY KEY CLUSTERED (AssesmentPelayananId)
)

