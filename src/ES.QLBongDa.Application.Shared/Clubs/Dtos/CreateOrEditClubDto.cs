using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Clubs.Dtos
{
    public class CreateOrEditClubDto : EntityDto<int?>
    {

        [Required]
        public string MACLB { get; set; }

        [Required]
        public string TENCLB { get; set; }

        public int? StadiumId { get; set; }

        public int? VilageId { get; set; }

    }
}