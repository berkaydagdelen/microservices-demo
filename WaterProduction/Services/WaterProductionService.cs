using WaterProduction.Models;
using WaterProduction.Repositories;

namespace WaterProduction.Services
{
    /// <summary>
    /// Su üretim iþlemleri için service implementation
    /// Ýþ mantýðý ve Ýzbb API entegrasyonu burada
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
            _logger.LogInformation("Service: Tüm su üretim verileri getiriliyor");
            return await _repository.GetAllAsync();
        }

        /// <summary>
        /// ID ile veri getirir
        /// </summary>
        public async Task<WaterProductionData?> GetDataByIdAsync(int id)
        {
            _logger.LogInformation($"Service: Su üretim verisi ID {id} getiriliyor");
            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Tarih aralýðýna göre verileri getirir
        /// </summary>
        public async Task<List<WaterProductionData>> GetDataByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation($"Service: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd} tarih aralýðýndaki veriler getiriliyor");
            return await _repository.GetByDateRangeAsync(startDate, endDate);
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
            _logger.LogInformation($"Service: Yeni su üretim verisi oluþturuluyor: {data.FacilityName}");

            // Ýþ kurallarý
            if (string.IsNullOrWhiteSpace(data.FacilityName))
            {
                throw new ArgumentException("Tesis adý boþ olamaz!");
            }

            if (data.ProductionAmount <= 0)
            {
                throw new ArgumentException("Üretim miktarý 0'dan büyük olmalýdýr!");
            }

            // Duplicate kontrolü
            var exists = await _repository.ExistsAsync(data.FacilityName, data.ProductionDate);
            if (exists)
            {
                throw new InvalidOperationException($"Bu tarih ve tesis için zaten veri mevcut!");
            }

            return await _repository.CreateAsync(data);
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

