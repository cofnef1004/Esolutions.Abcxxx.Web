using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.ListHLVs.Dtos
{
    public class CreateOrEditListHLVDto : EntityDto<int?>
    {

        public string Mahlv { get; set; }

        [Required]
        public string MACLB { get; set; }

        [Required]
        public string VAITRO { get; set; }

    }
}