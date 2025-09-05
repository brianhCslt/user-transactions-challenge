using System.Text.Json.Serialization;

/// Author: Brian Haynes
namespace TransactionApp.Domain
{
    public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public enum TransactionTypeEnum
    {
        Debit,
        Credit
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
