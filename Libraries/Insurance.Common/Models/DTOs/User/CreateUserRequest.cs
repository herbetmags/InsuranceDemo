using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Insurance.Common.Models.DTOs.User
{
    public class CreateUserRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 11)]
        public string Username { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 8)]
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
        [JsonIgnore]
        public Guid StatusId { get; set; }
        [JsonIgnore]
        public Guid RoleId { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [JsonIgnore]
        public Guid? ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; }
    }
}
