using System;
using Microsoft.AspNetCore.DataProtection;
using Orso.Arpa.Domain.FinanceDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.Finance
{
    public class CredentialEncryptionService : ICredentialEncryptionService
    {
        private readonly IDataProtector _protector;

        public CredentialEncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("Orso.Arpa.Finance.Credentials");
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            return _protector.Protect(plainText);
        }

        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
                return null;

            try
            {
                return _protector.Unprotect(encryptedText);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
