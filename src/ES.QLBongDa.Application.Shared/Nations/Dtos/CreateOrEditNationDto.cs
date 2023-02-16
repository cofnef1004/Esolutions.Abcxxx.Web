using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Nations.Dtos
{
    public class CreateOrEditNationDto : EntityDto<int?>
    {

        [Required]
        public string maqg { get; set; }

        [Required]
        public string tenqg { get; set; }

    }
}