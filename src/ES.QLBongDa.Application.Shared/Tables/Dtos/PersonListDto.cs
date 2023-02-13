using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Tables.Dtos
{
    public class GetTablesInput
    {
        public string Filter { get; set; }
    }

    public class PersonListDto : EntityDto
    {
        public string tenclb { get; set; }

        public int nam { get; set; }

        public int vong { get; set; }

        public int sotran { get; set; }

        public int thang { get; set; }

        public int hoa { get; set; }

        public int thua { get; set; }

        public int hieuso { get; set; }
        public int diem { get; set; }
    }
}
