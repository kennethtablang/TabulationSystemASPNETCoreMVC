using System.ComponentModel.DataAnnotations;

namespace TabulationSystem.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        // Navigation properties
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
     }
}