using UserService.Data;
using UserService.Models;

namespace UserService.Repositories
{
    /// <summary>
    /// Kullanýcý verilerine eriþim için repository implementation
    /// Generic Repository'den türer, özel metodlar eklenebilir
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }

        // Generic Repository'den gelen tüm metodlar otomatik kullanýlabilir:
        // - GetAllAsync()
        // - GetByIdAsync(int id)
        // - CreateAsync(User user)
        // - UpdateAsync(User user)
        // - DeleteAsync(int id)
        // - FindAsync(...)
        // - AnyAsync(...)
        // vb.

        // Özel metodlar buraya eklenebilir
    }
}

