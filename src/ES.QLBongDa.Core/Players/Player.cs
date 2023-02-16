using ES.QLBongDa.Clubs;
using ES.QLBongDa.Nations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Players
{
    [Table("CAUTHU")]
    public class Player : Entity
    {

        [Required]
        public virtual string Hoten { get; set; }

        public virtual string Vitri { get; set; }

        public virtual int soao { get; set; }

        public virtual int? ClubId { get; set; }

        [ForeignKey("ClubId")]
        public Club ClubFk { get; set; }

        public virtual int? NationId { get; set; }

        [ForeignKey("NationId")]
        public Nation NationFk { get; set; }

    }
}