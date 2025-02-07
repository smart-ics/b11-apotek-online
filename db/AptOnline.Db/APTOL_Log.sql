CREATE TABLE [dbo].[APTOL_Log]
(
    [JobId] VARCHAR(12) NOT NULL  DEFAULT '', 
    LogDateTime DateTime NOT NULL DEFAULT 0, 
    [Request] VARCHAR(1024) NOT NULL DEFAULT '',
    [ResultState] Bit NOT NULL DEFAULT 0, 
    [Response] VARCHAR(1024) NOT NULL DEFAULT ''
)