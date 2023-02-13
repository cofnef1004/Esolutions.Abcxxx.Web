using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Vilages
{
    [Table("Vilages")]
    public class Vilage : Entity
    {

        [Required]
        public virtual string matinh { get; set; }

        [Required]
        public virtual string tentinh { get; set; }

    }
}