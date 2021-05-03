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
            "EndTime must be later than StartTime",
            "Endzeit muss später Startzeit sein",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordMinLenght_de_DE = new(
            "Password must be at least 6 characters",
            "Das Passwort muss mindestens 6 Zeichen enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordUpperCase_de_DE = new(
            "Password must contain at least one uppercase letter",
            "Das Passwort muss mindestens einen Großbuchstaben enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordLowerCase_de_DE = new(
            "Password must contain at least one lowercase letter",
            "Das Passwort muss mindestens einen Kleinbuchstaben enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordDigitCase_de_DE = new(
            "Password must contain at least one digit",
            "Das Passwort muss mindestens eine Zahl enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation PasswordSpecialCharacterCase_de_DE = new(
            "Password must contain at least one special character",
            "Das Passwort muss mindestens ein Sonderzeichen enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation UsernameAlphanumericCase_de_DE = new(
            "Username may only contain alphanumeric characters",
            "Der Benutzername darf nur alphanumerische Zeichen enthalten",
            "de,de-DE",
            "Validator"
        );

        private static Translation ProjectAlreadyLinkedToAppointment_de_DE = new(
            "The project is already linked to the appointment",
            "Das Projekt ist bereits dem Termin zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation RoomAlreadyLinkedToAppointment_de_DE = new(
            "The room is already linked to the appointment",
            "Der Raum ist bereits dem Termin zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation SectionAlreadyLinkedToAppointment_de_DE = new(
            "The section is already linked to the Appointment",
            "Die Sektion ist bereits dem Termin zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation ProjectNotLinkedToAppointment_de_DE = new(
            "The project is not linked to the appointment",
            "Das Projekt ist dem Termin nicht zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation RoomNotLinkedToAppointment_de_DE = new(
            "The room is not linked to the appointment",
            "Der Raum ist dem Termin nicht zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation SectionNotLinkedToAppointment_de_DE = new(
            "The section is not linked to the Appointment",
            "Die Sektion ist dem Termin nicht zugeordnet",
            "de,de-DE",
            "Validator"
        );

        private static Translation IncorrectPassword_de_DE = new(
            "Incorrect password supplied",
            "Inkorrektes Passwort angegeben",
            "de,de-DE",
            "Validator"
        );

        private static Translation UserNotFound_de_DE = new(
            "The user could not be found",
            "Der Benutzer konnte nicht gefunden werden",
            "de,de-DE",
            "Validator"
        );


        private static Translation EmailNotConfirmed_de_DE = new(
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
            "Username already exists",
            "Der Benutzername existiert bereits",
            "de,de-DE",
            "Validator"
        );

        private static Translation EmailAlreadyExists_de_DE = new(
            "Email already exists",
            "Die Email existiert bereits",
            "de,de-DE",
            "Validator"
        );

        private static Translation RegionAlreadyExists_de_DE = new(
            "A region with the requested name does already exist",
            "Eine Region mit diesem Namen existiert bereits",
            "de,de-DE",
            "Validator"
        );
    }
}
