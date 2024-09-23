using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.REPO.Migrations
{
    public partial class initialCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "Nvarchar(Max)", nullable: false),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewsCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArticleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "583b6187-b741-4e1c-be84-76a03db8d039", "Admin", "ADMIN" },
                    { "2", "909c0b3a-549b-4ca6-af90-05a4a3fcf778", "Editor", "EDITOR" },
                    { "3", "19f8a308-fb4f-4bf4-8f95-ae9675d4a3ef", "Verifieduser", "VERIFIEDUSER" },
                    { "4", "bbd8327b-7ca8-458e-940f-d339400362c0", "Newuser", "NEWUSER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Photo", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "344f87b1-85e4-468f-ba4c-10632d2e4b56", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEI/sI4A7i/1eVW3uQZW7g854WMwZggCX2kgfr4reMBzkAkDx9E88DhIrjhd6cgk6Vw==", "1234567890", true, "Admin.jpg", "48d70f75-4ed5-4f46-b68a-be9255283f3e", false, "Admin" },
                    { "10", 0, "62823dc7-3f3b-45c8-b991-fbb7d1f2b0a6", "verifieduser6@mail.com", true, false, null, "VERIFIEDUSER6@MAIL.COM", "VERIFIEDUSER6", "AQAAAAEAACcQAAAAENkX8LT2Sy0x5n1z3GuP2cdD+AwRySINLW9CyHqrZYIoS3B6j20q4kfo3141QrlAxA==", "1234567890", true, "UserImage6.jpg", "df66d6dd-8f75-409a-81c0-5761b5564a63", false, "Verifieduser6" },
                    { "11", 0, "2c571703-29c7-4e3e-b2df-ab440ff158eb", "verifieduser7@mail.com", true, false, null, "VERIFIEDUSER7@MAIL.COM", "VERIFIEDUSER7", "AQAAAAEAACcQAAAAEFeb3ruI5knIxxIFsjfqpXbolkAJO46Rh4PT7QxfecCndgRIMjucDrZ2p4kT8BBrGQ==", "1234567890", true, "UserImage7.jpg", "adad4cba-f922-492f-b218-8b69401d1586", false, "Verifieduser7" },
                    { "12", 0, "7feb8646-aee5-4531-b6d6-bce8fc5a60fa", "verifieduser8@mail.com", true, false, null, "VERIFIEDUSER8@MAIL.COM", "VERIFIEDUSER8", "AQAAAAEAACcQAAAAEKgQgIptAihko61gITULJzpCPjyHBERtUlC//6VXXd+58aN+mdEQ9ePf8TF+x36DSw==", "1234567890", true, "UserImage8.jpg", "3990a10f-77a2-4f94-964d-4ea57a2b1e0b", false, "Verifieduser8" },
                    { "2", 0, "048eff32-cdde-4b9b-a97b-578bc8b50e42", "editor@mail.com", true, false, null, "EDITOR@MAIL.COM", "EDITOR", "AQAAAAEAACcQAAAAEFbeDzG2ILRbyj7bkfzOypSGT8fy6qA0LhsrcWGX3VuTt12qajPAeVMYKY230oo0pw==", "1234567890", true, "Admin.jpg", "50d0f16d-8f23-4db9-935b-39a4d19e4857", false, "Editor" },
                    { "3", 0, "8ed6bf0a-9199-423e-ae36-1d4333ea0f04", "verifieduser1@mail.com", true, false, null, "VERIFIEDUSER1@MAIL.COM", "VERIFIEDUSER1", "AQAAAAEAACcQAAAAEN3jlrMvMbMOnx+FaQZ6u3GXNrpSawbl+ulz72pcvA0LSC3brI7HYpWt0mUxdS06sg==", "1234567890", true, "UserImage1.jpg", "266a0290-ebde-4f28-b8fe-a5b140ca2111", false, "Verifieduser1" },
                    { "4", 0, "24ea349d-67c3-4efd-9417-8cab19ca6781", "newuse1r@mail.com", false, false, null, "NEWUSER1@MAIL.COM", "NEWUSER1", "AQAAAAEAACcQAAAAEAxQvzz/hf5ZvoBwZeB7lKY+eYRahQqO9WmCh6zqCA55e/HMvPwuJJlGzmbUbzPpGg==", "1234567890", false, "DefaultUser.jpg", "5f4b3683-cb1d-49cd-9c87-b3432cd83b36", false, "Newuser1" },
                    { "5", 0, "909ddb36-1ef6-4ac9-aba1-9ef48e29a9a5", "newuser2@mail.com", false, false, null, "NEWUSER2@MAIL.COM", "NEWUSER2", "AQAAAAEAACcQAAAAEBuBDlasQlWPbzXFBtDvAHXt/Blmnypgos3Mwa19KH/H7vhC5ihZDfdukxJwhYUolA==", "1234567890", false, "DefaultUser.jpg", "ddc2f7f4-0713-46a5-8dfd-efa4e76e051f", false, "Newuser2" },
                    { "6", 0, "611b87a6-a2d9-4fed-8875-4051fff4edf5", "verifieduser2@mail.com", true, false, null, "VERIFIEDUSER2@MAIL.COM", "VERIFIEDUSER2", "AQAAAAEAACcQAAAAEGwmXNT2o99OiNaGXau5+sH9xN9LT29t2La2davFYsB6Pzvv36ODl/AOb7WQVUjefg==", "1234567890", true, "UserImage2.jpg", "31d18320-9713-4a47-98fd-e3971b6dfafb", false, "Verifieduser2" },
                    { "7", 0, "3b624d55-7a32-46f8-b6d2-c73abbe3320e", "verifieduser3@mail.com", true, false, null, "VERIFIEDUSER3@MAIL.COM", "VERIFIEDUSER3", "AQAAAAEAACcQAAAAEN+Wj6iFFsCOSZDTIGN2tPhU+3spH8z1cy6To9K9aYxQGv+Y/M2hqSEdBGiZk8tLmA==", "1234567890", true, "UserImage3.jpg", "1b1c9b7e-0bb4-479b-8bb0-8270696dbedb", false, "Verifieduser3" },
                    { "8", 0, "e3e31b14-df47-481e-a959-401d3bb3f203", "verifieduser4@mail.com", true, false, null, "VERIFIEDUSER4@MAIL.COM", "VERIFIEDUSER4", "AQAAAAEAACcQAAAAEFCfQNIt3oF5hqdsr27W71y6X2VYFdxdyRZONapJNwirF2Czzvd6ZpvIlbSnpXUieA==", "1234567890", true, "UserImage4.jpg", "231cd67f-8107-4106-8bce-cb80bb5e96c8", false, "Verifieduser4" },
                    { "9", 0, "36b48d07-7326-4ce5-af3a-f011f0e1fdf7", "verifieduser5@mail.com", true, false, null, "VERIFIEDUSER5@MAIL.COM", "VERIFIEDUSER5", "AQAAAAEAACcQAAAAEHzczcENfEayt4eNsYWf6PrmITPbeHJk7Ycku3yLxFuCRjihwCEI7rCHFpC3X+KDxw==", "1234567890", true, "UserImage5.jpg", "efd653a5-7dfa-4568-9234-99e52c7b5ad7", false, "Verifieduser5" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Description", "Name", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { "1", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(207), null, "Yazılım geliştirme ve programlama dilleri üzerine içerikler.", "Yazılım", 0, null },
                    { "10", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(263), null, "Veri analitiği, veri işleme ve büyük veri teknolojileri hakkında içerikler.", "Veri Bilimi", 0, null },
                    { "11", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(266), null, "Yapay zeka, robotik süreç otomasyonu ve robot teknolojileri üzerine bilgiler.", "Yapay Zeka ve Robotik", 0, null },
                    { "12", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(269), null, "Bilgisayar donanımı, sistem bileşenleri ve teknik detaylar hakkında bilgiler.", "Donanım", 0, null },
                    { "13", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(272), null, "Kullanıcı deneyimi (UX) ve insan-bilgisayar etkileşimi üzerine bilgiler.", "İnsan Bilgisayar Etkileşimi", 0, null },
                    { "14", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(275), null, "Siber tehditler ve güvenlik açıkları hakkında içerikler.", "Siber Tehditler", 0, null },
                    { "15", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(280), null, "Yazılım geliştirme süreçleri, yazılım mühendisliği prensipleri üzerine bilgiler.", "Yazılım Mühendisliği", 0, null },
                    { "2", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(214), null, "Sistem yönetimi ve ağ altyapıları üzerine bilgiler.", "Sistem", 0, null },
                    { "3", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(217), null, "Web tasarımı ve grafik tasarım trendleri hakkında içerikler.", "Web Grafik", 0, null },
                    { "4", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(242), null, "Siber güvenlik, veri koruma ve sistem güvenliği üzerine bilgiler.", "Güvenlik", 0, null },
                    { "5", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(245), null, "Bulut bilişim ve bulut teknolojileri üzerine bilgiler.", "Bulut Sistemler", 0, null },
                    { "6", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(248), null, "Veritabanı yönetim sistemleri ve veri işleme teknikleri üzerine içerikler.", "Veritabanı", 0, null },
                    { "7", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(254), null, "Yapay zeka, makine öğrenimi ve derin öğrenme hakkında içerikler.", "Yapay Zeka", 0, null },
                    { "8", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(257), null, "Mobil uygulama geliştirme, Android ve iOS platformları üzerine bilgiler.", "Mobil Uygulama", 0, null },
                    { "9", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(260), null, "Oyun motorları, oyun geliştirme teknikleri ve oyun tasarımı üzerine içerikler.", "Oyun Geliştirme", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "AppUserId", "CategoryId", "CommentCount", "Content", "CreateDate", "DeleteDate", "Status", "Thumbnail", "Title", "UpdateDate", "ViewsCount" },
                values: new object[,]
                {
                    { "1", "1", "1", 0, "C# dili, yazılım dünyasında geniş bir kullanım alanına sahip olup nesne yönelimli programlamanın (OOP) güçlü özelliklerini barındırır. Nesne yönelimli programlama, yazılım geliştiricilerine daha modüler ve sürdürülebilir bir kod yapısı oluşturma fırsatı tanır. Bu programlama paradigması, gerçek dünya objelerini sınıflar ve nesneler aracılığıyla temsil etmeyi sağlar.\r\n\r\nNesne yönelimli programlamada dört ana kavram bulunur: encapsulation (kapsülleme), inheritance (kalıtım), polymorphism (çok biçimlilik) ve abstraction (soyutlama). Kapsülleme, verileri ve fonksiyonları bir sınıf içinde saklayarak yalnızca gerektiğinde kullanılmasına olanak tanır. Kalıtım, bir sınıfın başka bir sınıftan özellikler ve metotlar almasını sağlar. Bu, kod tekrarını en aza indirerek daha sürdürülebilir yazılımlar oluşturmayı kolaylaştırır. Polimorfizm, nesnelerin farklı şekillerde davranabilme yeteneğidir, bu sayede aynı isimdeki bir metot, farklı parametrelerle farklı işlevler gösterebilir. Soyutlama ise gereksiz detaylardan kaçınarak sadece gerekli olan fonksiyonların dışarıya açılmasını sağlar.\r\n\r\nBu dört temel özellik, C#'ın güçlü bir nesne yönelimli dil olarak kullanılmasını sağlar. Özellikle büyük ölçekli projelerde, OOP yaklaşımı yazılımın yönetilebilir ve genişletilebilir olmasını sağlar. Örneğin, bir e-ticaret uygulaması geliştirirken ürün, kullanıcı ve sipariş gibi sınıflar oluşturulabilir ve bu sınıflar birbirleriyle etkileşimde bulunabilir. Bu şekilde, uygulamanın farklı parçaları arasında daha az bağımlılık olur ve değişiklikler daha kolay yapılabilir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9863), null, 0, "Article1.jpg", "C# ile Nesne Yönelimli Programlamanın Temelleri", null, 0 },
                    { "10", "12", "4", 0, "Siber güvenlik, modern dijital dünyada en önemli konulardan biridir. Bilgi teknolojilerinin ve internet kullanımının hızla artması, veri güvenliği risklerini de beraberinde getirmiştir. Siber güvenlik, bilgisayar sistemlerini, ağları ve verileri kötü niyetli saldırılardan koruma amacını taşır. Bir işletme ya da birey, güvenlik açıklarını minimuma indirmek için siber güvenlik önlemlerini benimsemelidir.\r\n\r\nSiber güvenlik temelinde firewall'lar, antivirüs yazılımları, şifreleme ve çok faktörlü kimlik doğrulama gibi araçlar bulunur. Bu araçlar, verilerin yetkisiz erişimden korunmasını sağlar. Örneğin, firewall'lar internet üzerinden gelen tehditleri filtrelerken, şifreleme verilerin güvenli bir şekilde iletilmesini sağlar. Ayrıca, çok faktörlü kimlik doğrulama ile kullanıcılar, hesaplarına giriş yaparken ek güvenlik katmanları oluşturabilirler.\r\n\r\nSiber saldırılar, kişisel bilgilerin çalınmasından şirketlerin iş süreçlerini durdurmaya kadar geniş bir yelpazeye yayılabilir. Bu nedenle, her birey ve işletme, siber güvenlik konusunda bilinçli olmalı ve temel güvenlik uygulamalarını hayatlarına entegre etmelidir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9910), null, 0, "Article10.jpg", "Siber Güvenlik Temelleri: Bilmeniz Gerekenler", null, 0 },
                    { "11", "1", "4", 0, "Ransomware (fidye yazılımı), bilgisayar sistemlerini kilitleyen ve kullanıcılardan verilerini geri almak için fidye talep eden kötü amaçlı yazılımlardır. Bu tür saldırılar, son yıllarda artış göstermiş ve büyük şirketlerden bireylere kadar birçok hedefi etkilemiştir. Ransomware saldırıları, özellikle kritik verilerin şifrelenmesiyle kurbanları zor durumda bırakır.\r\n\r\nRansomware'den korunmanın en etkili yolu, düzenli olarak yedekleme yapmaktır. Verilerinizin düzenli olarak yedeklenmesi, bir saldırı durumunda verilerinizi geri yüklemenizi sağlar. Ayrıca, güncel antivirüs yazılımları ve güvenlik yamalarının kullanılması, fidye yazılımlarına karşı koruma sağlar. Özellikle bilinmeyen e-posta ekleri ve şüpheli bağlantılara tıklamaktan kaçınmak da bu tür saldırılardan korunmanın önemli bir yoludur.\r\n\r\nBir saldırıya maruz kalındığında, fidye ödemek yerine profesyonel siber güvenlik uzmanlarından destek almak en doğru yol olabilir. Ödeme yapmak, saldırganları cesaretlendirebilir ve gelecekte benzer saldırıların devam etmesine neden olabilir. Ransomware saldırılarıyla başa çıkmak için bilinçli ve hazırlıklı olmak çok önemlidir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9914), null, 0, "Article11.jpg", "Ransomware Saldırıları ve Korunma Yöntemleri", null, 0 },
                    { "12", "2", "4", 0, "Veri güvenliği, bir işletmenin ya da bireyin dijital varlıklarının korunması anlamına gelir. Özellikle hassas bilgiler, kişisel veriler ve finansal bilgiler gibi önemli verilerin çalınması, büyük kayıplara yol açabilir. Veri güvenliğini sağlamak, hem bireyler hem de şirketler için kritik bir sorumluluktur.\r\n\r\nVeri güvenliği, şifreleme, güvenli şifre kullanımı, yetkisiz erişimi engelleme ve veri yedekleme gibi uygulamaları içerir. Şifreleme, verilerin üçüncü şahıslar tarafından okunmasını engellerken, güvenli şifre kullanımı hesapların ele geçirilme riskini azaltır. Özellikle iş dünyasında, veri ihlalleri şirketlerin itibarını zedeleyebilir ve ciddi mali kayıplara yol açabilir.\r\n\r\nAyrıca, veri güvenliğini artırmak için düzenli olarak güvenlik denetimleri yapılmalı ve güvenlik açıkları tespit edilmelidir. Bu denetimler, siber tehditlere karşı bir savunma hattı oluşturur ve verilerin güvenli bir şekilde saklanmasını sağlar. Veri güvenliği, sadece teknik bir konu değil, aynı zamanda stratejik bir yönetim meselesidir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9920), null, 0, "Article12.jpg", "Veri Güvenliği: Hassas Bilgilerin Korunması", null, 0 },
                    { "13", "3", "5", 0, "Bulut bilişim, verilerin internet üzerinden depolanmasını, yönetilmesini ve işlenmesini sağlayan bir teknolojidir. Geleneksel yöntemlerde veriler yerel sunucularda veya bilgisayarlarda saklanırken, bulut bilişim sayesinde bu veriler bulut sunucularında güvenli bir şekilde saklanır ve internet bağlantısı olan her yerden erişilebilir hale gelir.\r\n\r\nBulut bilişimin en büyük avantajlarından biri, esnekliği ve ölçeklenebilirliğidir. İşletmeler, ihtiyaç duydukları kadar depolama ve işlem gücü kiralayabilirler ve gereksinim arttığında bu kaynakları hızlıca artırabilirler. Ayrıca, bulut bilişim sayesinde donanım maliyetleri düşer ve işletmeler, kendi veri merkezlerini yönetmek zorunda kalmazlar.\r\n\r\nVeri güvenliği, bulut bilişimin bir diğer önemli avantajıdır. Bulut sağlayıcıları, yüksek güvenlik standartlarına uygun veri merkezleri sunar ve veriler düzenli olarak yedeklenir. Bu sayede, işletmeler olası veri kayıplarına karşı korunmuş olur. Bulut bilişim, hem büyük işletmeler hem de küçük girişimler için büyük fırsatlar sunan bir teknolojidir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9923), null, 0, "Article13.jpg", "Bulut Bilişim Nedir ve Avantajları Nelerdir?", null, 0 },
                    { "14", "4", "5", 0, "Hibrit bulut, genel bulut ve özel bulut çözümlerinin bir kombinasyonudur. Genel bulut, bulut hizmetlerinin herkese açık bir şekilde sunulduğu modeldir ve genellikle maliyet avantajı sağlar. Özel bulut ise, bir işletmenin kendi veri merkezinde kurduğu ve sadece kendi ihtiyaçlarına hizmet eden bir yapıdır. Hibrit bulut, bu iki yapının en iyi yönlerini birleştirir.\r\n\r\nHibrit bulut çözümleri, işletmelere daha fazla esneklik ve kontrol sunar. Kritik ve hassas veriler özel bulutta saklanırken, daha az kritik iş yükleri genel buluta taşınabilir. Bu, hem maliyetlerin optimize edilmesini sağlar hem de veri güvenliğini artırır. Ayrıca, hibrit bulut yapısı, işletmelerin anlık talep artışlarına hızlıca yanıt vermesine olanak tanır.\r\n\r\nHibrit bulutun bir diğer avantajı, felaket kurtarma senaryolarında önemli bir rol oynamasıdır. Genel bulut sağlayıcıları, olağanüstü durumlarda verilerin güvenli bir şekilde yedeklenmesini ve kurtarılmasını sağlar. Bu nedenle, hibrit bulut çözümleri, modern iş dünyasında hızla benimsenen bir teknoloji haline gelmiştir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9927), null, 0, "Article14.jpg", "Hibrit Bulut: Hem Genel Hem Özel Bulutun Gücü", null, 0 },
                    { "15", "5", "5", 0, "Bulut bilişim, veri depolama ve işlem gücü sunmanın yanı sıra, veri güvenliği konusunda da büyük önem taşır. Verilerin internet üzerinden depolandığı bulut sistemlerinde, güvenlik tehditleri her zaman var olabilir. Bu nedenle, bulut bilişimde veri güvenliğini sağlamak, hem kullanıcıların hem de sağlayıcıların önceliği olmalıdır.\r\n\r\nBulut sağlayıcıları, verilerin güvenli bir şekilde saklanması ve yetkisiz erişimlerden korunması için çeşitli güvenlik önlemleri alır. Şifreleme, bu güvenlik önlemlerinin başında gelir. Veriler, hem aktarım sırasında hem de depolandıkları yerde şifrelenir. Ayrıca, çok faktörlü kimlik doğrulama ve güvenlik duvarları gibi ek önlemler, bulut bilişimde güvenliği artırır.\r\n\r\nKullanıcılar da kendi taraflarında güvenlik önlemleri almalıdır. Güçlü şifreler kullanmak, düzenli olarak hesapları gözden geçirmek ve güvenlik yamalarını güncellemek, veri güvenliğini artırmak için önemlidir. Sonuç olarak, bulut bilişimde güvenlik, sadece bir hizmet sağlayıcısının değil, aynı zamanda kullanıcıların da aktif rol oynaması gereken bir süreçtir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9931), null, 0, "Article15.jpg", "Bulut Bilişimde Veri Güvenliği Nasıl Sağlanır?", null, 0 },
                    { "16", "6", "6", 0, "Veritabanı tasarımı, yazılım projelerinin temel yapı taşlarından biridir. İyi bir veritabanı tasarımı, verilerin doğru ve verimli bir şekilde depolanmasını sağlar. Veritabanı tasarımında dikkat edilmesi gereken en önemli noktalardan biri, normalizasyon prensiplerine uygun hareket etmektir. Bu, verilerin gereksiz tekrarını önler ve depolama maliyetlerini azaltır.\r\n\r\nVeritabanı tasarımında ilişkisel yapıları doğru kurmak, verilerin tutarlı kalmasını sağlar. Tablo yapılarında birincil anahtarlar (primary keys) ve yabancı anahtarlar (foreign keys) kullanmak, verilerin birbirine bağlı olduğu yerlerde referans bütünlüğünü sağlar. Ayrıca, indeksleme, sorguların performansını artırarak veritabanı erişim sürelerini kısaltır.\r\n\r\nVeritabanı tasarımında bir diğer önemli konu ise veri güvenliğidir. Verilerin yetkisiz erişimlerden korunması için güvenlik mekanizmaları devreye alınmalıdır. Bu mekanizmalar arasında kullanıcı erişim haklarının doğru bir şekilde belirlenmesi ve şifreleme gibi yöntemler bulunur. İyi bir veritabanı tasarımı, hem performans hem de güvenlik açısından kritik bir rol oynar.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9935), null, 0, "Article16.jpg", "Veritabanı Tasarımında En İyi Uygulamalar", null, 0 },
                    { "17", "7", "6", 0, "Veritabanı seçimi, bir yazılım projesinin başarısını doğrudan etkileyen kararlardan biridir. Geleneksel olarak SQL (yapısal sorgulama dili) tabanlı veritabanları, yapılandırılmış verilerin depolanması için kullanılmıştır. Ancak, son yıllarda NoSQL (yapısal olmayan sorgulama dili) veritabanları, büyük veri ve yüksek performans gereksinimleri için popüler hale gelmiştir.\r\n\r\nSQL veritabanları, verilerin tablolar halinde depolandığı, ilişkisel bir yapı sunar. Bu yapı, verilerin düzenli ve tutarlı olmasını sağlar. Özellikle finansal uygulamalar ve kurumsal sistemlerde SQL veritabanları tercih edilir. Ancak, büyük ölçekli ve esnek veri yapıları gerektiren projelerde NoSQL veritabanları daha uygun olabilir.\r\n\r\nNoSQL veritabanları, verileri daha esnek bir şekilde depolayabilir ve yüksek performans sunar. Büyük veri ve gerçek zamanlı uygulamalar için ideal olan NoSQL veritabanları, ilişkisel olmayan yapıları ile dikkat çeker. Her iki veritabanı tipi de kendine özgü avantajlara sahiptir ve projenin ihtiyaçlarına göre bir seçim yapılmalıdır.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9938), null, 0, "Article17.jpg", "SQL vs NoSQL: Hangi Veritabanı Tipi Seçilmeli?", null, 0 },
                    { "18", "8", "6", 0, "Veritabanı yönetiminde yedekleme ve kurtarma stratejileri, verilerin güvenliğini sağlamak açısından büyük önem taşır. Olası veri kayıplarını önlemek ve verilerin sürekli olarak erişilebilir olmasını sağlamak için düzenli yedekleme planları oluşturulmalıdır. Bu stratejiler, hem küçük çaplı veri kayıplarını hem de büyük felaket senaryolarını kapsamalıdır.\r\n\r\nYedekleme stratejileri arasında tam yedekleme, artımlı yedekleme ve diferansiyel yedekleme yöntemleri bulunur. Tam yedekleme, veritabanının tüm verilerini belirli aralıklarla yedeklerken, artımlı yedekleme sadece son yedeklemeden bu yana değişen verileri yedekler. Diferansiyel yedekleme ise tam yedeklemeden bu yana değişen tüm verileri kapsar.\r\n\r\nVeritabanı yedeklerinin güvenli bir ortamda saklanması ve düzenli olarak test edilmesi de kritik öneme sahiptir. Kurtarma senaryoları üzerinde çalışmak, verilerin geri yüklenme sürelerini ve kurtarma prosedürlerini optimize eder. Bu stratejiler, veritabanı yönetiminin ayrılmaz bir parçasıdır ve iş sürekliliği açısından hayati bir rol oynar.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9942), null, 0, "Article18.jpg", "Veritabanı Yedekleme ve Kurtarma Stratejileri", null, 0 },
                    { "19", "9", "7", 0, "Yapay zeka (AI), makinelerin insanlar gibi düşünmesini ve kararlar almasını sağlayan bir teknolojidir. Yapay zeka sistemleri, büyük veri setlerini analiz ederek ve bu verilerden öğrenerek, belirli görevleri yerine getirme yeteneğine sahiptir. AI'nin temelinde makine öğrenimi, derin öğrenme ve doğal dil işleme gibi teknolojiler bulunur.\r\n\r\nMakine öğrenimi, yapay zekanın alt dallarından biridir ve makinelerin deneyimlerinden öğrenmesini sağlar. Bu öğrenme süreci, veriler üzerinde yapılan analizler ve algoritmalar aracılığıyla gerçekleşir. Örneğin, bir yapay zeka sistemi, geçmiş verilere dayalı tahminlerde bulunabilir ve bu tahminler doğrultusunda kararlar alabilir.\r\n\r\nYapay zeka teknolojisi, tıptan finansal hizmetlere, üretimden eğitime kadar birçok sektörde kullanılıyor. Otonom araçlar, akıllı asistanlar ve tıbbi teşhis sistemleri, yapay zekanın gücünü gösteren örneklerden sadece birkaçıdır. AI'nin geleceği, insan hayatını daha da kolaylaştıracak birçok yenilikçi uygulama ile şekilleniyor.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9946), null, 0, "Article19.jpg", "Yapay Zeka Nedir ve Nasıl Çalışır?", null, 0 },
                    { "2", "2", "1", 0, "Yazılım geliştirme süreçlerinde esneklik ve hız kazanmak, günümüz projelerinde büyük bir gereklilik haline gelmiştir. Bu ihtiyacı karşılamak adına, Agile ve Scrum gibi yöntemler geliştirilmiştir. Agile metodolojisi, geleneksel yazılım geliştirme yöntemlerinden farklı olarak, kısa döngülerde müşteri geri bildirimlerini dikkate alarak projeyi sürekli geliştirmeyi hedefler. Bu süreçte esneklik ve müşteri memnuniyeti ön plandadır.\r\n\r\nScrum, Agile yönteminin bir uygulaması olup, genellikle ekipler tarafından tercih edilir. Scrum süreçlerinde belirli roller ve etkinlikler yer alır. Örneğin, Scrum Master, ekibin süreçlerine rehberlik eden ve engelleri ortadan kaldıran kişidir. Product Owner ise müşteri ihtiyaçlarını ve iş gereksinimlerini belirler ve ekibe yön verir. Geliştirme ekibi ise yazılımın kodlama, test ve dağıtım aşamalarını gerçekleştirir.\r\n\r\nScrum, sprint adı verilen kısa periyotlarla çalışır ve her sprint sonunda kullanılabilir bir ürün parçası sunulur. Sprint süreleri genellikle 1 ila 4 hafta arasında değişir. Her sprint sonunda ekip, retrospektif toplantılar yaparak süreci değerlendirir ve gelecekteki iyileştirmeler için geri bildirimler alır.\r\n\r\nAgile ve Scrum yöntemleri, özellikle hızla değişen müşteri taleplerine cevap vermek isteyen yazılım ekipleri için idealdir. Bu yöntemler, projelerin başarısını artırırken, ekiplerin daha düzenli çalışmasını ve müşteri memnuniyetinin artmasını sağlar.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9868), null, 0, "Article2.jpg", "Yazılım Mühendisliğinde Agile ve Scrum Yöntemleri", null, 0 },
                    { "20", "10", "7", 0, "Makine öğrenimi (ML), bilgisayarların açıkça programlanmadan öğrenmesini ve kararlar almasını sağlayan bir yapay zeka alanıdır. ML algoritmaları, veriler üzerinde analizler yaparak, model oluşturur ve bu model üzerinden tahminlerde bulunur. Özellikle büyük veri setleri ile çalışıldığında, ML'nin gücü daha da belirgin hale gelir.\r\n\r\nMakine öğrenimi, gözetimli öğrenme, gözetimsiz öğrenme ve pekiştirmeli öğrenme olmak üzere üç ana kategoriye ayrılır. Gözetimli öğrenmede, algoritmalar, etiketlenmiş veri setleri üzerinden eğitilir ve bu veriler üzerinden tahminler yapar. Gözetimsiz öğrenmede ise, algoritmalar verilerdeki kalıpları ve ilişkileri keşfetmeye çalışır. Pekiştirmeli öğrenme ise, algoritmaların ödül ve ceza sistemine göre öğrenmesini sağlar.\r\n\r\nMakine öğrenimi, tıp, finans, oyun geliştirme ve e-ticaret gibi birçok alanda kullanılmaktadır. Özellikle tahmin modelleri, öneri sistemleri ve görüntü tanıma gibi uygulamalar, ML'nin yaygın kullanım alanları arasında yer alır. Gelecekte makine öğreniminin etkisi, daha sofistike ve karmaşık uygulamalarla daha da artacaktır.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9951), null, 0, "Article20.jpg", "Makine Öğrenimi: Geleceğin Teknolojisi", null, 0 },
                    { "21", "11", "7", 0, "Yapay zeka teknolojilerinin hızlı gelişimi, beraberinde birçok etik sorunu da getirmiştir. AI sistemlerinin karar verme yetenekleri ve otonom davranışları, insan hayatını doğrudan etkileyebilecek sonuçlar doğurabilir. Bu nedenle, yapay zeka kullanımıyla ilgili etik kuralların belirlenmesi büyük önem taşımaktadır.\r\n\r\nAI'nin etik sorunları arasında veri gizliliği, önyargı ve sorumluluk gibi konular yer alır. Örneğin, bir yapay zeka sistemi, yanlış veya önyargılı verilerle eğitildiğinde, yanlış kararlar alabilir ve bu da büyük sorunlara yol açabilir. Ayrıca, otonom sistemlerin hatalı çalışması durumunda sorumluluğun kimde olacağı da tartışılan bir diğer konudur.\r\n\r\nEtik sorunların çözülmesi, yapay zekanın güvenilir ve sorumlu bir şekilde kullanılmasını sağlar. Bu konuda hem yazılım geliştiricilere hem de düzenleyici kurumlara büyük sorumluluk düşmektedir. AI'nin etik standartlara uygun bir şekilde geliştirilmesi, teknolojinin topluma olan faydasını maksimize edecektir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9955), null, 0, "Article21.jpg", "Yapay Zeka ve Etik: Karşılaşılan Zorluklar", null, 0 },
                    { "22", "12", "8", 0, "Mobil uygulama geliştirme, modern yazılım dünyasında en popüler alanlardan biridir. Akıllı telefonların ve tabletlerin yaygınlaşmasıyla, mobil uygulamalar hayatımızın vazgeçilmez bir parçası haline geldi. Bir mobil uygulama geliştirme süreci, fikir aşamasından uygulamanın yayınlanmasına kadar birçok adımı içerir.\r\n\r\nİlk aşamada, uygulamanın ne amaçla geliştirileceği ve hangi kullanıcı kitlesine hitap edeceği belirlenmelidir. Ardından, uygulamanın tasarımı ve kullanıcı arayüzü (UI) oluşturulur. Kullanıcı deneyimi (UX), mobil uygulamalarda büyük önem taşır. Kullanıcı dostu ve sezgisel bir tasarım, uygulamanın başarısını artırır.\r\n\r\nMobil uygulama geliştirme sürecinde yazılım dilleri de önemlidir. Android için Java veya Kotlin, iOS için Swift veya Objective-C kullanılır. Ayrıca, React Native veya Flutter gibi çapraz platform geliştirme araçları da kullanılabilir. Uygulamanın geliştirilmesinin ardından, test aşaması başlar ve tüm hatalar giderildikten sonra uygulama mağazalarına sunulur.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9985), null, 0, "Article22.jpg", "Mobil Uygulama Geliştirmenin Temelleri", null, 0 },
                    { "23", "1", "8", 0, "Mobil uygulama geliştirme sürecinde geliştiricilerin karşılaştığı en büyük karar noktalarından biri, iOS ve Android platformları arasında seçim yapmaktır. Her iki platform da geniş kullanıcı kitlesine sahip olmasına rağmen, bazı önemli farklar içerir. Bu farklar, geliştirme sürecini ve kullanıcı deneyimini doğrudan etkileyebilir.\r\n\r\nAndroid, açık kaynaklı bir platformdur ve daha geniş bir cihaz yelpazesiyle uyumlu çalışır. Bu da geliştiricilere daha fazla esneklik sağlar. Ancak, Android uygulama geliştirme süreci, iOS'a göre daha karmaşık olabilir. iOS, Apple tarafından geliştirilen kapalı bir ekosistemdir ve daha sınırlı bir cihaz portföyü sunar. Ancak, bu platformda uygulama geliştirme ve test süreçleri genellikle daha tutarlıdır.\r\n\r\nHer iki platformun da kendine özgü avantajları ve dezavantajları vardır. Geliştiriciler, hedef kitlelerine ve uygulama ihtiyaçlarına göre hangi platformu seçeceklerine karar vermelidir. Ayrıca, çapraz platform geliştirme araçları sayesinde, tek bir kod tabanı ile her iki platform için de uygulama geliştirilebilir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9989), null, 0, "Article23.jpg", "iOS ve Android Arasındaki Farklar", null, 0 },
                    { "24", "2", "8", 0, "Mobil uygulama performansı, kullanıcıların uygulama deneyimini doğrudan etkileyen en önemli faktörlerden biridir. Bir uygulama ne kadar iyi tasarlanmış olursa olsun, yavaş veya gecikmeli çalışıyorsa kullanıcılar bu uygulamayı kullanmayı bırakabilir. Bu nedenle, uygulama performansını artırmak, başarılı bir mobil uygulama için kritik öneme sahiptir.\r\n\r\nUygulama performansını artırmak için ilk olarak optimize edilmiş kod yazılması gereklidir. Karmaşık algoritmalar ve gereksiz veriler, uygulamanın yavaşlamasına neden olabilir. Ayrıca, arka planda gereksiz çalışan süreçler, uygulamanın kaynaklarını tüketir ve performansı olumsuz etkiler. Bu nedenle, kaynak yönetimi dikkatlice yapılmalıdır.\r\n\r\nVeri yüklemeleri ve API çağrıları da uygulama performansını etkileyen önemli unsurlardır. Veri transferlerini optimize etmek ve gereksiz ağ isteklerinden kaçınmak, uygulamanın hızını artırabilir. Son olarak, uygulamanın test aşamalarında performans sorunları tespit edilmeli ve iyileştirmeler yapılmalıdır.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9993), null, 0, "Article24.jpg", "Mobil Uygulama Performansını Artırma Yöntemleri", null, 0 },
                    { "25", "3", "9", 0, "Oyun geliştirme dünyasında, oyun motorları, geliştiricilerin projelerini hayata geçirmelerinde kritik bir rol oynar. İki popüler oyun motoru olan Unity ve Unreal Engine, dünya genelinde milyonlarca geliştirici tarafından tercih edilmektedir. Ancak, hangi oyun motorunun kullanılacağına karar vermek, projenin gereksinimlerine göre değişiklik gösterebilir.\r\n\r\nUnity, kullanıcı dostu arayüzü ve geniş platform desteği ile bilinir. Hem 2D hem de 3D oyunlar için güçlü araçlar sunar. Unity, mobil oyunlardan sanal gerçeklik projelerine kadar geniş bir yelpazede kullanılır ve özellikle yeni başlayanlar için ideal bir oyun motorudur. Ayrıca, Unity'nin büyük bir geliştirici topluluğu ve kapsamlı dokümantasyonu, öğrenme sürecini hızlandırır.\r\n\r\nÖte yandan Unreal Engine, yüksek kaliteli grafikler ve fotogerçekçi görseller sunar. Özellikle büyük bütçeli AAA oyun projelerinde tercih edilen Unreal Engine, güçlü bir grafik motoru ve gelişmiş fizik simülasyonlarıyla dikkat çeker. Unreal Engine, C++ dilini kullanarak derinlemesine oyun mekaniği oluşturmayı sağlar, ancak öğrenme eğrisi Unity'ye göre daha dik olabilir. Her iki motor da güçlü araçlar sunarken, seçiminizi projenizin türüne ve teknik gereksinimlerine göre yapmalısınız.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9996), null, 0, "Article25.jpg", "Oyun Motorları: Unity ve Unreal Engine Karşılaştırması", null, 0 },
                    { "26", "6", "9", 0, "Tabii, 9. kategori olan Oyun Geliştirme için devam edelim.\r\n9. Kategori: Oyun Geliştirme\r\nMakale 1: \"Oyun Motorları: Unity ve Unreal Engine Karşılaştırması\"\r\n\r\nOyun geliştirme dünyasında, oyun motorları, geliştiricilerin projelerini hayata geçirmelerinde kritik bir rol oynar. İki popüler oyun motoru olan Unity ve Unreal Engine, dünya genelinde milyonlarca geliştirici tarafından tercih edilmektedir. Ancak, hangi oyun motorunun kullanılacağına karar vermek, projenin gereksinimlerine göre değişiklik gösterebilir.\r\n\r\nUnity, kullanıcı dostu arayüzü ve geniş platform desteği ile bilinir. Hem 2D hem de 3D oyunlar için güçlü araçlar sunar. Unity, mobil oyunlardan sanal gerçeklik projelerine kadar geniş bir yelpazede kullanılır ve özellikle yeni başlayanlar için ideal bir oyun motorudur. Ayrıca, Unity'nin büyük bir geliştirici topluluğu ve kapsamlı dokümantasyonu, öğrenme sürecini hızlandırır.\r\n\r\nÖte yandan Unreal Engine, yüksek kaliteli grafikler ve fotogerçekçi görseller sunar. Özellikle büyük bütçeli AAA oyun projelerinde tercih edilen Unreal Engine, güçlü bir grafik motoru ve gelişmiş fizik simülasyonlarıyla dikkat çeker. Unreal Engine, C++ dilini kullanarak derinlemesine oyun mekaniği oluşturmayı sağlar, ancak öğrenme eğrisi Unity'ye göre daha dik olabilir. Her iki motor da güçlü araçlar sunarken, seçiminizi projenizin türüne ve teknik gereksinimlerine göre yapmalısınız.\r\nMakale 2: \"Oyun Tasarımında Hikaye Anlatımının Önemi\"\r\n\r\nBir oyun sadece görsellikten ibaret değildir; oyuncuyu içine çeken bir hikaye, oyunun başarısında önemli bir rol oynar. Oyun tasarımında güçlü bir hikaye, oyuncuların duygusal olarak bağ kurmasını sağlar ve onları oyunun dünyasına daha derinden çeker. Hikaye anlatımı, oyun karakterlerinin gelişimi, olay örgüsü ve oyuncunun karşılaştığı seçimlerle şekillenir.\r\n\r\nHikaye odaklı oyunlar, oyunculara sadece mekanik bir deneyim sunmaz, aynı zamanda onları duygusal bir yolculuğa çıkarır. Örneğin, RPG (rol yapma oyunu) türündeki oyunlar, oyuncuya karakteri üzerinde tam kontrol sağlayarak kararlarının sonuçlarını görmesine olanak tanır. Bu, oyuncuların oyunla daha fazla bağ kurmasını sağlar.\r\n\r\nOyun tasarımında hikaye anlatımını başarılı bir şekilde entegre etmek için, karakterlerin derinliği ve dünyasının zenginliği önemlidir. Ayrıca, oyunun sunduğu dünyaya uygun bir anlatı tarzı seçmek de kritiktir. Hikaye, oyuncunun oyun dünyasında yapacağı keşifler ve karşılaşacağı zorluklar ile paralel ilerlemelidir. İyi bir hikaye, oyuncunun oyuna tekrar tekrar dönmesini sağlayan güçlü bir motivasyon kaynağıdır.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local), null, 0, "Article26.jpg", "Oyun Tasarımında Hikaye Anlatımının Önemi", null, 0 },
                    { "27", "7", "9", 0, "Oyun geliştirme sürecinde test ve hata ayıklama (debugging) aşamaları, oyunun son kullanıcıya sorunsuz bir deneyim sunmasını sağlamak için hayati önem taşır. Oyun geliştirme sürecinde karşılaşılan hataların büyük bir kısmı, karmaşık oyun mekanikleri, performans sorunları ve platformlar arası uyumsuzluklar ile ilgilidir. Bu nedenle, her oyun projesinde detaylı bir test süreci yürütülmelidir.\r\n\r\nOyun geliştirme sürecinde iki temel test yöntemi bulunur: fonksiyonel test ve performans testi. Fonksiyonel test, oyunun tüm mekaniğinin doğru çalışıp çalışmadığını kontrol eder. Bu testler, oyuncu etkileşimlerinden, yapay zeka davranışlarına kadar oyunun her yönünü kapsar. Performans testi ise oyunun hangi platformlarda nasıl çalıştığını değerlendirir ve optimizasyon gereksinimlerini belirler.\r\n\r\nHata ayıklama süreci ise, oyun kodunda bulunan hataların tespit edilmesi ve düzeltilmesini içerir. Bu süreçte, oyun motorunun sunduğu hata ayıklama araçları ve loglama teknikleri kullanılarak hatalar belirlenir ve düzeltilir. Özellikle çok oyunculu oyunlarda test ve hata ayıklama süreci, oyun dengesi ve sunucu performansı açısından kritik öneme sahiptir. Başarılı bir test ve hata ayıklama süreci, oyunun yayınlandığı gün kullanıcıların sorunsuz bir deneyim yaşamasını sağlar.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(3), null, 0, "Article27.jpg", "Oyun Geliştirmede Test ve Hata Ayıklama Süreçleri", null, 0 },
                    { "28", "8", "10", 0, "Tabii, 10. kategori olan Veri Bilimi için devam edelim.\r\n10. Kategori: Veri Bilimi\r\nMakale 1: \"Veri Bilimine Giriş: Temel Kavramlar ve Teknikler\"\r\n\r\nVeri bilimi, günümüzün en hızlı büyüyen alanlarından biridir ve büyük miktarda veriyi analiz ederek değerli bilgiler elde etmeyi amaçlar. Veri bilimi, istatistik, matematik, bilgisayar bilimi ve iş analitiği gibi disiplinleri bir araya getirir. Veri bilimciler, büyük veri setlerini analiz etmek, anlamak ve iş stratejilerine dönüştürmek için çeşitli teknikler kullanır.\r\n\r\nVeri biliminin temelinde, verilerin toplanması, temizlenmesi ve analiz edilmesi süreci bulunur. Bu süreçte kullanılan yaygın tekniklerden bazıları, regresyon analizi, kümeleme ve sınıflandırma algoritmalarıdır. Ayrıca, makine öğrenimi yöntemleri de veri biliminde önemli bir yer tutar. Örneğin, gözetimli öğrenme teknikleri, geçmiş verilere dayanarak gelecekteki olayları tahmin etmeye yardımcı olabilir.\r\n\r\nVeri bilimi, birçok sektörde büyük değer yaratır. Finans, sağlık, perakende ve teknoloji gibi alanlarda, verilerin doğru bir şekilde analiz edilmesi, daha iyi kararlar alınmasına ve müşteri deneyimlerinin iyileştirilmesine yardımcı olur. Veri bilimine başlamak isteyenler için Python, R ve SQL gibi programlama dilleri ve veri görselleştirme araçları öğrenmeye değer becerilerdir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(9), null, 0, "Article28.jpg", "Veri Bilimine Giriş: Temel Kavramlar ve Teknikler", null, 0 },
                    { "29", "9", "10", 0, "Makine öğrenimi, veri biliminin en önemli alt dallarından biridir ve veri setlerinden öğrenerek tahmin modelleri oluşturmaya dayanır. Tahmin modelleme, gelecekteki olayları tahmin etmek için geçmiş verilere dayanarak matematiksel modeller oluşturma sürecidir. Bu teknik, finansal tahminlerden, müşteri davranışlarını analiz etmeye kadar geniş bir yelpazede kullanılır.\r\n\r\nMakine öğrenimi algoritmaları, veri bilimi projelerinde tahmin modellemesi yapmak için kullanılır. Örneğin, bir e-ticaret sitesinde müşterilerin alışveriş alışkanlıklarına dayanarak hangi ürünleri satın alacaklarını tahmin eden bir model geliştirebiliriz. Bu model, lojistik regresyon, karar ağaçları veya sinir ağları gibi algoritmalar kullanılarak oluşturulabilir.\r\n\r\nMakine öğrenimi modellerini oluştururken, verilerin doğru bir şekilde ön işlenmesi büyük önem taşır. Verilerin eksiksiz, doğru ve tutarlı olması, modelin doğruluğunu etkiler. Ayrıca, modellerin performansını değerlendirmek için çapraz doğrulama ve test veri setleri kullanmak gerekir. Doğru yapılandırılmış bir makine öğrenimi modeli, veri bilimi projelerinde büyük bir katma değer sağlar ve iş süreçlerini optimize eder.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(13), null, 0, "Article29.jpg", "Makine Öğrenimi ile Tahmin Modelleme: Bir Veri Bilimi Uygulaması", null, 0 },
                    { "3", "3", "1", 0, "Yazılım projelerinde veritabanı entegrasyonu, verilerin güvenli ve hızlı bir şekilde işlenmesini sağlar. ORM (Object-Relational Mapping), bu işlemi kolaylaştıran bir tekniktir. ORM, nesne tabanlı programlama dillerini kullanan yazılımcılar için veritabanı yönetimini kolaylaştıran bir araçtır. Bu araç, veritabanındaki tabloları ve sütunları programlama dilindeki nesneler ve özelliklerle ilişkilendirir.\r\n\r\nORM kullanımı, yazılım geliştirme sürecinde büyük avantajlar sunar. SQL sorgularını doğrudan yazmak yerine, veritabanı işlemlerini nesneler üzerinden gerçekleştirmek mümkündür. Bu sayede, hem kod okunabilirliği artar hem de veritabanı ile programlama dili arasında daha az geçiş yapılır. C# programlama dilinde yaygın olarak kullanılan ORM araçlarından biri Entity Framework'tür. Entity Framework, geliştiricilere veritabanı işlemlerini kod yazarak değil, nesnelerle çalışarak yönetme olanağı sağlar.\r\n\r\nVeritabanı işlemleri sırasında en önemli konulardan biri performanstır. ORM araçları, performansı artırmak için çeşitli optimizasyonlar sağlar. Ancak, büyük veritabanları söz konusu olduğunda dikkatli olmak gerekir. ORM kullanırken, özellikle büyük ölçekli projelerde sorgu optimizasyonları yapmak ve gereksiz veri yükünü azaltmak önemlidir.\r\n\r\nORM kullanımı, yazılım projelerinde veritabanı işlemlerinin daha güvenli ve hızlı bir şekilde gerçekleştirilmesini sağlar. Nesne tabanlı programlama yaklaşımı ile uyumlu olan bu araçlar, projelerdeki veritabanı işlemlerini büyük ölçüde basitleştirir ve geliştiricilere büyük bir esneklik sunar.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9872), null, 0, "Article3.jpg", "Veritabanı Entegrasyonu ve ORM Kullanımı", null, 0 },
                    { "30", "10", "10", 0, "Veri biliminde büyük veri analizi, giderek daha önemli bir hale gelmiştir. Büyük veri, geleneksel veri işleme araçlarıyla işlenemeyecek kadar büyük ve karmaşık veri kümelerini ifade eder. Bu tür verilerin işlenmesi için özel teknolojiler geliştirilmiştir ve bunların başında Hadoop ve Spark gelir.\r\n\r\nHadoop, açık kaynaklı bir büyük veri işleme çerçevesidir ve verileri paralel olarak işleyebilme yeteneği sayesinde büyük veri kümelerini hızlı bir şekilde analiz edebilir. Hadoop’un HDFS (Hadoop Distributed File System) adlı dosya sistemi, büyük veri kümelerinin dağıtık bir şekilde depolanmasını sağlar. Hadoop ayrıca MapReduce adı verilen bir programlama modelini kullanarak verileri işlemektedir.\r\n\r\nSpark ise Hadoop’a alternatif bir çözüm olarak geliştirilmiştir ve büyük veri işleme süreçlerini hızlandırmayı hedefler. Spark, bellekte veri işleyerek Hadoop’a göre çok daha hızlı sonuçlar üretebilir. Ayrıca, Spark’ın veri işleme yetenekleri, makine öğrenimi algoritmaları ve veri akışları için de uygundur. Spark, Hadoop ile entegre bir şekilde çalışabildiği gibi bağımsız olarak da kullanılabilir.\r\n\r\nHer iki teknoloji de büyük veri işleme süreçlerinde önemli roller oynar. Hadoop, büyük veri kümelerinin depolanması ve işlenmesi için ideal bir çözüm sunarken, Spark, daha hızlı veri işleme ve makine öğrenimi uygulamaları için tercih edilir. Projenin ihtiyaçlarına göre bu iki teknolojiden biri seçilmelidir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(17), null, 0, "Article30.jpg", "Büyük Veri Teknolojileri: Hadoop ve Spark'ın Karşılaştırması", null, 0 },
                    { "31", "11", "11", 0, "Yapay zeka (AI) ve robotik süreç otomasyonu (RPA), endüstriyel süreçlerin otomasyonunda devrim yaratmıştır. Geleneksel olarak insan gücüne dayanan birçok iş, artık yapay zeka ve robotik teknolojiler sayesinde daha hızlı, verimli ve hatasız bir şekilde gerçekleştirilebiliyor. Bu teknolojiler, üretimden müşteri hizmetlerine kadar geniş bir yelpazede kullanılıyor.\r\n\r\nYapay zeka ile desteklenen robotik süreç otomasyonu, belirli görevleri öğrenip tekrarlayarak zaman ve maliyet tasarrufu sağlar. Örneğin, bir üretim hattında tekrarlayan işler yapay zeka algoritmaları tarafından öğrenilir ve robotlar bu işleri yüksek bir doğrulukla yerine getirir. Bu sistemler, insan müdahalesi olmadan çalışma yeteneğine sahip olduğundan, 7/24 kesintisiz operasyon sağlarlar.\r\n\r\nRPA, iş dünyasında da giderek daha yaygın hale gelmiştir. Özellikle finans, sağlık ve lojistik sektörlerinde süreçleri optimize etmek için kullanılır. Örneğin, bir bankada kredi başvurularını işleyen RPA sistemleri, yapay zeka algoritmalarını kullanarak verileri analiz eder ve başvuruların onaylanma sürecini hızlandırır. Bu, iş gücü maliyetlerini azaltırken, işlem sürelerini önemli ölçüde kısaltır. Yapay zeka ve robotik süreç otomasyonu, geleceğin iş dünyasında temel bir rol oynayacaktır.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(21), null, 0, "Article31.jpg", "Yapay Zeka ile Robotik Süreç Otomasyonu: Endüstrinin Geleceği", null, 0 },
                    { "32", "12", "11", 0, "Robot teknolojileri, yalnızca fabrikalarla sınırlı kalmayıp, sağlık, tarım, eğitim ve günlük yaşamda da kullanılmaya başlamıştır. Modern robotlar, daha gelişmiş sensörlerle donatılarak çevrelerini algılayabilme ve bu verilere dayanarak bağımsız kararlar alabilme yeteneklerine sahiptir. Bu gelişmeler, robotların daha karmaşık görevleri yerine getirmesine olanak tanımaktadır.\r\n\r\nÖrneğin, sağlık alanında robotik cerrahi sistemleri, hassas operasyonların daha hızlı ve daha güvenli bir şekilde yapılmasını sağlar. Da Vinci Cerrahi Robotu gibi cihazlar, doktorlara minimal invaziv cerrahi işlemlerinde yardımcı olmakta ve iyileşme sürelerini kısaltmaktadır. Tarım sektöründe ise, tarım robotları hasat işlemlerini hızlandırmakta ve tarım ilaçlarının hassas bir şekilde uygulanmasını sağlamaktadır.\r\n\r\nAyrıca, otonom araçlar ve insansı robotlar gibi ileri düzey robotik teknolojiler, insan hayatını kolaylaştırmak için hızla gelişmektedir. Tesla gibi firmalar otonom araç teknolojilerinde önemli ilerlemeler kaydederken, Boston Dynamics’in robotları da endüstriyel süreçlerden arama kurtarma operasyonlarına kadar birçok alanda kullanılmaktadır. Robotların daha fazla öğrenme yeteneği kazanmasıyla, robotik teknolojilerin gelecekte daha da yaygınlaşacağı öngörülmektedir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(24), null, 0, "Article32.jpg", "Robotik Teknolojilerde Son Gelişmeler ve Uygulama Alanları", null, 0 },
                    { "33", "1", "11", 0, "Yapay zeka destekli otonom robotlar, insansı hareketler ve algılama yetenekleri ile donatılmış robotlardır. Bu robotlar, çevrelerini algılayabilmekte, bağımsız olarak hareket edebilmekte ve insanlarla etkileşime geçebilmektedir. Yapay zeka teknolojisi, bu robotların daha esnek ve zeki bir şekilde davranmasına olanak sağlar.\r\n\r\nOtonom robotlar, genellikle karmaşık görevleri yerine getirebilmek için yapay sinir ağları ve makine öğrenimi algoritmaları kullanır. Örneğin, bir otonom robot, nesneleri tanıyıp sınıflandırarak, belirli bir görevi yerine getirebilir. Ayrıca, bu robotlar, gerçek zamanlı veri analizi yaparak, çevresindeki değişikliklere uyum sağlayabilirler. Sensörler ve kameralar sayesinde robotlar, engelleri algılayarak güvenli bir şekilde hareket edebilirler.\r\n\r\nİnsansı robotlar ise, özellikle hizmet sektörü ve bakım hizmetlerinde kullanılmak üzere geliştirilmektedir. Bu robotlar, insanların yaptığı birçok fiziksel işi yerine getirebilirler. Örneğin, hasta bakımı veya yaşlı bakımı gibi alanlarda insansı robotlar, insanların günlük işlerini kolaylaştırabilir. Ayrıca, bu robotlar, öğrenme algoritmaları sayesinde sürekli olarak kendilerini geliştirebilmekte ve daha karmaşık görevleri yerine getirebilmektedirler. Yapay zeka destekli otonom robotların gelecekte birçok alanda insanlara yardımcı olacağı öngörülmektedir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(28), null, 0, "Article33.jpg", "Yapay Zeka Destekli Otonom Robotlar: İnsansı Hareketler ve Algılama Yetenekleri", null, 0 },
                    { "34", "2", "12", 0, "Bir bilgisayarın performansını belirleyen en temel bileşenler arasında işlemci, RAM (rastgele erişim belleği) ve depolama birimleri yer alır. İşlemci, bilgisayarın beyni olarak kabul edilir ve tüm verileri işleyerek uygulamaların çalışmasını sağlar. Günümüzde, işlemciler çok çekirdekli yapılarıyla daha hızlı işlem kapasiteleri sunmaktadır. RAM ise, işlemciye geçici veri sağlayarak uygulamaların hızlı bir şekilde çalışmasını destekler. Belleğin yetersiz olduğu durumlarda, bilgisayar yavaşlayabilir ve uygulamalar daha geç yanıt verebilir. Son olarak, depolama birimleri arasında SSD ve HDD seçenekleri bulunmaktadır. SSD’ler, geleneksel HDD’lere göre daha hızlı veri erişimi sunarak bilgisayarın genel performansını artırır. Bu temel donanım bileşenleri, bir bilgisayarın hızını ve verimliliğini doğrudan etkileyen unsurlardır.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(32), null, 0, "Article34.jpg", "Bilgisayar Donanımının Temel Bileşenleri: İşlemci, RAM ve Depolama", null, 0 },
                    { "35", "3", "12", 0, "Ekran kartı, özellikle oyun severler ve grafik işleme işi yapan profesyoneller için büyük önem taşır. Oyunlarda yüksek kare hızlarına ve kaliteli grafiklere ulaşmak, güçlü bir ekran kartı gerektirir. Ekran kartları, işlemcinin yükünü hafifleterek oyunlardaki görsel öğelerin daha hızlı işlenmesini sağlar. NVIDIA ve AMD gibi büyük markalar, farklı bütçe ve kullanım ihtiyaçlarına yönelik ekran kartları üretmektedir. Oyunlarda yüksek performans almak isteyen kullanıcılar, genellikle en az 6 GB bellek kapasitesine sahip kartları tercih etmelidir. Grafik tasarımı ya da video düzenleme işleri için ise yüksek CUDA çekirdekli ekran kartları ideal bir seçenek olabilir. Doğru ekran kartı seçimi, bilgisayar performansını artırmanın yanı sıra oyun ve grafik işleme deneyimini de en üst düzeye çıkarır.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(35), null, 0, "Article35.jpg", "Ekran Kartı Seçimi: Oyun ve Grafik İşleme İçin İdeal Kartlar", null, 0 },
                    { "36", "6", "12", 0, "Bilgisayarın performansını koruyabilmesi için etkili bir soğutma sistemi kullanılması şarttır. Bilgisayar bileşenleri, özellikle yoğun işlem gücü gerektiren oyunlar veya uygulamalar çalıştırıldığında aşırı ısınabilir. Bu nedenle, CPU ve GPU gibi donanımların ısısını düşürmek için çeşitli soğutma sistemleri geliştirilmiştir. Hava soğutma sistemleri, fanlar aracılığıyla sıcak havayı dışarı atarak temel bir soğutma sağlar. Ancak sıvı soğutma sistemleri, daha ileri düzeyde soğutma sunarak yüksek performanslı sistemlerde tercih edilir. Özellikle hız aşırtma (overclocking) yapılan bilgisayarlarda sıvı soğutma kullanılması önerilir. Doğru soğutma sistemi, bilgisayarın ömrünü uzatırken, yüksek performansın sürdürülebilmesine de olanak tanır.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(41), null, 0, "Article36.jpg", "Bilgisayar Soğutma Sistemleri: Hangi Seçenek Sizin İçin Doğru?", null, 0 },
                    { "37", "7", "13", 0, "İnsan bilgisayar etkileşimi (HCI) tasarımında renklerin rolü, kullanıcı deneyimini doğrudan etkileyen önemli bir faktördür. Renkler, kullanıcıların bir arayüzde nasıl hissettiğini, bir işlem sırasında nasıl davrandığını ve genel deneyimini etkiler. Örneğin, mavi renk genellikle güven ve sadakati temsil ederken, kırmızı renk dikkat çekmek veya bir uyarı vermek amacıyla kullanılır. Kullanıcı arayüzlerinde renklerin doğru kullanılması, kullanıcıların uygulama veya web sitesinde daha rahat gezinmelerini sağlar ve etkileşim oranlarını artırır. Ancak aşırı renk kullanımı veya yanlış renk seçimi, kullanıcıları bunaltabilir ve istenen etkileşimi engelleyebilir. Bu yüzden, kullanıcı deneyimi tasarımında renklerin stratejik bir şekilde kullanılması gerekir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(44), null, 0, "Article37.jpg", "Kullanıcı Deneyimi Tasarımında Renklerin Önemi", null, 0 },
                    { "38", "8", "13", 0, "Tabii, 13. kategori olan İnsan Bilgisayar Etkileşimi ile devam edelim.\r\n13. Kategori: İnsan Bilgisayar Etkileşimi\r\nMakale 1: \"Kullanıcı Deneyimi Tasarımında Renklerin Önemi\"\r\n\r\nİnsan Bilgisayar Etkileşimi (HCI) alanında renklerin önemi, kullanıcı deneyimini büyük ölçüde etkileyen bir faktördür. Renkler, kullanıcıların dikkatini çekmek, onları yönlendirmek ve duygusal tepkiler oluşturmak için kullanılır. Örneğin, mavi renk genellikle güven duygusu yaratırken, kırmızı tehlike veya hata belirtmek için tercih edilir. İyi bir kullanıcı deneyimi tasarımı, renklerin psikolojik etkilerini göz önünde bulundurarak oluşturulmalıdır. Doğru renk seçimi, kullanıcıların bir arayüzü daha kolay ve verimli bir şekilde kullanmalarını sağlar.\r\nMakale 2: \"Kullanıcı Arayüzlerinde Simetri ve Hiyerarşi\"\r\n\r\nKullanıcı arayüzü (UI) tasarımında simetri ve hiyerarşi, görsel düzenin önemli unsurlarıdır. Simetri, kullanıcıların bir arayüzü daha kolay kavramalarına yardımcı olurken, hiyerarşi bilgilerin önem sırasına göre sunulmasını sağlar. HCI araştırmaları, simetrik tasarımların kullanıcılar tarafından daha estetik bulunduğunu ve daha hızlı algılandığını göstermektedir. Hiyerarşi ise, kullanıcıların dikkatini en önemli unsurlara yönlendirir. Bu nedenle, simetri ve hiyerarşi kurallarını dikkate alarak tasarlanan arayüzler, kullanıcı dostu deneyimler sunar.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(47), null, 0, "Article38.jpg", "Kullanıcı Arayüzlerinde Simetri ve Hiyerarşi", null, 0 },
                    { "39", "9", "13", 0, "İnsan Bilgisayar Etkileşimi alanında geri bildirim, kullanıcıların sistemle etkileşimde bulunduğunda sistemin durumu hakkında bilgilendirilmelerini sağlayan kritik bir unsurdur. Bir butona tıklamak, bir form doldurmak ya da bir işlem başlatmak gibi kullanıcı aksiyonları sonucunda, sistemin kullanıcıya geri bildirim vermesi hem güven oluşturur hem de işlemin başarıyla tamamlandığını gösterir. Bu geri bildirimler, görsel (örneğin, yüklenme çubuğu), işitsel (bildirim sesi) ya da dokunsal (vibrasyon) olabilir. Etkili geri bildirimler, kullanıcıların sistemle daha rahat etkileşime girmesini sağlar ve olası hataları en aza indirir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(51), null, 0, "Article39.jpg", "HCI’de Geri Bildirimin Önemi ve Uygulamaları", null, 0 },
                    { "4", "6", "2", 0, "Ağ yönetimi, modern bilgi sistemlerinin temel taşlarından biridir. Bir ağ, bilgisayarlar, sunucular ve diğer cihazlar arasındaki iletişimi sağlar. Ağ yönetimi, bu ağların sorunsuz bir şekilde çalışmasını sağlamak için yapılan işlemler ve uygulamalardır. Ağ yöneticileri, sistemlerin güvenliğini sağlamak, ağ trafiğini izlemek ve performans sorunlarını çözmekle sorumludur.\r\n\r\nAğ yönetiminde temel kavramlar arasında protokoller, IP adresleri ve yönlendirme bulunmaktadır. Protokoller, cihazlar arasındaki iletişimi düzenleyen kurallar bütünüdür. IP adresleri, her cihazın benzersiz bir kimlik numarasıdır ve bu adresler aracılığıyla cihazlar birbirleriyle iletişim kurabilir. Yönlendirme ise, verilerin doğru cihazlara ulaşmasını sağlayan süreçtir.\r\n\r\nAyrıca, ağ güvenliği, ağ yönetiminin kritik bir parçasıdır. Firewall'lar, antivirüs yazılımları ve şifreleme teknikleri gibi güvenlik önlemleri, ağın dış tehditlerden korunmasına yardımcı olur. Güçlü bir ağ yönetimi stratejisi, bir işletmenin veya kuruluşun verilerinin güvenli ve erişilebilir kalmasını sağlar.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9888), null, 0, "Article4.jpg", "Ağ Yönetimi ve Temel Kavramlar", null, 0 },
                    { "40", "10", "14", 0, "Siber tehditlerin giderek arttığı bir dünyada, güçlü ve karmaşık parolalar ilk savunma hattı olarak kabul edilir. Birçok kişi kolay hatırlanabilir parolalar kullanma eğiliminde olsa da, bu durum hackerlar için fırsat yaratır. Özellikle sosyal mühendislik saldırılarıyla, zayıf parolalar kolayca tahmin edilebilir. Parola yöneticileri kullanarak güçlü ve benzersiz parolalar oluşturmak, siber güvenliğin temel taşlarından biridir. Ayrıca, iki faktörlü kimlik doğrulama (2FA) gibi ek güvenlik katmanları kullanmak, hesapları daha da güvenli hale getirir. Bu tür uygulamalar, kullanıcıların siber saldırılara karşı daha iyi korunmasını sağlar.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(54), null, 0, "Article40.jpg", "Siber Saldırılara Karşı İlk Savunma Hattı: Güçlü Parolalar", null, 0 },
                    { "41", "11", "14", 0, "Sosyal mühendislik, teknolojik sistemlerden ziyade insanların zaaflarını hedef alan bir siber saldırı türüdür. Bu tür saldırılar, kullanıcıları kandırarak kritik bilgilerini (şifreler, kimlik bilgileri vb.) paylaşmalarını sağlamaya dayanır. Phishing (oltalama) saldırıları, en yaygın sosyal mühendislik tekniklerinden biridir. Saldırganlar, kullanıcıların güvenini kazanarak onları yanıltıcı e-postalar, mesajlar ya da web siteleri ile tuzağa düşürürler. Sosyal mühendislik saldırılarına karşı korunmak için kullanıcıların bilinçlendirilmesi ve şüpheli iletiler karşısında dikkatli olunması önemlidir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(57), null, 0, "Article41.jpg", "Sosyal Mühendislik Saldırıları: İnsan Faktörünü Hedef Alan Tehditler", null, 0 },
                    { "42", "12", "14", 0, "Dağıtılmış Hizmet Reddi (DDoS) saldırıları, bir sistemin kaynaklarını tüketerek hizmet veremez hale getirmeyi hedefler. DDoS saldırıları genellikle çok sayıda cihazın (botnet) eşzamanlı olarak bir hedefe yönlendirilmesiyle gerçekleştirilir. Bu tür saldırılar, özellikle büyük ölçekli web siteleri ve hizmet sağlayıcıları için ciddi sonuçlar doğurabilir; sistemlerin çökmesine ve uzun süreli hizmet kesintilerine neden olabilir. DDoS saldırılarına karşı korunmanın yolları arasında trafik filtreleme, yük dengeleme ve güvenlik duvarı yapılandırmaları gibi stratejiler bulunur. Bu önlemler, işletmelerin hizmet sürekliliğini sağlamada önemli rol oynar.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(62), null, 0, "Article42.jpg", "DDoS Saldırıları ve İşletmeler Üzerindeki Yıkıcı Etkileri", null, 0 },
                    { "43", "1", "15", 0, "Yazılım mühendisliğinde Agile metodolojisi, hızlı ve esnek yazılım geliştirme süreçlerini yönetmek için en yaygın kullanılan yaklaşımlardan biridir. Agile, özellikle değişen müşteri ihtiyaçlarına ve proje gereksinimlerine hızlı bir şekilde uyum sağlama yeteneğiyle öne çıkar. Kısa döngüler (sprintler) halinde yapılan çalışmalar, geliştiricilerin geri bildirimleri hızlıca alıp uygulamasına olanak tanır. Bu sayede, proje yönetimi daha şeffaf ve kontrol edilebilir hale gelir. Yazılım mühendisliği süreçlerinde Agile kullanımı, müşteri memnuniyetini artırırken proje maliyetlerini ve zaman kaybını da minimuma indirir.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(65), null, 0, "Article43.jpg", "Yazılım Geliştirme Süreçlerinde Agile Yönteminin Önemi", null, 0 },
                    { "44", "2", "15", 0, "Temiz kod, yazılım mühendisliğinde sürdürülebilir ve bakımı kolay kod yazma pratiğidir. Bir yazılımın işlevselliği kadar, okunabilirliği ve anlaşılabilirliği de önemlidir. Temiz kod, sadece geliştiricinin değil, gelecekte projeyi devralacak olan diğer yazılımcıların da hızlıca adapte olabilmesini sağlar. Kodun modüler, tekrar kullanılabilir ve iyi dökümante edilmiş olması, temiz kodun temel prensiplerindendir. Bu yaklaşım sayesinde, yazılım projeleri uzun vadede daha az hata içerir, bakım maliyetleri düşer ve geliştirme süreçleri hızlanır.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(71), null, 0, "Article44.jpg", "Yazılım Mühendisliğinde Clean Code (Temiz Kod) Yaklaşımı", null, 0 },
                    { "45", "3", "15", 0, "Yazılım mühendisliğinde kalite güvencesi, yazılımın güvenilir ve hatasız çalışmasını sağlamak için kritik bir adımdır. Test otomasyonu, bu sürecin önemli bir parçasıdır ve manuel testlerin yerini alarak daha hızlı ve etkili bir şekilde hataların tespit edilmesine olanak tanır. Otomatik testler, yazılımın her yeni versiyonunda test süreçlerinin tekrar edilmesini sağlar, bu da geliştirme döngüsünde hataların erken aşamada bulunmasına yardımcı olur. Unit testler, entegrasyon testleri ve sistem testleri gibi farklı test seviyeleri, yazılımın kalite standartlarını korumada önemli bir rol oynar.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(75), null, 0, "Article45.jpg", "Yazılım Mühendisliğinde Test Otomasyonu ve Kalite Güvencesi", null, 0 },
                    { "5", "7", "2", 0, "Sanal makineler (VM), fiziksel bir bilgisayar üzerinde sanal bir işletim sistemi çalıştırmak için kullanılan teknolojilerdir. Bu teknoloji, özellikle IT yöneticileri ve sistem mühendisleri için büyük bir avantaj sağlar. Hyper-V, Microsoft'un sanallaştırma platformudur ve Windows işletim sistemleri ile birlikte gelir.\r\n\r\nHyper-V, birden fazla sanal makineyi tek bir fiziksel sunucu üzerinde çalıştırma olanağı sunar. Bu, donanım maliyetlerini azaltırken, sistemlerin daha verimli kullanılmasını sağlar. Örneğin, bir geliştirme ortamında, Hyper-V ile farklı işletim sistemlerini ve yapılandırmaları aynı sunucu üzerinde test edebilirsiniz.\r\n\r\nHyper-V, aynı zamanda işletmelerin sistemlerini yedeklemelerini ve kurtarma planları yapmalarını da kolaylaştırır. Bir felaket durumunda, sanal makineler hızlı bir şekilde yeniden başlatılabilir ve işletmeler, veri kaybı yaşamadan iş süreçlerine devam edebilir. Sanallaştırma teknolojileri, sistem yönetimi süreçlerini daha esnek ve ölçeklenebilir hale getirir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9891), null, 0, "Article5.jpg", "Sanal Makineler ve Hyper-V Kullanımı", null, 0 },
                    { "6", "8", "2", 0, "Sunucu yönetimi, IT departmanları için önemli bir sorumluluktur. Özellikle büyük ağlarda, yüzlerce sunucunun manuel olarak yönetilmesi oldukça zor olabilir. Bu nedenle, otomasyon araçları ve script'ler, sunucu yönetimini büyük ölçüde kolaylaştırır. PowerShell, bu alanda yaygın olarak kullanılan bir komut satırı aracıdır ve Windows tabanlı sistemlerin yönetiminde güçlü bir otomasyon sağlar.\r\n\r\nPowerShell ile sunucular üzerinde otomatik görevler oluşturabilir, yazılım kurulumlarını gerçekleştirebilir ve sistem ayarlarını düzenleyebilirsiniz. Örneğin, yüzlerce sunucuda aynı yapılandırmayı yapmak gerektiğinde, PowerShell script'leri kullanarak bu işlemi hızlı bir şekilde gerçekleştirebilirsiniz.\r\n\r\nAyrıca, PowerShell'in modüler yapısı, sistem yöneticilerinin farklı görevleri kolayca entegre etmesine olanak tanır. Bu sayede, sunucuların izlenmesi, güncellenmesi ve performans analizleri gibi işlemler daha etkin bir şekilde yapılabilir. PowerShell ile sunucu yönetiminde otomasyon, insan hatalarını minimize eder ve iş süreçlerinin daha hızlı ilerlemesini sağlar.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9895), null, 0, "Article6.jpg", "Sunucu Yönetiminde PowerShell ve Otomasyon", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "AppUserId", "CategoryId", "CommentCount", "Content", "CreateDate", "DeleteDate", "Status", "Thumbnail", "Title", "UpdateDate", "ViewsCount" },
                values: new object[,]
                {
                    { "7", "9", "3", 0, "Web tasarımında renk ve tipografi, kullanıcı deneyiminin ve görsel estetiğin en kritik unsurlarından biridir. Doğru renk paleti ve tipografi seçimi, bir web sitesinin genel hissiyatını ve kullanıcıların algısını büyük ölçüde etkiler. Web tasarımında kullanılan renkler, markanın kimliğini yansıtırken, tipografi ise içeriğin okunabilirliğini ve kullanıcı etkileşimini artırır.\r\n\r\nRenklerin psikolojik etkileri göz önünde bulundurularak, her web sitesinde dikkatlice seçilmelidir. Örneğin, mavi güven ve profesyonelliği simgelerken, kırmızı dikkat çeker ve aciliyet hissi yaratır. Web tasarımcıları, kullanıcıları doğru yönlendirmek ve onlara olumlu bir deneyim sunmak için renkleri stratejik bir şekilde kullanmalıdır.\r\n\r\nTipografi ise web sitesinin içerik yapısını ve okunabilirliğini doğrudan etkiler. Büyük başlıklar, kullanıcının dikkatini çekmek için etkili bir yolken, küçük ve düzgün paragraflar, içeriğin kolayca sindirilmesini sağlar. Web tasarımında modern fontlar kullanmak, sitenin çağdaş ve profesyonel bir görünüm kazanmasına yardımcı olur.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9898), null, 0, "Article7.jpg", "Modern Web Tasarımında Renk ve Tipografi Kullanımı", null, 0 },
                    { "8", "10", "3", 0, "Günümüzde, mobil cihazların yaygın kullanımıyla birlikte responsive (duyarlı) web tasarımı, web projelerinin vazgeçilmez bir parçası haline gelmiştir. Responsive tasarım, bir web sitesinin farklı cihaz ekran boyutlarına uyum sağlamasını ifade eder. Bu sayede, kullanıcılar, hangi cihazı kullanırlarsa kullansınlar, web sitesinde rahatça gezinip içeriklere ulaşabilirler.\r\n\r\nResponsive tasarımın temelinde, esnek grid sistemleri, medya sorguları ve duyarlı görseller bulunur. Bu teknikler, web sitesinin hem masaüstü hem de mobil cihazlarda sorunsuz bir şekilde görüntülenmesini sağlar. Örneğin, bir web sayfasının üç sütunlu yapısı, mobil cihazlarda tek sütuna indirgenerek daha kullanıcı dostu bir hale gelir.\r\n\r\nMobil uyumluluk, arama motorları için de önemli bir faktördür. Google, mobil uyumlu web sitelerini arama sonuçlarında üst sıralarda gösterir. Bu nedenle, web tasarımcıları, kullanıcı deneyimini en üst düzeye çıkarmak ve SEO avantajı sağlamak için responsive tasarımı projelerine dahil etmelidir.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9902), null, 0, "Article8.jpg", "Responsive Tasarım ve Mobil Uyumlu Web Siteleri", null, 0 },
                    { "9", "11", "3", 0, "Minimalizm, son yıllarda grafik ve web tasarımında popüler hale gelen bir akımdır. Bu yaklaşım, gereksiz detaylardan arınmış, sade ve net tasarımlar oluşturmayı amaçlar. Minimalist tasarım, kullanıcıların dikkatini dağıtan unsurları ortadan kaldırır ve içeriğin ön planda olmasını sağlar. \"Az çoktur\" prensibi, bu tasarım felsefesinin temelini oluşturur.\r\n\r\nMinimalist tasarımda kullanılan unsurlar genellikle sınırlı bir renk paleti, basit tipografi ve bolca beyaz alan içerir. Bu tasarım yaklaşımı, kullanıcıların dikkatini dağıtmadan, içerikleri daha hızlı ve kolay bir şekilde tüketmelerine olanak tanır. Özellikle kurumsal web siteleri, minimalist tasarımın sunduğu sadelik ve profesyonellikten faydalanır.\r\n\r\nAncak, minimalizm sadece sadelikten ibaret değildir. Her bir tasarım öğesi dikkatlice seçilmeli ve estetik bir denge oluşturulmalıdır. Minimalist bir web sitesi, hem görsel olarak tatmin edici hem de kullanıcı dostu olmalıdır. Bu sayede, hem markanın profesyonel imajı güçlendirilir hem de kullanıcı deneyimi artırılır.", new DateTime(2024, 9, 20, 22, 58, 23, 384, DateTimeKind.Local).AddTicks(9906), null, 0, "Article9.jpg", "Grafik Tasarımda Minimalizm: Az Çoktur", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "3", "10" },
                    { "3", "11" },
                    { "3", "12" },
                    { "2", "2" },
                    { "3", "3" },
                    { "4", "4" },
                    { "4", "5" },
                    { "3", "6" },
                    { "3", "7" },
                    { "3", "8" },
                    { "3", "9" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AppUserId", "Approved", "ArticleId", "Content", "CreateDate", "DeleteDate", "Status", "UpdateDate" },
                values: new object[,]
                {
                    { "1", "7", true, "1", "C# dilinin OOP özelliklerini bu kadar net ve anlaşılır bir şekilde açıklamanız gerçekten çok faydalı oldu. Özellikle polimorfizm ve soyutlama kavramlarını bu kadar detaylı anlamak, projelerimde daha modüler yapılar kurmama yardımcı olacak.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(2206), null, 0, null },
                    { "2", "8", true, "1", "Makaledeki encapsulation (kapsülleme) ve inheritance (kalıtım) kavramları üzerine verdiğiniz örnekler çok açıklayıcıydı. C# dilinde daha önce bu kavramları yüzeysel biliyordum ama şimdi projelerimde daha etkin kullanabileceğimi düşünüyorum. Teşekkürler!", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(2211), null, 0, null },
                    { "3", "11", true, "5", "Hyper-V ile sanallaştırmanın nasıl donanım maliyetlerini düşürdüğünü çok güzel açıklamışsınız. Bu teknolojiyi kullanarak test ortamları oluşturmanın ne kadar kolay olduğunu öğrendim. Geliştirme ve test süreçlerimde kesinlikle kullanacağım.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(2215), null, 0, null },
                    { "4", "6", true, "5", "Felaket kurtarma planları oluştururken Hyper-V'nin ne kadar esnek ve etkili olduğunu vurgulamanız çok iyi olmuş. Sanal makinelerle çalışmak, IT yönetimi için büyük bir avantaj. Makale sayesinde bu teknolojiyi daha iyi anladım ve sistemlerimde kullanmayı düşünüyorum.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(2220), null, 0, null },
                    { "5", "10", true, "18", "Yedekleme stratejilerinin detaylı bir şekilde ele alınması çok faydalı oldu. Özellikle artımlı ve diferansiyel yedekleme arasındaki farkı öğrenmek benim için önemliydi. Şirketimizde veritabanı yönetimi yaparken bu bilgileri uygulayacağım.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(2224), null, 0, null },
                    { "6", "11", true, "18", "Yedekleme stratejilerinin sadece planlanmasının yeterli olmadığını, aynı zamanda kurtarma senaryolarının da düzenli olarak test edilmesi gerektiğini vurgulamanız çok önemli. Veri güvenliği için bu makalede önerilen yöntemleri uygulamaya başlayacağım.", new DateTime(2024, 9, 20, 22, 58, 23, 385, DateTimeKind.Local).AddTicks(2227), null, 0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AppUserId",
                table: "Articles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CategoryId",
                table: "Articles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AppUserId",
                table: "Comments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
