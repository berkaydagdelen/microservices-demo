# ADIM 1: Proje Ýncelemesi - Tamamlandý ?

## ?? Yeni Proje Yapýsý

Artýk projenizde **3 ayrý servis** var:

```
microservices-demo/
??? microservices-demo.sln          # Tüm projeleri bir arada tutan solution dosyasý
??? microservices-demo/              # Eski proje (þimdilik kullanmýyoruz)
??? UserService/                     # Kullanýcý servisi
?   ??? Controllers/
?   ?   ??? UsersController.cs      # Kullanýcý iþlemleri için API endpoint'leri
?   ??? Models/
?   ?   ??? User.cs                 # Kullanýcý veri modeli
?   ??? Program.cs                  # Servisin baþlangýç noktasý
?   ??? Properties/
?       ??? launchSettings.json     # Port ayarlarý (5001)
??? OrderService/                    # Sipariþ servisi
    ??? Controllers/
    ?   ??? OrdersController.cs     # Sipariþ iþlemleri için API endpoint'leri
    ??? Models/
    ?   ??? Order.cs                # Sipariþ veri modeli
    ??? Program.cs                  # Servisin baþlangýç noktasý
    ??? Properties/
        ??? launchSettings.json     # Port ayarlarý (5002)
```

## ?? Her Dosyanýn Görevi

### 1. **Program.cs** (Her serviste var)
- **Ne yapar?**: Servisin baþlangýç noktasý
- **Önemli kýsýmlar**:
  - `builder.Services.AddControllers()` ? Controller'larý aktif eder
  - `builder.Services.AddSwaggerGen()` ? API dokümantasyonu için Swagger ekler
  - `app.MapControllers()` ? Controller'lardaki endpoint'leri kaydeder

### 2. **Models/** Klasörü
- **Ne yapar?**: Veri modellerini tutar
- **User.cs**: Kullanýcý bilgileri (Id, Name, Email)
- **Order.cs**: Sipariþ bilgileri (Id, UserId, ProductName, Price, OrderDate)

### 3. **Controllers/** Klasörü
- **Ne yapar?**: HTTP isteklerini karþýlayan sýnýflar
- **UsersController.cs**: 
  - `GET /api/users` ? Tüm kullanýcýlarý getir
  - `GET /api/users/{id}` ? Belirli bir kullanýcýyý getir
  - `POST /api/users` ? Yeni kullanýcý oluþtur
- **OrdersController.cs**:
  - `GET /api/orders` ? Tüm sipariþleri getir
  - `GET /api/orders/{id}` ? Belirli bir sipariþi getir
  - `GET /api/orders/user/{userId}` ? Kullanýcýnýn sipariþlerini getir
  - `POST /api/orders` ? Yeni sipariþ oluþtur

### 4. **Properties/launchSettings.json**
- **Ne yapar?**: Servisin hangi port'ta çalýþacaðýný belirler
- **UserService**: Port 5001
- **OrderService**: Port 5002
- **Neden farklý portlar?**: Her servis baðýmsýz çalýþtýðý için farklý portlarda olmalý

## ?? Önemli Kavramlar

### Microservice Nedir?
- **Monolithic (Tek Parça)**: Tüm özellikler tek bir uygulamada
- **Microservice**: Her özellik ayrý bir servis olarak çalýþýr
  - UserService ? Sadece kullanýcý iþlemleri
  - OrderService ? Sadece sipariþ iþlemleri
  - Her biri baðýmsýz çalýþýr, farklý portlarda çalýþýr

### Neden Ayrý Servisler?
1. **Baðýmsýzlýk**: Bir servis çökerse diðeri çalýþmaya devam eder
2. **Ölçeklenebilirlik**: Sadece ihtiyaç olan servisi ölçeklendirebilirsiniz
3. **Teknoloji Özgürlüðü**: Her servis farklý teknoloji kullanabilir
4. **Takým Çalýþmasý**: Farklý takýmlar farklý servisler üzerinde çalýþabilir

## ? Þimdi Ne Yapmalýsýnýz?

1. Proje yapýsýný inceleyin
2. Her dosyayý açýp içeriðini okuyun
3. Kodlarý anlamaya çalýþýn
4. Sorularýnýz varsa not alýn

**Bir sonraki adýma geçmek için hazýr olduðunuzda bana söyleyin!** ??
