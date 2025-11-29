using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    /// <summary>
    /// Sipariþ iþlemleri için service implementation
    /// Ýþ mantýðý burada uygulanýr
    /// </summary>
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderService> _logger;

        // Dependency Injection ile Repository enjekte edilir
        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        /// <summary>
        /// Tüm sipariþleri getirir
        /// </summary>
        public List<Order> GetAllOrders()
        {
            _logger.LogInformation("Service: Tüm sipariþler getiriliyor");
            return _orderRepository.GetAll();
        }

        /// <summary>
        /// ID ile sipariþ getirir
        /// </summary>
        public Order? GetOrderById(int id)
        {
            _logger.LogInformation($"Service: Sipariþ ID {id} getiriliyor");
            return _orderRepository.GetById(id);
        }

        /// <summary>
        /// Kullanýcý ID'sine göre sipariþleri getirir
        /// </summary>
        public List<Order> GetOrdersByUserId(int userId)
        {
            _logger.LogInformation($"Service: Kullanýcý ID {userId} için sipariþler getiriliyor");
            return _orderRepository.GetByUserId(userId);
        }

        /// <summary>
        /// Yeni sipariþ oluþturur
        /// Ýþ kurallarý burada uygulanýr:
        /// - Fiyat kontrolü
        /// - Ürün adý kontrolü
        /// - Kullanýcý ID kontrolü
        /// </summary>
        public Order CreateOrder(Order order)
        {
            _logger.LogInformation($"Service: Yeni sipariþ oluþturuluyor: {order.ProductName}");

            // Ýþ Kuralý 1: Ürün adý kontrolü
            if (string.IsNullOrWhiteSpace(order.ProductName))
            {
                throw new ArgumentException("Ürün adý boþ olamaz!");
            }

            // Ýþ Kuralý 2: Fiyat kontrolü
            if (order.Price <= 0)
            {
                throw new ArgumentException("Fiyat 0'dan büyük olmalýdýr!");
            }

            // Ýþ Kuralý 3: Kullanýcý ID kontrolü
            if (order.UserId <= 0)
            {
                throw new ArgumentException("Geçerli bir kullanýcý ID'si giriniz!");
            }

            // Ýþ Kuralý 4: Maksimum fiyat kontrolü (örnek: 100.000 TL)
            if (order.Price > 100000)
            {
                throw new InvalidOperationException("Sipariþ fiyatý çok yüksek! Maksimum 100.000 TL olabilir.");
            }

            // Tüm kontroller geçti, sipariþi oluþtur
            return _orderRepository.Create(order);
        }
    }
}

