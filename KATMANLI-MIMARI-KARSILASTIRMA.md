# ?? Katmanlý Mimari: Önce ve Sonra Karþýlaþtýrmasý

## ?? Dönüþüm Özeti

UserService'i **basit yapýdan** **katmanlý mimariye** dönüþtürdük!

## ?? Klasör Yapýsý Karþýlaþtýrmasý

### ? ÖNCE (Basit Yapý):
```
UserService/
??? Controllers/
?   ??? UsersController.cs  ? Her þey burada!
??? Models/
?   ??? User.cs
??? Program.cs
```

### ? SONRA (Katmanlý Mimari):
```
UserService/
??? Controllers/              ? API Katmaný (HTTP istekleri)
?   ??? UsersController.cs
??? Services/                ? Ýþ Mantýðý Katmaný (YENÝ!)
?   ??? IUserService.cs
?   ??? UserService.cs
??? Repositories/             ? Veri Eriþim Katmaný (YENÝ!)
?   ??? IUserRepository.cs
?   ??? UserRepository.cs
??? Models/                  ? Domain Katmaný
?   ??? User.cs
??? Program.cs               ? DI ayarlarý eklendi
```

## ?? Kod Karþýlaþtýrmasý

### 1. Controller - ÖNCE vs SONRA

#### ? ÖNCE (Her þey Controller'da):
```csharp
public class UsersController : ControllerBase
{
    // Veri deposu Controller'da!
    private static readonly List<User> _users = new();
    
    [HttpGet]
    public ActionResult GetAllUsers()
    {
        return Ok(_users); // Direkt veri döndürüyor
    }
    
    [HttpPost]
    public ActionResult CreateUser([FromBody] User user)
    {
        user.Id = _users.Max(u => u.Id) + 1;
        _users.Add(user); // Direkt ekliyor
        return Ok(user);
    }
}
```

#### ? SONRA (Sadece HTTP iþlemleri):
```csharp
public class UsersController : ControllerBase
{
    private readonly IUserService _userService; // Service'e baðýmlý
    
    public UsersController(IUserService userService)
    {
        _userService = userService; // DI ile enjekte edilir
    }
    
    [HttpGet]
    public ActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers(); // Service'e soruyor
        return Ok(users);
    }
    
    [HttpPost]
    public ActionResult CreateUser([FromBody] User user)
    {
        var createdUser = _userService.CreateUser(user); // Service'e soruyor
        return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
    }
}
```

**Fark**: Controller artýk sadece HTTP iþlemlerini yapýyor, iþ mantýðý yok!

---

### 2. Service Katmaný (YENÝ!)

#### ? IUserService.cs (Interface):
```csharp
public interface IUserService
{
    List<User> GetAllUsers();
    User? GetUserById(int id);
    User CreateUser(User user);
    bool IsEmailTaken(string email);
}
```

#### ? UserService.cs (Implementation):
```csharp
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository; // Repository'ye baðýmlý
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository; // DI ile enjekte edilir
    }
    
    public List<User> GetAllUsers()
    {
        return _userRepository.GetAll(); // Repository'ye soruyor
    }
    
    public User CreateUser(User user)
    {
        // Ýþ Kuralý 1: Email kontrolü
        if (IsEmailTaken(user.Email))
        {
            throw new InvalidOperationException("Email zaten kullanýlýyor!");
        }
        
        // Ýþ Kuralý 2: Validasyon
        if (string.IsNullOrWhiteSpace(user.Email))
        {
            throw new ArgumentException("Email boþ olamaz!");
        }
        
        // Tüm kontroller geçti, oluþtur
        return _userRepository.Create(user);
    }
}
```

**Fark**: Ýþ mantýðý artýk Service katmanýnda!

---

### 3. Repository Katmaný (YENÝ!)

#### ? IUserRepository.cs (Interface):
```csharp
public interface IUserRepository
{
    List<User> GetAll();
    User? GetById(int id);
    User Create(User user);
    bool Exists(int id);
}
```

#### ? UserRepository.cs (Implementation):
```csharp
public class UserRepository : IUserRepository
{
    // Veri deposu Repository'de
    private static readonly List<User> _users = new();
    
    public List<User> GetAll()
    {
        return _users.ToList();
    }
    
    public User Create(User user)
    {
        user.Id = _users.Max(u => u.Id) + 1;
        _users.Add(user);
        return user;
    }
}
```

**Fark**: Veri eriþimi artýk Repository katmanýnda!

---

### 4. Program.cs - Dependency Injection (YENÝ!)

#### ? ÖNCE:
```csharp
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// DI yok!
```

#### ? SONRA:
```csharp
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Dependency Injection Ayarlarý
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
```

**Fark**: Artýk katmanlar birbirine baðlý!

---

## ?? Ýstek Akýþý

### ÖNCE:
```
HTTP Request ? Controller ? Direkt Veri ? Response
```

### SONRA:
```
HTTP Request 
    ?
Controller (HTTP iþlemleri)
    ?
Service (Ýþ mantýðý, validasyon)
    ?
Repository (Veri eriþimi)
    ?
Veri ? Service ? Controller ? Response
```

---

## ?? Avantajlar

### 1. Ayrý Sorumluluklar
- ? Controller: Sadece HTTP
- ? Service: Sadece iþ mantýðý
- ? Repository: Sadece veri eriþimi

### 2. Test Edilebilirlik
```csharp
// Service'i mock repository ile test edebilirsiniz
var mockRepository = new Mock<IUserRepository>();
var service = new UserService(mockRepository.Object);
```

### 3. Esneklik
```csharp
// Veritabaný deðiþtiðinde sadece Repository deðiþir
// Service ve Controller ayný kalýr!
```

### 4. Yeniden Kullanýlabilirlik
```csharp
// Ayný Service'i farklý controller'larda kullanabilirsiniz
// Örn: Web API + gRPC + SignalR
```

---

## ?? Kod Satýrý Karþýlaþtýrmasý

| Katman | Önce | Sonra | Fark |
|--------|------|-------|------|
| Controller | 66 satýr | 55 satýr | -11 (daha temiz!) |
| Service | 0 | 80 satýr | +80 (iþ mantýðý ayrýldý) |
| Repository | 0 | 50 satýr | +50 (veri eriþimi ayrýldý) |
| **TOPLAM** | **66** | **185** | **+119** (daha organize!) |

**Not**: Daha fazla kod ama çok daha organize ve bakýmý kolay!

---

## ? Sonuç

### ÖNCE:
- ? Her þey Controller'da
- ? Ýþ mantýðý ve veri eriþimi karýþýk
- ? Test etmesi zor
- ? Deðiþiklik yapmasý zor

### SONRA:
- ? Her katman kendi iþini yapýyor
- ? Ýþ mantýðý ayrý, veri eriþimi ayrý
- ? Test etmesi kolay
- ? Deðiþiklik yapmasý kolay

---

## ?? Sonraki Adýmlar

1. ? Katmanlý mimariyi anladýnýz
2. ?? OrderService'i de katmanlý mimariye dönüþtürebilirsiniz
3. ?? Unit test yazabilirsiniz
4. ?? Veritabaný ekleyebilirsiniz (Repository'de deðiþiklik yeterli!)

**Sorularýnýz varsa sorun!** ??

