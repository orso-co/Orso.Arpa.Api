using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileCredential : BaseEntity
    {
        public MusicianProfileCredential(Guid? id, MusicianProfile musicianProfile, Credential credential) : base(id)
        {
            MusicianProfile = musicianProfile;
            Credential = credential;
        }

        public MusicianProfileCredential(Guid musicianProfileId, Guid credentialId)
        {
            MusicianProfileId = musicianProfileId;
            CredentialId = credentialId;
        }

        public MusicianProfileCredential()
        {
        }

        public Guid CredentialId { get; private set; }
        public virtual Credential Credential { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
