# ADIM 3: Projeyi Çalýþtýrma (Docker Olmadan)

## ?? Bu Adýmda Öðrenecekleriniz
- Her servisi nasýl çalýþtýracaðýnýz
- Debug modda nasýl çalýþtýracaðýnýz
- Ýki servisi ayný anda nasýl çalýþtýracaðýnýz
- Swagger UI ile API'leri nasýl test edeceðiniz

## ?? Yöntem 1: Visual Studio/Rider ile Debug Modda Çalýþtýrma

### Adým 1: UserService'i Çalýþtýrma

1. **Visual Studio'da**:
   - Solution Explorer'da `UserService` projesine sað týklayýn
   - "Set as Startup Project" seçin
   - F5 tuþuna basýn veya "Start Debugging" butonuna týklayýn

2. **Rider'da**:
   - `UserService` projesine sað týklayýn
   - "Set as Startup Project" seçin
   - Shift+F10 veya Run butonuna týklayýn

3. **VS Code'da**:
   - Terminal açýn
   - `cd UserService` komutu ile klasöre gidin
   - `dotnet run` komutunu çalýþtýrýn

### Adým 2: OrderService'i Çalýþtýrma

**ÖNEMLÝ**: UserService çalýþýrken, OrderService'i de çalýþtýrmak için:

1. **Visual Studio'da** (Birden fazla proje çalýþtýrma):
   - Solution'a sað týklayýn ? "Properties"
   - "Multiple startup projects" seçin
   - Her iki servisi de "Start" olarak ayarlayýn

2. **Veya Terminal'de**:
   - Ýki ayrý terminal penceresi açýn
   - Birinde: `cd UserService && dotnet run`
   - Diðerinde: `cd OrderService && dotnet run`

## ?? Yöntem 2: Terminal/Command Line ile Çalýþtýrma

### UserService'i Çalýþtýrma

```bash
cd UserService
dotnet run
```

**Beklenen Çýktý:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### OrderService'i Çalýþtýrma

**Yeni bir terminal penceresi açýn** ve:

```bash
cd OrderService
dotnet run
```

**Beklenen Çýktý:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5002
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

## ?? Servisleri Test Etme

### Swagger UI ile Test

Her servis çalýþtýðýnda otomatik olarak Swagger UI açýlýr. Eðer açýlmazsa:

- **UserService**: http://localhost:5001/swagger
- **OrderService**: http://localhost:5002/swagger

Swagger UI'da:
1. Endpoint'leri görebilirsiniz
2. "Try it out" butonuna týklayarak test edebilirsiniz
3. Response'larý görebilirsiniz

### Manuel Test (Tarayýcý veya Postman)

**UserService Endpoint'leri:**
- GET http://localhost:5001/api/users
- GET http://localhost:5001/api/users/1
- POST http://localhost:5001/api/users

**OrderService Endpoint'leri:**
- GET http://localhost:5002/api/orders
- GET http://localhost:5002/api/orders/1
- GET http://localhost:5002/api/orders/user/1
- POST http://localhost:5002/api/orders

## ?? Debug Yapma

### Breakpoint Koyma

1. Visual Studio/Rider'da:
   - Kod satýrýnýn soluna týklayýn (kýrmýzý nokta görünür)
   - F5 ile çalýþtýrýn
   - Ýstek geldiðinde kod durur ve deðiþkenleri inceleyebilirsiniz

2. Örnek: `UsersController.cs` dosyasýnda `GetAllUsers` metoduna breakpoint koyun
   - Tarayýcýdan http://localhost:5001/api/users adresine gidin
   - Kod durur, deðiþkenleri görebilirsiniz

### Debug Adýmlarý

- **F10**: Step Over (Bir sonraki satýra geç)
- **F11**: Step Into (Fonksiyonun içine gir)
- **F5**: Continue (Devam et)
- **Shift+F5**: Stop Debugging

## ?? Sorun Giderme

### Port Zaten Kullanýlýyor Hatasý

Eðer "port already in use" hatasý alýrsanýz:

1. Port'u kullanan process'i bulun:
   ```powershell
   netstat -ano | findstr :5001
   ```

2. Process'i sonlandýrýn veya `launchSettings.json`'da port'u deðiþtirin

### Servisler Çalýþmýyor

1. Projeleri build edin:
   ```bash
   dotnet build
   ```

2. NuGet paketlerini restore edin:
   ```bash
   dotnet restore
   ```

## ? Þimdi Yapmanýz Gerekenler

1. **UserService'i çalýþtýrýn** ve http://localhost:5001/swagger adresine gidin
2. **GET /api/users** endpoint'ini test edin
3. **OrderService'i çalýþtýrýn** (yeni terminal) ve http://localhost:5002/swagger adresine gidin
4. **GET /api/orders** endpoint'ini test edin
5. Her iki servisin de çalýþtýðýný doðrulayýn

**Hazýr olduðunuzda bir sonraki adýma geçelim!** ??

