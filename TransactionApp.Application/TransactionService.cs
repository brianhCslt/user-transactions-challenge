/// Author: Brian Haynes
using AutoMapper;
using TransactionApp.Domain;
using TransactionApp.Infrastructure;

namespace TransactionApp.Application
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _repo;
        private readonly IMapper _mapper;

        public TransactionService(IRepository<Transaction> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TransactionDto> AddTransactionAsync(TransactionDto dto)
        {
            var entity = _mapper.Map<Transaction>(dto);

            var result = await _repo.AddAsync(entity);
            return _mapper.Map<TransactionDto>(result);
        }

        public async Task<List<TransactionDto>> GetAllAsync()
        {
            var transactions = await _repo.GetAllAsync();
            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task<List<TransactionDto>> GetHighVolumeTransactions(decimal threshold)
        {
            var all = await _repo.GetAllAsync();
            var filtered = all.Where(t => t.Amount > threshold).ToList();
            return _mapper.Map<List<TransactionDto>>(filtered);
        }

        public async Task<List<TransactionSummaryPerUserDto>> GetTotalAmountPerUser()
        {
            var all = await _repo.GetAllAsync();
            return all.GroupBy(t => t.UserId)
                      .Select(g => new TransactionSummaryPerUserDto
                      {
                          UserId = g.Key,
                          TotalAmount = g.Sum(x => x.Amount)
                      }).ToList();
        }

        public async Task<List<TransactionSummaryPerTypeDto>> GetTotalAmountPerType()
        {
            var all = await _repo.GetAllAsync();
            return all.GroupBy(t => t.TransactionType)
                      .Select(g => new TransactionSummaryPerTypeDto
                      {
                          TransactionType = g.Key,
                          TotalAmount = g.Sum(x => x.Amount)
                      }).ToList();
        }
    }
}
