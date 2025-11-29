# ?? Sorun Giderme Rehberi

## ? "ECONNREFUSED" Hatasý

Bu hata, servislerin çalýþmadýðý anlamýna gelir.

### ? Çözüm: Servisleri Çalýþtýrýn

#### Yöntem 1: Terminal ile (Önerilen)

**Terminal 1 - UserService:**
```powershell
cd UserService
dotnet run
```

**Terminal 2 - OrderService (Yeni Pencere):**
```powershell
cd OrderService
dotnet run
```

#### Yöntem 2: Visual Studio ile

1. Solution Explorer'da **UserService** ? Sað týk ? **"Set as Startup Project"**
2. **F5** ile çalýþtýrýn
3. Yeni bir Visual Studio penceresi açýn veya Solution Properties'ten Multiple Startup Projects ayarlayýn
4. **OrderService**'i de çalýþtýrýn

### ?? Servislerin Çalýþtýðýný Kontrol Etme

#### 1. Terminal Çýktýsýný Kontrol Edin

Servisler çalýþtýðýnda þu mesajý görürsünüz:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

#### 2. Tarayýcýdan Kontrol Edin

- **UserService**: http://localhost:5001/swagger
- **OrderService**: http://localhost:5002/swagger

Bu sayfalar açýlýyorsa servisler çalýþýyordur! ?

#### 3. Postman'den Test Edin

Servisler çalýþtýktan sonra Postman'den tekrar deneyin:
- `GET http://localhost:5001/api/users`

## ?? Diðer Yaygýn Hatalar

### Port Zaten Kullanýlýyor

**Hata:**
```
Failed to bind to address http://localhost:5001: address already in use
```

**Çözüm:**

1. Port'u kullanan process'i bulun:
```powershell
netstat -ano | findstr :5001
```

2. Process ID'yi not edin (son sütun)

3. Process'i sonlandýrýn:
```powershell
taskkill /PID <process_id> /F
```

Veya Task Manager'dan sonlandýrýn.

### Servis Çalýþmýyor

**Kontrol Listesi:**
- ? Projeyi build edin: `dotnet build`
- ? NuGet paketlerini restore edin: `dotnet restore`
- ? Port numaralarýný kontrol edin (`launchSettings.json`)
- ? Baþka bir uygulama ayný portu kullanýyor mu?

### 404 Not Found

**Hata:** `404 Not Found`

**Kontrol:**
- ? URL'yi kontrol edin: `http://localhost:5001/api/users`
- ? Endpoint adýný kontrol edin
- ? Servislerin çalýþtýðýndan emin olun

### 500 Internal Server Error

**Hata:** `500 Internal Server Error`

**Çözüm:**
- ? Servis loglarýný kontrol edin
- ? Breakpoint koyup debug yapýn
- ? JSON formatýný kontrol edin (POST istekleri için)

## ?? Hýzlý Kontrol Listesi

Servisleri çalýþtýrmadan önce:

- [ ] Projeler build edildi mi? (`dotnet build`)
- [ ] Port'lar boþ mu? (5001 ve 5002)
- [ ] .NET SDK yüklü mü? (`dotnet --version`)

Servisleri çalýþtýrdýktan sonra:

- [ ] Terminal'de "Now listening on" mesajý görünüyor mu?
- [ ] Swagger sayfalarý açýlýyor mu?
- [ ] Postman'den istek gönderebiliyor musunuz?

## ?? Adým Adým Çözüm

1. **Terminal 1'i açýn**
   ```powershell
   cd C:\Users\berkay\source\repos\microservices-demo\UserService
   dotnet run
   ```

2. **Terminal 2'yi açýn (Yeni Pencere)**
   ```powershell
   cd C:\Users\berkay\source\repos\microservices-demo\OrderService
   dotnet run
   ```

3. **Tarayýcýdan kontrol edin**
   - http://localhost:5001/swagger
   - http://localhost:5002/swagger

4. **Postman'den tekrar deneyin**
   - `GET http://localhost:5001/api/users`

**Sorun devam ederse bana söyleyin!** ??

