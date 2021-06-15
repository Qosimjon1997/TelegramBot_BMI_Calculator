using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot_BMI_Calculator.Models;

namespace TelegramBot_BMI_Calculator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=;Database=TelBot_BMI_Calc;User Id=; Password=;");
        }
        public DbSet<BMI> BMIs { get; set; }
        public DbSet<myTest> MyTests { get; set; }


    }
}
