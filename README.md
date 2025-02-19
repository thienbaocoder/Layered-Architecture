# **📌 Hệ Thống Quản Lý Khách Hàng - Kiến Trúc Phân Tầng**

## **1️⃣ Giới Thiệu**
Hệ thống này là một **Web API** được phát triển bằng **.NET Core** và sử dụng **MongoDB** làm cơ sở dữ liệu.
Hệ thống được thiết kế theo **kiến trúc phân tầng (Layered Architecture)** nhằm đảm bảo:
- **Tính linh hoạt**, dễ mở rộng và bảo trì.
- **Tách biệt rõ ràng trách nhiệm** giữa các tầng.
- **Dễ dàng thay đổi hoặc nâng cấp** từng phần mà không ảnh hưởng đến hệ thống.

---

## **2️⃣ Kiến Trúc Phân Tầng**
Hệ thống bao gồm **4 tầng chính**, mỗi tầng có vai trò và nhiệm vụ riêng:

### **📌 1. Presentation Layer (Giao diện & API)**
- **Vai trò:** Nhận yêu cầu từ client và trả kết quả.
- **Thành phần:**  
  - `CustomerController.cs`
- **Chức năng chính:**  
  - Nhận request từ client (GET, POST, PUT, DELETE).
  - Gọi **Business Layer (`CustomerService`)** để xử lý logic.
  - Trả kết quả về cho client.

📌 **Ví dụ trong `CustomerController.cs`**:
```csharp
[HttpPost]
public IActionResult Create([FromForm] string name, [FromForm] string email)
{
    try
    {
        var newCustomer = _service.CreateCustomer(name, email);
        return Ok(newCustomer);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
```

---

### **📌 2. Business Layer (Xử lý nghiệp vụ)**
- **Vai trò:** Xử lý logic nghiệp vụ trước khi lưu vào database.
- **Thành phần:**  
  - `CustomerService.cs`
- **Chức năng chính:**  
  - Kiểm tra dữ liệu đầu vào hợp lệ.
  - Gọi **Persistence Layer (`CustomerRepository`)** để lưu trữ dữ liệu.

📌 **Ví dụ trong `CustomerService.cs`**:
```csharp
public Customer CreateCustomer(string name, string email)
{
    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
        throw new ArgumentException("Tên và email không được để trống!");

    var newCustomer = new Customer { Name = name, Email = email };
    _repository.InsertCustomer(newCustomer);
    return newCustomer;
}
```

---

### **📌 3. Persistence Layer (Truy vấn & Lưu trữ dữ liệu)**
- **Vai trò:** Quản lý truy xuất dữ liệu từ database.
- **Thành phần:**  
  - `CustomerRepository.cs`
  - `CustomerContext.cs`
- **Chức năng chính:**  
  - Cung cấp các phương thức để lưu, sửa, xóa, truy vấn dữ liệu.

📌 **Ví dụ trong `CustomerRepository.cs`**:
```csharp
public void InsertCustomer(Customer customer)
{
    _context.Customers.InsertOne(customer);
}
```

---

### **📌 4. Database Layer (Lưu trữ dữ liệu thực tế)**
- **Vai trò:** Lưu trữ dữ liệu khách hàng.
- **Thành phần:**  
  - `MongoDB Database`
- **Chức năng chính:**  
  - Lưu trữ thông tin khách hàng.
  - Nhận và xử lý truy vấn từ `CustomerRepository`.

📌 **Ví dụ trong `CustomerContext.cs`**:
```csharp
public class CustomerContext
{
    private readonly IMongoDatabase _database;

    public CustomerContext(IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<Customer> Customers => _database.GetCollection<Customer>("Customers");
}
```

---

## **3️⃣ Luồng Xử Lý Dữ Liệu**
1. **Client gửi request đến API (`CustomerController`)**  
2. **Gọi `CustomerService` để xử lý logic**  
3. **`CustomerRepository` lưu dữ liệu vào MongoDB**  
4. **Trả kết quả về lại cho Client**  

---

## **4️⃣ Lợi Ích Của Kiến Trúc Phân Tầng**
✅ **Dễ bảo trì & mở rộng**: Thay đổi logic không ảnh hưởng đến Database.  
✅ **Tách biệt rõ trách nhiệm**: Controller, Service, Repository có nhiệm vụ riêng.  
✅ **Dễ dàng thay thế Database**: Chỉ cần chỉnh `CustomerRepository`.  

🚀 **Hệ thống đã tuân theo kiến trúc phân tầng chuẩn!** 💡

