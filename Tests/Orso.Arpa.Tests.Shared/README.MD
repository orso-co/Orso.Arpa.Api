# Test Data Overview

## Musician Profiles
| Bezeichnung|User|Instrument|Doubling Instrument|DeactivationStart|Projects|
|---|---|---|---|---|---|
|PerformerMusicianProfile|Performer|Alto|||RockingXMas<br>Schneekönigin|
|PerformersHornMusicianProfile|Performer|Horn|Wagner Tuba|||
|PerformersDeactivatedTubaProfile|Performer|Tuba||13.1.2030|||
|StaffMusicianProfile1|Staff|Tenor|||RockingXMas|
|StaffMusicianProfile2|Staff|Basso|||RockingXMas|
|AdminMusicianSopranoProfile|Admin|Soprano|||RockingXMas|
|AdminMusicianFluteProfile|Admin|Flute||||			
|UserWithoutRoleMusicianProfile|UserWithoutRole|Basso|

## Appointments
|Bezeichnung|Projects|Sections|Persons (Musician Profiles)|
|---|---|---|---|
|RockingXMasRehearsal|RockingXMas||Performer (PerformerMusicianProfile)<br>Staff (StaffMusicianProfile1, StaffMusicianProfile2)<br>Admin (AdminMusicianSopranoProfile)|
|RockingXMasConcert|||All|
|AfterShowParty|RockingXMas|Alto|Performer (PerformerMusicianProfile)|
|StaffMeeting|HoorayForHollywood|		
|PhotoSession||Choir|Performer (PerformerMusicianProfile)<br>Staff (StaffMusicianProfile1, StaffMusicianProfile2)<br>Admin (AdminMusicianSopranoProfile)<br>UserWithoutRole (UserWithoutRoleMusicianProfile)|
|RehearsalWeekend||Accordion||	
|AuditionDays|HoorayForHollywood|PiccoloFlute||	
|AltoRehearsal|HoorayForHollywood|Alto||	
|SopranoRehearsal|RockingXMas|Soprano|Admin (AdminMusicianSopranoProfile)|
