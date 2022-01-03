using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string userId { get; set; } = "";
        public string role { get; set; } = "none";
    }
}