using System.ComponentModel.DataAnnotations.Schema;

namespace Orso.Arpa.Domain._General.Model
{
    [ComplexType]
    public record BankAccount(string AccountOwner, string Iban, string Bic);
}