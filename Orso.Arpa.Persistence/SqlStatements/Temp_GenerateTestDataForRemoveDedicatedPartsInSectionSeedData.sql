-- Temporär: Generierung von TestDaten in der qrso-arpa Datenbank via pgAdmin4 Query Tool (einfach copy & paste & run in pgAdmin4)

----------- nur einmal ausführen!
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('111a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('211a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('311a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('411a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('511a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('611a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('711a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('811a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('911a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);
--insert into persons (id,created_at, created_by, deleted, reliability ,favorization) values('a11a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false,1,1);

delete from section_appointments;
delete from appointments;
delete from person_sections;
delete from musician_profile_sections;
delete from musician_profiles;

insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('12412174-3da2-4817-afa3-d7894eb298f4',4,'8470ddf0-43ab-477e-b3bc-47ede014b359', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('2674a9af-3073-4c39-8d62-5656613f3ddb',4,'22d7cf92-7b29-4cf1-a6fa-2954377589b4', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('35cadade-b412-4bbc-904a-74a17f4e80b5',4,'e809ee90-23f9-44de-b80e-2fddd5ee3683', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('4d0c482f-044f-4bed-b65b-841581db8da5',4,'50dfa2be-85e2-4638-aa53-22dadc97a844', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('5e58233b-68d5-4918-b472-df3cc57f86d2',4,'3db46ff0-9165-46cc-8f28-6a1d52dee518', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('66f1743f-c05f-4821-ab6c-2c05c3cfd923',4,'afef89cf-90e1-4d4f-83ab-d2b47e97af0f', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('708fbc52-aeef-4e62-bb6d-963f06f8ad53',4,'bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('8e9ba3d4-e622-4e33-b84f-86af77725d40',4,'61fa66ec-3103-43fe-800c-930547dff82c', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('9e53866a-ddb3-4e87-9c55-88c3a4cece8c',4,'eb5728b5-b1fd-4a70-8894-7bb152087837', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);
insert into musician_profiles(id, profile_preference_performer, instrument_id, level_assessment_staff,profile_preference_staff,is_main_profile,person_id, created_at, created_by, deleted) values('a6fd3bec-49b7-40df-ba41-da01c0230db1',4,'f3ee3c42-4e4e-411d-a839-6e0420bc35a3', 5,5,false,'56ed7c20-ba78-4a02-936e-5e840ef0748c', '2030-02-02 00:00:00', 'me',false);

insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('11412174-3da2-4817-afa3-d7894eb298f4','8470ddf0-43ab-477e-b3bc-47ede014b359','12412174-3da2-4817-afa3-d7894eb298f4','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('21412174-3da2-4817-afa3-d7894eb298f4','22d7cf92-7b29-4cf1-a6fa-2954377589b4','2674a9af-3073-4c39-8d62-5656613f3ddb','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('31412174-3da2-4817-afa3-d7894eb298f4','e809ee90-23f9-44de-b80e-2fddd5ee3683','35cadade-b412-4bbc-904a-74a17f4e80b5','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('41412174-3da2-4817-afa3-d7894eb298f4','50dfa2be-85e2-4638-aa53-22dadc97a844','4d0c482f-044f-4bed-b65b-841581db8da5','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('51412174-3da2-4817-afa3-d7894eb298f4','3db46ff0-9165-46cc-8f28-6a1d52dee518','5e58233b-68d5-4918-b472-df3cc57f86d2','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('61412174-3da2-4817-afa3-d7894eb298f4','afef89cf-90e1-4d4f-83ab-d2b47e97af0f','66f1743f-c05f-4821-ab6c-2c05c3cfd923','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('71412174-3da2-4817-afa3-d7894eb298f4','bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21','708fbc52-aeef-4e62-bb6d-963f06f8ad53','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('81412174-3da2-4817-afa3-d7894eb298f4','61fa66ec-3103-43fe-800c-930547dff82c','8e9ba3d4-e622-4e33-b84f-86af77725d40','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('91412174-3da2-4817-afa3-d7894eb298f4','eb5728b5-b1fd-4a70-8894-7bb152087837','9e53866a-ddb3-4e87-9c55-88c3a4cece8c','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');
insert into musician_profile_sections(id,section_id, musician_profile_id,created_at, created_by,deleted, instrument_availability_id) values('a1412174-3da2-4817-afa3-d7894eb298f4','f3ee3c42-4e4e-411d-a839-6e0420bc35a3','a6fd3bec-49b7-40df-ba41-da01c0230db1','2030-02-02 00:00:00', 'me',false,'d33ea034-0c5f-458d-bef5-26d2c12b6b03');

insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('12412174-3da2-4817-afa3-d7894eb298f4','8470ddf0-43ab-477e-b3bc-47ede014b359','111a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('22412174-3da2-4817-afa3-d7894eb298f4','22d7cf92-7b29-4cf1-a6fa-2954377589b4','211a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('32412174-3da2-4817-afa3-d7894eb298f4','e809ee90-23f9-44de-b80e-2fddd5ee3683','311a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('42412174-3da2-4817-afa3-d7894eb298f4','50dfa2be-85e2-4638-aa53-22dadc97a844','411a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('52412174-3da2-4817-afa3-d7894eb298f4','3db46ff0-9165-46cc-8f28-6a1d52dee518','511a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('62412174-3da2-4817-afa3-d7894eb298f4','afef89cf-90e1-4d4f-83ab-d2b47e97af0f','611a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('72412174-3da2-4817-afa3-d7894eb298f4','bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21','711a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('82412174-3da2-4817-afa3-d7894eb298f4','61fa66ec-3103-43fe-800c-930547dff82c','811a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('92412174-3da2-4817-afa3-d7894eb298f4','eb5728b5-b1fd-4a70-8894-7bb152087837','911a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);
insert into person_sections(id,section_id,person_id,created_at, created_by, deleted) values('a2412174-3da2-4817-afa3-d7894eb298f4','f3ee3c42-4e4e-411d-a839-6e0420bc35a3','a11a8fe4-83a0-4745-8eb5-e83efae0d6b9','2030-02-02 00:00:00', 'me',false);

insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('1e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('2e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('3e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('4e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('5e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('6e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('7e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('8e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('9e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );
insert into appointments(id, created_at, created_by, deleted, start_time, end_time) values('ae24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false,'2030-02-02 00:00:00', '2030-02-02 00:00:01' );

insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('13412174-3da2-4817-afa3-d7894eb298f4','8470ddf0-43ab-477e-b3bc-47ede014b359','1e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('23412174-3da2-4817-afa3-d7894eb298f4','22d7cf92-7b29-4cf1-a6fa-2954377589b4','2e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('33412174-3da2-4817-afa3-d7894eb298f4','e809ee90-23f9-44de-b80e-2fddd5ee3683','3e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('43412174-3da2-4817-afa3-d7894eb298f4','50dfa2be-85e2-4638-aa53-22dadc97a844','4e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('53412174-3da2-4817-afa3-d7894eb298f4','3db46ff0-9165-46cc-8f28-6a1d52dee518','5e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('63412174-3da2-4817-afa3-d7894eb298f4','afef89cf-90e1-4d4f-83ab-d2b47e97af0f','6e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('73412174-3da2-4817-afa3-d7894eb298f4','bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21','7e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('83412174-3da2-4817-afa3-d7894eb298f4','61fa66ec-3103-43fe-800c-930547dff82c','8e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('93412174-3da2-4817-afa3-d7894eb298f4','eb5728b5-b1fd-4a70-8894-7bb152087837','9e24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
insert into section_appointments(id, section_id,appointment_id,created_at,created_by, deleted) values('a3412174-3da2-4817-afa3-d7894eb298f4','f3ee3c42-4e4e-411d-a839-6e0420bc35a3','ae24a450-fbd0-4cbd-ac84-e78982d82c31','2030-02-02 00:00:00', 'me',false);
