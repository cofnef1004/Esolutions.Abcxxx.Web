using ES.QLBongDa.Stadiums;
using ES.QLBongDa.Vilages;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Clubs
{
    [Table("Clubs")]
    public class Club : Entity
    {

        [Required]
        public virtual string MACLB { get; set; }

        [Required]
        public virtual string TENCLB { get; set; }

        public virtual int? StadiumId { get; set; }

        [ForeignKey("StadiumId")]
        public Stadium StadiumFk { get; set; }

        public virtual int? VilageId { get; set; }

        [ForeignKey("VilageId")]
        public Vilage VilageFk { get; set; }

    }
}