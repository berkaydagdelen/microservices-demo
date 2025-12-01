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
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            _logger.LogInformation("Controller: Tüm kullanýcýlar isteniyor");
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/1
        // Belirli bir kullanýcýyý ID ile getir
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogInformation($"Controller: Kullanýcý ID {id} isteniyor");
            var user = await _userService.GetUserByIdAsync(id);
            
            if (user == null)
            {
                return NotFound($"ID {id} olan kullanýcý bulunamadý");
            }
            
            return Ok(user);
        }

        // POST: api/users
        // Yeni kullanýcý oluþtur
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            try
            {
                _logger.LogInformation($"Controller: Yeni kullanýcý oluþturuluyor: {user.Name}");
                var createdUser = await _userService.CreateUserAsync(user);
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

