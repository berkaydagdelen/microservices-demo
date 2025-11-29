# ?? Debug Rehberi - Adým Adým

## Yöntem 1: Visual Studio ile Debug (Önerilen)

### Adým 1: UserService'i Debug Modda Çalýþtýrma

1. **Solution Explorer'da** `UserService` projesine **sað týklayýn**
2. **"Set as Startup Project"** seçin (kalýn yazý ile gösterilir)
3. `UsersController.cs` dosyasýný açýn
4. **Breakpoint koyun**:
   - `GetAllUsers()` metodunun içinde, `return Ok(_users);` satýrýna
   - Satýr numarasýnýn soluna týklayýn ? **Kýrmýzý nokta** görünür
5. **F5** tuþuna basýn veya üstteki **?? Start Debugging** butonuna týklayýn
6. Tarayýcý otomatik açýlýr: http://localhost:5001/swagger

### Adým 2: Breakpoint'te Durma ve Ýnceleme

1. Swagger UI'da **GET /api/users** endpoint'ini bulun
2. **"Try it out"** ? **"Execute"** butonuna týklayýn
3. Visual Studio'da kod **durur** (breakpoint'te)
4. Þimdi yapabilecekleriniz:
   - **Fareyi deðiþkenlerin üzerine getirin** ? Deðerlerini görün
   - **F10** ? Bir sonraki satýra geç
   - **F11** ? Fonksiyonun içine gir
   - **F5** ? Devam et (kod çalýþmaya devam eder)

### Adým 3: Ýkinci Servisi Çalýþtýrma

**ÖNEMLÝ**: UserService çalýþýrken, OrderService'i de çalýþtýrmak için:

#### Seçenek A: Solution Properties ile (Önerilen)

1. Solution'a sað týklayýn ? **"Properties"**
2. Sol menüden **"Startup Project"** seçin
3. **"Multiple startup projects"** seçin
4. Her iki servisi de **"Start"** olarak ayarlayýn:
   - UserService ? Start
   - OrderService ? Start
5. **OK** butonuna týklayýn
6. **F5** ile çalýþtýrýn ? Her iki servis de baþlar!

#### Seçenek B: Ýki Ayrý Visual Studio Instance

1. Ýlk Visual Studio'da UserService çalýþýyor
2. Yeni bir Visual Studio penceresi açýn
3. Ayný solution'ý açýn
4. OrderService'i startup project yapýn
5. F5 ile çalýþtýrýn

## Yöntem 2: Terminal ile Çalýþtýrma (Hýzlý Test)

### UserService'i Çalýþtýrma

1. **Terminal/PowerShell** açýn
2. Proje klasörüne gidin:
   ```powershell
   cd C:\Users\berkay\source\repos\microservices-demo\UserService
   ```
3. Çalýþtýrýn:
   ```powershell
   dotnet run
   ```
4. Çýktý:
   ```
   Now listening on: http://localhost:5001
   ```

### OrderService'i Çalýþtýrma (Yeni Terminal)

1. **Yeni bir Terminal/PowerShell** penceresi açýn
2. Proje klasörüne gidin:
   ```powershell
   cd C:\Users\berkay\source\repos\microservices-demo\OrderService
   ```
3. Çalýþtýrýn:
   ```powershell
   dotnet run
   ```
4. Çýktý:
   ```
   Now listening on: http://localhost:5002
   ```

## ?? Test Etme

### Swagger UI ile Test

1. **UserService**: http://localhost:5001/swagger
2. **OrderService**: http://localhost:5002/swagger

### Tarayýcý ile Test

**UserService:**
- http://localhost:5001/api/users
- http://localhost:5001/api/users/1

**OrderService:**
- http://localhost:5002/api/orders
- http://localhost:5002/api/orders/1
- http://localhost:5002/api/orders/user/1

### Postman/Thunder Client ile Test

Aþaðýdaki dosyayý kullanabilirsiniz: `test-requests.http`

## ?? Debug Ýpuçlarý

### 1. Watch Window
- **Debug** ? **Windows** ? **Watch**
- Deðiþkenleri ekleyip deðerlerini izleyin

### 2. Immediate Window
- **Debug** ? **Windows** ? **Immediate**
- Kod çalýþtýrýp sonuçlarý görebilirsiniz

### 3. Call Stack
- **Debug** ? **Windows** ? **Call Stack**
- Hangi fonksiyonlarýn çaðrýldýðýný görün

### 4. Locals Window
- Otomatik olarak mevcut scope'taki tüm deðiþkenleri gösterir

## ? Þimdi Yapmanýz Gerekenler

1. ? Visual Studio'da UserService'i debug modda çalýþtýrýn
2. ? `GetAllUsers` metoduna breakpoint koyun
3. ? Swagger'dan test edin ve breakpoint'te durduðunu görün
4. ? Deðiþkenleri inceleyin
5. ? OrderService'i de çalýþtýrýn (Multiple startup projects ile)
6. ? Her iki servisi de test edin

**Sorun yaþarsanýz bana söyleyin!** ??

