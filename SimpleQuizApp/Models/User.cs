using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleQuizApp.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastLogin { get; set; } = DateTime.UtcNow;
        public string Role { get; set; } = "User"; // Default role is User
        // Navigation property for quizzes created by the user
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
