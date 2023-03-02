using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.ListHLVs
{
    [Table("HLV_CLB")]
    public class ListHLV : Entity
    {
        [Required]
        public virtual string Mahlv { get; set; }

        [Required]
        public virtual string MACLB { get; set; }

        [Required]
        public virtual string VAITRO { get; set; }

    }
}