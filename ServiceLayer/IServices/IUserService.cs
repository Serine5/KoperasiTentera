using KoperasiTenteraDAL.Entities;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        public Task<User> AddUserAsync(User newUser);

        public Task<IEnumerable<User>> GetAllUsersAsync();
    }
}