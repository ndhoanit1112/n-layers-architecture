using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace NC.Infrastructure.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
