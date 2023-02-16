using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Tables.Dtos
{
    public class CreateOrEditTableDto : EntityDto<int?>
    {

        public int? nam { get; set; }

        public int vong { get; set; }

        public int sotran { get; set; }

        public int thang { get; set; }

        public int hoa { get; set; }

        public int thua { get; set; }

        public int hieuso { get; set; }

        public int diem { get; set; }

        public int? maclb { get; set; }

    }
}