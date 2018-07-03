using System.ComponentModel.DataAnnotations;

namespace PingPong.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public SkillLevel SkillLevel { get; set; }
        public int? Age { get; set; }
    }
}