using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Common.Models.Bases
{
    public class DeleteRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}