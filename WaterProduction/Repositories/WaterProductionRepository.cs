using Microsoft.EntityFrameworkCore;
using WaterProduction.Data;
using WaterProduction.Models;

namespace WaterProduction.Repositories
{
    /// <summary>
    /// Su üretim verilerine eriþim için repository implementation
    /// Veri eriþim katmaný - Entity Framework Core kullanýr
    /// </summary>
    public class WaterProductionRepository : IWaterProductionRepository
    {
        private readonly WaterProductionDbContext _context;
        private readonly ILogger<WaterProductionRepository> _logger;

        public WaterProductionRepository(
            WaterProductionDbContext context,
            ILogger<WaterProductionRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Tüm verileri getirir
        /// </summary>
        public async Task<List<WaterProductionData>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Repository: Tüm veriler getiriliyor");
                return await _context.WaterProductionData
                    .OrderByDescending(x => x.ProductionDate)
                    .ThenBy(x => x.FacilityName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Tüm veriler getirilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// ID ile veri getirir
        /// </summary>
        public async Task<WaterProductionData?> GetByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Repository: ID {id} ile veri getiriliyor");
                return await _context.WaterProductionData
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Repository: ID {id} ile veri getirilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Tarih aralýðýna göre verileri getirir
        /// </summary>
        public async Task<List<WaterProductionData>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.LogInformation($"Repository: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd} aralýðýndaki veriler getiriliyor");
                return await _context.WaterProductionData
                    .Where(x => x.ProductionDate >= startDate && x.ProductionDate <= endDate)
                    .OrderByDescending(x => x.ProductionDate)
                    .ThenBy(x => x.FacilityName)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Tarih aralýðýna göre veriler getirilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Tesis adý ve tarihe göre veri var mý kontrol eder
        /// </summary>
        public async Task<bool> ExistsAsync(string facilityName, DateTime productionDate)
        {
            try
            {
                _logger.LogInformation($"Repository: Duplicate kontrolü - Tesis: {facilityName}, Tarih: {productionDate:yyyy-MM-dd}");
                return await _context.WaterProductionData
                    .AnyAsync(x => x.FacilityName == facilityName && x.ProductionDate.Date == productionDate.Date);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Duplicate kontrolü yapýlýrken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Yeni veri ekler
        /// </summary>
        public async Task<WaterProductionData> CreateAsync(WaterProductionData data)
        {
            try
            {
                _logger.LogInformation($"Repository: Yeni veri ekleniyor - Tesis: {data.FacilityName}");
                
                data.CreatedAt = DateTime.Now;
                _context.WaterProductionData.Add(data);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation($"Repository: Veri baþarýyla eklendi - ID: {data.Id}");
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Veri eklenirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Toplu veri ekler
        /// </summary>
        public async Task<int> CreateBatchAsync(List<WaterProductionData> dataList)
        {
            try
            {
                _logger.LogInformation($"Repository: {dataList.Count} adet veri toplu olarak ekleniyor");
                
                foreach (var data in dataList)
                {
                    data.CreatedAt = DateTime.Now;
                }
                
                _context.WaterProductionData.AddRange(dataList);
                var savedCount = await _context.SaveChangesAsync();
                
                _logger.LogInformation($"Repository: {savedCount} adet veri baþarýyla eklendi");
                return savedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Repository: Toplu veri eklenirken hata oluþtu");
                throw;
            }
        }
    }
}

