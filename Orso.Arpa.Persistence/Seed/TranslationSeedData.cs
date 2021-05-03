using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class TranslationSeedData
    {
        public static IList<Translation> Translations
        {
            get
            {
                return new List<Translation>
                {
                    NotEndTimeLaterStartTime_de_DE,
                    PasswordMinLenght_de_DE,
                    PasswordUpperCase_de_DE,
                    PasswordLowerCase_de_DE,
                    PasswordDigitCase_de_DE,
                    PasswordSpecialCharacterCase_de_DE,
                    UsernameAlphanumericCase_de_DE,
                    ProjectAlreadyLinkedToAppointment_de_DE,
                    RoomAlreadyLinkedToAppointment_de_DE,
                    SectionAlreadyLinkedToAppointment_de_DE,
                    ProjectNotLinkedToAppointment_de_DE,
                    RoomNotLinkedToAppointment_de_DE,
                    SectionNotLinkedToAppointment_de_DE,
                    IncorrectPassword_de_DE,
                    UserNotFound_de_DE,
                    EmailNotConfirmed_de_DE,
                    AccountLocked_de_DE,
                    UsernameAlreadyExists_de_DE,
                    EmailAlreadyExists_de_DE,
                    RegionAlreadyExists_de_DE
                };
            }
        }

        private static Translation NotEndTimeLaterStartTime_de_DE = new(
            Guid.Parse("f55993f0-2629-4a6e-95b8-758f6e68f774"),
            "EndTime must be later than StartTime",
            "Endzeit muss später Startzeit sein",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordMinLenght_de_DE = new(
            Guid.Parse("62b75f0f-ca82-48bf-acd0-853c642b4a52"),
            "Password must be at least 6 characters",
            "Das Passwort muss mindestens 6 Zeichen enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordUpperCase_de_DE = new(
            Guid.Parse("122894ba-c993-47c0-af17-87e57e8daf35"),
            "Password must contain at least one uppercase letter",
            "Das Passwort muss mindestens einen Großbuchstaben enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordLowerCase_de_DE = new(
            Guid.Parse("4fcedf65-e57d-429c-8fa3-266383205b45"),
            "Password must contain at least one lowercase letter",
            "Das Passwort muss mindestens einen Kleinbuchstaben enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordDigitCase_de_DE = new(
            Guid.Parse("acf85e3f-d013-49cd-9943-e00c97ea458d"),
            "Password must contain at least one digit",
            "Das Passwort muss mindestens eine Zahl enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordSpecialCharacterCase_de_DE = new(
            Guid.Parse("3848468e-cd87-4cac-9e38-274b5ae481e4"),
            "Password must contain at least one special character",
            "Das Passwort muss mindestens ein Sonderzeichen enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation UsernameAlphanumericCase_de_DE = new(
            Guid.Parse("ae32fa39-9e66-49e3-aefb-b22583201845"),
            "Username may only contain alphanumeric characters",
            "Der Benutzername darf nur alphanumerische Zeichen enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation ProjectAlreadyLinkedToAppointment_de_DE = new(
            Guid.Parse("355ba10a-3611-4609-8d35-1f47d58d8379"),
            "The project is already linked to the appointment",
            "Das Projekt ist bereits dem Termin zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation RoomAlreadyLinkedToAppointment_de_DE = new(
            Guid.Parse("27d1b47c-1f49-482d-af2a-ba655a925f7e"),
            "The room is already linked to the appointment",
            "Der Raum ist bereits dem Termin zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation SectionAlreadyLinkedToAppointment_de_DE = new(
            Guid.Parse("a08b8308-f219-41ee-8e2f-f5b1c7c1421a"),
            "The section is already linked to the Appointment",
            "Die Sektion ist bereits dem Termin zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation ProjectNotLinkedToAppointment_de_DE = new(
            Guid.Parse("9dab3175-3bab-4faa-a92c-efdd54bbfc3d"),
            "The project is not linked to the appointment",
            "Das Projekt ist dem Termin nicht zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation RoomNotLinkedToAppointment_de_DE = new(
            Guid.Parse("54580bf2-7a47-4211-9842-dcb1ea083933"),
            "The room is not linked to the appointment",
            "Der Raum ist dem Termin nicht zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation SectionNotLinkedToAppointment_de_DE = new(
            Guid.Parse("71f27314-0515-402e-9a9c-f32dc2335e2c"),
            "The section is not linked to the Appointment",
            "Die Sektion ist dem Termin nicht zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation IncorrectPassword_de_DE = new(
            Guid.Parse("77ed3f2e-fecf-477c-ab03-de14019d2147"),
            "Incorrect password supplied",
            "Inkorrektes Passwort angegeben",
            "de,de-DE",
            "Validator"
        );

        private static Translation UserNotFound_de_DE = new(
            Guid.Parse("573c4551-d766-48bd-b6ee-16dc5cfdb913"),
            "The user could not be found",
            "Der Benutzer konnte nicht gefunden werden",
            "de,de-DE",
            "Validator"
        );


        private static Translation EmailNotConfirmed_de_DE = new(
            Guid.Parse("635f453f-7caf-4f65-99dd-787882ad02ac"),
            "Your email address is not confirmed. Please confirm your email address first",
            "Deine Email wurde noch nicht bestätigt. Bitte bestätige zuerst deine Email",
            "de,de-DE",
            "Validator"
        );


        private static Translation AccountLocked_de_DE = new(
            Guid.Parse("ebc99220-9b0b-4e8f-b6b8-ea6dde34f3f4"),
            "Your account is locked. Kindly wait for 10 minutes and try again",
            "Dein Konto wurde gesperrt. Bitte warte 10 Minuten und versuche es anschließend erneut",
            "de,de-DE",
            "Validator"
        );

        private static Translation UsernameAlreadyExists_de_DE = new(
            Guid.Parse("982692bf-f056-46f6-8c0f-47d14da90e0d"),
            "Username already exists",
            "Der Benutzername existiert bereits",
            "de,de-DE",
            "Validator"
        );

        private static Translation EmailAlreadyExists_de_DE = new(
            Guid.Parse("77ca5561-d75c-4e2c-91a5-34f896fc3152"),
            "Email already exists",
            "Die Email existiert bereits",
            "de,de-DE",
            "Validator"
        );

        private static Translation RegionAlreadyExists_de_DE = new(
            Guid.Parse("ddf7ce73-8f66-4547-9c58-7cc051b6e887"),
            "A region with the requested name does already exist",
            "Eine Region mit diesem Namen existiert bereits",
            "de,de-DE",
            "Validator"
        );
    }
}
