using NC.Infrastructure.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NC.Infrastructure.Entities
{
    [Table("RefreshToken")]
    public class RefreshToken : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string TokenHashed { get; set; }

        public DateTime Expires { get; set; }

        public string UserAgent { get; set; }

        [ForeignKey("InsertUserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
