using System;
using System.Collections.Generic;
using Orso.Arpa.Application.SectionApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class SectionDtoData
    {
        public static IList<SectionDto> Sections
        {
            get
            {
                return new List<SectionDto>
                {
                    Choir,
                    Orchestra,
                    Soloists,
                    Other,
                    Miscellaneous,
                    FemaleVoices,
                    MaleVoices,
                    DeepFemaleVoices,
                    HighFemaleVoices,
                    DeepMaleVoices,
                    HighMaleVoices,
                    Alto,
                    Alto1,
                    Alto2,
                    Soprano,
                    Soprano1,
                    Soprano2,
                    Tenor,
                    Tenor1,
                    Tenor2,
                    Basso,
                    Basso1,
                    Basso2,
                    WindSection,
                    Strings,
                    Percussion,
                    Woodwind,
                    Brass
                };
            }
        }

        public static SectionDto Choir
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"),
                    Name = "Choir",
                };
            }
        }

        public static SectionDto Orchestra
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("308659d6-6014-4d2c-a62a-be75bf202e62"),
                    Name = "Orchestra",
                };
            }
        }

        public static SectionDto Soloists
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("e0fdb057-c9b7-4477-be75-cbf920a26af6"),
                    Name = "Soloist",
                };
            }
        }

        public static SectionDto Other
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("c9403ca4-6b75-44c3-b567-e53bbd78fb75"),
                    Name = "Other",
                };
            }
        }

        public static SectionDto Miscellaneous
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("a19fa9af-dcba-48e3-bc21-be2130fa528c"),
                    Name = "Miscellaneous",
                };
            }
        }

        public static SectionDto FemaleVoices
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("3ed0960c-1eed-4a45-a1ef-343aa8e7b2d6"),
                    Name = "Female Voices",
                };
            }
        }

        public static SectionDto MaleVoices
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("4599103d-f220-4744-92d1-7c6993e9bda4"),
                    Name = "Male Voices",
                };
            }
        }

        public static SectionDto DeepFemaleVoices
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("48337b78-70f0-493e-911b-296632b06ef8"),
                    Name = "Deep Female Voices",
                };
            }
        }

        public static SectionDto HighFemaleVoices
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("5d469fc5-b3e6-40b8-9fa9-542981083ce3"),
                    Name = "High Female Voices",
                };
            }
        }

        public static SectionDto DeepMaleVoices
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("b9673cfd-7cdb-472c-86e0-1304cbb3840a"),
                    Name = "Deep Male Voices",
                };
            }
        }

        public static SectionDto HighMaleVoices
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("7924daef-2542-4648-a42f-4c4374ee09db"),
                    Name = "High Male Voices",
                };
            }
        }

        public static SectionDto Alto
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("a06431be-f9d6-44dc-8fdb-fbf8aa2bb940"),
                    Name = "Alto",
                };
            }
        }

        public static SectionDto Soprano
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("7daa1394-a70d-4a24-88a6-ccf511d75c4d"),
                    Name = "Soprano",
                };
            }
        }

        public static SectionDto Basso
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("e7dd10ef-1c39-4440-9a6c-65d397f010ca"),
                    Name = "Basso",
                };
            }
        }

        public static SectionDto Tenor
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("1579d7e7-4f55-4532-a078-69fd1ec939da"),
                    Name = "Tenor",
                };
            }
        }

        public static SectionDto Alto1
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("e809ee90-23f9-44de-b80e-2fddd5ee3683"),
                    Name = "Alto 1",
                };
            }
        }

        public static SectionDto Alto2
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("50dfa2be-85e2-4638-aa53-22dadc97a844"),
                    Name = "Alto 2",
                };
            }
        }

        public static SectionDto Soprano1
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("8470ddf0-43ab-477e-b3bc-47ede014b359"),
                    Name = "Soprano 1",
                };
            }
        }

        public static SectionDto Soprano2
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("22d7cf92-7b29-4cf1-a6fa-2954377589b4"),
                    Name = "Soprano 2",
                };
            }
        }

        public static SectionDto Tenor1
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("3db46ff0-9165-46cc-8f28-6a1d52dee518"),
                    Name = "Tenor 1",
                };
            }
        }

        public static SectionDto Tenor2
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("afef89cf-90e1-4d4f-83ab-d2b47e97af0f"),
                    Name = "Tenor 2",
                };
            }
        }

        public static SectionDto Basso1
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("bfe0e1ca-95ce-4cb6-a9c9-3c23c70bab21"),
                    Name = "Basso 1",
                };
            }
        }

        public static SectionDto Basso2
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("61fa66ec-3103-43fe-800c-930547dff82c"),
                    Name = "Basso 2",
                };
            }
        }

        public static SectionDto WindSection
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("b289cfe7-d66e-48d8-83a9-f4b1f7710863"),
                    Name = "Wind Section",
                };
            }
        }

        public static SectionDto Strings
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("1bde9862-3ed5-45cd-8d80-0a52c6b4c0fb"),
                    Name = "Strings",
                };
            }
        }

        public static SectionDto Percussion
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("0558a5ff-ee27-44a1-82ab-d0c0cc018c3c"),
                    Name = "Percussion",
                };
            }
        }

        public static SectionDto Woodwind
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("a6abdeec-8185-40ac-a418-2e422bb9adbd"),
                    Name = "Woodwind",
                };
            }
        }

        public static SectionDto Brass
        {
            get
            {
                return new SectionDto
                {
                    Id = Guid.Parse("f4c70178-d069-44dc-8956-7160c5fef52e"),
                    Name = "Brass",
                };
            }
        }
    }
}
