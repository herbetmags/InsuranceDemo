using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Insurance.Data.Entities
{
    [Table("User")]
    [Index(nameof(Username), Name = "IX_User_Username")]
    public partial class User
    {
        public User()
        {
            UserPolicies = new HashSet<UserPolicy>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(50)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        public Guid StatusId { get; set; }
        public Guid RoleId { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey(nameof(StatusId))]
        [InverseProperty("Users")]
        public virtual Status Status { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(UserPolicy.User))]
        public virtual ICollection<UserPolicy> UserPolicies { get; set; }
    }
}
