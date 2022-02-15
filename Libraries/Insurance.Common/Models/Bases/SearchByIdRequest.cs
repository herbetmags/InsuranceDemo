using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Common.Models.Bases
{
    public class SearchByIdRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}