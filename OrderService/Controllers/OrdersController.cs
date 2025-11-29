using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    /// <summary>
    /// Sipariþ iþlemleri için API Controller
    /// Sadece HTTP isteklerini yönetir, iþ mantýðý Service katmanýnda
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        // Dependency Injection ile Service enjekte edilir
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // GET: api/orders
        // Tüm sipariþleri getir
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            _logger.LogInformation("Controller: Tüm sipariþler isteniyor");
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }

        // GET: api/orders/1
        // Belirli bir sipariþi ID ile getir
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            _logger.LogInformation($"Controller: Sipariþ ID {id} isteniyor");
            var order = _orderService.GetOrderById(id);
            
            if (order == null)
            {
                return NotFound($"ID {id} olan sipariþ bulunamadý");
            }
            
            return Ok(order);
        }

        // GET: api/orders/user/1
        // Belirli bir kullanýcýnýn tüm sipariþlerini getir
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Order>> GetOrdersByUser(int userId)
        {
            _logger.LogInformation($"Controller: Kullanýcý ID {userId} için sipariþler isteniyor");
            var userOrders = _orderService.GetOrdersByUserId(userId);
            return Ok(userOrders);
        }

        // POST: api/orders
        // Yeni sipariþ oluþtur
        [HttpPost]
        public ActionResult<Order> CreateOrder([FromBody] Order order)
        {
            try
            {
                _logger.LogInformation($"Controller: Yeni sipariþ oluþturuluyor: {order.ProductName}");
                var createdOrder = _orderService.CreateOrder(order);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
            }
            catch (InvalidOperationException ex)
            {
                // Ýþ kuralý hatasý (örn: fiyat çok yüksek)
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

