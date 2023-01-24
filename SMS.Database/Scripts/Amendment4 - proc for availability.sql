CREATE PROCEDURE usp_TogglePlayerAvailability
	@playerId INT,
	@fixtureId INT,
	@isAvailable BIT
AS
SET NOCOUNT ON

DECLARE @alreadyHasAvailabilityRecord INT
SET @alreadyHasAvailabilityRecord = (
	SELECT COUNT(*) AS 'Total'
	FROM Availability AS a 
	WHERE a.FixtureId = @fixtureId
	AND a.playerId = @playerId)


	IF(@alreadyHasAvailabilityRecord =0 AND @isAvailable = 1)
		BEGIN
			-- Need to add a new record to the availability table
			-- player and fixture provided as input variables
			-- i.e. check availaibility box on user interface
			INSERT INTO [Availability]
				(FixtureId, PlayerId)
			VALUES
				(@fixtureId, @playerId)
			SELECT SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			IF(@isAvailable = 0  AND @alreadyHasAvailabilityRecord = 1)
				BEGIN
					-- Player already has an availability record for
					-- this fixture therefore they are asking to remove
					-- this availability (i.e. un check availability box
					-- on user interface)
					DELETE FROM [Availability] 
					WHERE FixtureId = @fixtureId 
					AND PlayerId = @playerId
				END
		END
