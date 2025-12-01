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
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// ID ile kullanýcý getirir
        /// </summary>
        Task<User?> GetUserByIdAsync(int id);

        /// <summary>
        /// Yeni kullanýcý oluþturur (iþ kurallarý uygulanýr)
        /// </summary>
        Task<User> CreateUserAsync(User user);

        /// <summary>
        /// Email'in daha önce kullanýlýp kullanýlmadýðýný kontrol eder
        /// </summary>
        Task<bool> IsEmailTakenAsync(string email);
    }
}

