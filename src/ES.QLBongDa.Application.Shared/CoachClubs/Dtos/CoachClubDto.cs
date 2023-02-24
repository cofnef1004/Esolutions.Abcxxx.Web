using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.CoachClubs.Dtos
{
    public class CoachClubDto : EntityDto
    {
        public string Mahlv { get; set; }

        public string MACLB { get; set; }

        public string Vaitro { get; set; }

    }
}