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
        public User AddUser(User newUser)
        {
            return _userRepository.Add(newUser);
        }
        public IList<User> GetAllUsers()
        {
            return _userRepository.GetAll().ToList();
        }
    }
}
