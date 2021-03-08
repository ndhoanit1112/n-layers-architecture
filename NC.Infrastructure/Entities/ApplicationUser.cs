using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NC.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
