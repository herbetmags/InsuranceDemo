using System.ComponentModel.DataAnnotations;

namespace Insurance.Common.Models.Bases
{
    public class AuthenticationRequest
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
