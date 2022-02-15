using System;

namespace Insurance.Common.Models.Bases
{
    public class CreateOrUpdateResponse : ResponseBase
    {
        public Guid Id { get; set; }
    }
}