# ??? Code First vs Database First

## ?? Bu Rehberde Öðrenecekleriniz
- Code First nedir?
- Database First nedir?
- Hangisi ne zaman kullanýlýr?
- Büyük projelerde hangisi tercih edilir?

## ?? Code First (Kod Öncelikli)

### Nasýl Çalýþýr?
1. **Önce kod yazarsýnýz** (Entity sýnýflarý, DbContext)
2. **Entity Framework** bu kodlardan veritabanýný oluþturur
3. Veritabaný otomatik oluþturulur

### Örnek:
```csharp
// 1. Önce Entity sýnýfýný yazarsýnýz
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

// 2. DbContext'i yazarsýnýz
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}

// 3. Migration oluþturursunuz
// dotnet ef migrations add InitialCreate

// 4. Veritabaný oluþturulur
// dotnet ef database update
```

### ? Avantajlarý:
1. **Versiyon Kontrolü**: Tüm deðiþiklikler kodda (Git'te)
2. **Migration Sistemi**: Veritabaný deðiþiklikleri takip edilir
3. **Hýzlý Geliþtirme**: Kod yazýp veritabaný otomatik oluþur
4. **Takým Çalýþmasý**: Herkes ayný migration'larý çalýþtýrýr
5. **Test Edilebilirlik**: In-memory veritabaný kullanýlabilir

### ? Dezavantajlarý:
1. **Mevcut Veritabaný**: Var olan veritabanýna uygulamak zor
2. **Karmaþýk Þemalar**: Çok karmaþýk veritabaný þemalarý için zor olabilir
3. **Öðrenme Eðrisi**: Migration'larý öðrenmek gerekir

---

## ??? Database First (Veritabaný Öncelikli)

### Nasýl Çalýþýr?
1. **Önce veritabanýný oluþturursunuz** (SQL Server Management Studio, vs.)
2. **Entity Framework** veritabanýndan kod oluþturur
3. Entity sýnýflarý otomatik oluþturulur

### Örnek:
```bash
# 1. Önce veritabanýný oluþturursunuz (SQL)
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Email NVARCHAR(100)
)

# 2. EF Core'dan kod oluþturursunuz
dotnet ef dbcontext scaffold "ConnectionString" Microsoft.EntityFrameworkCore.SqlServer

# 3. Entity sýnýflarý otomatik oluþturulur
```

### ? Avantajlarý:
1. **Mevcut Veritabaný**: Var olan veritabanýna kolayca baðlanýr
2. **DBA Kontrolü**: Veritabaný yöneticileri þemayý kontrol eder
3. **Karmaþýk Þemalar**: Çok karmaþýk veritabanlarý için uygun
4. **Performans**: Veritabaný optimize edilmiþ olabilir

### ? Dezavantajlarý:
1. **Versiyon Kontrolü Zor**: Veritabaný deðiþiklikleri takip edilmez
2. **Manuel Senkronizasyon**: Kod ve veritabaný manuel senkronize edilir
3. **Takým Çalýþmasý Zor**: Herkes ayný veritabanýna ihtiyaç duyar
4. **Otomatik Kod**: Oluþturulan kodlar genelde düzenlenmez

---

## ?? Büyük Projelerde Hangisi?

### Code First Tercih Edilir Çünkü:

1. **? Versiyon Kontrolü**
   - Tüm deðiþiklikler Git'te
   - Migration geçmiþi var
   - Geri alma kolay

2. **? Takým Çalýþmasý**
   - Her geliþtirici migration'larý çalýþtýrýr
   - Ayný veritabaný þemasý
   - Çakýþma yok

3. **? CI/CD Pipeline**
   - Otomatik test ortamlarý
   - Migration'lar otomatik çalýþýr
   - Deployment kolay

4. **? Modern Yaklaþým**
   - .NET Core / .NET 5+ projelerinde standart
   - Entity Framework Core ile uyumlu
   - Microservice mimarisi ile uyumlu

### Database First Ne Zaman Kullanýlýr?

1. **Mevcut Veritabaný Varsa**
   - Legacy sistemler
   - Var olan veritabanýna entegrasyon

2. **DBA Kontrolü Gerekiyorsa**
   - Veritabaný yöneticileri þemayý yönetiyor
   - Performans optimizasyonu önemli

3. **Çok Karmaþýk Þemalar**
   - Stored procedure'lar
   - View'lar
   - Çok fazla iliþki

---

## ?? Karþýlaþtýrma Tablosu

| Özellik | Code First | Database First |
|---------|------------|----------------|
| **Versiyon Kontrolü** | ? Mükemmel | ? Zor |
| **Migration Sistemi** | ? Var | ? Yok |
| **Takým Çalýþmasý** | ? Kolay | ?? Zor |
| **Mevcut DB** | ?? Zor | ? Kolay |
| **Hýzlý Geliþtirme** | ? Hýzlý | ?? Yavaþ |
| **Modern Projeler** | ? Standart | ?? Eski |
| **Büyük Projeler** | ? Önerilen | ?? Nadiren |

---

## ?? Öneri: Büyük Projeler Ýçin

### ? Code First Kullanýn Çünkü:

1. **Microservice Mimarisi**
   - Her servis kendi veritabanýna sahip
   - Migration'lar servis bazýnda yönetilir
   - Baðýmsýz geliþtirme

2. **DevOps Uyumlu**
   - CI/CD pipeline'larý
   - Otomatik deployment
   - Test ortamlarý

3. **Takým Çalýþmasý**
   - Pull request'lerde migration'lar görünür
   - Code review yapýlabilir
   - Çakýþmalar önlenir

4. **Modern Standart**
   - .NET Core projelerinde standart
   - Entity Framework Core ile uyumlu
   - Best practice

---

## ?? Sonuç

### Büyük Projeler Ýçin:
**? Code First Önerilir**

### Ne Zaman Database First?
- Var olan veritabanýna entegrasyon
- DBA kontrolü gerekiyorsa
- Legacy sistemler

### Microservice Projelerinde:
**? Kesinlikle Code First**

Her microservice kendi veritabanýna sahip olmalý ve Code First ile yönetilmeli.

---

## ?? Özet

| Senaryo | Önerilen Yaklaþým |
|---------|-------------------|
| **Yeni Proje** | ? Code First |
| **Büyük Proje** | ? Code First |
| **Microservice** | ? Code First |
| **Mevcut DB** | ?? Database First |
| **Legacy Sistem** | ?? Database First |

**Sizin projeniz için: Code First öneriyorum!** ??

