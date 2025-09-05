/// Author: Brian Haynes
using AutoMapper;
using TransactionApp.Infrastructure;
using TransactionApp.Domain;

namespace TransactionApp.Application
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<UserDto> AddAsync(UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);

            var result = await _repo.AddAsync(entity);
            return _mapper.Map<UserDto>(result);
        }

        public async Task DeleteAsync(string id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var entity = _mapper.Map<User>(id);

            var result = await _repo.GetByIdAsync(entity);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<UserDto> UpdateAsync(string id, UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);

            var result = await _repo.UpdateAsync(entity);
            return _mapper.Map<UserDto>(result);
        }
    }
}
