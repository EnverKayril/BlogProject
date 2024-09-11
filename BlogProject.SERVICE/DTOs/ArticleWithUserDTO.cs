using BlogProject.CORE.CoreModels.Enums;
using BlogProject.CORE.CoreModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.DTOs
{
    public class ArticleWithUserDTO
    {
        [DisplayName("Id")]
        public string Id { get; set; }
        [DisplayName("Başlık")]
        public string Title { get; set; }
        [DisplayName("İçerik")]
        public string Content { get; set; }
        [DisplayName("Küçük Resim")]
        public string Thumbnail { get; set; }
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [DisplayName("Değişiklik Tarihi")]
        public DateTime? UpdateDate { get; set; }
        [DisplayName("Kullanıcı Id")]
        public string AppUserId { get; set; }
        [DisplayName("Kategori Id")]
        public string CategoryId { get; set; }
        [DisplayName("Kategori Adı")]
        public string CategoryName { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [DisplayName("Durum")]
        public EntityStatus Status { get; set; }
        [DisplayName("Görüntülenme Sayısı")]
        public int ViewsCount { get; set; }
        [DisplayName("Yorum Sayısı")]
        public int CommentCount { get; set; }
    }
}
