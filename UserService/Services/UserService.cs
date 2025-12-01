using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    /// <summary>
    /// Kullanýcý iþlemleri için service implementation
    /// Ýþ mantýðý burada uygulanýr
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        // Dependency Injection ile Repository enjekte edilir
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Tüm kullanýcýlarý getirir
        /// </summary>
        public async Task<List<User>> GetAllUsersAsync()
        {
            _logger.LogInformation("Service: Tüm kullanýcýlar getiriliyor");
            return await _userRepository.GetAllAsync();
        }

        /// <summary>
        /// ID ile kullanýcý getirir
        /// </summary>
        public async Task<User?> GetUserByIdAsync(int id)
        {
            _logger.LogInformation($"Service: Kullanýcý ID {id} getiriliyor");
            return await _userRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Yeni kullanýcý oluþturur
        /// Ýþ kurallarý burada uygulanýr:
        /// - Email kontrolü
        /// - Validasyon
        /// </summary>
        public async Task<User> CreateUserAsync(User user)
        {
            _logger.LogInformation($"Service: Yeni kullanýcý oluþturuluyor: {user.Name}");

            // Ýþ Kuralý 1: Email kontrolü
            if (await IsEmailTakenAsync(user.Email))
            {
                throw new InvalidOperationException($"Email '{user.Email}' zaten kullanýlýyor!");
            }

            // Ýþ Kuralý 2: Email format kontrolü (basit)
            if (string.IsNullOrWhiteSpace(user.Email) || !user.Email.Contains("@"))
            {
                throw new ArgumentException("Geçerli bir email adresi giriniz!");
            }

            // Ýþ Kuralý 3: Ýsim kontrolü
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ArgumentException("Kullanýcý adý boþ olamaz!");
            }

            // Tüm kontroller geçti, kullanýcýyý oluþtur
            return await _userRepository.CreateAsync(user);
        }

        /// <summary>
        /// Email'in daha önce kullanýlýp kullanýlmadýðýný kontrol eder
        /// </summary>
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _userRepository.AnyAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}

