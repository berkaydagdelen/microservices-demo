# ? OrderService - Katmanlý Mimari Uygulandý!

## ?? Tamamlandý!

OrderService artýk **katmanlý mimari** kullanýyor!

## ?? Yeni Klasör Yapýsý

```
OrderService/
??? Controllers/              ? API Katmaný (HTTP istekleri)
?   ??? OrdersController.cs
??? Services/                ? Ýþ Mantýðý Katmaný (YENÝ!)
?   ??? IOrderService.cs
?   ??? OrderService.cs
??? Repositories/            ? Veri Eriþim Katmaný (YENÝ!)
?   ??? IOrderRepository.cs
?   ??? OrderRepository.cs
??? Models/                  ? Domain Katmaný
?   ??? Order.cs
??? Program.cs               ? DI ayarlarý eklendi
```

## ?? Katmanlar Arasý Ýletiþim

```
HTTP Request
    ?
[Controller] ? Service'e soruyor
    ?
[Service] ? Repository'ye soruyor
    ?
[Repository] ? Veriyi döndürüyor
    ?
Response
```

## ?? Her Katmanýn Görevi

### 1. Controller (API Katmaný)
- ? HTTP isteklerini alýr
- ? Service'e sorar
- ? Response döner
- ? Ýþ mantýðý YOK
- ? Veri eriþimi YOK

### 2. Service (Ýþ Mantýðý Katmaný)
- ? Ýþ kurallarýný uygular:
  - Ürün adý kontrolü
  - Fiyat kontrolü (0'dan büyük olmalý)
  - Kullanýcý ID kontrolü
  - Maksimum fiyat kontrolü (100.000 TL)
- ? Validasyon yapar
- ? Repository'ye sorar

### 3. Repository (Veri Eriþim Katmaný)
- ? Veriye eriþir
- ? CRUD iþlemlerini yapar
- ? Kullanýcý ID'sine göre filtreleme

## ?? Örnek: Ýþ Kurallarý

### Service Katmanýnda Uygulanan Kurallar:

```csharp
public Order CreateOrder(Order order)
{
    // Ýþ Kuralý 1: Ürün adý kontrolü
    if (string.IsNullOrWhiteSpace(order.ProductName))
    {
        throw new ArgumentException("Ürün adý boþ olamaz!");
    }

    // Ýþ Kuralý 2: Fiyat kontrolü
    if (order.Price <= 0)
    {
        throw new ArgumentException("Fiyat 0'dan büyük olmalýdýr!");
    }

    // Ýþ Kuralý 3: Kullanýcý ID kontrolü
    if (order.UserId <= 0)
    {
        throw new ArgumentException("Geçerli bir kullanýcý ID'si giriniz!");
    }

    // Ýþ Kuralý 4: Maksimum fiyat kontrolü
    if (order.Price > 100000)
    {
        throw new InvalidOperationException("Sipariþ fiyatý çok yüksek! Maksimum 100.000 TL olabilir.");
    }

    // Tüm kontroller geçti, sipariþi oluþtur
    return _orderRepository.Create(order);
}
```

## ?? Dependency Injection

### Program.cs'de Kayýt:

```csharp
// Repository'leri kaydet
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Service'leri kaydet
builder.Services.AddScoped<IOrderService, OrderService>();
```

### Controller'da Kullaným:

```csharp
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService; // DI ile otomatik enjekte edilir
    }
}
```

## ?? Test Etme

### 1. Build Edin:
```powershell
cd OrderService
dotnet build
```

### 2. Çalýþtýrýn:
```powershell
dotnet run
```

### 3. Swagger'da Test Edin:
- http://localhost:5002/swagger

### 4. Postman'de Test Edin:

**Baþarýlý Senaryo:**
```json
POST http://localhost:5002/api/orders
{
  "userId": 1,
  "productName": "Test Ürün",
  "price": 500.00
}
```

**Hata Senaryolarý:**

1. **Ürün adý boþ:**
```json
{
  "userId": 1,
  "productName": "",
  "price": 500.00
}
```
? Hata: "Ürün adý boþ olamaz!"

2. **Fiyat negatif:**
```json
{
  "userId": 1,
  "productName": "Test Ürün",
  "price": -100
}
```
? Hata: "Fiyat 0'dan büyük olmalýdýr!"

3. **Fiyat çok yüksek:**
```json
{
  "userId": 1,
  "productName": "Test Ürün",
  "price": 200000
}
```
? Hata: "Sipariþ fiyatý çok yüksek! Maksimum 100.000 TL olabilir."

## ?? Önce ve Sonra Karþýlaþtýrmasý

### ? ÖNCE:
- Her þey Controller'da
- Ýþ mantýðý ve veri eriþimi karýþýk
- Validasyon yok

### ? SONRA:
- Her katman kendi iþini yapýyor
- Ýþ mantýðý ayrý, veri eriþimi ayrý
- Validasyon ve iþ kurallarý Service'de

## ?? Öðrendikleriniz

1. ? Katmanlý mimariyi OrderService'e uyguladýnýz
2. ? Ýþ kurallarýný Service katmanýnda uyguladýnýz
3. ? Dependency Injection kullandýnýz
4. ? Her katmanýn sorumluluðunu anladýnýz

## ?? Sonraki Adýmlar

1. ? Her iki servis de katmanlý mimari kullanýyor
2. ?? Servisler arasý iletiþim ekleyebilirsiniz
3. ?? Unit test yazabilirsiniz
4. ?? Veritabaný ekleyebilirsiniz (Repository'de deðiþiklik yeterli!)

**Tebrikler! Her iki servis de artýk katmanlý mimari kullanýyor!** ??

