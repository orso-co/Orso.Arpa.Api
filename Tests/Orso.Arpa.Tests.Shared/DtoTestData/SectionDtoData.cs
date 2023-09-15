using System;
using System.Collections.Generic;
using Orso.Arpa.Application.SectionApplication.Model;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class SectionDtoData
    {
        public static IList<SectionDto> Sections => new List<SectionDto>
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
                            Bass,

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
                    SoloSoprano,
                    SoloMezzoSoprano,
                    SoloAlto,
                    SoloTenor,
                    SoloBaritone,
                    SoloBass,
                    Narrator,
                    Moderator,
                    SoloVocal,

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

        public static IList<SectionDto> InstrumentsWithChildren => new List<SectionDto>
        {
            Soprano,
            MezzoSoprano,
            Alto,
            Tenor,
            Baritone,
            Bass,
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
            Horn,
            WagnerTuba,
            Trumpet,
            Flugelhorn,
            PiccoloTrumpet,
            SopranoCornet,
            Cornet,
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
            Timpani,
            Mallets,
            Glockenspiel,
            Vibraphone,
            Xylophone,
            Marimbaphone,
            Harp,
            Keyboards,
            Piano,
            Celesta,
            Cembalo,
            Organ,
            Synthesizer,
            Accordion,
            Bandoneon,
            DrumSet,
            Guitars,
            AcousticGuitar,
            ElectricGuitar,
            ElectricBass,
            GlassHarp,
            Bagpipes,
            Didgeridoo,
            Violin,
            Viola,
            Violoncello,
            DoubleBass,
            ElectricGuitarBand,
            ElectricBassBand,
            DrumSetBand,
            KeyboardsBand,
            SoloSoprano,
            SoloMezzoSoprano,
            SoloAlto,
            SoloTenor,
            SoloBaritone,
            SoloBass,
            Narrator,
            Moderator,
            SoloVocal,
            Conductor,
            AssistantConductor,
            Repetiteur,
            VocalCoach
        };

        public static SectionDto Performers => new() { Id = Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"), Name = "Performers", };
        public static SectionDto Conductor => new() { Id = Guid.Parse("4E7A61C5-D2E4-4E3B-B21D-34A90CF958B2"), Name = "Conductor", };
        public static SectionDto AssistantConductor => new() { Id = Guid.Parse("18f1e750-f50d-4f06-8205-21203981bff6"), Name = "Assistant Conductor", };
        public static SectionDto Repetiteur => new() { Id = Guid.Parse("6fc908f0-da26-4237-80ca-dfe30453123c"), Name = "Répétiteur", };
        public static SectionDto VocalCoach => new() { Id = Guid.Parse("94c42496-fdb6-4341-b82f-735fd1706d39"), Name = "Vocal Coach", };
        public static SectionDto Choir => new() { Id = Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"), Name = "Choir", };
        public static SectionDto FemaleVoices => new() { Id = Guid.Parse("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"), Name = "Female Voices", };
        public static SectionDto HighFemaleVoices => new() { Id = Guid.Parse("5d469fc5-b3e6-40b8-9fa9-542981083ce3"), Name = "High Female Voices", };
        public static SectionDto Soprano => new() { Id = Guid.Parse("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), Name = "Soprano", InstrumentPartCount = 2 };
        public static SectionDto MezzoSoprano => new() { Id = Guid.Parse("eb42b2f7-413e-4c1a-ab79-23c74b02d054"), Name = "Mezzo Soprano", };
        public static SectionDto LowFemaleVoices => new() { Id = Guid.Parse("48337b78-70f0-493e-911b-296632b06ef8"), Name = "Low Female Voices", };
        public static SectionDto Alto => new() { Id = Guid.Parse("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), Name = "Alto", InstrumentPartCount = 2 };
        public static SectionDto MaleVoices => new() { Id = Guid.Parse("4599103d-f220-4744-92d1-7c6993e9bda4"), Name = "Male Voices", };
        public static SectionDto HighMaleVoices => new() { Id = Guid.Parse("7924daef-2542-4648-a42f-4c4374ee09db"), Name = "High Male Voices", };
        public static SectionDto Tenor => new() { Id = Guid.Parse("1579d7e7-4f55-4532-a078-69fd1ec939da"), Name = "Tenor", InstrumentPartCount = 2 };
        public static SectionDto LowMaleVoices => new() { Id = Guid.Parse("b9673cfd-7cdb-472c-86e0-1304cbb3840a"), Name = "Low Male Voices", };
        public static SectionDto Baritone => new() { Id = Guid.Parse("bb647161-8394-47d3-9f43-825762a70fc2"), Name = "Baritone", };
        public static SectionDto Bass => new() { Id = Guid.Parse("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), Name = "Bass", InstrumentPartCount = 2 };
        public static SectionDto Orchestra => new() { Id = Guid.Parse("308659d6-6014-4d2c-a62a-be75bf202e62"), Name = "Orchestra", };
        public static SectionDto Winds => new() { Id = Guid.Parse("b289cfe7-d66e-48d8-83a9-f4b1f7710863"), Name = "Winds", };
        public static SectionDto Woodwinds => new() { Id = Guid.Parse("a6abdeec-8185-40ac-a418-2e422bb9adbd"), Name = "Woodwinds", };
        public static SectionDto Flute => new() { Id = Guid.Parse("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), Name = "Flute", InstrumentPartCount = 4 };
        public static SectionDto PiccoloFlute => new() { Id = Guid.Parse("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"), Name = "Piccolo Flute", InstrumentPartCount = 2 };
        public static SectionDto AltoFlute => new() { Id = Guid.Parse("f9c1924b-2b45-459c-b919-99059cb41e73"), Name = "Alto Flute", };
        public static SectionDto TenorFlute => new() { Id = Guid.Parse("D0A18A79-AD5A-450D-92CC-20A58496AAF0"), Name = "Tenor Flute", };
        public static SectionDto BassFlute => new() { Id = Guid.Parse("fc66c8b8-d9de-4ff0-a695-37e717103686"), Name = "Bass Flute", };
        public static SectionDto Oboe => new() { Id = Guid.Parse("2327a9c3-2c6f-41b7-9045-bb00af798b42"), Name = "Oboe", InstrumentPartCount = 4 };
        public static SectionDto OboeDAmore => new() { Id = Guid.Parse("4e71ffc3-e086-4c16-a932-3d80fd302971"), Name = "Oboe d'Amore", };
        public static SectionDto EnglishHorn => new() { Id = Guid.Parse("abe0d27b-2c99-4755-891c-fb0b91f19bb6"), Name = "English Horn", InstrumentPartCount = 2 };
        public static SectionDto BaritonOboe => new() { Id = Guid.Parse("2f8d732f-bf82-4a62-86a1-62bffd708189"), Name = "Bariton Oboe", };
        public static SectionDto Clarinet => new() { Id = Guid.Parse("cdc390d5-0649-441d-a086-df2e3b9d3512"), Name = "Clarinet", InstrumentPartCount = 4 };
        public static SectionDto EbClarinet => new() { Id = Guid.Parse("d2551427-d727-42d9-be0e-dea2ae82f2d6"), Name = "Eb Clarinet", InstrumentPartCount = 2 };
        public static SectionDto AltoClarinet => new() { Id = Guid.Parse("BE75913A-9703-4A8D-9E07-7A8D32C459F8"), Name = "Alto Clarinet", };
        public static SectionDto Bassetthorn => new() { Id = Guid.Parse("8c0a80d1-5889-4794-89b6-b80a3828aa5b"), Name = "Basset Horn", InstrumentPartCount = 2 };
        public static SectionDto BassClarinet => new() { Id = Guid.Parse("5109e464-7b01-40bd-a5e0-398ac3d1bb83"), Name = "Bass Clarinet", InstrumentPartCount = 2 };
        public static SectionDto DoubleBassClarinet => new() { Id = Guid.Parse("a5cc5e9d-b318-4edc-af84-ff3d701d0bcb"), Name = "Double Bass Clarinet", };
        public static SectionDto Bassoon => new() { Id = Guid.Parse("5c14f673-13f2-488f-8c21-7286d3ee10c3"), Name = "Bassoon", InstrumentPartCount = 4 };
        public static SectionDto ContraBassoon => new() { Id = Guid.Parse("8d01524c-7c22-4a20-8f26-711d11addbfd"), Name = "Contra Bassoon", InstrumentPartCount = 2 };
        public static SectionDto ContraForte => new() { Id = Guid.Parse("7cb00d2e-5a98-4b68-b775-3b5d1f267d96"), Name = "Contraforte", };
        public static SectionDto Saxophone => new() { Id = Guid.Parse("566260fb-b6be-41dc-956d-4070d30fa88d"), Name = "Saxophone", };
        public static SectionDto SopranoSaxophone => new() { Id = Guid.Parse("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"), Name = "Soprano Saxophone", };
        public static SectionDto AltoSaxophone => new() { Id = Guid.Parse("4a31447d-63c2-4e28-ab39-255a956fbe18"), Name = "Alto Saxophone", InstrumentPartCount = 3 };
        public static SectionDto TenorSaxophone => new() { Id = Guid.Parse("da998fcb-92b9-4828-976e-826e97e05cb3"), Name = "Tenor Saxophone", InstrumentPartCount = 3 };
        public static SectionDto BaritoneSaxophone => new() { Id = Guid.Parse("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"), Name = "Baritone Saxophone", };
        public static SectionDto BassSaxophone => new() { Id = Guid.Parse("fb4f9841-294a-4b6c-bfec-02d3735b1ea0"), Name = "Bass Saxophone", };
        public static SectionDto Brass => new() { Id = Guid.Parse("f4c70178-d069-44dc-8956-7160c5fef52e"), Name = "Brass", };
        public static SectionDto HighBrass => new() { Id = Guid.Parse("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f"), Name = "High Brass", };
        public static SectionDto Horn => new() { Id = Guid.Parse("b9532add-efec-4510-831c-902c32ef7dbb"), Name = "Horn", InstrumentPartCount = 8 };
        public static SectionDto WagnerTuba => new() { Id = Guid.Parse("c42591db-4e41-413f-8b98-6607e2f12e39"), Name = "Wagner Tuba", InstrumentPartCount = 2 };
        public static SectionDto Trumpet => new() { Id = Guid.Parse("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), Name = "Trumpet", InstrumentPartCount = 8 };
        public static SectionDto Flugelhorn => new() { Id = Guid.Parse("69e64d64-419f-4f9c-9948-a117b02ff198"), Name = "Flugelhorn", InstrumentPartCount = 3 };
        public static SectionDto PiccoloTrumpet => new() { Id = Guid.Parse("2393549e-5b16-4414-a896-3cebb7bcc9df"), Name = "Piccolo Trumpet", };
        public static SectionDto SopranoCornet => new() { Id = Guid.Parse("290F84D4-BB3F-41C3-9F42-C649C8EEEA26"), Name = "Soprano Cornet", };
        public static SectionDto Cornet => new() { Id = Guid.Parse("305C06E0-B99F-4F91-AE83-869D8B25C63D"), Name = "Cornet", InstrumentPartCount = 3 };
        public static SectionDto LowBrass => new() { Id = Guid.Parse("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041"), Name = "Low Brass", };
        public static SectionDto Trombone => new() { Id = Guid.Parse("e20ce055-5715-42f4-97e6-4025559b15f7"), Name = "Trombone", InstrumentPartCount = 4 };
        public static SectionDto AltoTrombone => new() { Id = Guid.Parse("80f15184-6417-476a-87ac-0f752d011391"), Name = "Alto Trombone", };
        public static SectionDto BassTrombone => new() { Id = Guid.Parse("da660c21-0151-4255-a81b-4d25fede199b"), Name = "Bass Trombone", InstrumentPartCount = 2 };
        public static SectionDto DoubleBassTrombone => new() { Id = Guid.Parse("32F3FDD9-9517-4DB5-856E-376E9FA52B84"), Name = "Double Bass Trombone", };
        public static SectionDto Euphonium => new() { Id = Guid.Parse("554fd3db-110b-4335-bc2a-1d5070f6621a"), Name = "Euphonium", InstrumentPartCount = 3 };
        public static SectionDto TenorHorn => new() { Id = Guid.Parse("803219AA-1A32-4A68-95AE-348BD487135A"), Name = "Tenor Horn", InstrumentPartCount = 3 };
        public static SectionDto BaritoneHorn => new() { Id = Guid.Parse("B525E539-7FA4-49D7-AE93-EC0748022D4D"), Name = "Baritone Horn", InstrumentPartCount = 3 };
        public static SectionDto Tuba => new() { Id = Guid.Parse("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), Name = "Tuba", InstrumentPartCount = 2 };
        public static SectionDto EbTuba => new() { Id = Guid.Parse("2FABD3A1-D398-4108-A74F-2665710133D1"), Name = "Eb Tuba", InstrumentPartCount = 2 };
        public static SectionDto FTuba => new() { Id = Guid.Parse("31A2B9BF-0C2B-47EC-B8BC-34C9423B74D4"), Name = "F Tuba", InstrumentPartCount = 2 };
        public static SectionDto Percussion => new() { Id = Guid.Parse("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), Name = "Percussion", };
        public static SectionDto Timpani => new() { Id = Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), Name = "Timpani", InstrumentPartCount = 2 };
        public static SectionDto Mallets => new() { Id = Guid.Parse("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), Name = "Mallets", };
        public static SectionDto Glockenspiel => new() { Id = Guid.Parse("dcf267e6-5b58-4534-8e4b-a8c5747b1816"), Name = "Glockenspiel", };
        public static SectionDto Vibraphone => new() { Id = Guid.Parse("852d8129-a5b7-4378-ad9c-df89dc878b4f"), Name = "Vibraphone", };
        public static SectionDto Xylophone => new() { Id = Guid.Parse("2804ed14-7b73-4e17-bd21-edd048a60cb4"), Name = "Xylophone", };
        public static SectionDto Marimbaphone => new() { Id = Guid.Parse("bb0715dc-7f9d-4ddb-b5f5-9e7806e1069f"), Name = "Marimbaphone", };
        public static SectionDto DrumSet => new() { Id = Guid.Parse("C15C3649-D7BB-4BBF-BDD3-F6146EBC825C"), Name = "Drum Set (Orchestra)", };
        public static SectionDto Others => new() { Id = Guid.Parse("c9403ca4-6b75-44c3-b567-e53bbd78fb75"), Name = "Others", };
        public static SectionDto Harp => new() { Id = Guid.Parse("0cf93477-f42f-46c3-8e3d-45ccdc54ad8c"), Name = "Harp", InstrumentPartCount = 2 };
        public static SectionDto Keyboards => new() { Id = Guid.Parse("614a8fd0-acfa-4268-b716-3b35a6a17b7a"), Name = "Keyboards", };
        public static SectionDto Piano => new() { Id = Guid.Parse("8ed82e0e-0354-4192-8f26-5a2437e9208d"), Name = "Piano", InstrumentPartCount = 2 };
        public static SectionDto Celesta => new() { Id = Guid.Parse("bc6cfeb7-569d-4c22-8e80-647aed560bf0"), Name = "Celesta", };
        public static SectionDto Cembalo => new() { Id = Guid.Parse("f6af00f5-e81c-4d85-aadd-1e33748e9a64"), Name = "Cembalo", };
        public static SectionDto Organ => new() { Id = Guid.Parse("182019da-bde2-44d7-8c77-88cfb0ce428c"), Name = "Organ", };
        public static SectionDto Synthesizer => new() { Id = Guid.Parse("d22fb8aa-7d38-42c4-9586-30e559f63799"), Name = "Synthesizer", };
        public static SectionDto Accordion => new() { Id = Guid.Parse("76891771-b5f2-4666-8972-ba7f494fc9de"), Name = "Accordion", };
        public static SectionDto Bandoneon => new() { Id = Guid.Parse("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"), Name = "Bandoneon", };
        public static SectionDto Guitars => new() { Id = Guid.Parse("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"), Name = "Guitars", };
        public static SectionDto AcousticGuitar => new() { Id = Guid.Parse("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"), Name = "Acoustic Guitar (Orchestra)", InstrumentPartCount = 2 };
        public static SectionDto ElectricGuitar => new() { Id = Guid.Parse("ed0829d0-d978-430e-96ec-b93cf75f3fd6"), Name = "Electric Guitar (Orchestra)", InstrumentPartCount = 2 };
        public static SectionDto ElectricBass => new() { Id = Guid.Parse("9cd74865-f82a-4be9-afc1-384fb25b7fe4"), Name = "Electric Bass (Orchestra)", };
        public static SectionDto GlassHarp => new() { Id = Guid.Parse("08bc313b-d0dd-4b78-bdbf-d976682d965e"), Name = "GlassHarp", InstrumentPartCount = 2 };
        public static SectionDto Bagpipes => new() { Id = Guid.Parse("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"), Name = "Bagpipes", };
        public static SectionDto Didgeridoo => new() { Id = Guid.Parse("8903B8C5-0EF8-48FD-9C2B-71FBAE827965"), Name = "Didgeridoo", };
        public static SectionDto Strings => new() { Id = Guid.Parse("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"), Name = "Strings", };
        public static SectionDto HighStrings => new() { Id = Guid.Parse("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8"), Name = "High Strings", };
        public static SectionDto Violin => new() { Id = Guid.Parse("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), Name = "Violin", InstrumentPartCount = 2 };
        public static SectionDto Viola => new() { Id = Guid.Parse("df541ea1-a5fd-4975-b6fd-7cd652a79073"), Name = "Viola", };
        public static SectionDto LowStrings => new() { Id = Guid.Parse("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f"), Name = "Low Strings", };
        public static SectionDto Violoncello => new() { Id = Guid.Parse("d8686f68-78da-4022-b0b8-97e0c263d694"), Name = "Violoncello", };
        public static SectionDto DoubleBass => new() { Id = Guid.Parse("e45ec6fa-7595-4084-9e01-991746b7f5e9"), Name = "Double Bass", };
        public static SectionDto Band => new() { Id = Guid.Parse("1994cb6c-877e-4d7c-aeca-26e68967c2ab"), Name = "Band", };
        public static SectionDto ElectricGuitarBand => new() { Id = Guid.Parse("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"), Name = "Electric Guitar (Band)", InstrumentPartCount = 2 };
        public static SectionDto ElectricBassBand => new() { Id = Guid.Parse("454c2ad6-e3c8-428a-b74e-c73873159c0e"), Name = "Electric Bass (Band)", };
        public static SectionDto DrumSetBand => new() { Id = Guid.Parse("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), Name = "Drum Set (Band)", };
        public static SectionDto KeyboardsBand => new() { Id = Guid.Parse("7f811b88-e7db-461a-af5d-e249b1ce9e7d"), Name = "Keyboards (Band)", };
        public static SectionDto Soloists => new() { Id = Guid.Parse("e0fdb057-c9b7-4477-be75-cbf920a26af6"), Name = "Soloists" };
        public static SectionDto SoloSoprano => new()
        {
            Id = Guid.Parse("54cef8d8-e891-4d27-be25-94e44d3c365a"),
            Name = "Soprano (Soloist)"
        };
        public static SectionDto SoloMezzoSoprano => new()
        {
            Id = Guid.Parse("e84ffc93-fc24-481c-916f-b5aef4ba2d1f"),
            Name = "Mezzo Soprano (Soloist)"
        };
        public static SectionDto SoloAlto => new()
        {
            Id = Guid.Parse("d0762cb0-4a6b-4935-b560-af4f148c949a"),
            Name = "Alto (Soloist)"
        };
        public static SectionDto SoloTenor => new()
        {
            Id = Guid.Parse("71738278-1583-4875-9830-b182043e4ac3"),
            Name = "Tenor (Soloist)"
        };
        public static SectionDto SoloBaritone => new()
        {
            Id = Guid.Parse("08afd287-82b6-4259-b4f4-c40b78d3b69d"),
            Name = "Baritone (Soloist)"
        };
        public static SectionDto SoloBass => new()
        {
            Id = Guid.Parse("d1f8bd21-efa8-41d8-96ac-fe87e2b0092f"),
            Name = "Bass (Soloist)"
        };
        public static SectionDto Narrator => new()
        {
            Id = Guid.Parse("5c9d7048-1c80-4e16-b783-e39cd99dfc89"),
            Name = "Narrator"
        };
        public static SectionDto Moderator => new()
        {
            Id = Guid.Parse("f33d5126-4cd8-41d7-8d35-4c188591ec3b"),
            Name = "Moderator"
        };
        public static SectionDto SoloVocal => new()
        {
            Id = Guid.Parse("ac157b00-106e-4277-99f1-9404f0df96b8"),
            Name = "Vocal (Soloist)"
        };
        public static SectionDto Members => new() { Id = Guid.Parse("067647c0-3f25-449e-9212-03f39fa88f0f"), Name = "Members", };
        public static SectionDto Visitors => new() { Id = Guid.Parse("b58d047f-ec04-41e9-a728-06a8a160f55b"), Name = "Visitors", };
        public static SectionDto Volunteers => new() { Id = Guid.Parse("75f593aa-fd20-4c05-9300-b31dbb90712e"), Name = "Volunteers", };
        public static SectionDto Suppliers => new() { Id = Guid.Parse("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"), Name = "Suppliers", };
        public static SectionDto Contractors => new() { Id = Guid.Parse("6a107070-daae-41fc-b27d-416d44d36374"), Name = "Contractors", };
    }
}
