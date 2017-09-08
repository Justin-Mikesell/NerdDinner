CREATE TABLE [dbo].[Dinners] (
    [DinnerID]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (50)  NOT NULL,
    [EventDate]    DATETIME       NOT NULL,
    [Description]  NVARCHAR (MAX) NOT NULL,
    [Host]         NVARCHAR (20)  NOT NULL,
    [ContactPhone] NVARCHAR (20)  NOT NULL,
    [Address]      NVARCHAR (50)  NOT NULL,
    [Country]      NVARCHAR (20)  NOT NULL,
    [Latitude]     FLOAT (53)     NOT NULL,
    [Longitude]    FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([DinnerID] ASC)
);

