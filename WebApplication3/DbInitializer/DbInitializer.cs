using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;


namespace WebApplication3.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }
            Category category = _db.Categories.FirstOrDefault(i => i.Id == 1);
            if (category == null)
            {
                Category category1 = new Category { Type = "Steel" };
                Category category2 = new Category { Type = "Rubble" };
                Category category3 = new Category { Type = "Leather" };
                Category category4 = new Category { Type = "Fabric" };
                _db.Categories.Add(category1);
                _db.Categories.Add(category2);
                _db.Categories.Add(category3);
                _db.Categories.Add(category4);
                _db.SaveChanges();

                Gender maleGender = new Gender { Name = "Men" };
                Gender femaleGender = new Gender { Name = "Women" };
                _db.Genders.Add(maleGender);
                _db.Genders.Add(femaleGender);
                _db.SaveChanges();

                Product p1 = new Product
                {

                    Type = "Casual",
                    Material = "Steel",
                    Color = "Gray steel, Navy dial",
                    Price = 20,
                    Size = "38.8 × 29.8 × 8.1 mm",
                    Quantity = 4,
                    Description = " 1/100second stopwatch\r\n- Daily alarm\r\n- Hourly time signal\r\n- 12/24-hour format \r\n- Analog: 2 hands (hour, minute)\r\n- Digital: Hour, minute, second, pm, month, date, day\r\n- Approx. battery life: 3 years on SR920W\r\n- Water Resistant",
                     
                    CategoryId = 1,
                };
                p1.Genders.Add(maleGender);


                Product p2 = new Product
                {

                    Type = "Casual",
                    Material = "Steel",
                    Color = "Gray steel, Gold dial",
                    Price = 21,
                    Size = "38.8 × 29.8 × 8.1 mm",
                    Quantity = 10,
                    Description = " 1/100second stopwatch\r\n- Daily alarm\r\n- Hourly time signal\r\n- 12/24-hour format \r\n- Analog: 2 hands (hour, minute)\r\n- Digital: Hour, minute, second, pm, month, date, day\r\n- Approx. battery life: 3 years on SR920W\r\n- Water Resistant",
                     
                    CategoryId = 1
                };
                p2.Genders.Add(maleGender);


                Product p3 = new Product
                {

                    Type = "Casual",
                    Material = "Steel",
                    Color = "Silver steel, Black dial",
                    Price = 18,
                    Size = "38.8 × 29.8 × 8.1 mm",
                    Quantity = 4,
                    Description = " 1/100second stopwatch\r\n- Daily alarm\r\n- Hourly time signal\r\n- 12/24-hour format \r\n- Analog: 2 hands (hour, minute)\r\n- Digital: Hour, minute, second, pm, month, date, day\r\n- Approx. battery life: 3 years on SR920W\r\n- Water Resistant",
                     
                    CategoryId = 1
                };
                p3.Genders.Add(maleGender);


                Product p4 = new Product
                {

                    Type = "Casual",
                    Material = "Steel",
                    Color = "Silver steel, White dial",
                    Price = 18,
                    Size = "38.8 × 29.8 × 8.1 mm",
                    Quantity = 4,
                    Description = " 1/100second stopwatch\r\n- Daily alarm\r\n- Hourly time signal\r\n- 12/24-hour format \r\n- Analog: 2 hands (hour, minute)\r\n- Digital: Hour, minute, second, pm, month, date, day\r\n- Approx. battery life: 3 years on SR920W\r\n- Water Resistant",
                    
                    CategoryId = 1
                };
                p4.Genders.Add(maleGender);


                Product p5 = new Product
                {
                    Type = "Casual",
                    Material = "Steel",
                    Color = "Gold steel, Gold dial",
                    Price = 20,
                    Size = "38.8 × 29.8 × 8.1 mm",
                    Quantity = 4,
                    Description = " 1/100second stopwatch\r\n- Daily alarm\r\n- Hourly time signal\r\n- 12/24-hour format \r\n- Analog: 2 hands (hour, minute)\r\n- Digital: Hour, minute, second, pm, month, date, day\r\n- Approx. battery life: 3 years on SR920W\r\n- Water Resistant",
                     
                    CategoryId = 1
                };
                p5.Genders.Add(maleGender);


                Product p6 = new Product
                {

                    Type = "رسمي",
                    Material = "ربل أسود",
                    Color = "مينا كحلي",
                    Price = 10,
                    Size = "26.5mm x 36.2mm",
                    Quantity = 4,
                    Description = "• تبسيط أسلوبك\r\n•" +
                         " ساعة أنالوج ضد الماء\r\n•" +
                         " خفيفة الوزن بتصميم نحيف\r\n• ألوان ترابية غير لامعة" +
                         " \r\n• توفر لك هذه الساعه العصريه كل من الأناقة والعملي\r\n• " +
                         "عمر بطاريه ٣ سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
                    CategoryId = 2
                };
                p6.Genders.Add(femaleGender);
                p6.Genders.Add(maleGender);


                Product p7 = new Product
                {

                    Type = "رسمي",
                    Material = "ربل أسود",
                    Color = "مينا رمادي",
                    Price = 10,
                    Size = "26.5mm x 36.2mm",
                    Quantity = 6,
                    Description = "• تبسيط أسلوبك\r\n•" +
                         " ساعة أنالوج ضد الماء\r\n•" +
                         " خفيفة الوزن بتصميم نحيف\r\n• ألوان ترابية غير لامعة" +
                         " \r\n• توفر لك هذه الساعه العصريه كل من الأناقة والعملي\r\n• " +
                         "عمر بطاريه ٣ سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
                    CategoryId = 2
                };
                p7.Genders.Add(femaleGender);
                p7.Genders.Add(maleGender);


                Product p8 = new Product
                {
                    Type = "رسمي",
                    Material = "ربل أسود",
                    Color = "مينا أسود",
                    Price = 10,
                    Size = "26.5mm x 36.2mm",
                    Quantity = 6,
                    Description = "• تبسيط أسلوبك\r\n•" +
                         " ساعة أنالوج ضد الماء\r\n•" +
                         " خفيفة الوزن بتصميم نحيف\r\n• ألوان ترابية غير لامعة" +
                         " \r\n• توفر لك هذه الساعه العصريه كل من الأناقة والعملي\r\n• " +
                         "عمر بطاريه ٣ سنوات\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
                    CategoryId = 2
                };
                p8.Genders.Add(femaleGender);
                p8.Genders.Add(maleGender);


                Product p9 = new Product
                {

                    Type = "رسمي",
                    Material = "سير جلد بني فاتح",
                    Color = " مينا ذهبي بعقارب كحلية",
                    Price = 25,
                    Size = "46mm x 41mm",
                    Quantity = 1,
                    Description = "• حزام من الجلد الطبيعي\r\n• " +
                             "زجاج معدني، أرقام وعقارب مطلي بطلاء كحلي غامق جداً\r\n• " +
                             "مقاومة الماء حتى عمق 100 متر\r\n• عرض اليوم والتاريخ\r\n•" +
                             " عمر بطارية 3 سنوات\r\n\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
                    CategoryId = 3

                };
                p9.Genders.Add(maleGender);


                Product p10 = new Product
                {

                    Type = "عملي",
                    Material = "قماش أسود",
                    Color = "مينا زيتي",
                    Price = 15,
                    Size = "45mm x 42mm",
                    Quantity = 3,
                    Description = "• عمر البطارية 10 سنوات\r\n•" +
                             " ساعة توقيت 1/100 ثانية\r\n• خريطة العالم للتوقيت العالمي" +
                             "\r\n• 5 منبهات\r\n• ضوء LED\r\n• مقاومة للماء حتى عمق 100 متر" +
                             "\r\n\r\nأصليه مع جميع الملحقات\r\n\r\nQuality Assurance",
                    CategoryId = 4,
                };
                p10.Genders.Add(maleGender);

                _db.Products.Add(p1);
                _db.Products.Add(p2);
                _db.Products.Add(p3);
                _db.Products.Add(p4);
                _db.Products.Add(p5);
                _db.Products.Add(p6);
                _db.Products.Add(p7);
                _db.Products.Add(p8);
                _db.Products.Add(p9);
                _db.Products.Add(p10);
                _db.SaveChanges();
            }
            if (!_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Customer")).GetAwaiter().GetResult();
                _userManager.CreateAsync(new User
                {
                    UserName = "abeeralh98@gmail.com",
                    Email = "abeeralh98@gmail.com",
                    Name = "Admin",
                    PhoneNumber = "12345",
                    EmailConfirmed = true,
                }, "Admin123*").GetAwaiter().GetResult();
                User user = _db.Users.FirstOrDefault(u => u.Email == "abeeralh98@gmail.com");
                _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
            }
            return;
        }
    }
}
