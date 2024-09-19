using BlogProject.CORE.CoreModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = "1",
                    Name = "Yazılım",
                    Description = "Yazılım geliştirme ve programlama dilleri üzerine içerikler."
                },
                new Category
                {
                    Id = "2",
                    Name = "Sistem",
                    Description = "Sistem yönetimi ve ağ altyapıları üzerine bilgiler."
                },
                new Category
                {
                    Id = "3",
                    Name = "Web Grafik",
                    Description = "Web tasarımı ve grafik tasarım trendleri hakkında içerikler."
                },
                new Category
                {
                    Id = "4",
                    Name = "Güvenlik",
                    Description = "Siber güvenlik, veri koruma ve sistem güvenliği üzerine bilgiler."
                },
                new Category
                {
                    Id = "5",
                    Name = "Bulut Sistemler",
                    Description = "Bulut bilişim ve bulut teknolojileri üzerine bilgiler."
                },
                new Category
                {
                    Id = "6",
                    Name = "Veritabanı",
                    Description = "Veritabanı yönetim sistemleri ve veri işleme teknikleri üzerine içerikler."
                },
                new Category
                {
                    Id = "7",
                    Name = "Yapay Zeka",
                    Description = "Yapay zeka, makine öğrenimi ve derin öğrenme hakkında içerikler."
                },
                new Category
                {
                    Id = "8",
                    Name = "Mobil Uygulama",
                    Description = "Mobil uygulama geliştirme, Android ve iOS platformları üzerine bilgiler."
                },
                new Category
                {
                    Id = "9",
                    Name = "Oyun Geliştirme",
                    Description = "Oyun motorları, oyun geliştirme teknikleri ve oyun tasarımı üzerine içerikler."
                },
                new Category
                {
                    Id = "10",
                    Name = "Veri Bilimi",
                    Description = "Veri analitiği, veri işleme ve büyük veri teknolojileri hakkında içerikler."
                },
                new Category
                {
                    Id = "11",
                    Name = "Yapay Zeka ve Robotik",
                    Description = "Yapay zeka, robotik süreç otomasyonu ve robot teknolojileri üzerine bilgiler."
                },
                new Category
                {
                    Id = "12",
                    Name = "Donanım",
                    Description = "Bilgisayar donanımı, sistem bileşenleri ve teknik detaylar hakkında bilgiler."
                },
                new Category
                {
                    Id = "13",
                    Name = "İnsan Bilgisayar Etkileşimi",
                    Description = "Kullanıcı deneyimi (UX) ve insan-bilgisayar etkileşimi üzerine bilgiler."
                },
                new Category
                {
                    Id = "14",
                    Name = "Siber Tehditler",
                    Description = "Siber tehditler ve güvenlik açıkları hakkında içerikler."
                },
                new Category
                {
                    Id = "15",
                    Name = "Yazılım Mühendisliği",
                    Description = "Yazılım geliştirme süreçleri, yazılım mühendisliği prensipleri üzerine bilgiler."
                }
            );

            builder.Property(c => c.Name).IsRequired().HasMaxLength(70);
            builder.HasMany(c => c.Articles).WithOne(a => a.Category).HasForeignKey(a => a.CategoryId);
        }
    }
}
