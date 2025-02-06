using KoperasiTenteraDAL.Context;
using KoperasiTenteraDAL.Entities;

namespace KoperasiTenteraDAL.Repositories
{
    public class UserRepository : IRepository<User, int>, IDisposable
    {
        private readonly UserContext _context;
        public UserRepository(UserContext context)
        {
            _context = context;
        }
        public User Add(User entity)
        {
            _context.Users.Add(entity);
            return entity;
        }

        public void Delete(int entityId)
        {
            var user = _context.Users.Find(entityId);
            if (user != null) 
            {
                _context.Remove(user);
                _context.SaveChanges();
            }
        }

        public User Get(int entityId)
        {
            return _context.Users.FirstOrDefault(u => u.Id == entityId);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User Update(User entity)
        {
            _context.Users.Update(entity);
            return entity;
        }

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
