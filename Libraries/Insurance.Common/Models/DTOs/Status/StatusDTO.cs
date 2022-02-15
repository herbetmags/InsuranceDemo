using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Common.Models.DTOs.Status
{
    public class StatusDTO
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}