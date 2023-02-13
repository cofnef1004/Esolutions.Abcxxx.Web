using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Vilages.Dtos
{
    public class CreateOrEditVilageDto : EntityDto<int?>
    {

        [Required]
        public string matinh { get; set; }

        [Required]
        public string tentinh { get; set; }

    }
}