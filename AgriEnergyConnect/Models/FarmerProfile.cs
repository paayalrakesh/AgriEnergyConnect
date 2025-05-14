using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class FarmerProfile
    {
        public int FarmerProfileId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Region { get; set; }

        public string ContactNumber { get; set; }

        // Link to User (Optional if Farmers are Users)
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
