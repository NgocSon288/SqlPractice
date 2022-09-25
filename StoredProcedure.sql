USE Fund_Management;  
GO  

CREATE PROCEDURE GetIncomeByMonthAndTeam    
    @Month int,
	@Year int,
    @TeamID int  
AS   
SELECT SUM(Money) AS TotalMoney
FROM Donations as d
WHERE MONTH(d.Date) = @Month and 
	  YEAR(d.Date) = @Year and 
	  d.DestinationTeamID = @TeamID
GO  

CREATE PROCEDURE GetOutcomeByMonthAndTeam    
    @Month int,
	@Year int,
    @TeamID int  
AS   
SELECT SUM(Money) AS TotalMoney
FROM Consumes as c
WHERE MONTH(c.Date) = @Month and 
	  YEAR(c.Date) = @Year and 
	  c.TeamID = @TeamID
GO

CREATE PROCEDURE GetCurrentMoneyByTeam    
    @TeamID int  
AS   
DECLARE @income decimal,
		@outcome decimal;

SELECT @income = SUM(Money)
FROM Donations as d
WHERE d.DestinationTeamID = @TeamID

SELECT @outcome = SUM(Money)
FROM Consumes as c
WHERE c.TeamID = @TeamID

SELECT (@income-@outcome) as TotalMoney
	  
GO

