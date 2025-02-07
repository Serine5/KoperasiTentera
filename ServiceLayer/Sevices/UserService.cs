using KoperasiTenteraDAL.Entities;
using KoperasiTenteraDAL.Repositories;
using ServiceLayer.IServices;

namespace ServiceLayer.Sevices
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUserAsync(User newUser)
        {
            return await _userRepository.AddAsync(newUser);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}