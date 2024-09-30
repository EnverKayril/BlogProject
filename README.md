# BLOG PROJECT
![Anasayfa](https://ibb.co/x7hznZW)
## Genel Özellikler
- **Üyelik Sistemi**: E-posta ile kayıt ve mail doğrulama işlemleri.
- **Kullanıcı Profili Yönetimi**: Kullanıcılar profil bilgilerini güncelleyebilir ve hesaplarını silebilir. Hesap silme işlemi tüm kullanıcı içerikleri ile birlikte gerçekleşir.
- **Makale Yönetimi**: Makale oluşturma, düzenleme ve silme. Kategori seçimi ile makaleler kategorilere atanabilir.
- **Yorum Yönetimi**: Yorumlar admin ve editör tarafından onaylanabilir, düzenlenebilir ve silinebilir.
- **Kategori Yönetimi**: Admin tarafından kategoriler oluşturulabilir ve düzenlenebilir.
- **Rol Tabanlı Erişim Kontrolü**: Admin, Editör, VerifiedUser ve NewUser gibi farklı roller için yetki yönetimi.
- **SEO Uyumlu URL'ler**: Temiz ve SEO dostu URL yapısı.
- **Hesap Silme Güvenliği**: Kullanıcı hesabı silinirken e-posta ve şifre doğrulaması istenir.
- **Makale Görüntüleme Sayısı Takibi**: Her makale okunduğunda görüntülenme sayısı artırılır ve takip edilir.
- **En Çok Okunan Makaleler**: Ana sayfada en çok okunan makaleler listesi gösterilir.
- **Hesap Doğrulama ve E-posta Gönderimi**: Kullanıcı hesap doğrulama işlemleri için e-posta ile doğrulama linki gönderilir.
- **Yorum Onay Sistemi**: Yorumlar admin ve editör tarafından onaylanmadıkça görünmez.
- **Kapsamlı Yorum Sistemi**: Yorumlar makale altında sıralı şekilde listelenir, düzenlenebilir ve silinebilir.
- **Makale İçerik Sınırlaması**: Kullanıcılar sadece metin tabanlı içerik ekleyebilirler. Görsel ya da medya içeriği için genişletilebilir bir yapı mevcuttur.
- **Responsive Tasarım**: Proje mobil cihazlarda da uyumlu bir görünüm sunmaktadır.

## Kullanıcı Rolleri
- **Admin**: Tüm sistemi yönetir (kullanıcılar, makaleler, yorumlar).
- **Editor**: Yorum ve makale düzenleme yetkisine sahiptir.
- **VerifiedUser**: Doğrulanmış kullanıcılar makale yazma ve yorum yapma yetkisine sahiptir.
- **NewUser**: Yeni kullanıcılar makale yazabilmek için hesap doğrulaması yapmalıdır.

## Teknik Detaylar
- **Teknolojiler**: ASP.NET Core MVC, Entity Framework Core, AutoMapper, NLog, Dependency Injection
- **Veritabanı**: Microsoft SQL Server, Code First yaklaşımı ile veritabanı yönetimi.
- **Yüksek Performans**: Lazy loading ve diğer optimizasyon teknikleri kullanıldı.

## Tasarım Desenleri ve SOLID İlkeleri

### Design Patterns
- **Repository Pattern**: Veritabanı işlemleri her entity için ayrı repository sınıflarında yönetilmiştir.
- **Unit of Work Pattern**: Tüm veritabanı işlemleri tek bir iş birimi (UnitOfWork) altında toplanmıştır.
- **Dependency Injection**: Tüm servis ve repository'ler DI aracılığıyla bağımsız hale getirilmiştir.

### SOLID Principles
- **SRP (Single Responsibility Principle)**: Her sınıfın tek bir sorumluluğu vardır.
- **OCP (Open/Closed Principle)**: Sistem, yeni özellikler eklenerek genişletilebilir.
- **LSP (Liskov Substitution Principle)**: Türetilmiş sınıflar, temel sınıfın yerine kullanılabilir.
- **ISP (Interface Segregation Principle)**: Arayüzler, sadece ihtiyaç duyulan metotlarla ayrıştırılmıştır.
- **DIP (Dependency Inversion Principle)**: Soyut arayüzlerle bağımlılıkların çözülmesi sağlanmıştır.

## Kurulum ve Kullanım
1. Proje klonlayın.
2. `appsettings.json` dosyasındaki veritabanı bağlantı ayarlarını yapın.
3. Entity Framework Migration işlemlerini çalıştırın.
4. Visual Studio veya terminal ile projeyi çalıştırın.
