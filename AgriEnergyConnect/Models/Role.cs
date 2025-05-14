namespace AgriEnergyConnect.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } // "Farmer" or "Employee"

        public ICollection<User> Users { get; set; }
    }
}
