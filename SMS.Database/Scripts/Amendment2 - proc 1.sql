/*
Using Stored Procedures rather than inline SQL statement to issue queries and commands
against our SQL Express database
*/

--DROP PROCEDURE IF EXISTS [dbo].[usp_DeleteFixture]
--GO

-- Delete a fixture - handles the cascade delete
-- i.e. go to delete a fixture but it has associated availability records
-- we need to delete the availability records first, then delete the fixture
-- this stored procedure will treat this as one operation.
-- Either both related availability and fixtures will be deleted
-- OR
-- Nothing will be deleted!
CREATE PROCEDURE [dbo].[usp_DeleteFixture]
	@FixtureId INT
AS
SET NOCOUNT ON
BEGIN TRANSACTION
	DELETE FROM [Availability]
	WHERE FixtureId = @FixtureId
	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	DELETE FROM [Fixture]
	WHERE id = @FixtureId
	IF @@ERROR <> 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	COMMIT TRANSACTION





