/// Author: Brian Haynes
using System.Text.Json.Serialization;
using TransactionApp.Domain;

namespace TransactionApp.Application
{
    public class TransactionDto
    {
        public TransactionDto() { }
        public TransactionDto(int id)
        {
            Id = id;
        }
        public int Id { get; protected set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public override bool Equals(object obj)
        {
            return obj != null && obj is TransactionDto model &&
                   Id == model.Id;
        }

    }

    public class UserDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }

    public class TransactionSummaryPerUserDto
    {
        public string? UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class TransactionSummaryPerTypeDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
