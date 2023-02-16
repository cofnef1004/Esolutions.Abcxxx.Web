using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Matchs.Dtos
{
    public class CreateOrEditMatchDto : EntityDto<int?>
    {

        public int Nam { get; set; }

        public int Vong { get; set; }

        [Required]
        public string Ketqua { get; set; }

        public int? Maclb1 { get; set; }

        public int? Maclb2 { get; set; }

        public int? Masan { get; set; }

    }
}