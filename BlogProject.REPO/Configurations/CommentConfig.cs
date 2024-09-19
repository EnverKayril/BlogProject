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
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(c => c.Content).HasMaxLength(1000).IsRequired();
            builder.HasOne(c => c.AppUser).WithMany(a => a.Comments).HasForeignKey(c => c.AppUserId).OnDelete(DeleteBehavior.NoAction);

            var comment1 = new Comment
            {
                Id = "1",
                AppUserId = "7",
                ArticleId = "1",
                CreateDate = DateTime.Now,
                Status = CORE.CoreModels.Enums.EntityStatus.Created,
                Content = "C# dilinin OOP özelliklerini bu kadar net ve anlaşılır bir şekilde açıklamanız gerçekten çok faydalı oldu. Özellikle polimorfizm ve soyutlama kavramlarını bu kadar detaylı anlamak, projelerimde daha modüler yapılar kurmama yardımcı olacak."
            };

            var comment2 = new Comment
            {
                Id = "2",
                AppUserId = "8",
                ArticleId = "1",
                CreateDate = DateTime.Now,
                Status = CORE.CoreModels.Enums.EntityStatus.Created,
                Content = "Makaledeki encapsulation (kapsülleme) ve inheritance (kalıtım) kavramları üzerine verdiğiniz örnekler çok açıklayıcıydı. C# dilinde daha önce bu kavramları yüzeysel biliyordum ama şimdi projelerimde daha etkin kullanabileceğimi düşünüyorum. Teşekkürler!"
            };

            var comment3 = new Comment
            {
                Id = "3",
                AppUserId = "11",
                ArticleId = "5",
                CreateDate = DateTime.Now,
                Status = CORE.CoreModels.Enums.EntityStatus.Created,
                Content = "Hyper-V ile sanallaştırmanın nasıl donanım maliyetlerini düşürdüğünü çok güzel açıklamışsınız. Bu teknolojiyi kullanarak test ortamları oluşturmanın ne kadar kolay olduğunu öğrendim. Geliştirme ve test süreçlerimde kesinlikle kullanacağım."
            };

            var comment4 = new Comment
            {
                Id = "4",
                AppUserId = "6",
                ArticleId = "5",
                CreateDate = DateTime.Now,
                Status = CORE.CoreModels.Enums.EntityStatus.Created,
                Content = "Felaket kurtarma planları oluştururken Hyper-V'nin ne kadar esnek ve etkili olduğunu vurgulamanız çok iyi olmuş. Sanal makinelerle çalışmak, IT yönetimi için büyük bir avantaj. Makale sayesinde bu teknolojiyi daha iyi anladım ve sistemlerimde kullanmayı düşünüyorum."
            };

            var comment5 = new Comment
            {
                Id = "5",
                AppUserId = "10",
                ArticleId = "18",
                CreateDate = DateTime.Now,
                Status = CORE.CoreModels.Enums.EntityStatus.Created,
                Content = "Yedekleme stratejilerinin detaylı bir şekilde ele alınması çok faydalı oldu. Özellikle artımlı ve diferansiyel yedekleme arasındaki farkı öğrenmek benim için önemliydi. Şirketimizde veritabanı yönetimi yaparken bu bilgileri uygulayacağım."
            };

            var comment6 = new Comment
            {
                Id = "6",
                AppUserId = "11",
                ArticleId = "18",
                CreateDate = DateTime.Now,
                Status = CORE.CoreModels.Enums.EntityStatus.Created,
                Content = "Yedekleme stratejilerinin sadece planlanmasının yeterli olmadığını, aynı zamanda kurtarma senaryolarının da düzenli olarak test edilmesi gerektiğini vurgulamanız çok önemli. Veri güvenliği için bu makalede önerilen yöntemleri uygulamaya başlayacağım."
            };

            builder.HasData(comment1, comment2, comment3, comment4, comment5, comment6);
        }
    }
}
