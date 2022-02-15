using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Insurance.Common.Models.DTOs.UserPolicy
{
    public class CreateUserPolicyRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid PolicyId { get; set; }
        [Required]
        public Guid StatusId { get; set; }
    }
}
