using UserService.Models;

namespace UserService.Services
{
    /// <summary>
    /// Kullanýcý iþlemleri için service interface'i
    /// Bu interface, iþ mantýðý katmanýný tanýmlar
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Tüm kullanýcýlarý getirir
        /// </summary>
        List<User> GetAllUsers();

        /// <summary>
        /// ID ile kullanýcý getirir
        /// </summary>
        User? GetUserById(int id);

        /// <summary>
        /// Yeni kullanýcý oluþturur (iþ kurallarý uygulanýr)
        /// </summary>
        User CreateUser(User user);

        /// <summary>
        /// Email'in daha önce kullanýlýp kullanýlmadýðýný kontrol eder
        /// </summary>
        bool IsEmailTaken(string email);
    }
}

