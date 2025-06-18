# Azure .NET Image Web Project

โปรเจคนี้เป็นเว็บแอปพลิเคชันที่พัฒนาด้วย .NET สำหรับการจัดการและแสดงผลรูปภาพ โดยสามารถนำไปใช้งานร่วมกับ Azure ได้อย่างสะดวก

## คุณสมบัติ

- อัพโหลดและแสดงผลรูปภาพผ่านเว็บอินเตอร์เฟซ
- จัดเก็บไฟล์รูปภาพใน Azure Blob Storage (หรือ local storage)
- รองรับการแสดงผลรูปภาพแบบ gallery
- พัฒนาและรันด้วย .NET (ASP.NET Core)

## การติดตั้งและใช้งาน

1. **Clone โปรเจค**
    ```bash
    git clone <repo-url>
    cd azure-dotnet-imgweb-repo
    ```

2. **ตั้งค่า Environment Variables**
    - สร้างไฟล์ `.env` หรือกำหนดค่าใน `appsettings.json` สำหรับ Azure Storage

3. **รันโปรเจค**
    ```bash
    dotnet run
    ```

4. **เข้าถึงเว็บ**
    - เปิดเบราว์เซอร์ไปที่ `http://localhost:5000` (หรือพอร์ตที่กำหนด)

## โครงสร้างโปรเจค

- `/Controllers` - ควบคุมการรับส่งข้อมูลระหว่างผู้ใช้และระบบ
- `/Models` - โมเดลข้อมูล
- `/Views` - ไฟล์สำหรับแสดงผลหน้าเว็บ
- `/wwwroot` - ไฟล์ static เช่น รูปภาพ, CSS, JS

## การ deploy ขึ้น Azure

1. สร้าง Azure App Service และ Azure Blob Storage
2. ตั้งค่า connection string ใน environment variables
3. Deploy ด้วย Azure CLI หรือ GitHub Actions

## License

MIT License

## บริษัท

[https://www.yourcompany.com](https://www.yourcompany.com)

