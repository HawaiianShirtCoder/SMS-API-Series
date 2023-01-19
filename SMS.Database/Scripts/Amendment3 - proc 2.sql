/*
Using Stored Procedures rather than inline SQL statement to issue queries and commands
against our SQL Express database
*/

--DROP PROCEDURE IF EXISTS [dbo].[usp_DeletePlayer]
--GO

-- Delete a player - handles the cascade delete
-- i.e. go to delete a player but it has associated availability records
-- we need to delete the availability records first, then delete the player
-- this stored procedure will treat this as one operation.
-- Either both related availability and players will be deleted
-- OR
-- Nothing will be deleted!
CREATE PROCEDURE [dbo].[usp_DeletePlayer]
	@PlayerId INT
AS
SET NOCOUNT ON
BEGIN TRANSACTION
	DELETE FROM [Availability]
	WHERE PlayerId = @PlayerId
	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	DELETE FROM [Player]
	WHERE id = @PlayerId
	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	COMMIT TRANSACTION


GO




