using OrderService.Models;

namespace OrderService.Repositories
{
    /// <summary>
    /// Sipariþ verilerine eriþim için repository implementation
    /// Þu anda hafýza içi liste kullanýyor, gerçek uygulamada veritabaný kullanýlýr
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        // Veri deposu (gerçek uygulamada veritabaný olur)
        private static readonly List<Order> _orders = new List<Order>
        {
            new Order { Id = 1, UserId = 1, ProductName = "Laptop", Price = 15000.00m, OrderDate = DateTime.Now.AddDays(-5) },
            new Order { Id = 2, UserId = 1, ProductName = "Mouse", Price = 250.00m, OrderDate = DateTime.Now.AddDays(-3) },
            new Order { Id = 3, UserId = 2, ProductName = "Klavye", Price = 500.00m, OrderDate = DateTime.Now.AddDays(-2) },
            new Order { Id = 4, UserId = 3, ProductName = "Monitör", Price = 3000.00m, OrderDate = DateTime.Now.AddDays(-1) }
        };

        /// <summary>
        /// Tüm sipariþleri getirir
        /// </summary>
        public List<Order> GetAll()
        {
            return _orders.ToList(); // Yeni bir liste döndür (referans kopyalamayý önle)
        }

        /// <summary>
        /// ID ile sipariþ getirir
        /// </summary>
        public Order? GetById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        /// <summary>
        /// Kullanýcý ID'sine göre sipariþleri getirir
        /// </summary>
        public List<Order> GetByUserId(int userId)
        {
            return _orders.Where(o => o.UserId == userId).ToList();
        }

        /// <summary>
        /// Yeni sipariþ ekler
        /// </summary>
        public Order Create(Order order)
        {
            // Yeni ID atama
            order.Id = _orders.Count > 0 ? _orders.Max(o => o.Id) + 1 : 1;
            order.OrderDate = DateTime.Now;
            _orders.Add(order);
            return order;
        }

        /// <summary>
        /// Sipariþ var mý kontrol eder
        /// </summary>
        public bool Exists(int id)
        {
            return _orders.Any(o => o.Id == id);
        }
    }
}

