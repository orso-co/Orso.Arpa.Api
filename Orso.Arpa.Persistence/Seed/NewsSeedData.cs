using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.NewsDomain.Commands;
using Orso.Arpa.Domain.NewsDomain.Model;

namespace Orso.Arpa.Persistence.Seed
{
    public static class NewsSeedData
    {
        public static IList<News> News =>
        [
            News1,
            News2,
            News3,
            News4,
            News5
        ];

        public static News News1 => new(
            Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-0e1f2a3b4c5d"),
            new CreateNews.Command
            {
                Title = "Willkommen bei ARPA!",
                Content = "ARPA (Artistic Resource Planning Application) ist eure zentrale Plattform für Proben, Konzerte und Vereinsaktivitäten. Hier findet ihr alle wichtigen Informationen zu kommenden Veranstaltungen, könnt eure Verfügbarkeit angeben und mit anderen Mitgliedern in Kontakt treten. Bei Fragen wendet euch gerne an das Staff-Team.",
                Url = "https://orso.co",
                Show = true
            });

        public static News News2 => new(
            Guid.Parse("b2c3d4e5-f6a7-4b8c-9d0e-1f2a3b4c5d6e"),
            new CreateNews.Command
            {
                Title = "Probenwochenende im März",
                Content = "Vom 15. bis 17. März findet unser intensives Probenwochenende in der Musikakademie statt. Bitte tragt eure Verfügbarkeit bis spätestens 1. März ein. Die Unterbringung erfolgt in Doppelzimmern, Einzelzimmer sind auf Anfrage verfügbar. Das detaillierte Programm wird in Kürze veröffentlicht.",
                Url = null,
                Show = true
            });

        public static News News3 => new(
            Guid.Parse("c3d4e5f6-a7b8-4c9d-0e1f-2a3b4c5d6e7f"),
            new CreateNews.Command
            {
                Title = "Neue Noten verfügbar",
                Content = "Die Noten für unser Frühjahrskonzert sind jetzt im Downloadbereich verfügbar. Bitte ladet euch die Stimmen herunter und beginnt mit dem Einstudieren. Bei der nächsten Probe werden wir die schwierigeren Passagen gemeinsam durchgehen. Denkt daran, einen Bleistift mitzubringen!",
                Url = "https://orso.co/noten",
                Show = true
            });

        public static News News4 => new(
            Guid.Parse("d4e5f6a7-b8c9-4d0e-1f2a-3b4c5d6e7f8a"),
            new CreateNews.Command
            {
                Title = "Mitgliederversammlung 2026",
                Content = "Die jährliche Mitgliederversammlung findet am 20. April um 18:00 Uhr im Vereinsheim statt. Auf der Tagesordnung stehen: Jahresbericht des Vorstands, Kassenbericht, Entlastung des Vorstands, Wahlen und Verschiedenes. Anträge zur Tagesordnung können bis zum 6. April eingereicht werden.",
                Url = null,
                Show = true
            });

        public static News News5 => new(
            Guid.Parse("e5f6a7b8-c9d0-4e1f-2a3b-4c5d6e7f8a9b"),
            new CreateNews.Command
            {
                Title = "Sommerkonzert: Kartenvorverkauf gestartet",
                Content = "Der Vorverkauf für unser großes Sommerkonzert am 21. Juni hat begonnen! Sichert euch jetzt eure Karten zum Frühbucherpreis. Mitglieder erhalten wie gewohnt Rabatt auf den regulären Eintrittspreis. Ermäßigte Karten für Schüler, Studenten und Senioren sind ebenfalls verfügbar.",
                Url = "https://pretix.orso.co",
                Show = true
            });
    }
}
