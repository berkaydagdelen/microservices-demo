using WaterProduction.Models;
using WaterProduction.Repositories;

namespace WaterProduction.Services
{
    /// <summary>
    /// Su üretim iþlemleri için service implementation
    /// Ýþ mantýðý ve ÝZBB API entegrasyonu burada
    /// </summary>
    public class WaterProductionService : IWaterProductionService
    {
        private readonly IWaterProductionRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WaterProductionService> _logger;
        private readonly IConfiguration _configuration;

        public WaterProductionService(
            IWaterProductionRepository repository,
            IHttpClientFactory httpClientFactory,
            ILogger<WaterProductionService> logger,
            IConfiguration configuration)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Tüm verileri getirir
        /// </summary>
        public async Task<List<WaterProductionData>> GetAllDataAsync()
        {
            try
            {
                _logger.LogInformation("Service: Tüm su üretim verileri getiriliyor");
                var data = await _repository.GetAllAsync();
                _logger.LogInformation($"Service: {data.Count} adet veri getirildi");
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Tüm veriler getirilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// ID ile veri getirir
        /// </summary>
        public async Task<WaterProductionData?> GetDataByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning($"Service: Geçersiz ID deðeri: {id}");
                    throw new ArgumentException("ID 0'dan büyük olmalýdýr", nameof(id));
                }

                _logger.LogInformation($"Service: Su üretim verisi ID {id} getiriliyor");
                var data = await _repository.GetByIdAsync(id);
                
                if (data == null)
                {
                    _logger.LogWarning($"Service: ID {id} ile veri bulunamadý");
                }
                else
                {
                    _logger.LogInformation($"Service: ID {id} ile veri bulundu");
                }
                
                return data;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Service: ID {id} ile veri getirilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Tarih aralýðýna göre verileri getirir
        /// </summary>
        public async Task<List<WaterProductionData>> GetDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                {
                    _logger.LogWarning($"Service: Geçersiz tarih aralýðý - Baþlangýç: {startDate:yyyy-MM-dd}, Bitiþ: {endDate:yyyy-MM-dd}");
                    throw new ArgumentException("Baþlangýç tarihi bitiþ tarihinden büyük olamaz");
                }

                _logger.LogInformation($"Service: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd} tarih aralýðýndaki veriler getiriliyor");
                var data = await _repository.GetByDateRangeAsync(startDate, endDate);
                _logger.LogInformation($"Service: {data.Count} adet veri getirildi");
                return data;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Service: Tarih aralýðýna göre veriler getirilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Ýzbb API'den veri çeker ve veritabanýna kaydeder
        /// </summary>
        public async Task<int> FetchAndSaveDataFromIzbbAsync()
        {
            _logger.LogInformation("Service: Ýzbb API'den veri çekiliyor...");

            try
            {
                // Ýzbb API URL'i (appsettings.json'dan okunacak)
                var apiUrl = _configuration["IzbbApi:WaterProductionUrl"] 
                    ?? "https://api.izmir.bel.tr/api/su-uretim"; // Varsayýlan URL

                var httpClient = _httpClientFactory.CreateClient();
                httpClient.Timeout = TimeSpan.FromSeconds(30);

                // API'den veri çek
                var response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Ýzbb API hatasý: {response.StatusCode}");
                    throw new HttpRequestException($"Ýzbb API hatasý: {response.StatusCode}");
                }

                var jsonContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Ýzbb API'den veri alýndý: {jsonContent.Length} karakter");

                // JSON'u parse et (þimdilik basit bir örnek, gerçek API yanýtýna göre güncellenecek)
                var dataList = ParseIzbbApiResponse(jsonContent);

                if (dataList == null || !dataList.Any())
                {
                    _logger.LogWarning("Ýzbb API'den veri alýnamadý veya boþ");
                    return 0;
                }

                // Duplicate kontrolü yap ve sadece yeni verileri ekle
                var newDataList = new List<WaterProductionData>();
                foreach (var data in dataList)
                {
                    var exists = await _repository.ExistsAsync(data.FacilityName, data.ProductionDate);
                    if (!exists)
                    {
                        newDataList.Add(data);
                    }
                }

                if (!newDataList.Any())
                {
                    _logger.LogInformation("Tüm veriler zaten mevcut, yeni veri eklenmedi");
                    return 0;
                }

                // Veritabanýna kaydet
                var savedCount = await _repository.CreateBatchAsync(newDataList);
                _logger.LogInformation($"{savedCount} adet yeni veri kaydedildi");

                return savedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ýzbb API'den veri çekilirken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Manuel olarak veri ekler
        /// </summary>
        public async Task<WaterProductionData> CreateDataAsync(WaterProductionData data)
        {
            try
            {
                if (data == null)
                {
                    _logger.LogWarning("Service: Null veri ile create iþlemi yapýlmaya çalýþýldý");
                    throw new ArgumentNullException(nameof(data), "Veri boþ olamaz");
                }

                _logger.LogInformation($"Service: Yeni su üretim verisi oluþturuluyor: {data.FacilityName}");

                // Ýþ kurallarý
                if (string.IsNullOrWhiteSpace(data.FacilityName))
                {
                    _logger.LogWarning("Service: Tesis adý boþ olamaz");
                    throw new ArgumentException("Tesis adý boþ olamaz!", nameof(data));
                }

                if (data.ProductionAmount <= 0)
                {
                    _logger.LogWarning($"Service: Geçersiz üretim miktarý: {data.ProductionAmount}");
                    throw new ArgumentException("Üretim miktarý 0'dan büyük olmalýdýr!", nameof(data));
                }

                // Duplicate kontrolü
                var exists = await _repository.ExistsAsync(data.FacilityName, data.ProductionDate);
                if (exists)
                {
                    _logger.LogWarning($"Service: Duplicate veri - Tesis: {data.FacilityName}, Tarih: {data.ProductionDate:yyyy-MM-dd}");
                    throw new InvalidOperationException($"Bu tarih ve tesis için zaten veri mevcut!");
                }

                var createdData = await _repository.CreateAsync(data);
                _logger.LogInformation($"Service: Veri baþarýyla oluþturuldu - ID: {createdData.Id}");
                return createdData;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Service: Veri oluþturulurken hata oluþtu");
                throw;
            }
        }

        /// <summary>
        /// Ýzbb API yanýtýný parse eder
        /// NOT: Bu metod gerçek API yanýtýna göre güncellenecek
        /// </summary>
        private List<WaterProductionData> ParseIzbbApiResponse(string jsonContent)
        {
            // Þimdilik örnek veri döndürüyoruz
            // Gerçek API yanýtý geldiðinde bu metod güncellenecek
            _logger.LogWarning("ParseIzbbApiResponse: Gerçek API yanýtý parse edilecek (þimdilik örnek veri)");

            // TODO: Gerçek API yanýtýný parse et
            // Örnek:
            // var apiResponse = JsonSerializer.Deserialize<IzbbApiResponse>(jsonContent);
            // return apiResponse.Data.Select(x => new WaterProductionData { ... }).ToList();

            return new List<WaterProductionData>();
        }
    }
}

