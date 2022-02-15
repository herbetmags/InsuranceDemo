using System;

namespace Insurance.Common.Models.DTOs.UserPolicy
{
    public class UserPolicyDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PolicyId { get; set; }
        public Guid StatusId { get; set; }
    }
}
