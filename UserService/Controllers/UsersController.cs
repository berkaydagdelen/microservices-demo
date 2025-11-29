using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    /// <summary>
    /// Kullanýcý iþlemleri için API Controller
    /// Sadece HTTP isteklerini yönetir, iþ mantýðý Service katmanýnda
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        // Dependency Injection ile Service enjekte edilir
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/users
        // Tüm kullanýcýlarý getir
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            _logger.LogInformation("Controller: Tüm kullanýcýlar isteniyor");
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/users/1
        // Belirli bir kullanýcýyý ID ile getir
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            _logger.LogInformation($"Controller: Kullanýcý ID {id} isteniyor");
            var user = _userService.GetUserById(id);
            
            if (user == null)
            {
                return NotFound($"ID {id} olan kullanýcý bulunamadý");
            }
            
            return Ok(user);
        }

        // POST: api/users
        // Yeni kullanýcý oluþtur
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            try
            {
                _logger.LogInformation($"Controller: Yeni kullanýcý oluþturuluyor: {user.Name}");
                var createdUser = _userService.CreateUser(user);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (InvalidOperationException ex)
            {
                // Ýþ kuralý hatasý (örn: email zaten kullanýlýyor)
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Validasyon hatasý
                return BadRequest(ex.Message);
            }
        }
    }
}

