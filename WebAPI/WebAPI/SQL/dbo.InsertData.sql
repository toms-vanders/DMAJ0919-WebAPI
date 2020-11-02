USE [Corona]
GO

/****** Object: SqlProcedure [dbo].[InsertData] Script Date: 02/11/2020 13:27:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertData]
	@CC char(4),
	@Date smalldatetime,
	@Cases int,
	@Death int,
	@Recovered int

AS
BEGIN

INSERT INTO dbo.theStats
VALUES (@CC, @Date, @Cases, @Death, @Recovered)

END
