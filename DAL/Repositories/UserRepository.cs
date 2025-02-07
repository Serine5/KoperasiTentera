using KoperasiTenteraDAL.Context;
using KoperasiTenteraDAL.Entities;
using KoperasiTenteraDAL.Repositories;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IRepository<User, int>, IAsyncDisposable
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User> AddAsync(User entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int entityId)
    {
        var user = await _context.Users.FindAsync(entityId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> GetAsync(int entityId)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == entityId);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User> UpdateAsync(User entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    private bool _disposed;

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
            _disposed = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        GC.SuppressFinalize(this);
    }
}
