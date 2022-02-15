using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Insurance.Common.Models.Bases
{
    public class SearchRequest
    {
        [Required]
        [Range(minimum: 1, maximum: 100)]
        public ushort PageSize { get; set; }
        [JsonIgnore]
        public ushort PageIndex { get; set; }
    }
}