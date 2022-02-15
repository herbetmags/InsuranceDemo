using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Insurance.Common.Models.DTOs.User
{
    public class UpdateUserRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
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
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        public Guid StatusId { get; set; }
        [JsonIgnore]
        public Guid CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
        [Required]
        public Guid? ModifiedBy { get; set; }
        [Required]
        public DateTime? ModifiedDate { get; set; }
    }
}
