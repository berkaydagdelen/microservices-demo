using UserService.Models;

namespace UserService.Repositories
{
    /// <summary>
    /// Kullanýcý verilerine eriþim için repository interface'i
    /// Generic Repository'den türer, özel metodlar ekler
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        // Generic Repository'den gelen metodlar:
        // - GetAllAsync()
        // - GetByIdAsync(int id)
        // - CreateAsync(User user)
        // - AnyAsync(...)
        // - UpdateAsync(...)
        // - DeleteAsync(...)
        // vb.

        // Özel metodlar buraya eklenebilir
    }
}

