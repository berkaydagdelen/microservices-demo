using UserService.Models;

namespace UserService.Repositories
{
    /// <summary>
    /// Kullanýcý verilerine eriþim için repository interface'i
    /// Bu interface, veri eriþim katmanýný tanýmlar
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Tüm kullanýcýlarý getirir
        /// </summary>
        List<User> GetAll();

        /// <summary>
        /// ID ile kullanýcý getirir
        /// </summary>
        User? GetById(int id);

        /// <summary>
        /// Yeni kullanýcý ekler
        /// </summary>
        User Create(User user);

        /// <summary>
        /// Kullanýcý var mý kontrol eder
        /// </summary>
        bool Exists(int id);
    }
}

