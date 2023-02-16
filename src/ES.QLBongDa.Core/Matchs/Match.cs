using ES.QLBongDa.Clubs;
using ES.QLBongDa.Clubs;
using ES.QLBongDa.Stadiums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Matchs
{
    [Table("TRANDAU")]
    public class Match : Entity
    {

        public virtual int Nam { get; set; }

        public virtual int Vong { get; set; }

        [Required]
        public virtual string Ketqua { get; set; }

        public virtual int? Maclb1 { get; set; }

        [ForeignKey("Maclb1")]
        public Club Maclb1Fk { get; set; }

        public virtual int? Maclb2 { get; set; }

        [ForeignKey("Maclb2")]
        public Club Maclb2Fk { get; set; }

        public virtual int? Masan { get; set; }

        [ForeignKey("Masan")]
        public Stadium MasanFk { get; set; }

    }
}