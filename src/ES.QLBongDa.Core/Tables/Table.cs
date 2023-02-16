using ES.QLBongDa.Clubs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Tables
{
    [Table("Tables")]
    public class Table : Entity
    {

        public virtual int? nam { get; set; }

        public virtual int vong { get; set; }

        public virtual int sotran { get; set; }

        public virtual int thang { get; set; }

        public virtual int hoa { get; set; }

        public virtual int thua { get; set; }

        public virtual int hieuso { get; set; }

        public virtual int diem { get; set; }

        public virtual int? maclb { get; set; }

        [ForeignKey("maclb")]
        public Club maclbFk { get; set; }

    }
}