using Microsoft.AspNetCore.Mvc;
using WaterProduction.Models;
using WaterProduction.Services;

namespace WaterProduction.Controllers
{
    /// <summary>
    /// ÝZSU - Su Üretim Verileri API Controller
    /// Ýzmir Büyükþehir Belediyesi API'sinden su üretim verilerini yönetir
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WaterProductionController : ControllerBase
    {
        private readonly IWaterProductionService _waterProductionService;
        private readonly ILogger<WaterProductionController> _logger;

        public WaterProductionController(
            IWaterProductionService waterProductionService,
            ILogger<WaterProductionController> logger)
        {
            _waterProductionService = waterProductionService;
            _logger = logger;
        }

        /// <summary>
        /// Tüm su üretim verilerini getirir
        /// </summary>
        /// <returns>Su üretim verileri listesi</returns>
        [HttpGet]
        public async Task<ActionResult<List<WaterProductionData>>> GetAll()
        {
            _logger.LogInformation("GET /api/WaterProduction - Tüm veriler isteniyor");
            var data = await _waterProductionService.GetAllDataAsync();
            return Ok(data);
        }

        /// <summary>
        /// ID'ye göre su üretim verisi getirir
        /// </summary>
        /// <param name="id">Veri ID'si</param>
        /// <returns>Su üretim verisi</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<WaterProductionData>> GetById(int id)
        {
            _logger.LogInformation($"GET /api/WaterProduction/{id} - Veri isteniyor");
            var data = await _waterProductionService.GetDataByIdAsync(id);
            
            if (data == null)
            {
                return NotFound(new { message = $"ID {id} ile veri bulunamadý" });
            }

            return Ok(data);
        }

        /// <summary>
        /// Tarih aralýðýna göre su üretim verilerini getirir
        /// </summary>
        /// <param name="startDate">Baþlangýç tarihi (yyyy-MM-dd)</param>
        /// <param name="endDate">Bitiþ tarihi (yyyy-MM-dd)</param>
        /// <returns>Su üretim verileri listesi</returns>
        [HttpGet("date-range")]
        public async Task<ActionResult<List<WaterProductionData>>> GetByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            _logger.LogInformation($"GET /api/WaterProduction/date-range - {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd} aralýðý isteniyor");
            var data = await _waterProductionService.GetDataByDateRangeAsync(startDate, endDate);
            return Ok(data);
        }

        /// <summary>
        /// ÝZBB API'sinden veri çeker ve veritabanýna kaydeder
        /// </summary>
        /// <returns>Kaydedilen veri sayýsý</returns>
        [HttpPost("fetch-from-izbb")]
        public async Task<ActionResult<object>> FetchFromIzbb()
        {
            _logger.LogInformation("POST /api/WaterProduction/fetch-from-izbb - ÝZBB API'sinden veri çekiliyor");
            var savedCount = await _waterProductionService.FetchAndSaveDataFromIzbbAsync();
            
            return Ok(new 
            { 
                message = "ÝZBB API'sinden veri baþarýyla çekildi ve kaydedildi",
                savedCount = savedCount 
            });
        }

        /// <summary>
        /// Yeni su üretim verisi ekler
        /// </summary>
        /// <param name="data">Su üretim verisi</param>
        /// <returns>Oluþturulan veri</returns>
        [HttpPost]
        public async Task<ActionResult<WaterProductionData>> Create([FromBody] WaterProductionData data)
        {
            _logger.LogInformation("POST /api/WaterProduction - Yeni veri oluþturuluyor");
            
            if (data == null)
            {
                return BadRequest(new { message = "Veri boþ olamaz" });
            }

            var createdData = await _waterProductionService.CreateDataAsync(data);
            return CreatedAtAction(nameof(GetById), new { id = createdData.Id }, createdData);
        }
    }
}

