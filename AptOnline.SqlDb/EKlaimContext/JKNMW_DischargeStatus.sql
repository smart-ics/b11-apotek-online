CREATE TABLE JKNMW_DischargeStatus(
    DischargeStatusId VARCHAR(1) NOT NULL CONSTRAINT DF_JKNMW_DischargeStatus_DischargeStatusId DEFAULT(''),
    DischargeStatusName VARCHAR(25) NOT NULL CONSTRAINT DF_JKNMW_DischargeStatus_DischargeStatusName DEFAULT(''),

    CONSTRAINT PK_JKNMW_DischargeStatus PRIMARY KEY CLUSTERED (DischargeStatusId)
)

