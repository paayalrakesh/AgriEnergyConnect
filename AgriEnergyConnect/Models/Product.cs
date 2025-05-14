using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        // Link to Farmer (UserId)
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
