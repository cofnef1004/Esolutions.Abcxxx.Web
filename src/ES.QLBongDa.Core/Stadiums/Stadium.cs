using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Stadiums
{
    [Table("SVD")]
    public class Stadium : Entity
    {

        [Required]
        public virtual string Masan { get; set; }

        [StringLength(StadiumConsts.MaxTensanLength, MinimumLength = StadiumConsts.MinTensanLength)]
        public virtual string Tensan { get; set; }

    }
}