using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class SectionSeedData
    {
        public static IList<Section> Sections => new List<Section>
        {
            Performers,
                Conductor,
                    AssistantConductor,
                    Repetiteur,
                    VocalCoach,

                Choir,
                    FemaleVoices,
                        HighFemaleVoices,
                            Soprano,
                            MezzoSoprano,
                        LowFemaleVoices,
                            Alto,
                    MaleVoices,
                        HighMaleVoices,
                                Tenor,
                        LowMaleVoices,
                            Baritone,
                            Basso,

                Orchestra,
                    Winds,
                        Woodwinds,
                            Flute,
                                PiccoloFlute,
                                AltoFlute,
                                TenorFlute,
                                BassFlute,
                            Oboe,
                                OboeDAmore,
                                EnglishHorn,
                                BaritonOboe,
                            Clarinet,
                                EbClarinet,
                                AltoClarinet,
                                Bassetthorn,
                                BassClarinet,
                                DoubleBassClarinet,
                            Bassoon,
                                ContraBassoon,
                                ContraForte,
                            Saxophone,
                                SopranoSaxophone,
                                AltoSaxophone,
                                TenorSaxophone,
                                BaritoneSaxophone,
                                BassSaxophone,

                        Brass,
                            HighBrass,
                                Horn,
                                    WagnerTuba,
                                Trumpet,
                                    Flugelhorn,
                                    PiccoloTrumpet,
                                    SopranoCornet,
                                    Cornet,
                            LowBrass,
                                Trombone,
                                    AltoTrombone,
                                    BassTrombone,
                                    DoubleBassTrombone,
                                Euphonium,
                                    TenorHorn,
                                    BaritoneHorn,
                                Tuba,
                                    EbTuba,
                                    FTuba,
// Percussion noch nicht fertig!
                        Percussion,
                            Timpani,
                            Mallets,
                                Glockenspiel,
                                Vibraphone,
                                Xylophone,
                                Marimbaphone,
                                DrumSet,

                        Others,
                            Harp,
                            Keyboards,
                                Piano,
                                Celesta,
                                Cembalo,
                                Organ,
                                Synthesizer,
                            Accordion,
                            Bandoneon,
                            Guitars,
                                AcousticGuitar,
                                ElectricGuitar,
                                ElectricBass,
                            GlassHarp,
                            Bagpipes,
                            Didgeridoo,

                        Strings,
                            HighStrings,
                                Violin,
                                Viola,
                            LowStrings,
                                Violoncello,
                                DoubleBass,

                Band,
                    ElectricGuitarBand,
                    ElectricBassBand,
                    DrumSetBand,
                    KeyboardsBand,

                Soloists,
/* Soloists - work in progress
                    SoloSoprano,
                    SoloMezzoSoprano,
                    SoloAlto,
                    SoloTenor,
                    SoloBaritone,
                    SoloBass,
                    Narrator,
                    Moderator,
*/

/* Crew, etc. noch nicht fertig!

            Crew,
                Stage,
                    RiggingTeam,
                    RaiserTeam,
                    OrchestraSetup,
                    ChoirSetup,
                Media,
                    VideoTeam,
                    Photographer,
                Sound,
                    SoundEngineer,
                    SoundTeam,
                    RecordingTeam,
                Light,
                    LightDesigner,
                    LightingTeam,
            Volunteers,
                BoxOffice,
                Entrance,
                Merchandise,
                Catering,
                Backstage,
                ArtistSupport,
                GeneralHelper,
                ArtisticManagementOffice,
*/

            Members,
            Visitors,
            Volunteers,
            Suppliers,
            Contractors,

        };

        // PERFORMERS
        public static Section Performers => new(Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"), "Performers", null, false);

        // Conductor
        public static Section Conductor => new(Guid.Parse("4E7A61C5-D2E4-4E3B-B21D-34A90CF958B2"), "Conductor", Performers.Id, false);
        public static Section AssistantConductor => new(Guid.Parse("18f1e750-f50d-4f06-8205-21203981bff6"), "Assistant Conductor", Conductor.Id, false);
        public static Section Repetiteur => new(Guid.Parse("6fc908f0-da26-4237-80ca-dfe30453123c"), "Répétiteur", Conductor.Id, false);
        public static Section VocalCoach => new(Guid.Parse("94c42496-fdb6-4341-b82f-735fd1706d39"), "Vocal Coach", Conductor.Id, false);

        // CHOIR VOICES
        public static Section Choir => new(Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"), "Choir", Performers.Id, false);
        public static Section FemaleVoices => new(Guid.Parse("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"), "Female Voices", Choir.Id, false);
        public static Section HighFemaleVoices => new(Guid.Parse("5d469fc5-b3e6-40b8-9fa9-542981083ce3"), "High Female Voices", FemaleVoices.Id, false);
        public static Section Soprano => new(Guid.Parse("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), "Soprano", HighFemaleVoices.Id, true, 2);
        public static Section MezzoSoprano => new(Guid.Parse("eb42b2f7-413e-4c1a-ab79-23c74b02d054"), "Mezzo Soprano", HighFemaleVoices.Id, true);
        public static Section LowFemaleVoices => new(Guid.Parse("48337b78-70f0-493e-911b-296632b06ef8"), "Low Female Voices", FemaleVoices.Id, false);
        public static Section Alto => new(Guid.Parse("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), "Alto", LowFemaleVoices.Id, true, 2);
        public static Section MaleVoices => new(Guid.Parse("4599103d-f220-4744-92d1-7c6993e9bda4"), "Male Voices", Choir.Id, false);
        public static Section HighMaleVoices => new(Guid.Parse("7924daef-2542-4648-a42f-4c4374ee09db"), "High Male Voices", MaleVoices.Id, false);
        public static Section Tenor => new(Guid.Parse("1579d7e7-4f55-4532-a078-69fd1ec939da"), "Tenor", HighMaleVoices.Id, true, 2);
        public static Section LowMaleVoices => new(Guid.Parse("b9673cfd-7cdb-472c-86e0-1304cbb3840a"), "Low Male Voices", MaleVoices.Id, false);
        public static Section Baritone => new(Guid.Parse("bb647161-8394-47d3-9f43-825762a70fc2"), "Baritone", LowMaleVoices.Id, true);
        public static Section Basso => new(Guid.Parse("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), "Basso", LowMaleVoices.Id, true, 2);

        // ORCHESTRAL INSTRUMENTS
        public static Section Orchestra => new(Guid.Parse("308659d6-6014-4d2c-a62a-be75bf202e62"), "Orchestra", Performers.Id, false);
        // WINDS
        public static Section Winds => new(Guid.Parse("b289cfe7-d66e-48d8-83a9-f4b1f7710863"), "Winds", Orchestra.Id, false);
        // WOODWINDS
        public static Section Woodwinds => new(Guid.Parse("a6abdeec-8185-40ac-a418-2e422bb9adbd"), "Woodwinds", Winds.Id, false);
        public static Section Flute => new(Guid.Parse("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), "Flute", Woodwinds.Id, true, 4);
        public static Section PiccoloFlute => new(Guid.Parse("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"), "Piccolo Flute", Flute.Id, false, 2);
        public static Section AltoFlute => new(Guid.Parse("f9c1924b-2b45-459c-b919-99059cb41e73"), "Alto Flute", Flute.Id, false);
        public static Section TenorFlute => new(Guid.Parse("D0A18A79-AD5A-450D-92CC-20A58496AAF0"), "Tenor Flute", Flute.Id, false);
        public static Section BassFlute => new(Guid.Parse("fc66c8b8-d9de-4ff0-a695-37e717103686"), "Bass Flute", Flute.Id, false);
        public static Section Oboe => new(Guid.Parse("2327a9c3-2c6f-41b7-9045-bb00af798b42"), "Oboe", Woodwinds.Id, true, 4);
        public static Section OboeDAmore => new(Guid.Parse("4e71ffc3-e086-4c16-a932-3d80fd302971"), "Oboe d'Amore", Oboe.Id, false);
        public static Section EnglishHorn => new(Guid.Parse("abe0d27b-2c99-4755-891c-fb0b91f19bb6"), "English Horn", Oboe.Id, false, 2);
        public static Section BaritonOboe => new(Guid.Parse("2f8d732f-bf82-4a62-86a1-62bffd708189"), "Bariton Oboe", Oboe.Id, false);
        public static Section Clarinet => new(Guid.Parse("cdc390d5-0649-441d-a086-df2e3b9d3512"), "Clarinet", Woodwinds.Id, true, 4);
        public static Section EbClarinet => new(Guid.Parse("d2551427-d727-42d9-be0e-dea2ae82f2d6"), "Eb Clarinet", Clarinet.Id, false, 2);
        public static Section AltoClarinet => new(Guid.Parse("BE75913A-9703-4A8D-9E07-7A8D32C459F8"), "Alto Clarinet", Clarinet.Id, false);
        public static Section Bassetthorn => new(Guid.Parse("8c0a80d1-5889-4794-89b6-b80a3828aa5b"), "Basset Horn", Clarinet.Id, false, 2);
        public static Section BassClarinet => new(Guid.Parse("5109e464-7b01-40bd-a5e0-398ac3d1bb83"), "Bass Clarinet", Clarinet.Id, false, 2);
        public static Section DoubleBassClarinet => new(Guid.Parse("a5cc5e9d-b318-4edc-af84-ff3d701d0bcb"), "Double Bass Clarinet", Clarinet.Id, false);
        public static Section Bassoon => new(Guid.Parse("5c14f673-13f2-488f-8c21-7286d3ee10c3"), "Bassoon", Woodwinds.Id, true, 4);
        public static Section ContraBassoon => new(Guid.Parse("8d01524c-7c22-4a20-8f26-711d11addbfd"), "Contra Bassoon", Bassoon.Id, false, 2);
        public static Section ContraForte => new(Guid.Parse("7cb00d2e-5a98-4b68-b775-3b5d1f267d96"), "Contraforte", Bassoon.Id, false);
        public static Section Saxophone => new(Guid.Parse("566260fb-b6be-41dc-956d-4070d30fa88d"), "Saxophone", Woodwinds.Id, true);
        public static Section SopranoSaxophone => new(Guid.Parse("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"), "Soprano Saxophone", Saxophone.Id, false);
        public static Section AltoSaxophone => new(Guid.Parse("4a31447d-63c2-4e28-ab39-255a956fbe18"), "Alto Saxophone", Saxophone.Id, false, 3);
        public static Section TenorSaxophone => new(Guid.Parse("da998fcb-92b9-4828-976e-826e97e05cb3"), "Tenor Saxophone", Saxophone.Id, false, 3);
        public static Section BaritoneSaxophone => new(Guid.Parse("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"), "Baritone Saxophone", Saxophone.Id, false);
        public static Section BassSaxophone => new(Guid.Parse("fb4f9841-294a-4b6c-bfec-02d3735b1ea0"), "Bass Saxophone", Saxophone.Id, false);

        // BRASS
        public static Section Brass => new(Guid.Parse("f4c70178-d069-44dc-8956-7160c5fef52e"), "Brass", Winds.Id, false);
        public static Section HighBrass => new(Guid.Parse("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f"), "High Brass", Brass.Id, false);
        public static Section Horn => new(Guid.Parse("b9532add-efec-4510-831c-902c32ef7dbb"), "Horn", HighBrass.Id, true, 8);
        public static Section WagnerTuba => new(Guid.Parse("c42591db-4e41-413f-8b98-6607e2f12e39"), "Wagner Tuba", Horn.Id, false, 2);
        public static Section Trumpet => new(Guid.Parse("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), "Trumpet", HighBrass.Id, true, 8);
        public static Section Flugelhorn => new(Guid.Parse("69e64d64-419f-4f9c-9948-a117b02ff198"), "Flugelhorn", Trumpet.Id, false, 3);
        public static Section PiccoloTrumpet => new(Guid.Parse("2393549e-5b16-4414-a896-3cebb7bcc9df"), "Piccolo Trumpet", Trumpet.Id, false);
        public static Section SopranoCornet => new(Guid.Parse("290F84D4-BB3F-41C3-9F42-C649C8EEEA26"), "Soprano Cornet", Trumpet.Id, false);
        public static Section Cornet => new(Guid.Parse("305C06E0-B99F-4F91-AE83-869D8B25C63D"), "Cornet", Trumpet.Id, false, 3);
        public static Section LowBrass => new(Guid.Parse("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041"), "Low Brass", Brass.Id, false);
        public static Section Trombone => new(Guid.Parse("e20ce055-5715-42f4-97e6-4025559b15f7"), "Trombone", LowBrass.Id, true, 4);
        public static Section AltoTrombone => new(Guid.Parse("80f15184-6417-476a-87ac-0f752d011391"), "Alto Trombone", Trombone.Id, false);
        public static Section BassTrombone => new(Guid.Parse("da660c21-0151-4255-a81b-4d25fede199b"), "Bass Trombone", Trombone.Id, false, 2);
        public static Section DoubleBassTrombone => new(Guid.Parse("32F3FDD9-9517-4DB5-856E-376E9FA52B84"), "Double Bass Trombone", Trombone.Id, false);
        public static Section Euphonium => new(Guid.Parse("554fd3db-110b-4335-bc2a-1d5070f6621a"), "Euphonium", LowBrass.Id, true, 3);
        public static Section TenorHorn => new(Guid.Parse("803219AA-1A32-4A68-95AE-348BD487135A"), "Tenor Horn", Euphonium.Id, false, 3);
        public static Section BaritoneHorn => new(Guid.Parse("B525E539-7FA4-49D7-AE93-EC0748022D4D"), "Baritone Horn", Euphonium.Id, false, 3);
        public static Section Tuba => new(Guid.Parse("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), "Tuba", LowBrass.Id, true, 2);
        public static Section EbTuba => new(Guid.Parse("2FABD3A1-D398-4108-A74F-2665710133D1"), "Eb Tuba", Tuba.Id, false, 2);
        public static Section FTuba => new(Guid.Parse("31A2B9BF-0C2B-47EC-B8BC-34C9423B74D4"), "F Tuba", Tuba.Id, false, 2);

        // PERCUSSION
        public static Section Percussion => new(Guid.Parse("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), "Percussion", Orchestra.Id, false);
        public static Section Timpani => new(Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), "Timpani", Percussion.Id, true, 2);
        public static Section Mallets => new(Guid.Parse("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), "Mallets", Percussion.Id, true);
        public static Section Glockenspiel => new(Guid.Parse("dcf267e6-5b58-4534-8e4b-a8c5747b1816"), "Glockenspiel", Mallets.Id, false);
        public static Section Vibraphone => new(Guid.Parse("852d8129-a5b7-4378-ad9c-df89dc878b4f"), "Vibraphone", Mallets.Id, false);
        public static Section Xylophone => new(Guid.Parse("2804ed14-7b73-4e17-bd21-edd048a60cb4"), "Xylophone", Mallets.Id, false);
        public static Section Marimbaphone => new(Guid.Parse("bb0715dc-7f9d-4ddb-b5f5-9e7806e1069f"), "Marimbaphone", Mallets.Id, false);
        public static Section DrumSet => new(Guid.Parse("C15C3649-D7BB-4BBF-BDD3-F6146EBC825C"), "Drum Set (Orchestra)", Percussion.Id, true);

        // OTHERS
        public static Section Others => new(Guid.Parse("c9403ca4-6b75-44c3-b567-e53bbd78fb75"), "Others", Orchestra.Id, false);
        public static Section Harp => new(Guid.Parse("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), "Harp", Others.Id, true, 2);
        public static Section Keyboards => new(Guid.Parse("614a8fd0-acfa-4268-b716-3b35a6a17b7a"), "Keyboards", Others.Id, true);
        public static Section Piano => new(Guid.Parse("8ed82e0e-0354-4192-8f26-5a2437e9208d"), "Piano", Keyboards.Id, false, 2);
        public static Section Celesta => new(Guid.Parse("bc6cfeb7-569d-4c22-8e80-647aed560bf0"), "Celesta", Keyboards.Id, false);
        public static Section Cembalo => new(Guid.Parse("f6af00f5-e81c-4d85-aadd-1e33748e9a64"), "Cembalo", Keyboards.Id, false);
        public static Section Organ => new(Guid.Parse("182019da-bde2-44d7-8c77-88cfb0ce428c"), "Organ", Keyboards.Id, false);
        public static Section Synthesizer => new(Guid.Parse("d22fb8aa-7d38-42c4-9586-30e559f63799"), "Synthesizer", Keyboards.Id, false);
        public static Section Accordion => new(Guid.Parse("76891771-b5f2-4666-8972-ba7f494fc9de"), "Accordion", Others.Id, true);
        public static Section Bandoneon => new(Guid.Parse("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"), "Bandoneon", Others.Id, true);
        public static Section Guitars => new(Guid.Parse("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"), "Guitars", Others.Id, true);
        public static Section AcousticGuitar => new(Guid.Parse("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"), "Acoustic Guitar (Orchestra)", Guitars.Id, false, 2);
        public static Section ElectricGuitar => new(Guid.Parse("ed0829d0-d978-430e-96ec-b93cf75f3fd6"), "Electric Guitar (Orchestra)", Guitars.Id, false, 2);
        public static Section ElectricBass => new(Guid.Parse("9cd74865-f82a-4be9-afc1-384fb25b7fe4"), "Electric Bass (Orchestra)", Guitars.Id, false);
        public static Section GlassHarp => new(Guid.Parse("08bc313b-d0dd-4b78-bdbf-d976682d965e"), "GlassHarp", Others.Id, true, 2);
        public static Section Bagpipes => new(Guid.Parse("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"), "Bagpipes", Others.Id, true);
        public static Section Didgeridoo => new(Guid.Parse("8903B8C5-0EF8-48FD-9C2B-71FBAE827965"), "Didgeridoo", Others.Id, true);


        // STRINGS
        public static Section Strings => new(Guid.Parse("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"), "Strings", Orchestra.Id, false);
        public static Section HighStrings => new(Guid.Parse("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8"), "High Strings", Strings.Id, false);
        public static Section Violin => new(Guid.Parse("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), "Violin", HighStrings.Id, true, 2);
        public static Section Viola => new(Guid.Parse("df541ea1-a5fd-4975-b6fd-7cd652a79073"), "Viola", HighStrings.Id, true);
        public static Section LowStrings => new(Guid.Parse("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f"), "Low Strings", Strings.Id, false);
        public static Section Violoncello => new(Guid.Parse("d8686f68-78da-4022-b0b8-97e0c263d694"), "Violoncello", LowStrings.Id, true);
        public static Section DoubleBass => new(Guid.Parse("e45ec6fa-7595-4084-9e01-991746b7f5e9"), "Double Bass", LowStrings.Id, true);

        // BAND
        public static Section Band => new(Guid.Parse("1994cb6c-877e-4d7c-aeca-26e68967c2ab"), "Band", Performers.Id, false);
        public static Section ElectricGuitarBand => new(Guid.Parse("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"), "Electric Guitar (Band)", Band.Id, true, 2);
        public static Section ElectricBassBand => new(Guid.Parse("454c2ad6-e3c8-428a-b74e-c73873159c0e"), "Electric Bass (Band)", Band.Id, true);
        public static Section DrumSetBand => new(Guid.Parse("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), "Drum Set (Band)", Band.Id, true);
        public static Section KeyboardsBand => new(Guid.Parse("7f811b88-e7db-461a-af5d-e249b1ce9e7d"), "Keyboards (Band)", Band.Id, true);

        // SOLOISTS
        public static Section Soloists => new(Guid.Parse("e0fdb057-c9b7-4477-be75-cbf920a26af6"), "Soloists", Performers.Id, false);



        // MEMBERS
        public static Section Members => new(Guid.Parse("067647c0-3f25-449e-9212-03f39fa88f0f"), "Members", null, false);

        // VISITORS
        public static Section Visitors => new(Guid.Parse("b58d047f-ec04-41e9-a728-06a8a160f55b"), "Visitors", null, false);

        // VOLUNTEERS
        public static Section Volunteers => new(Guid.Parse("75f593aa-fd20-4c05-9300-b31dbb90712e"), "Volunteers", null, false);

        // SUPPLIERS
        public static Section Suppliers => new(Guid.Parse("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"), "Suppliers", null, false);

        // CONTRACTORS
        public static Section Contractors => new(Guid.Parse("6a107070-daae-41fc-b27d-416d44d36374"), "Contractors", null, false);


    }
}