using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Insurance.Common.Models.DTOs.Role
{
    public class UpdateRoleRequest
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
