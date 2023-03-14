using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;

namespace ES.QLBongDa.Rankings.Dtos
{
    public class UpdatePoint : Entity
    {
        public int vong { get; set; }

        public int tran { get; set; }

        public int thang { get; set; }

        public int hoa { get; set; }

        public int thua { get; set; }

        public int diem { get; set; }
    }
}
