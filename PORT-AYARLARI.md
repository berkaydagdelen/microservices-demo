# ?? Port Ayarlarý ve Swagger URL Sorunu

## ? Sorun: Swagger'da Yanlýþ Port Görünüyor

Swagger'da `https://localhost:44361/api/Users` görünüyorsa, bu **IIS Express** portunu kullanýyor demektir.

## ? Çözüm 1: HTTP Profilini Kullan (Önerilen)

### Visual Studio'da:

1. **UserService** projesine sað týklayýn
2. **"Properties"** seçin
3. **"Debug"** sekmesine gidin
4. **"Open debug launch profiles UI"** týklayýn
5. **"http"** profilini seçin (https deðil!)
6. **F5** ile çalýþtýrýn

**Sonuç**: Swagger'da `http://localhost:5001` görünecek ?

### Veya Dropdown'dan Seçin:

Visual Studio'da üstteki toolbar'da profil dropdown'ýndan **"http"** seçin.

## ? Çözüm 2: Swagger'da URL'yi Manuel Deðiþtir

Swagger UI'da:
1. Endpoint'i açýn
2. URL'yi þu þekilde deðiþtirin:
   - `https://localhost:44361/api/users` ?
   - `http://localhost:5001/api/users` ?

## ?? Port Numaralarý

| Servis | HTTP Port | HTTPS Port | IIS Express Port |
|--------|-----------|------------|------------------|
| UserService | **5001** ? | 7001 | 44361 |
| OrderService | **5002** ? | 7002 | 44358 |

**Önerilen**: HTTP portlarýný (5001, 5002) kullanýn, daha basit!

## ?? Postman Ýçin Doðru URL'ler

Postman'de þu URL'leri kullanýn:

### UserService:
- ? `http://localhost:5001/api/users`
- ? `https://localhost:44361/api/users` (IIS Express)

### OrderService:
- ? `http://localhost:5002/api/orders`
- ? `https://localhost:44358/api/orders` (IIS Express)

## ?? Hangi Port Kullanýlýyor?

Terminal'de servisi çalýþtýrdýðýnýzda þunu görürsünüz:
```
Now listening on: http://localhost:5001
```

Bu, **5001** portunu kullanmanýz gerektiðini gösterir.

## ?? Ýpucu: launchSettings.json'da Varsayýlan Profil

Eðer her zaman HTTP kullanmak istiyorsanýz, `launchSettings.json` dosyasýnda profillerin sýrasýný deðiþtirebilirsiniz. Ýlk profil varsayýlan olarak seçilir.

**Þu anki sýra:**
1. "http" ? (Ýyi - HTTP önce)
2. "https"
3. "IIS Express"

## ? Kontrol Listesi

- [ ] Visual Studio'da **"http"** profilini seçtiniz mi?
- [ ] Swagger'da `http://localhost:5001` görünüyor mu?
- [ ] Postman'de `http://localhost:5001/api/users` çalýþýyor mu?

**Sorun devam ederse bana söyleyin!** ??

