using WaterProduction.Models;

namespace WaterProduction.Services
{
    /// <summary>
    /// Su üretim iþlemleri için service interface'i
    /// Ýþ mantýðý katmanýný tanýmlar
    /// </summary>
    public interface IWaterProductionService
    {
        /// <summary>
        /// Tüm su üretim verilerini getirir
        /// </summary>
        Task<List<WaterProductionData>> GetAllDataAsync();

        /// <summary>
        /// ID ile su üretim verisi getirir
        /// </summary>
        Task<WaterProductionData?> GetDataByIdAsync(int id);

        /// <summary>
        /// Tarih aralýðýna göre su üretim verilerini getirir
        /// </summary>
        Task<List<WaterProductionData>> GetDataByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// ÝZBB API'sinden veri çeker ve veritabanýna kaydeder
        /// </summary>
        Task<int> FetchAndSaveDataFromIzbbAsync();

        /// <summary>
        /// Yeni su üretim verisi ekler
        /// </summary>
        Task<WaterProductionData> CreateDataAsync(WaterProductionData data);
    }
}

