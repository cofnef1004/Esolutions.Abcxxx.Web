using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Clubs.Dtos
{
    public class ClubDto : EntityDto
    {
        public string MACLB { get; set; }

        public string TENCLB { get; set; }

        public int? StadiumId { get; set; }

        public int? VilageId { get; set; }

    }
}