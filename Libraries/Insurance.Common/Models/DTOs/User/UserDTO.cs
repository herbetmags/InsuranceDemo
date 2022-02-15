using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Common.Models.DTOs.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public Guid StatusId { get; set; }
        public Guid RoleId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [StringLength(20)]
        public string RoleName { get; set; }
        [StringLength(20)]
        public string StatusCode { get; set; }
    }
}
