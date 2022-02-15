using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Insurance.Data.Entities
{
    [Table("UserPolicy")]
    public partial class UserPolicy
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PolicyId { get; set; }
        public Guid StatusId { get; set; }

        [ForeignKey(nameof(PolicyId))]
        [InverseProperty("UserPolicies")]
        public virtual Policy Policy { get; set; }
        [ForeignKey(nameof(StatusId))]
        [InverseProperty("UserPolicies")]
        public virtual Status Status { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserPolicies")]
        public virtual User User { get; set; }
    }
}
