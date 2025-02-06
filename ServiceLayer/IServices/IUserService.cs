using KoperasiTenteraDAL.Entities;

namespace ServiceLayer.IServices
{
    public interface IUserService
    {
        public User AddUser(User newUser);

        public IList<User> GetAllUsers();
    }
}
