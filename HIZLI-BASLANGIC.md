# ?? Hýzlý Baþlangýç - Projeyi Çalýþtýrma

## ? En Hýzlý Yol: Visual Studio ile Debug

### 1?? Ýlk Adým: UserService'i Çalýþtýrma

```
1. Visual Studio'da solution'ý açýn
2. Solution Explorer'da "UserService" ? Sað týk ? "Set as Startup Project"
3. UsersController.cs dosyasýný açýn
4. GetAllUsers() metodunda breakpoint koyun (satýr numarasýnýn soluna týklayýn)
5. F5 tuþuna basýn
```

**? Beklenen Sonuç:**
- Tarayýcý açýlýr: http://localhost:5001/swagger
- Visual Studio'da "Debugging" modunda çalýþýyor yazýsý görünür

### 2?? Ýkinci Adým: OrderService'i de Çalýþtýrma

**Yöntem A: Multiple Startup Projects (Önerilen)**

```
1. Solution'a sað týklayýn ? "Properties"
2. Sol menüden "Startup Project" seçin
3. "Multiple startup projects" radio button'unu seçin
4. Tabloda:
   - UserService ? Action: "Start"
   - OrderService ? Action: "Start"
5. OK butonuna týklayýn
6. F5 ile çalýþtýrýn
```

**Yöntem B: Ýki Ayrý Terminal**

Terminal 1:
```powershell
cd UserService
dotnet run
```

Terminal 2 (yeni pencere):
```powershell
cd OrderService
dotnet run
```

## ?? Test Etme

### Swagger UI ile (En Kolay)

1. **UserService Swagger**: http://localhost:5001/swagger
   - GET /api/users ? "Try it out" ? "Execute"
   
2. **OrderService Swagger**: http://localhost:5002/swagger
   - GET /api/orders ? "Try it out" ? "Execute"

### Tarayýcý ile

- http://localhost:5001/api/users
- http://localhost:5002/api/orders

### VS Code REST Client ile

`test-requests.http` dosyasýný açýn ve istekleri çalýþtýrýn.

## ?? Debug Yapma

### Breakpoint Koyma

1. Kod satýrýnýn soluna týklayýn (kýrmýzý nokta)
2. F5 ile çalýþtýrýn
3. Swagger'dan istek gönderin
4. Kod durur ? Deðiþkenleri inceleyin

### Debug Tuþlarý

- **F5**: Continue (Devam et)
- **F10**: Step Over (Bir sonraki satýr)
- **F11**: Step Into (Fonksiyonun içine gir)
- **Shift+F5**: Stop Debugging

## ?? Port Bilgileri

| Servis | HTTP Port | HTTPS Port | Swagger URL |
|--------|-----------|------------|-------------|
| UserService | 5001 | 7001 | http://localhost:5001/swagger |
| OrderService | 5002 | 7002 | http://localhost:5002/swagger |

## ?? Sorun Giderme

### Port Zaten Kullanýlýyor

```powershell
# Port'u kullanan process'i bul
netstat -ano | findstr :5001

# Process ID'yi not alýn ve Task Manager'dan sonlandýrýn
```

### Servis Çalýþmýyor

```powershell
# Projeyi temizle ve yeniden build et
dotnet clean
dotnet build
dotnet run
```

## ? Kontrol Listesi

- [ ] UserService çalýþýyor (Port 5001)
- [ ] OrderService çalýþýyor (Port 5002)
- [ ] Swagger UI'larý açýlýyor
- [ ] GET istekleri çalýþýyor
- [ ] Breakpoint'te duruyor (debug modda)

**Hazýrsanýz bir sonraki adýma geçelim!** ??

