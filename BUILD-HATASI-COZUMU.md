# ?? Build Hatasý: "File is locked" Çözümü

## ? Hata Mesajý

```
Could not copy "UserService.exe" to "bin\Debug\net8.0\UserService.exe". 
The file is locked by: "UserService (35040)"
```

## ?? Sorunun Nedeni

Bu hata, **UserService hala çalýþýyor** olduðu için oluþur. Visual Studio veya terminal'den çalýþan servis, `.exe` dosyasýný kilitliyor ve yeni build yapýlamýyor.

## ? Çözüm: Çalýþan Servisi Durdurun

### Yöntem 1: Visual Studio'da Durdurma (En Kolay)

1. Visual Studio'da **"Stop Debugging"** butonuna týklayýn (kýrmýzý kare)
2. Veya **Shift+F5** tuþlarýna basýn
3. Servis durduktan sonra tekrar **F5** ile çalýþtýrýn

### Yöntem 2: Terminal'de Durdurma

Eðer terminal'den `dotnet run` ile çalýþtýrdýysanýz:

1. Terminal penceresine gidin
2. **Ctrl+C** tuþlarýna basýn
3. Servis durur
4. Tekrar `dotnet run` ile baþlatabilirsiniz

### Yöntem 3: Task Manager ile Durdurma

1. **Task Manager** açýn (Ctrl+Shift+Esc)
2. **"Details"** sekmesine gidin
3. **"UserService.exe"** veya **"dotnet.exe"** process'ini bulun
4. Sað týk ? **"End Task"**

### Yöntem 4: PowerShell ile Durdurma

```powershell
# Process ID ile durdurma (hatada görünen ID: 35040)
taskkill /PID 35040 /F

# Veya process adý ile
taskkill /IM UserService.exe /F
taskkill /IM dotnet.exe /F
```

## ?? Önleme: Doðru Çalýþtýrma Sýrasý

### Visual Studio'da:

1. ? Önce çalýþan servisi durdurun (Shift+F5)
2. ? Sonra build edin (Ctrl+Shift+B)
3. ? Sonra çalýþtýrýn (F5)

### Terminal'de:

1. ? Önce çalýþan servisi durdurun (Ctrl+C)
2. ? Sonra build edin: `dotnet build`
3. ? Sonra çalýþtýrýn: `dotnet run`

## ?? Otomatik Restart

Visual Studio'da **"Enable Edit and Continue"** özelliði varsa, kod deðiþikliklerinde otomatik restart yapabilir. Ama bazen manuel durdurmak gerekir.

## ?? Kontrol Listesi

Build hatasý aldýðýnýzda:

- [ ] Çalýþan servisi durdurdunuz mu? (Shift+F5 veya Ctrl+C)
- [ ] Task Manager'da process kaldý mý kontrol ettiniz mi?
- [ ] Tekrar build denediniz mi? (Ctrl+Shift+B)
- [ ] Servisi tekrar çalýþtýrdýnýz mý? (F5)

## ?? Ýpucu: Multiple Startup Projects

Eðer birden fazla servis çalýþtýrýyorsanýz (UserService + OrderService), her ikisini de durdurmak gerekir:

1. Visual Studio'da **Shift+F5** ? Tüm servisler durur
2. Veya her birini ayrý ayrý durdurun

## ? Hýzlý Çözüm Özeti

1. **Shift+F5** ? Servisi durdur
2. **Ctrl+Shift+B** ? Build et
3. **F5** ? Tekrar çalýþtýr

**Sorun devam ederse bana söyleyin!** ??

