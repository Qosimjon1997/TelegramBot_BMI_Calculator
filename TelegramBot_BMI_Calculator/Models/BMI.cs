using System.ComponentModel.DataAnnotations;

namespace TelegramBot_BMI_Calculator.Models
{
    public class BMI
    {
        [Key]
        public int Id { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }


    }
}
