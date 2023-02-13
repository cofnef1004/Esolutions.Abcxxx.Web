using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Stadiums.Dtos
{
    public class CreateOrEditStadiumDto : EntityDto<int?>
    {

        [Required]
        public string Masan { get; set; }

        [StringLength(StadiumConsts.MaxTensanLength, MinimumLength = StadiumConsts.MinTensanLength)]
        public string Tensan { get; set; }

    }
}