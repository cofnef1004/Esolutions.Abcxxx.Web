using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Nations
{
    [Table("QUOCGIA")]
    public class Nation : Entity
    {

        [Required]
        public virtual string maqg { get; set; }

        [Required]
        public virtual string tenqg { get; set; }

    }
}