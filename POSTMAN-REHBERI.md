# ?? Postman ile API Test Rehberi

## ?? Bu Rehberde Öðrenecekleriniz
- Postman'i nasýl kullanacaðýnýz
- GET istekleri nasýl gönderilir
- POST istekleri nasýl gönderilir
- Response'larý nasýl okuyacaðýnýz

## ?? Postman Kurulumu

1. **Postman'i Ýndirin**: https://www.postman.com/downloads/
2. **Hesap Oluþturun** (ücretsiz)
3. **Postman'i Açýn**

## ?? Adým 1: Servisleri Çalýþtýrma

**ÖNEMLÝ**: Postman'den istek göndermeden önce servislerin çalýþýyor olmasý gerekir!

### Terminal ile Çalýþtýrma:

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

**? Kontrol**: 
- UserService: http://localhost:5001/swagger açýlmalý
- OrderService: http://localhost:5002/swagger açýlmalý

## ?? Adým 2: Postman'de Ýstek Oluþturma

### UserService - GET Tüm Kullanýcýlar

1. **Yeni Request Oluþtur**:
   - Sol üstte **"New"** butonuna týklayýn
   - **"HTTP Request"** seçin
   - Ýstek adýný deðiþtirin: "Get All Users"

2. **Ýstek Ayarlarý**:
   - **Method**: `GET` (dropdown'dan seçin)
   - **URL**: `http://localhost:5001/api/users`
   - **Send** butonuna týklayýn

3. **Response'u Ýnceleyin**:
   - Alt panelde JSON formatýnda kullanýcý listesi görünür
   - Status: `200 OK` olmalý

### UserService - GET Tek Kullanýcý (ID ile)

1. **Yeni Request Oluþtur**: "Get User By ID"
2. **Method**: `GET`
3. **URL**: `http://localhost:5001/api/users/1`
4. **Send** butonuna týklayýn

**? Beklenen Response:**
```json
{
  "id": 1,
  "name": "Ahmet Yýlmaz",
  "email": "ahmet@example.com"
}
```

### UserService - POST Yeni Kullanýcý

1. **Yeni Request Oluþtur**: "Create New User"
2. **Method**: `POST`
3. **URL**: `http://localhost:5001/api/users`

4. **Body Ayarlarý**:
   - **Body** sekmesine týklayýn
   - **raw** radio button'unu seçin
   - Dropdown'dan **"JSON"** seçin
   - Aþaðýdaki JSON'u yapýþtýrýn:

```json
{
  "name": "Yeni Kullanýcý",
  "email": "yeni@example.com"
}
```

5. **Headers Kontrolü**:
   - **Headers** sekmesine gidin
   - `Content-Type: application/json` otomatik eklenmiþ olmalý
   - Yoksa manuel ekleyin

6. **Send** butonuna týklayýn

**? Beklenen Response:**
- Status: `201 Created`
- Response body'de yeni oluþturulan kullanýcý görünür

### OrderService - GET Tüm Sipariþler

1. **Yeni Request**: "Get All Orders"
2. **Method**: `GET`
3. **URL**: `http://localhost:5002/api/orders`
4. **Send**

### OrderService - GET Kullanýcýnýn Sipariþleri

1. **Yeni Request**: "Get Orders By User"
2. **Method**: `GET`
3. **URL**: `http://localhost:5002/api/orders/user/1`
4. **Send**

**? Beklenen Response:**
```json
[
  {
    "id": 1,
    "userId": 1,
    "productName": "Laptop",
    "price": 15000.00,
    "orderDate": "2024-01-15T10:30:00"
  },
  {
    "id": 2,
    "userId": 1,
    "productName": "Mouse",
    "price": 250.00,
    "orderDate": "2024-01-17T10:30:00"
  }
]
```

### OrderService - POST Yeni Sipariþ

1. **Yeni Request**: "Create New Order"
2. **Method**: `POST`
3. **URL**: `http://localhost:5002/api/orders`

4. **Body** (JSON):
```json
{
  "userId": 1,
  "productName": "Yeni Ürün",
  "price": 999.99
}
```

5. **Send**

## ?? Postman Collection Oluþturma

Tüm istekleri bir arada tutmak için Collection oluþturun:

1. Sol panelde **"Collections"** ? **"+"** butonuna týklayýn
2. Collection adý: **"Microservices Demo"**
3. Oluþturduðunuz tüm istekleri bu collection'a sürükleyin

### Collection'ý Organize Etme

```
Microservices Demo/
??? UserService/
?   ??? Get All Users
?   ??? Get User By ID
?   ??? Create New User
??? OrderService/
    ??? Get All Orders
    ??? Get Order By ID
    ??? Get Orders By User
    ??? Create New Order
```

## ?? Postman Özellikleri

### 1. Environment Variables

Farklý ortamlar için deðiþkenler kullanýn:

1. Sað üstte **"Environments"** ? **"+"** ? **"Create Environment"**
2. Environment adý: **"Local Development"**
3. Deðiþkenler ekleyin:
   - `userServiceUrl`: `http://localhost:5001`
   - `orderServiceUrl`: `http://localhost:5002`

4. URL'lerde kullanýn:
   - `{{userServiceUrl}}/api/users`
   - `{{orderServiceUrl}}/api/orders`

### 2. Tests (Otomatik Test)

Response'u otomatik test etmek için:

1. Ýstek açýn ? **"Tests"** sekmesi
2. Aþaðýdaki kodu ekleyin:

```javascript
// Status code kontrolü
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});

// Response body kontrolü
pm.test("Response has users", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.be.an('array');
});
```

### 3. Pre-request Scripts

Ýstek göndermeden önce çalýþan scriptler:

```javascript
// Otomatik timestamp ekleme
pm.environment.set("timestamp", new Date().toISOString());
```

## ?? Response'larý Ýnceleme

### Response Sekmeleri:

1. **Body**: Response içeriði (JSON, XML, HTML)
2. **Headers**: Response header'larý
3. **Cookies**: Cookie'ler
4. **Test Results**: Test sonuçlarý

### Response Formatlarý:

- **Pretty**: Okunabilir format (JSON için önerilen)
- **Raw**: Ham format
- **Preview**: HTML önizleme

## ?? Hata Ayýklama

### Yaygýn Hatalar:

1. **"Could not get response"**
   - ? Servislerin çalýþtýðýndan emin olun
   - ? Port numaralarýný kontrol edin

2. **"404 Not Found"**
   - ? URL'yi kontrol edin
   - ? Endpoint adýný kontrol edin

3. **"400 Bad Request"**
   - ? JSON formatýný kontrol edin
   - ? Gerekli alanlarýn doldurulduðundan emin olun

4. **"500 Internal Server Error"**
   - ? Servis loglarýný kontrol edin
   - ? Breakpoint koyup debug yapýn

## ? Pratik Alýþtýrma

### Alýþtýrma 1: Temel GET Ýstekleri

1. ? Tüm kullanýcýlarý getirin
2. ? ID=1 olan kullanýcýyý getirin
3. ? Tüm sipariþleri getirin
4. ? ID=1 olan sipariþi getirin

### Alýþtýrma 2: POST Ýstekleri

1. ? Yeni bir kullanýcý oluþturun
2. ? Oluþturduðunuz kullanýcýnýn ID'sini not edin
3. ? Bu kullanýcý için yeni bir sipariþ oluþturun
4. ? Kullanýcýnýn sipariþlerini getirin

### Alýþtýrma 3: Collection ve Environment

1. ? Bir Collection oluþturun
2. ? Environment variables ekleyin
3. ? Tüm istekleri collection'a ekleyin
4. ? Environment'ý kullanarak istekleri çalýþtýrýn

## ?? Ýpuçlarý

1. **Save Request**: Her isteði kaydedin (Ctrl+S)
2. **Duplicate**: Benzer istekleri kopyalayýn
3. **History**: Geçmiþ istekleri görüntüleyin
4. **Import**: Swagger/OpenAPI dosyalarýný import edin

## ?? Örnek Ýstek Listesi

### UserService Ýstekleri:

| Method | URL | Açýklama |
|--------|-----|----------|
| GET | `http://localhost:5001/api/users` | Tüm kullanýcýlar |
| GET | `http://localhost:5001/api/users/1` | ID=1 kullanýcý |
| POST | `http://localhost:5001/api/users` | Yeni kullanýcý |

### OrderService Ýstekleri:

| Method | URL | Açýklama |
|--------|-----|----------|
| GET | `http://localhost:5002/api/orders` | Tüm sipariþler |
| GET | `http://localhost:5002/api/orders/1` | ID=1 sipariþ |
| GET | `http://localhost:5002/api/orders/user/1` | Kullanýcýnýn sipariþleri |
| POST | `http://localhost:5002/api/orders` | Yeni sipariþ |

**Hazýr olduðunuzda test etmeye baþlayýn!** ??

