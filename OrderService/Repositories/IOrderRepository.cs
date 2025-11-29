using OrderService.Models;

namespace OrderService.Repositories
{
    /// <summary>
    /// Sipariþ verilerine eriþim için repository interface'i
    /// Bu interface, veri eriþim katmanýný tanýmlar
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Tüm sipariþleri getirir
        /// </summary>
        List<Order> GetAll();

        /// <summary>
        /// ID ile sipariþ getirir
        /// </summary>
        Order? GetById(int id);

        /// <summary>
        /// Kullanýcý ID'sine göre sipariþleri getirir
        /// </summary>
        List<Order> GetByUserId(int userId);

        /// <summary>
        /// Yeni sipariþ ekler
        /// </summary>
        Order Create(Order order);

        /// <summary>
        /// Sipariþ var mý kontrol eder
        /// </summary>
        bool Exists(int id);
    }
}

