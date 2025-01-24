using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace TabulationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int ApplicationUserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(50)]
        public required string UserType { get; set; } // e.g., "Admin" or "Judge"

        public bool Status { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        //// Navigation properties
        public int RoleId { get; set; } //foreign key for Role

        public Role? Role { get; set; } //Role associated with the user

        public ICollection<Event> ManagedEvents { get; set; } = new List<Event>(); // For Admins managing events
        public ICollection<Score> AssignedScores { get; set; } = new List<Score>(); // For Judges scoring events
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
