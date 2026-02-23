namespace Orso.Arpa.Domain.FinanceDomain.Interfaces
{
    public interface ICredentialEncryptionService
    {
        string Encrypt(string plainText);
        string Decrypt(string encryptedText);
    }
}
