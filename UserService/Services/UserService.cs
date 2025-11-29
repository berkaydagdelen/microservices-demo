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
        public List<User> GetAllUsers()
        {
            _logger.LogInformation("Service: Tüm kullanýcýlar getiriliyor");
            return _userRepository.GetAll();
        }

        /// <summary>
        /// ID ile kullanýcý getirir
        /// </summary>
        public User? GetUserById(int id)
        {
            _logger.LogInformation($"Service: Kullanýcý ID {id} getiriliyor");
            return _userRepository.GetById(id);
        }

        /// <summary>
        /// Yeni kullanýcý oluþturur
        /// Ýþ kurallarý burada uygulanýr:
        /// - Email kontrolü
        /// - Validasyon
        /// </summary>
        public User CreateUser(User user)
        {
            _logger.LogInformation($"Service: Yeni kullanýcý oluþturuluyor: {user.Name}");

            // Ýþ Kuralý 1: Email kontrolü
            if (IsEmailTaken(user.Email))
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
            return _userRepository.Create(user);
        }

        /// <summary>
        /// Email'in daha önce kullanýlýp kullanýlmadýðýný kontrol eder
        /// </summary>
        public bool IsEmailTaken(string email)
        {
            var users = _userRepository.GetAll();
            return users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}

