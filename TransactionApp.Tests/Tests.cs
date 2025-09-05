/// Author: Brian Haynes
using AutoMapper;
using TransactionApp.Application;
using TransactionApp.Domain;
using TransactionApp.Infrastructure;
using Moq;
using System.Linq;

namespace TransactionApp.Tests
{
    public class TransactionServiceTests
    {
        private readonly Mock<IRepository<Transaction>> _repoMock;
        private readonly ITransactionService _service;
        private readonly IMapper _mapper;

        public TransactionServiceTests()
        {
            _repoMock = new Mock<IRepository<Transaction>>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
            _service = new TransactionService(_repoMock.Object, _mapper);
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldReturnTransactionDto()
        {
            // Arrange
            var transactionDto = new TransactionDto { UserId = "User1", Amount = 100, TransactionType = TransactionTypeEnum.Debit };
            var transactionEntity = new Transaction { UserId = "User1", Amount = 100, TransactionType = TransactionTypeEnum.Debit };

            _repoMock.Setup(repo => repo.AddAsync(It.IsAny<Transaction>())).ReturnsAsync(transactionEntity);

            // Act
            var result = await _service.AddTransactionAsync(transactionDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(transactionDto.UserId, result.UserId);
        }
        [Fact]
        public async Task HighVolumeTest()
        {
            // Arrange
            var lowTransactionDto = new TransactionDto(0) { UserId = "User1", Amount = 100, TransactionType = TransactionTypeEnum.Debit };
            var highTransactionDto = new TransactionDto(1) { UserId = "User1", Amount = 200, TransactionType = TransactionTypeEnum.Debit };
            var lowTransactionEntity = new Transaction { Id = 0, UserId = "User1", Amount = 100, TransactionType = TransactionTypeEnum.Debit };
            var highTransactionEntity = new Transaction { Id = 1, UserId = "User1", Amount = 200, TransactionType = TransactionTypeEnum.Debit };

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(lowTransactionEntity);
            transactions.Add(highTransactionEntity);
            _repoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(transactions);

            // Act
            var lowResult = await _service.AddTransactionAsync(lowTransactionDto);
            var highResult = await _service.AddTransactionAsync(highTransactionDto);
            var result = await _service.GetHighVolumeTransactions(100);
            // Assert
            Assert.NotNull(result);
            Assert.Contains(highTransactionDto, result);
            Assert.DoesNotContain(lowTransactionDto, result);
        }
    }
}
