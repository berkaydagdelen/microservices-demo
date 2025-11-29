using OrderService.Models;

namespace OrderService.Services
{
    /// <summary>
    /// Sipariþ iþlemleri için service interface'i
    /// Bu interface, iþ mantýðý katmanýný tanýmlar
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Tüm sipariþleri getirir
        /// </summary>
        List<Order> GetAllOrders();

        /// <summary>
        /// ID ile sipariþ getirir
        /// </summary>
        Order? GetOrderById(int id);

        /// <summary>
        /// Kullanýcý ID'sine göre sipariþleri getirir
        /// </summary>
        List<Order> GetOrdersByUserId(int userId);

        /// <summary>
        /// Yeni sipariþ oluþturur (iþ kurallarý uygulanýr)
        /// </summary>
        Order CreateOrder(Order order);
    }
}

