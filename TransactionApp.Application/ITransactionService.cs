/// Author: Brian Haynes
namespace TransactionApp.Application
{
    public interface ITransactionService
    {
        Task<TransactionDto> AddTransactionAsync(TransactionDto dto);
        Task<List<TransactionDto>> GetAllAsync();
        Task<List<TransactionDto>> GetHighVolumeTransactions(decimal threshold);
        Task<List<TransactionSummaryPerUserDto>> GetTotalAmountPerUser();
        Task<List<TransactionSummaryPerTypeDto>> GetTotalAmountPerType();
    }
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> AddAsync(UserDto dto);
        Task<UserDto> UpdateAsync(string id, UserDto dto);
        Task DeleteAsync(string id);
    }
}
