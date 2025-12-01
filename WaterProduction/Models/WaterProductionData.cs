namespace WaterProduction.Models
{
    /// <summary>
    /// Su üretim verilerini temsil eden model
    /// Ýzbb API'den gelecek veriler ve veritabanýnda saklanacak veriler için
    /// </summary>
    public class WaterProductionData
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Tesis adý veya lokasyon
        /// </summary>
        public string FacilityName { get; set; } = string.Empty;
        
        /// <summary>
        /// Üretim miktarý (m³)
        /// </summary>
        public decimal ProductionAmount { get; set; }
        
        /// <summary>
        /// Tarih
        /// </summary>
        public DateTime ProductionDate { get; set; }
        
        /// <summary>
        /// Veri kaynaðý (Ýzbb API)
        /// </summary>
        public string Source { get; set; } = "Ýzbb API";
        
        /// <summary>
        /// Veri çekilme zamaný
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        /// <summary>
        /// Son güncelleme zamaný
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

