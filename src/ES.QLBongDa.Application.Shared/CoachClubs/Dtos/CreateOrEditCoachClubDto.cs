using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.CoachClubs.Dtos
{
    public class CreateOrEditCoachClubDto : EntityDto<int?>
    {

        public string Mahlv { get; set; }

        [Required]
        public string MACLB { get; set; }

        [Required]
        public string Vaitro { get; set; }

    }
}