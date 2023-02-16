using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Managers.Dtos
{
    public class CreateOrEditManagerDto : EntityDto<int?>
    {

        [Required]
        public string Mahlv { get; set; }

        [Required]
        public string Tenhlv { get; set; }

        public int Namsinh { get; set; }

        public int? NationId { get; set; }

    }
}