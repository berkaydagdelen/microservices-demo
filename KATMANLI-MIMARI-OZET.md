# ??? Katmanlý Mimari - Hýzlý Özet

## ? Ne Yaptýk?

UserService'i **basit yapýdan** **katmanlý mimariye** dönüþtürdük!

## ?? Yeni Klasör Yapýsý

```
UserService/
??? Controllers/        ? HTTP isteklerini alýr
?   ??? UsersController.cs
??? Services/          ? Ýþ mantýðýný yönetir (YENÝ!)
?   ??? IUserService.cs
?   ??? UserService.cs
??? Repositories/      ? Veri eriþimini yönetir (YENÝ!)
?   ??? IUserRepository.cs
?   ??? UserRepository.cs
??? Models/            ? Veri modelleri
?   ??? User.cs
??? Program.cs         ? DI ayarlarý eklendi
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
- ? Ýþ kurallarýný uygular
- ? Validasyon yapar
- ? Repository'ye sorar
- ? HTTP iþlemleri YOK
- ? Direkt veri eriþimi YOK

### 3. Repository (Veri Eriþim Katmaný)
- ? Veriye eriþir
- ? CRUD iþlemlerini yapar
- ? Ýþ mantýðý YOK
- ? HTTP iþlemleri YOK

## ?? Dependency Injection (DI)

Katmanlar birbirine **interface'ler** üzerinden baðlanýr:

```csharp
// Program.cs'de kayýt
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Controller'da kullaným
public UsersController(IUserService userService)
{
    _userService = userService; // Otomatik enjekte edilir
}
```

## ?? Test Etme

Projeyi çalýþtýrýn ve test edin:

1. **Build edin**: `dotnet build`
2. **Çalýþtýrýn**: `dotnet run` veya F5
3. **Swagger'da test edin**: http://localhost:5001/swagger
4. **Postman'de test edin**: `GET http://localhost:5001/api/users`

## ?? Daha Fazla Bilgi

- `KATMANLI-MIMARI-ACIKLAMA.md` - Detaylý açýklama
- `KATMANLI-MIMARI-KARSILASTIRMA.md` - Önce/Sonra karþýlaþtýrmasý

## ?? Öðrendikleriniz

1. ? Katmanlý mimari nedir?
2. ? Her katmanýn görevi nedir?
3. ? Dependency Injection nasýl çalýþýr?
4. ? Neden katmanlý mimari kullanýlýr?

**Hazýr olduðunuzda OrderService'i de dönüþtürebiliriz!** ??

