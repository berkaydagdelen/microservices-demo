# ?? Postman Hýzlý Baþlangýç

## ? 5 Dakikada Baþlayýn!

### Adým 1: Servisleri Çalýþtýrýn

**Terminal 1:**
```powershell
cd UserService
dotnet run
```

**Terminal 2 (Yeni Pencere):**
```powershell
cd OrderService
dotnet run
```

? **Kontrol**: 
- http://localhost:5001/swagger açýlmalý
- http://localhost:5002/swagger açýlmalý

### Adým 2: Postman Collection'ý Import Edin

1. **Postman'i açýn**
2. Sol üstte **"Import"** butonuna týklayýn
3. **"Upload Files"** seçin
4. `Microservices-Demo.postman_collection.json` dosyasýný seçin
5. **"Import"** butonuna týklayýn

? **Sonuç**: Sol panelde "Microservices Demo" collection'ý görünür!

### Adým 3: Ýlk Ýsteði Gönderin

1. **"Microservices Demo"** ? **"UserService"** ? **"Get All Users"** isteðini açýn
2. **"Send"** butonuna týklayýn
3. Alt panelde kullanýcý listesi görünür! ??

## ?? Tüm Ýstekler

### UserService Ýstekleri:

1. **Get All Users** ? Tüm kullanýcýlarý getir
2. **Get User By ID** ? ID=1 kullanýcýyý getir
3. **Create New User** ? Yeni kullanýcý oluþtur

### OrderService Ýstekleri:

1. **Get All Orders** ? Tüm sipariþleri getir
2. **Get Order By ID** ? ID=1 sipariþi getir
3. **Get Orders By User** ? Kullanýcýnýn sipariþlerini getir
4. **Create New Order** ? Yeni sipariþ oluþtur

## ?? Ýlk Test Senaryosu

1. ? **Get All Users** ? Kullanýcýlarý görün
2. ? **Create New User** ? Yeni kullanýcý oluþturun (Body'deki JSON'u düzenleyin)
3. ? **Get Orders By User** ? Kullanýcýnýn sipariþlerini görün
4. ? **Create New Order** ? Yeni sipariþ oluþturun

## ?? Ýpuçlarý

- **Body Düzenleme**: POST isteklerinde Body sekmesinden JSON'u düzenleyin
- **ID Deðiþtirme**: URL'deki ID numaralarýný deðiþtirerek farklý kayýtlarý getirebilirsiniz
- **Save**: Ýstekleri düzenledikten sonra Save edin

**Hazýrsanýz test etmeye baþlayýn!** ??

