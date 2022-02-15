using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Insurance.Data.Entities
{
    [Table("Policy")]
    [Index(nameof(Name), Name = "IX_Policy_Name")]
    public partial class Policy
    {
        public Policy()
        {
            UserPolicies = new HashSet<UserPolicy>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [InverseProperty(nameof(UserPolicy.Policy))]
        public virtual ICollection<UserPolicy> UserPolicies { get; set; }
    }
}
