using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Insurance.Data.Entities
{
    [Table("Status")]
    [Index(nameof(Code), Name = "IX_Status_Code")]
    public partial class Status
    {
        public Status()
        {
            UserPolicies = new HashSet<UserPolicy>();
            Users = new HashSet<User>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(UserPolicy.Status))]
        public virtual ICollection<UserPolicy> UserPolicies { get; set; }
        [InverseProperty(nameof(User.Status))]
        public virtual ICollection<User> Users { get; set; }
    }
}
