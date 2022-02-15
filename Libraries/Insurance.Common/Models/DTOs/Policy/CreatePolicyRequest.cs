using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Insurance.Common.Models.DTOs.Policy
{
    public class CreatePolicyRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [JsonIgnore]
        public bool IsDeleted { get; set; }
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