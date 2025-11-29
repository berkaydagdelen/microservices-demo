using UserService.Models;

namespace UserService.Repositories
{
    /// <summary>
    /// Kullanýcý verilerine eriþim için repository implementation
    /// Þu anda hafýza içi liste kullanýyor, gerçek uygulamada veritabaný kullanýlýr
    /// </summary>
    public class UserRepository : IUserRepository
    {
        // Veri deposu (gerçek uygulamada veritabaný olur)
        private static readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Ahmet Yýlmaz", Email = "ahmet@example.com" },
            new User { Id = 2, Name = "Ayþe Demir", Email = "ayse@example.com" },
            new User { Id = 3, Name = "Mehmet Kaya", Email = "mehmet@example.com" }
        };

        /// <summary>
        /// Tüm kullanýcýlarý getirir
        /// </summary>
        public List<User> GetAll()
        {
            return _users.ToList(); // Yeni bir liste döndür (referans kopyalamayý önle)
        }

        /// <summary>
        /// ID ile kullanýcý getirir
        /// </summary>
        public User? GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        /// <summary>
        /// Yeni kullanýcý ekler
        /// </summary>
        public User Create(User user)
        {
            // Yeni ID atama
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return user;
        }

        /// <summary>
        /// Kullanýcý var mý kontrol eder
        /// </summary>
        public bool Exists(int id)
        {
            return _users.Any(u => u.Id == id);
        }
    }
}

