# ??? Katmanlý Mimari (Layered Architecture)

## ?? Bu Rehberde Öðrenecekleriniz
- Katmanlý mimari nedir?
- Her katmanýn görevi nedir?
- Neden katmanlý mimari kullanýlýr?
- Projede nasýl uygulanýr?

## ?? Katmanlý Mimari Nedir?

Katmanlý mimari, uygulamayý **sorumluluklara göre ayrýlmýþ katmanlara** böler. Her katmanýn kendine özgü bir görevi vardýr.

## ?? Katmanlar ve Görevleri

### 1. **Controllers (API/Presentation Layer)** 
**Görevi**: HTTP isteklerini alýr ve yanýt verir

**Sorumluluklarý**:
- HTTP isteklerini karþýlar (GET, POST, PUT, DELETE)
- Request'leri doðrular
- Response'larý formatlar
- HTTP status code'larý döner

**Örnek**: `UsersController.cs`

### 2. **Services (Business Logic Layer)**
**Görevi**: Ýþ mantýðýný yönetir

**Sorumluluklarý**:
- Ýþ kurallarýný uygular
- Validasyon yapar
- Controller ve Repository arasýnda köprü görevi görür
- Karmaþýk iþlemleri yönetir

**Örnek**: `UserService.cs` - Kullanýcý iþlemlerini yönetir

### 3. **Repositories (Data Access Layer)**
**Görevi**: Veri eriþimini yönetir

**Sorumluluklarý**:
- Veritabaný iþlemlerini yapar
- CRUD (Create, Read, Update, Delete) iþlemlerini yönetir
- Veri kaynaðýndan baðýmsýzdýr (veritabaný, dosya, API olabilir)

**Örnek**: `UserRepository.cs` - Kullanýcý verilerini yönetir

### 4. **Models/Entities (Domain Layer)**
**Görevi**: Veri modellerini tanýmlar

**Sorumluluklarý**:
- Veri yapýlarýný tanýmlar
- Domain kurallarýný içerir

**Örnek**: `User.cs` - Kullanýcý modeli

## ?? Ýstek Akýþý

```
HTTP Request
    ?
[Controllers] ? HTTP isteklerini alýr
    ?
[Services] ? Ýþ mantýðýný uygular
    ?
[Repositories] ? Veriye eriþir
    ?
[Models/Entities] ? Veri modelleri
```

## ?? Klasör Yapýsý

### Þu Anki Yapý (Basit):
```
UserService/
??? Controllers/
?   ??? UsersController.cs  (Her þey burada!)
??? Models/
?   ??? User.cs
??? Program.cs
```

### Katmanlý Mimari Yapýsý:
```
UserService/
??? Controllers/           ? API Katmaný
?   ??? UsersController.cs
??? Services/             ? Ýþ Mantýðý Katmaný
?   ??? UserService.cs
??? Repositories/         ? Veri Eriþim Katmaný
?   ??? UserRepository.cs
??? Models/              ? Domain Katmaný
?   ??? User.cs
??? Program.cs
```

## ?? Neden Katmanlý Mimari?

### ? Avantajlarý:

1. **Ayrý Sorumluluklar**: Her katman kendi iþini yapar
2. **Bakým Kolaylýðý**: Deðiþiklikler tek bir katmanda yapýlýr
3. **Test Edilebilirlik**: Her katman ayrý test edilebilir
4. **Yeniden Kullanýlabilirlik**: Service'ler farklý controller'larda kullanýlabilir
5. **Esneklik**: Veritabaný deðiþtiðinde sadece Repository deðiþir

### ? Dezavantajlarý:

1. **Daha Fazla Kod**: Daha fazla dosya ve sýnýf
2. **Karmaþýklýk**: Küçük projeler için fazla olabilir
3. **Performans**: Katmanlar arasý geçiþler ekstra iþlem yapar

## ?? Dependency Injection (Baðýmlýlýk Enjeksiyonu)

Katmanlar arasý iletiþim **Dependency Injection** ile yapýlýr.

**Örnek**:
```csharp
// Controller, Service'e baðýmlý
public class UsersController
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService; // DI ile enjekte edilir
    }
}
```

## ?? Örnek: Basit vs Katmanlý Mimari

### Basit Yapý (Þu Anki):
```csharp
// Controller'da her þey var!
public class UsersController
{
    private static List<User> _users = new();
    
    [HttpGet]
    public ActionResult GetUsers()
    {
        return Ok(_users); // Direkt veri döndürüyor
    }
}
```

### Katmanlý Mimari:
```csharp
// Controller sadece HTTP iþlemlerini yapar
public class UsersController
{
    private readonly IUserService _userService;
    
    [HttpGet]
    public ActionResult GetUsers()
    {
        var users = _userService.GetAllUsers(); // Service'e soruyor
        return Ok(users);
    }
}

// Service iþ mantýðýný yönetir
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    
    public List<User> GetAllUsers()
    {
        return _repository.GetAll(); // Repository'ye soruyor
    }
}

// Repository veri eriþimini yönetir
public class UserRepository : IUserRepository
{
    private static List<User> _users = new();
    
    public List<User> GetAll()
    {
        return _users; // Veriyi döndürüyor
    }
}
```

## ?? Öðrenme Sýrasý

1. ? **Basit Yapý** (Þu anki) - Her þey Controller'da
2. ?? **Service Katmaný Ekle** - Ýþ mantýðýný ayýr
3. ?? **Repository Katmaný Ekle** - Veri eriþimini ayýr
4. ?? **Dependency Injection** - Katmanlar arasý baðlantý

## ?? Sonraki Adým

Þimdi UserService'i katmanlý mimariye dönüþtürelim! Her katmaný adým adým oluþturacaðýz.

**Hazýr mýsýnýz?** ??

