
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Tables
{
    [Table("Tables")]
    public class Table:Entity
    {
        [Required]
        public virtual string tenclb { get; set; }

        [Required]
        public virtual int nam { get; set; }

        [Required]
        public virtual int vong { get; set; }
        [Required]
        public virtual int tran { get; set; }
        [Required]
        public virtual int thang { get; set; }
        [Required]
        public virtual int hoa { get; set; }
        [Required]
        public virtual int thua { get; set; }
        [Required]
        public virtual int hieuso { get; set; }
        [Required]
        public virtual int diem { get; set; }
    }
}
