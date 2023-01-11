-- NOTE:  Only run this script if you have succcessfully ran the CreateDb.sql script.

/*
This script make an amendment to the fixture table and the Venue column.
Converting it from a string (nvarchar) to an int as in our Fixture.cs class
the venur property is now an enum (VenueEnum) that only allows Home (1) or Away as a value (2)
*/

-- First update the dummy values in the fixtures table
UPDATE [dbo].[Fixture]
SET [Venue] = '1'
WHERE [Venue] = 'Home'

GO

UPDATE [dbo].[Fixture]
SET [Venue] = '2'
WHERE [Venue] = 'Away'

GO
ALTER TABLE [dbo].[Fixture]
ALTER COLUMN [Venue] INT NOT NULL

GO

