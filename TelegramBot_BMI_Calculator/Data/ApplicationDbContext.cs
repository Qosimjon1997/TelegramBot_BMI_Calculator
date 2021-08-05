using Microsoft.EntityFrameworkCore;
using TelegramBot_BMI_Calculator.Models;

namespace TelegramBot_BMI_Calculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"User Id=postgres; Password=123456789_Qwert; Server=localhost; Port=5432;Database=BMI_Telegram_Bot;Integrated Security=true;Pooling=true;");
        }
        public DbSet<BMI> BMIs { get; set; }
        public DbSet<myTest> MyTests { get; set; }


    }
}
