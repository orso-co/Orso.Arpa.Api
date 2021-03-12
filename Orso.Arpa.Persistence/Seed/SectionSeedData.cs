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
                        Choir,
                            FemaleVoices,
                                DeepFemaleVoices,
                                    Alto,
                                        Alto1,
                                        Alto2,
                                HighFemaleVoices,
                                   Soprano,
                                        Soprano1,
                                        Soprano2,
                                    MezzoSoprano,
                            MaleVoices,
                                DeepMaleVoices,
                                    Baritone,
                                    Bass,
                                        Bass1,
                                        Bass2,

                                HighMaleVoices,
                                        Tenor,
                                        Tenor1,
                                        Tenor2,

                        Orchestra,
                            Winds,
                                Woodwind,
                                    Flute,
                                        PiccoloFlute,
                                        AltoFlute,
                                    Oboe,
                                        EnglishHorn,
                                    Clarinet,
                                        EbClarinet,
                                        BassClarinet,
                                    Bassoon,
                                        ContraBassoon,
                                Brass,
                                    Horn,
                                        WagnerTuba,
                                    Trumpet,
                                        Flugelhorn,
                                    Trombone,
                                        AltoTrombone,
                                        BassTrombone,
                                    Euphonium,
                                    Tuba,
// Percussion noch nicht fertig!
                                Percussion,
                                    Timpani,
                                    Mallets,
                                        Glockenspiel,
                                        Marimbaphone,
                                        Xylophone,
                                        Vibraphone,
                                Others,
                                    Keyboards,
                                        Piano,
                                        Cembalo,
                                        Celesta,
                                        Organ,
                                        Synthesizer,
                                    Guitars,
                                        AcousticGuitar,
                                        ElectricGuitar,
                                        ElectricBass,
                                    DrumSet,
                                    GlassHarp,
                                    Accordion,
                                    Bandoneon,
                                    Bagpipes,

                                Strings,
                                    ViolinI,
                                    ViolinII,
                                    Viola,
                                    Violoncello,
                                    DoubleBass,

