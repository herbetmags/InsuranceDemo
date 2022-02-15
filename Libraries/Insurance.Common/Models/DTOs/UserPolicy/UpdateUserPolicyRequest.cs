using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Common.Models.DTOs.UserPolicy
{
    public class UpdateUserPolicyRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid PolicyId { get; set; }
        [Required]
        public Guid StatusId { get; set; }
    }
}
