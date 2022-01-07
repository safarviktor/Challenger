/*

INSERT INTO clg.[User]
([Name])
SELECT 'Vik'

set identity_insert clg.ChallengeType on
INSERT INTO clg.ChallengeType
(Id, [Name])
select 1, 'AddOneMoreEachDay'

set identity_insert clg.ChallengeType off


INSERT INTO clg.Challenge
(UserId, [Name], ChallengeTypeId)
select 1, 'Pushups 2019', 1 union all
select 1, 'Squats 2019', 1 

exec clg.spFixAddOneMoreEachDayChallenge 1
exec clg.spFixAddOneMoreEachDayChallenge 2


CREATE USER ChallengerApp FOR LOGIN ChallengerApp
sp_addrolemember 'db_datareader',  'ChallengerApp'
sp_addrolemember 'db_datawriter', 'ChallengerApp'

*/

