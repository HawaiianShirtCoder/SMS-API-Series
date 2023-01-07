Video 3 agenda
==============
- Quick and dirty approach - all done in the controller method (with a refactor in later videos)

- Add a new player into the system (still in memory we have no database yet!) [HTTP Post]
- Delete a player from the system [HTTP DELETE]
- Update an existing player [HTTP PUT]

- What is HTTP Patch?  How is it different from HTTP PUT

==================================================================================================================================

PUT
===
PUT is a method of modifying resources where the client sends data that updates the entire resource. PUT is similar to POST 
in that it can create resources, but it does so when there is a defined URL wherein PUT replaces the entire 
resource if it exists or creates new if it does not exist.

UPDATE STATEMENT
SET A = 1, B = 2
WHERE ID = 2

PATCH
=====
Unlike PUT Request, PATCH does partial update e.g. 
Fields that need to be updated by the client, only that field is updated without modifying the other field.
UPDATE
SET A = 1
WHERE ID = 2

C (reate) = Http Post
R (ead) = Http Get
U (update) = http Put or Http Patch
D (elete) = http Delete