/* Soloists - work in progress
                        Soloists,
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

                    Visitors,
                    Members,
                    Suppliers,
                    Contractors,

                };

// PERFORMERS
    public static Section Performers => new Section(Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"), "Performers", null);

    // CHOIR VOICES

        public static Section Choir => new Section(Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"), "Choir", Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"));
            public static Section FemaleVoices => new Section(Guid.Parse("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"), "Female Voices", Choir.Id);
                public static Section HighFemaleVoices => new Section(Guid.Parse("5d469fc5-b3e6-40b8-9fa9-542981083ce3"), "High Female Voices", FemaleVoices.Id);
                    public static Section Soprano => new Section(Guid.Parse("7daa1394-a70d-4a24-88a6-ccf511d75c4d"), "Soprano", HighFemaleVoices.Id);
                    public static Section Soprano1 => new Section(Guid.Parse("8470ddf0-43ab-477e-b3bc-47ede014b359"), "Soprano 1", Soprano.Id);
                    public static Section Soprano2 => new Section(Guid.Parse("22d7cf92-7b29-4cf1-a6fa-2954377589b4"), "Soprano 2", Soprano.Id);
                    public static Section MezzoSoprano => new Section(Guid.Parse("eb42b2f7-413e-4c1a-ab79-23c74b02d054"), "Mezzo Soprano", HighFemaleVoices.Id);
            public static Section DeepFemaleVoices => new Section(Guid.Parse("48337b78-70f0-493e-911b-296632b06ef8"), "Deep Female Voices", FemaleVoices.Id);
                    public static Section Alto => new Section(Guid.Parse("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"), "Alto", DeepFemaleVoices.Id);
                        public static Section Alto1 => new Section(Guid.Parse("e809ee90-23f9-44de-b80e-2fddd5ee3683"), "Alto 1", Alto.Id);
                        public static Section Alto2 => new Section(Guid.Parse("50dfa2be-85e2-4638-aa53-22dadc97a844"), "Alto 2", Alto.Id);
            public static Section MaleVoices => new Section(Guid.Parse("4599103d-f220-4744-92d1-7c6993e9bda4"), "Male Voices", Choir.Id);
                public static Section HighMaleVoices => new Section(Guid.Parse("7924daef-2542-4648-a42f-4c4374ee09db"), "High Male Voices", MaleVoices.Id);
                    public static Section Tenor => new Section(Guid.Parse("1579d7e7-4f55-4532-a078-69fd1ec939da"), "Tenor", HighMaleVoices.Id);
                    public static Section Tenor1 => new Section(Guid.Parse("3db46ff0-9165-46cc-8f28-6a1d52dee518"), "Tenor 1", Tenor.Id);
                    public static Section Tenor2 => new Section(Guid.Parse("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"), "Tenor 2", Tenor.Id);
        public static Section DeepMaleVoices => new Section(Guid.Parse("b9673cfd-7cdb-472c-86e0-1304cbb3840a"), "Deep Male Voices", MaleVoices.Id);
        public static Section Baritone => new Section(Guid.Parse("bb647161-8394-47d3-9f43-825762a70fc2"), "Baritone", DeepMaleVoices.Id);
        public static Section Bass => new Section(Guid.Parse("e7dd10ef-1c39-4440-9a6c-65d397f010ca"), "Basso", DeepMaleVoices.Id);
        public static Section Bass1 => new Section(Guid.Parse("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"), "Basso 1", Bass.Id);
        public static Section Bass2 => new Section(Guid.Parse("61fa66ec-3103-43fe-800c-930547dff82c"), "Basso 2", Bass.Id);

    // ORCHESTRAL INSTRUMENTS
        public static Section Orchestra => new Section(Guid.Parse("308659d6-6014-4d2c-a62a-be75bf202e62"), "Orchestra", Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"));
        // WINDS
        public static Section Winds => new Section(Guid.Parse("b289cfe7-d66e-48d8-83a9-f4b1f7710863"), "Winds", Orchestra.Id);
            // WOODWINDS
            public static Section Woodwind => new Section(Guid.Parse("a6abdeec-8185-40ac-a418-2e422bb9adbd"), "Woodwind", Winds.Id);
            public static Section Flute => new Section(Guid.Parse("d6961f83-e792-4ddf-b91a-ae0867caeb3b"), "Flute", Woodwind.Id);
                public static Section PiccoloFlute => new Section(Guid.Parse("ec8aeaf8-f370-4ac8-bd12-ccce0cbcfa0f"), "Piccolo Flute", Flute.Id);
                public static Section AltoFlute => new Section(Guid.Parse("f9c1924b-2b45-459c-b919-99059cb41e73"), "Alto Flute", Flute.Id);
                public static Section BassFlute => new Section(Guid.Parse("fc66c8b8-d9de-4ff0-a695-37e717103686"), "Bass Flute", Flute.Id);
            public static Section Oboe => new Section(Guid.Parse("2327a9c3-2c6f-41b7-9045-bb00af798b42"), "Oboe", Woodwind.Id);
                public static Section OboeDAmore => new Section (Guid.Parse("4e71ffc3-e086-4c16-a932-3d80fd302971"), "Oboe d'Amore", Oboe.Id);
                public static Section EnglishHorn => new Section(Guid.Parse("abe0d27b-2c99-4755-891c-fb0b91f19bb6"), "English Horn", Oboe.Id);
                public static Section BaritonOboe => new Section(Guid.Parse("2f8d732f-bf82-4a62-86a1-62bffd708189"), "Bariton Oboe", Oboe.Id);
            public static Section Clarinet => new Section(Guid.Parse("cdc390d5-0649-441d-a086-df2e3b9d3512"), "Clarinet", Woodwind.Id);
                public static Section EbClarinet => new Section(Guid.Parse("d2551427-d727-42d9-be0e-dea2ae82f2d6"), "Eb Clarinet", Clarinet.Id);
                public static Section Bassetthorn => new Section(Guid.Parse("8c0a80d1-5889-4794-89b6-b80a3828aa5b"), "Basset Horn", Clarinet.Id);
                public static Section BassClarinet => new Section(Guid.Parse("5109e464-7b01-40bd-a5e0-398ac3d1bb83"), "Bass Clarinet", Clarinet.Id);
                public static Section DoubleBassClarinet => new Section(Guid.Parse("a5cc5e9d-b318-4edc-af84-ff3d701d0bcb"), "Double Bass Clarinet", Clarinet.Id);
            public static Section Bassoon => new Section(Guid.Parse("5c14f673-13f2-488f-8c21-7286d3ee10c3"), "Bassoon", Woodwind.Id);
                public static Section ContraBassoon => new Section(Guid.Parse("8d01524c-7c22-4a20-8f26-711d11addbfd"), "Contra Bassoon", Bassoon.Id);
                public static Section ContraForte => new Section(Guid.Parse("7cb00d2e-5a98-4b68-b775-3b5d1f267d96"), "Contraforte", Bassoon.Id);
            public static Section Saxophone => new Section(Guid.Parse("566260fb-b6be-41dc-956d-4070d30fa88d"), "Saxophone", Woodwind.Id);
                public static Section SopranoSaxophone => new Section(Guid.Parse("b5d01e60-af61-4d29-bfb3-2f0dbac1e2fb"), "Soprano Saxophone", Saxophone.Id);
                public static Section AltoSaxophone => new Section(Guid.Parse("4a31447d-63c2-4e28-ab39-255a956fbe18"), "Alto Saxophone", Saxophone.Id);
                public static Section TenorSaxophone => new Section(Guid.Parse("da998fcb-92b9-4828-976e-826e97e05cb3"), "Tenor Saxophone", Saxophone.Id);
                public static Section BaritoneSaxophone => new Section(Guid.Parse("e4622ea3-f6a0-40b2-ac80-a2c9df099aeb"), "Baritone Saxophone", Saxophone.Id);
                public static Section BassSaxophone => new Section(Guid.Parse("fb4f9841-294a-4b6c-bfec-02d3735b1ea0"), "Bass Saxophone", Saxophone.Id);
            // BRASS
            public static Section Brass => new Section(Guid.Parse("f4c70178-d069-44dc-8956-7160c5fef52e"), "Brass", Winds.Id);
                public static Section HighBrass => new Section(Guid.Parse("7d0d2295-df8a-4cfa-9f43-87dbf9fc133f"), "High Brass", Brass.Id);
                    public static Section Horn => new Section(Guid.Parse("b9532add-efec-4510-831c-902c32ef7dbb"), "Horn", HighBrass.Id);
                        public static Section WagnerTuba => new Section(Guid.Parse("c42591db-4e41-413f-8b98-6607e2f12e39"), "Wagner Tuba", Horn.Id);
                    public static Section Trumpet => new Section(Guid.Parse("205b0a0e-1a36-48e9-8b45-df37dc5effa5"), "Trumpet", HighBrass.Id);
                        public static Section Flugelhorn => new Section(Guid.Parse("b9532add-efec-4510-831c-902c32ef7dbb"), "Flugelhorn", Trumpet.Id);
                        public static Section PiccoloTrumpet => new Section(Guid.Parse("2393549e-5b16-4414-a896-3cebb7bcc9df"), "Piccolo Trumpet", Trumpet.Id);
                public static Section LowBrass => new Section(Guid.Parse("e4e7239e-0d0d-4a30-93b6-8a61e3ab8041"), "Low Brass", Brass.Id);
                    public static Section Trombone => new Section(Guid.Parse("e20ce055-5715-42f4-97e6-4025559b15f7"), "Trombone", LowBrass.Id);
                        public static Section AltoTrombone => new Section(Guid.Parse("80f15184-6417-476a-87ac-0f752d011391"), "Alto Trombone", Trombone.Id);
                        public static Section BassTrombone => new Section(Guid.Parse("da660c21-0151-4255-a81b-4d25fede199b"), "Bass Trombone", Trombone.Id);
                    public static Section Euphonium => new Section(Guid.Parse("554fd3db-110b-4335-bc2a-1d5070f6621a"), "Euphonium", LowBrass.Id);
                    public static Section Tuba => new Section(Guid.Parse("18cbded8-0d64-4e0e-bc19-d6903e0fd5a9"), "Tuba", LowBrass.Id);

        // PERCUSSION
            public static Section Percussion => new Section(Guid.Parse("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"), "Percussion", Orchestra.Id);
            public static Section Timpani => new Section(Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), "Timpani", Percussion.Id);
            public static Section Mallets => new Section(Guid.Parse("d12ebc93-4b55-455c-a9db-a826fca9a1f2"), "Mallets", Percussion.Id);
                public static Section Glockenspiel => new Section(Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), "Glockenspiel", Mallets.Id);
                public static Section Vibraphone => new Section(Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), "Vibraphone", Mallets.Id);
                public static Section Xylophone => new Section(Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), "Xylophone", Mallets.Id);
                public static Section Marimbaphone => new Section(Guid.Parse("ea916a8d-1bce-4e87-b5b0-ff6304bb01a5"), "Marimbaphone", Mallets.Id);
            public static Section DrumSet => new Section(Guid.Parse("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), "Drum Set (Orchestra)", Percussion.Id);

        // OTHERS
            public static Section Others => new Section(Guid.Parse("49966aee-18d0-4884-ad34-038ca5390b83"), "Others", Orchestra.Id);
                public static Section Harp => new Section(Guid.Parse("34cab62e-68d7-49d2-8f18-791f2bc6090f"), "Harp", Others.Id);
                public static Section Keyboards => new Section(Guid.Parse("2a777891-847a-4014-b801-639c0cacf18d"), "Keyboards", Others.Id);
                    public static Section Piano => new Section(Guid.Parse("76422714-9571-45d7-bb3a-567a95fd893d"), "Piano", Keyboards.Id);
                    public static Section Celesta => new Section(Guid.Parse("b1639d96-347b-4ee2-bef0-ab73a0194d8e"), "Celesta", Keyboards.Id);
                    public static Section Cembalo => new Section(Guid.Parse("78831a47-8469-4a0c-aa39-343f44a0bb09"), "Cembalo", Keyboards.Id);
                    public static Section Organ => new Section(Guid.Parse("370d72de-4185-4a91-a3e6-ea83c15ec51c"), "Organ", Keyboards.Id);
                    public static Section Synthesizer => new Section(Guid.Parse("d22fb8aa-7d38-42c4-9586-30e559f63799"), "Synthesizer", Keyboards.Id);
                public static Section Accordion => new Section(Guid.Parse("76891771-b5f2-4666-8972-ba7f494fc9de"), "Accordion", Others.Id);
                public static Section Bandoneon => new Section(Guid.Parse("d7ff1f62-e5c5-4662-823b-f77ff7706b4e"), "Bandoneon", Others.Id);
                public static Section Guitars => new Section(Guid.Parse("a22b6f19-3e9c-4389-824b-22db7b8cf8fd"), "Guitars", Others.Id);
                    public static Section AcousticGuitar => new Section(Guid.Parse("1d0ed0b3-b87b-439f-932e-616d7e03a0d6"), "Electric Guitar (Orchestra)", Guitars.Id);
                    public static Section ElectricGuitar => new Section(Guid.Parse("ed0829d0-d978-430e-96ec-b93cf75f3fd6"), "Electric Guitar (Orchestra)", Guitars.Id);
                    public static Section ElectricBass => new Section(Guid.Parse("9cd74865-f82a-4be9-afc1-384fb25b7fe4"), "Electric Bass (Orchestra)", Guitars.Id);
                public static Section GlassHarp => new Section(Guid.Parse("08bc313b-d0dd-4b78-bdbf-d976682d965e"), "GlassHarp", Others.Id);
                public static Section Bagpipes => new Section(Guid.Parse("0031e6f5-2d51-4e88-9e82-7bd2c8340cac"), "Bagpipes", Others.Id);


        // STRINGS
            public static Section Strings => new Section(Guid.Parse("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"), "Strings", Orchestra.Id);
                public static Section HighStrings => new Section(Guid.Parse("7cef5e36-fe7f-4acb-b17a-24feeac8d5f8"), "High Strings", Strings.Id);
                    public static Section Violins => new Section(Guid.Parse("fab9a49a-9fa4-4af3-9e40-e13bdc930513"), "Violins", HighStrings.Id);
                        public static Section ViolinI => new Section(Guid.Parse("eb5728b5-b1fd-4a70-8894-7bb152087837"), "Violin I", Violins.Id);
                        public static Section ViolinII => new Section(Guid.Parse("f3ee3c42-4e4e-411d-a839-6e0420bc35a3"), "Strings", Violins.Id);
                    public static Section Viola => new Section(Guid.Parse("df541ea1-a5fd-4975-b6fd-7cd652a79073"), "Strings", HighStrings.Id);
            public static Section LowStrings => new Section(Guid.Parse("fdd5d68c-2620-47a3-80e4-64fda6dc7e3f"), "Low Strings", Strings.Id);
                public static Section Violoncello => new Section(Guid.Parse("d8686f68-78da-4022-b0b8-97e0c263d694"), "Violoncello", LowStrings.Id);
                public static Section DoubleBass => new Section(Guid.Parse("e45ec6fa-7595-4084-9e01-991746b7f5e9"), "Double Bass", LowStrings.Id);

    // BAND
        public static Section Band => new Section(Guid.Parse("1994cb6c-877e-4d7c-aeca-26e68967c2ab"), "Band", Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"));
            public static Section ElectricGuitarBand => new Section(Guid.Parse("48833c1b-cbc1-43b2-a4c5-f1fa4289f5ab"), "Electric Guitar (Band)", Band.Id);
            public static Section ElectricBassBand => new Section(Guid.Parse("454c2ad6-e3c8-428a-b74e-c73873159c0e"), "Electric Bass (Band)", Band.Id);
            public static Section DrumSetBand => new Section(Guid.Parse("d787fe9a-2283-43f6-bbc8-8a098e1f1c81"), "Drum Set (Band)", Band.Id);
            public static Section KeyboardsBand => new Section(Guid.Parse("7f811b88-e7db-461a-af5d-e249b1ce9e7d"), "Keyboards (Band)", Band.Id);

    // SOLOISTS
            public static Section Soloists => new Section(Guid.Parse("e0fdb057-c9b7-4477-be75-cbf920a26af6"), "Soloists", Guid.Parse("8bba816f-2315-43c0-b18e-99a27b1c9668"));



// MEMBERS
        public static Section Members => new Section(Guid.Parse("067647c0-3f25-449e-9212-03f39fa88f0f"), "Members", null);

// VISITORS
        public static Section Visitors => new Section(Guid.Parse("b58d047f-ec04-41e9-a728-06a8a160f55b"), "Visitors", null);

// VOLUNTEERS
        public static Section Volunteers => new Section(Guid.Parse("75f593aa-fd20-4c05-9300-b31dbb90712e"), "Volunteers", null);

// SUPPLIERS
        public static Section Suppliers => new Section(Guid.Parse("13802d8b-4c73-4a52-8748-20bf3ba0c2b1"), "Suppliers", null);

// CONTRACTORS
        public static Section Contractors => new Section(Guid.Parse("6a107070-daae-41fc-b27d-416d44d36374"), "Contractors", null);


    }
}
