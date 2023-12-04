using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;


namespace WebApplication3.Data

{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        //public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)

        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Category>().HasData(

        //        new Category { Id = 1, Type = "Steel" },
        //        new Category { Id = 2, Type = "Rubble" },
        //        new Category { Id = 3, Type = "Leather" },
        //        new Category { Id = 4, Type = "Fabric" }
        //        );
        //    Gender maleGender = _db.Genders.Find(1);
        //    Gender femaleGender = _db.Genders.Find(2);

        //    modelBuilder.Entity<Gender>().HasData(
        //       new Gender { Id = 1, Name = "Men" },
        //       new Gender { Id = 2, Name = "Women" }
        //       );

        //    modelBuilder.Entity<Product>().HasData(
        //       new Product
        //       {
        //           Id = 1,
        //           Type = "رسمي",
        //           Material = "ستيل رمادي",
        //           Color = "مينا كحلي",
        //           Price = 20,
        //           Size = "30mm",
        //           Quantity = 4,
        //           Description = " وقت المزدوج\r\n•" +
        //           " ساعة توقيت 1/100 ثانية\r\n• " +
        //           "عرض اليوم والتاريخ \r\n• الساعه التناظريه: عقربان (ساعة ، دقيقة" +
        //           ")\r\n• الساعه الرقميه: الساعة ، الدقيقة ، الثانية ، مساءً ، الشهر ، التاريخ ، اليوم\r\n•" +
        //           " عمر بطارية 3 سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //           CategoryId = 1,


        //       },

        //    new Product
        //    {
        //        Id = 2,
        //        Type = "رسمي",
        //        Material = "ستيل رمادي",
        //        Color = "مينا ذهبي",
        //        Price = 21,
        //        Size = "30mm",
        //        Quantity = 10,
        //        Description = " وقت المزدوج\r\n•" +
        //           " ساعة توقيت 1/100 ثانية\r\n• " +
        //           "عرض اليوم والتاريخ \r\n• الساعه التناظريه: عقربان (ساعة ، دقيقة" +
        //           ")\r\n• الساعه الرقميه: الساعة ، الدقيقة ، الثانية ، مساءً ، الشهر ، التاريخ ، اليوم\r\n•" +
        //           " عمر بطارية 3 سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //        CategoryId = 1

        //    },
        //      new Product
        //      {
        //          Id = 3,
        //          Type = "رسمي",
        //          Material = "ستيل فضي",
        //          Color = "مينا أسود",
        //          Price = 18,
        //          Size = "30mm",
        //          Quantity = 4,
        //          Description = " وقت المزدوج\r\n•" +
        //           " ساعة توقيت 1/100 ثانية\r\n• " +
        //           "عرض اليوم والتاريخ \r\n• الساعه التناظريه: عقربان (ساعة ، دقيقة" +
        //           ")\r\n• الساعه الرقميه: الساعة ، الدقيقة ، الثانية ، مساءً ، الشهر ، التاريخ ، اليوم\r\n•" +
        //           " عمر بطارية 3 سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //          CategoryId = 1
        //      },
        //       new Product
        //       {
        //           Id = 4,
        //           Type = "رسمي",
        //           Material = "ستيل فضي",
        //           Color = "مينا أبيض",
        //           Price = 18,
        //           Size = "30mm",
        //           Quantity = 4,
        //           Description = " وقت المزدوج\r\n•" +
        //           " ساعة توقيت 1/100 ثانية\r\n• " +
        //           "عرض اليوم والتاريخ \r\n• الساعه التناظريه: عقربان (ساعة ، دقيقة" +
        //           ")\r\n• الساعه الرقميه: الساعة ، الدقيقة ، الثانية ، مساءً ، الشهر ، التاريخ ، اليوم\r\n•" +
        //           " عمر بطارية 3 سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //           CategoryId = 1
        //       },
        //        new Product
        //        {
        //            Id = 5,
        //            Type = "رسمي",
        //            Material = "ستيل ذهبي",
        //            Color = "مينا ذهبي",
        //            Price = 20,
        //            Size = "30mm",
        //            Quantity = 4,
        //            Description = " وقت المزدوج\r\n•" +
        //           " ساعة توقيت 1/100 ثانية\r\n• " +
        //           "عرض اليوم والتاريخ \r\n• الساعه التناظريه: عقربان (ساعة ، دقيقة" +
        //           ")\r\n• الساعه الرقميه: الساعة ، الدقيقة ، الثانية ، مساءً ، الشهر ، التاريخ ، اليوم\r\n•" +
        //           " عمر بطارية 3 سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //            CategoryId = 1
        //        },
        //         new Product
        //         {
        //             Id = 6,
        //             Type = "رسمي",
        //             Material = "ربل أسود",
        //             Color = "مينا كحلي",
        //             Price = 10,
        //             Size = "26.5mm x 36.2mm",
        //             Quantity = 4,
        //             Description = "• تبسيط أسلوبك\r\n•" +
        //             " ساعة أنالوج ضد الماء\r\n•" +
        //             " خفيفة الوزن بتصميم نحيف\r\n• ألوان ترابية غير لامعة" +
        //             " \r\n• توفر لك هذه الساعه العصريه كل من الأناقة والعملي\r\n• " +
        //             "عمر بطاريه ٣ سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //             CategoryId = 2
        //         },
        //           new Product
        //           {
        //               Id = 7,
        //               Type = "رسمي",
        //               Material = "ربل أسود",
        //               Color = "مينا رمادي",
        //               Price = 10,
        //               Size = "26.5mm x 36.2mm",
        //               Quantity = 6,
        //               Description = "• تبسيط أسلوبك\r\n•" +
        //             " ساعة أنالوج ضد الماء\r\n•" +
        //             " خفيفة الوزن بتصميم نحيف\r\n• ألوان ترابية غير لامعة" +
        //             " \r\n• توفر لك هذه الساعه العصريه كل من الأناقة والعملي\r\n• " +
        //             "عمر بطاريه ٣ سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //               CategoryId = 2
        //           },
        //           new Product
        //           {
        //               Id = 8,
        //               Type = "رسمي",
        //               Material = "ربل أسود",
        //               Color = "مينا أسود",
        //               Price = 10,
        //               Size = "26.5mm x 36.2mm",
        //               Quantity = 6,
        //               Description = "• تبسيط أسلوبك\r\n•" +
        //             " ساعة أنالوج ضد الماء\r\n•" +
        //             " خفيفة الوزن بتصميم نحيف\r\n• ألوان ترابية غير لامعة" +
        //             " \r\n• توفر لك هذه الساعه العصريه كل من الأناقة والعملي\r\n• " +
        //             "عمر بطاريه ٣ سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //               CategoryId = 2
        //           },
        //             new Product
        //             {
        //                 Id = 9,
        //                 Type = "رسمي",
        //                 Material = "سير جلد بني فاتح",
        //                 Color = " مينا ذهبي بعقارب كحلية",
        //                 Price = 25,
        //                 Size = "46mm x 41mm",
        //                 Quantity = 1,
        //                 Description = "• حزام من الجلد الطبيعي\r\n• " +
        //                 "زجاج معدني، أرقام وعقارب مطلي بطلاء كحلي غامق جداً\r\n• " +
        //                 "مقاومة الماء حتى عمق 100 متر\r\n• عرض اليوم والتاريخ\r\n•" +
        //                 " عمر بطارية 3 سنوات\r\n\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //                 CategoryId = 3

        //             },

        //             new Product
        //             {
        //                 Id = 10,
        //                 Type = "عملي",
        //                 Material = "قماش أسود",
        //                 Color = "مينا زيتي",
        //                 Price = 15,
        //                 Size = "45mm x 42mm",
        //                 Quantity = 3,
        //                 Description = "• عمر البطارية 10 سنوات\r\n•" +
        //                 " ساعة توقيت 1/100 ثانية\r\n• خريطة العالم للتوقيت العالمي" +
        //                 "\r\n• 5 منبهات\r\n• ضوء LED\r\n• مقاومة للماء حتى عمق 100 متر" +
        //                 "\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
        //                 CategoryId = 4,



        //}

        //       ) ;


        //            Product p1 = _db.Products.Find(1);
        //            p1.Genders.Add(maleGender);


        //            Product p2 = _db.Products.Find(2);
        //            p2.Genders.Add(maleGender);


        //            Product p3 = _db.Products.Find(3);
        //            p3.Genders.Add(maleGender);


        //            Product p4 = _db.Products.Find(4);
        //            p4.Genders.Add(maleGender);


        //            Product p5 = _db.Products.Find(5);
        //            p5.Genders.Add(maleGender);


        //            Product p6 = _db.Products.Find(6);
        //            p6.Genders.Add(femaleGender);
        //            p6.Genders.Add(maleGender);


        //            Product p7 = _db.Products.Find(7);
        //            p7.Genders.Add(femaleGender);
        //            p7.Genders.Add(maleGender);


        //            Product p8 = _db.Products.Find(8);
        //            p8.Genders.Add(femaleGender);
        //            p8.Genders.Add(maleGender);


        //            Product p9 = _db.Products.Find(9);
        //            p9.Genders.Add(maleGender);


        //            Product p10 = _db.Products.Find(10);
        //            p10.Genders.Add(maleGender);

        //            _db.SaveChanges();





        //}

    }
}
