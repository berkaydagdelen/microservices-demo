using WaterProduction.Models;

namespace WaterProduction.Repositories
{
    /// <summary>
    /// Su üretim verilerine eriþim için repository interface'i
    /// Veri eriþim katmanýný tanýmlar
    /// </summary>
    public interface IWaterProductionRepository
    {
        /// <summary>
        /// Tüm verileri getirir
        /// </summary>
        Task<List<WaterProductionData>> GetAllAsync();

        /// <summary>
        /// ID ile veri getirir
        /// </summary>
        Task<WaterProductionData?> GetByIdAsync(int id);

        /// <summary>
        /// Tarih aralýðýna göre verileri getirir
        /// </summary>
        Task<List<WaterProductionData>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Tesis adý ve tarihe göre veri var mý kontrol eder
        /// </summary>
        Task<bool> ExistsAsync(string facilityName, DateTime productionDate);

        /// <summary>
        /// Yeni veri ekler
        /// </summary>
        Task<WaterProductionData> CreateAsync(WaterProductionData data);

        /// <summary>
        /// Toplu veri ekler
        /// </summary>
        Task<int> CreateBatchAsync(List<WaterProductionData> dataList);
    }
}

