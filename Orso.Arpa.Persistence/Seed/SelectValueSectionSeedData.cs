using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class SelectValueSectionSeedData
    {
        public static IList<SelectValueSection> SelectValueSections
        {
            get
            {
                return new List<SelectValueSection>
                {
                    SopranoSolo,
                    SopranoSectionLead,
                    SopranoHigh,
                    SopranoLow,
                    SopranoCoach,
                    AltoSolo,
                    AltoSectionlead,
                    AltoHigh,
                    AltoLow,
                    AltoCoach,
                    TenorSolo,
                    TenorSectionLead,
                    TenorHigh,
                    TenorLow,
                    TenorCoach,
                    BassSolo,
                    BassSectionLead,
                    BassHigh,
                    BassLow,

                    FluteSolo,
                    FluteTutti,
                    FluteCoach,
                    OboeSolo,
                    OboeCoach,
                    ClarinetSolo,
                    ClarinetTutti,
                    ClarinetCoach,
                    BassoonSolo,
                    BassoonCoach,
                    ContraBassoonSolo,
                    SopranoSaxophoneSolo,
                    AltoSaxophoneSolo,
                    AltoSaxophoneCoach,
                    TenorSaxophoneSolo,
                    TenorSaxophoneCoach,
                    BaritoneSaxophoneSolo,

                    HornSolo,
                    HornLow,
                    HornHigh,
                    HornCoach,
                    TrumpetSolo,
                    TrumpetHigh,
                    TrumpetLow,
                    TrumpetCoach,
                    TromboneSolo,
                    TromboneCoach,
                    EuphoniumSolo,
                    EuphoniumCoach,
                    TubaSolo,
                    TubaCoach,

                    PercussionSolo,
                    PercussionSectionLead,
                    PercussionCoach,
                    TimpaniSolo,
                    TimpaniCoach,
                    MalletsSolo,
                    MalletsCoach,
                    HarpSolo,
                    HarpCoach,
                    PianoSolo,
                    PianoOrchestra,

                    ViolinSolo,
                    ViolinConcertmaster,
                    ViolinSecondConcertmaster,
                    ViolinSectionLead,
                    ViolinTutti,
                    ViolinCoach,
                    ViolaSolo,
                    ViolaSectionLead,
                    ViolaTutti,
                    ViolaCoach,
                    VioloncelloSolo,
                    VioloncelloSectionLead,
                    VioloncelloTutti,
                    VioloncelloCoach,
                    DoublebassSolo,
                    DoublebassSectionLead,
                    DoublebassTutti,
                    DoublebassCoach,
                };
            }
        }
        // Choir
        public static SelectValueSection SopranoSolo => new(Guid.Parse("5748698c-fc7f-437e-867c-d3c3dc4dcf4e"), SectionSeedData.Soprano.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection SopranoSectionLead => new(Guid.Parse("81e75718-d8dc-4316-bc7d-bac9da549245"), SectionSeedData.Soprano.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection SopranoHigh => new(Guid.Parse("3ecfed41-1b06-4dca-b3e1-ed84459e2493"), SectionSeedData.Soprano.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection SopranoLow => new(Guid.Parse("a08ba21d-c850-4485-aabc-c42a1a016953"), SectionSeedData.Soprano.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection SopranoCoach => new(Guid.Parse("497d2236-48a4-46a2-90c5-ef6f7d13f6a8"), SectionSeedData.Soprano.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection AltoSolo => new(Guid.Parse("640eaff9-0234-46cb-8dfe-2ba97399e6d3"), SectionSeedData.Alto.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection AltoSectionlead => new(Guid.Parse("7b01cc1c-15c7-4d66-8971-d2bf5507a676"), SectionSeedData.Alto.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection AltoHigh => new(Guid.Parse("de6a82d3-4374-491d-8125-dca3d55dcdf1"), SectionSeedData.Alto.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection AltoLow => new(Guid.Parse("f85ecc0c-f793-49ee-a7e1-780edde12ec5"), SectionSeedData.Alto.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection AltoCoach => new(Guid.Parse("6993ab28-3a79-4941-8a14-f07bdae5a3ba"), SectionSeedData.Alto.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection TenorSolo => new(Guid.Parse("9e5d6525-4916-4294-8ace-a2b698925c7f"), SectionSeedData.Tenor.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection TenorSectionLead => new(Guid.Parse("e0eadd53-5de4-4d3a-82ad-3551b9a22766"), SectionSeedData.Tenor.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection TenorHigh => new(Guid.Parse("c1e830ce-77c9-4253-af52-e6f350bfe479"), SectionSeedData.Tenor.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection TenorLow => new(Guid.Parse("abc02ea8-8785-49b4-b519-07cb02a10e06"), SectionSeedData.Tenor.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection TenorCoach => new(Guid.Parse("d5aa0e4e-ae90-4924-96be-05fb5459abe4"), SectionSeedData.Tenor.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection BassSolo => new(Guid.Parse("d0987cc0-f924-4d76-985f-b1e85be9e7b5"), SectionSeedData.Bass.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection BassSectionLead => new(Guid.Parse("4cb43aeb-68ac-4d2d-b66b-a3b252178c11"), SectionSeedData.Bass.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection BassHigh => new(Guid.Parse("2da6c9c0-3d83-4ee0-9c56-c9b3a8356081"), SectionSeedData.Bass.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection BassLow => new(Guid.Parse("f8aef705-7e10-4db9-9d2b-06b90194b7d2"), SectionSeedData.Bass.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection BassCoach => new(Guid.Parse("8f6d28f2-10f6-47c2-8259-aad9fb7a6f6b"), SectionSeedData.Bass.Id, SelectValueSeedData.Coach.Id);


        // Orchestra
        //Winds
        //Woodwinds

        public static SelectValueSection FluteSolo => new(Guid.Parse("1dee84b7-5cd3-4a6d-9819-2d507606398b"), SectionSeedData.Flute.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection FluteTutti => new(Guid.Parse("8da412fa-830e-4f16-a387-8e0e5a8bc5a9"), SectionSeedData.Flute.Id, SelectValueSeedData.Tutti.Id);
        public static SelectValueSection FluteCoach => new(Guid.Parse("1279d4e8-c50b-4835-93f0-5f31d7345770"), SectionSeedData.Flute.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection OboeSolo => new(Guid.Parse("5d335fff-919a-4deb-b313-9d0b7cfc7bde"), SectionSeedData.Oboe.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection OboeCoach => new(Guid.Parse("6f78a38f-2366-4ee2-bd5e-7b67f388b993"), SectionSeedData.Oboe.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection ClarinetSolo => new(Guid.Parse("c7b2bf38-3fb0-46a1-93c1-a41f3d865d96"), SectionSeedData.Clarinet.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection ClarinetTutti => new(Guid.Parse("1524b2d5-609c-41b2-bbd3-bba7cfa521f9"), SectionSeedData.Clarinet.Id, SelectValueSeedData.Tutti.Id);
        public static SelectValueSection ClarinetCoach => new(Guid.Parse("d3b924d1-68ad-429f-a6e4-fab48b251470"), SectionSeedData.Clarinet.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection BassoonSolo => new(Guid.Parse("1886d75e-26cd-49f1-8ad9-a35d6c1786fd"), SectionSeedData.Bassoon.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection BassoonCoach => new(Guid.Parse("7676806b-2f80-47f1-991f-b731b89182f0"), SectionSeedData.Bassoon.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection ContraBassoonSolo => new(Guid.Parse("50da7fa5-8d15-475c-8ebf-154aeac181d5"), SectionSeedData.ContraBassoon.Id, SelectValueSeedData.Solo.Id);

        public static SelectValueSection SopranoSaxophoneSolo => new(Guid.Parse("eec68681-b8d1-4142-9a82-c38cf342101d"), SectionSeedData.SopranoSaxophone.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection AltoSaxophoneSolo => new(Guid.Parse("024c5961-9f0d-4b1e-a695-39b3222635f9"), SectionSeedData.AltoSaxophone.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection AltoSaxophoneCoach => new(Guid.Parse("b1f7b38f-2624-4526-99a5-46c3eef1152b"), SectionSeedData.AltoSaxophone.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection TenorSaxophoneSolo => new(Guid.Parse("e383f6ee-eac0-4bca-85a6-e4f024c0db81"), SectionSeedData.TenorSaxophone.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection TenorSaxophoneCoach => new(Guid.Parse("d2297caf-03e0-44d9-979a-f4fbd53812fb"), SectionSeedData.TenorSaxophone.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection BaritoneSaxophoneSolo => new(Guid.Parse("bc61e9e1-c344-4269-a851-84af7b43db54"), SectionSeedData.BaritoneSaxophone.Id, SelectValueSeedData.Solo.Id);



        //Brass
        public static SelectValueSection HornSolo => new(Guid.Parse("4abea964-f83c-4973-a376-6e7782da6e7e"), SectionSeedData.Horn.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection HornHigh => new(Guid.Parse("b43fc897-ebcf-4d2a-8682-33b6337b5ab2"), SectionSeedData.Horn.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection HornLow => new(Guid.Parse("42525d3a-e158-44ee-88b5-1a4332a77862"), SectionSeedData.Horn.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection HornCoach => new(Guid.Parse("2e43c349-0a3b-4860-94fc-34e87a306845"), SectionSeedData.Horn.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection TrumpetSolo => new(Guid.Parse("99def608-eea1-4738-8cd4-aeb786b38c91"), SectionSeedData.Trumpet.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection TrumpetCoach => new(Guid.Parse("d606db59-9900-4f0d-9aaa-677d76329fc9"), SectionSeedData.Trumpet.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection TrumpetHigh => new(Guid.Parse("ebb29506-4552-413a-a43b-0f8dba5fba8d"), SectionSeedData.Trumpet.Id, SelectValueSeedData.High.Id);
        public static SelectValueSection TrumpetLow => new(Guid.Parse("7466eccf-3450-4fc1-948e-1de04d17e5b3"), SectionSeedData.Trumpet.Id, SelectValueSeedData.Low.Id);
        public static SelectValueSection TromboneSolo => new(Guid.Parse("e971bdf0-d36f-4ce8-9bdd-ae027edd0bb0"), SectionSeedData.Trombone.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection TromboneCoach => new(Guid.Parse("b85474fd-327d-4a52-8404-9ca9dc699daa"), SectionSeedData.Trumpet.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection EuphoniumSolo => new(Guid.Parse("774dc855-901a-41df-8b99-9cba9e973b7f"), SectionSeedData.Euphonium.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection EuphoniumCoach => new(Guid.Parse("00241b8c-7b88-4e32-b391-69b6e3b6acf2"), SectionSeedData.Euphonium.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection TubaSolo => new(Guid.Parse("3fdaad51-200d-4578-b9bb-bc3a00480fef"), SectionSeedData.Tuba.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection TubaCoach => new(Guid.Parse("4027a00d-4370-46f9-82b3-8618d572a117"), SectionSeedData.Tuba.Id, SelectValueSeedData.Coach.Id);

        //Strings
        public static SelectValueSection ViolinSolo => new(Guid.Parse("602609d7-2f1a-4a3b-90f1-390515c531f9"), SectionSeedData.Violins.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection ViolinConcertmaster => new(Guid.Parse("c810b38a-d80a-4f16-9c01-3f9183507804"), SectionSeedData.Violins.Id, SelectValueSeedData.ConcertMaster.Id);
        public static SelectValueSection ViolinSecondConcertmaster => new(Guid.Parse("be79168e-620e-46c2-862c-efaffbeb82ee"), SectionSeedData.Violins.Id, SelectValueSeedData.SecondConcertMaster.Id);
        public static SelectValueSection ViolinSectionLead => new(Guid.Parse("a4365301-bea0-40c9-b6a6-626c4cf00f74"), SectionSeedData.Violins.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection ViolinTutti => new(Guid.Parse("776c3d50-0958-490e-98b0-6043cf580c3f"), SectionSeedData.Violins.Id, SelectValueSeedData.Tutti.Id);

        public static SelectValueSection ViolinCoach => new(Guid.Parse("12f2912a-139d-42af-99b4-61eb02a73701"), SectionSeedData.Violins.Id, SelectValueSeedData.Coach.Id);


        public static SelectValueSection ViolaSolo => new(Guid.Parse("c883b3ea-6522-499d-aebc-0e2937d7a09e"), SectionSeedData.Viola.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection ViolaSectionLead => new(Guid.Parse("ed6d7457-869d-433f-9a14-e7327120bad2"), SectionSeedData.Viola.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection ViolaTutti => new(Guid.Parse("7361f67c-4fe1-49c5-9d47-fb7225296ad7"), SectionSeedData.Viola.Id, SelectValueSeedData.Tutti.Id);
        public static SelectValueSection ViolaCoach => new(Guid.Parse("127bcbee-e946-44db-99ee-7e5645902689"), SectionSeedData.Viola.Id, SelectValueSeedData.Coach.Id);

        public static SelectValueSection VioloncelloSolo => new(Guid.Parse("e155d063-df88-4629-ba50-8213b59501fd"), SectionSeedData.Violoncello.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection VioloncelloSectionLead => new(Guid.Parse("141d8189-4731-4a24-9e20-0cdef1d8d150"), SectionSeedData.Violoncello.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection VioloncelloTutti => new(Guid.Parse("081e7457-5d72-4de2-adfe-beb427425738"), SectionSeedData.Violoncello.Id, SelectValueSeedData.Tutti.Id);
        public static SelectValueSection VioloncelloCoach => new(Guid.Parse("6c14c8e3-64aa-42a4-b6c2-366dc1fe89b5"), SectionSeedData.Violoncello.Id, SelectValueSeedData.Coach.Id);

        public static SelectValueSection DoublebassSolo => new(Guid.Parse("8e849991-2d30-41b4-85f2-4258d458def2"), SectionSeedData.DoubleBass.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection DoublebassSectionLead => new(Guid.Parse("e75597e0-6173-4171-b5a7-ace60484967f"), SectionSeedData.DoubleBass.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection DoublebassTutti => new(Guid.Parse("097923c1-e85e-4afc-af85-8af01ae27655"), SectionSeedData.DoubleBass.Id, SelectValueSeedData.Tutti.Id);
        public static SelectValueSection DoublebassCoach => new(Guid.Parse("ad3014d9-336f-4ca0-9f37-1937b8da8bff"), SectionSeedData.DoubleBass.Id, SelectValueSeedData.Coach.Id);

        //Percussion
        public static SelectValueSection PercussionSolo => new(Guid.Parse("069141f0-9ba3-4e10-822e-8f83d5282bda"), SectionSeedData.Percussion.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection PercussionCoach => new(Guid.Parse("040cbf2a-e70b-4dcf-98d0-45a7a4592093"), SectionSeedData.Percussion.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection PercussionSectionLead => new(Guid.Parse("3a9c04d8-ec63-4b46-b01e-fc1729ed529c"), SectionSeedData.Percussion.Id, SelectValueSeedData.SectionLead.Id);
        public static SelectValueSection TimpaniSolo => new(Guid.Parse("f3e64014-b6c6-46ca-8334-c744fb2b07cc"), SectionSeedData.Timpani.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection TimpaniCoach => new(Guid.Parse("706fc83a-fbe8-4446-bc89-d42c6d4b5c76"), SectionSeedData.Timpani.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection MalletsSolo => new(Guid.Parse("281aa638-cc0c-45a1-a3d7-ae5a07644933"), SectionSeedData.Mallets.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection MalletsCoach => new(Guid.Parse("1ba1e082-fcf9-4b41-a996-2204038b5026"), SectionSeedData.Mallets.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection HarpSolo => new(Guid.Parse("7ead42e9-7ea6-4bea-9ebd-9e232bd71a93"), SectionSeedData.Harp.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection HarpCoach => new(Guid.Parse("4199dbe4-9544-46c9-96af-3f1bb8488230"), SectionSeedData.Harp.Id, SelectValueSeedData.Coach.Id);
        public static SelectValueSection PianoSolo => new(Guid.Parse("b27010dd-82dd-4f2a-af3e-d18c73fc4a31"), SectionSeedData.Piano.Id, SelectValueSeedData.Solo.Id);
        public static SelectValueSection PianoOrchestra => new(Guid.Parse("63e9c074-df8c-4d68-9c69-3e61bb5518ad"), SectionSeedData.Piano.Id, SelectValueSeedData.OrchestraPiano.Id);
    }
}
