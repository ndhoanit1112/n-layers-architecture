﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NC.Infrastructure.Entities.Base
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? InsertDate { get; set; }

        [MaxLength(255)]
        public string InsertUserId { get; set; }

        public DateTime? UpdateDate { get; set; }

        [MaxLength(255)]
        public string UpdateUserId { get; set; }
    }
}
