CREATE TABLE JKNMW_BayiLahirStatus(
    BayiLahirStatusId VARCHAR(1) NOT NULL CONSTRAINT DF_JKNMW_BayiLahirStatus_BayiLahirStatusId DEFAULT(''),
    BayiLahirStatusName VARCHAR(15) NOT NULL CONSTRAINT DF_JKNMW_BayiLahirStatus_BayiLahirStatusName DEFAULT(''),
    
    CONSTRAINT PK_JKNMW_BayiLahirStatus PRIMARY KEY CLUSTERED (BayiLahirStatusId)
)