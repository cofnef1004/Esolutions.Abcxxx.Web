using ES.QLBongDa.Nations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Managers
{
    [Table("HLV")]
    public class Manager : Entity
    {

        [Required]
        public virtual string Mahlv { get; set; }

        [Required]
        public virtual string Tenhlv { get; set; }

        public virtual int Namsinh { get; set; }

        public virtual int? NationId { get; set; }

        [ForeignKey("NationId")]
        public Nation NationFk { get; set; }

    }
}