using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Stadiums.Dtos
{
    public class StadiumDto : EntityDto
    {
        public string Masan { get; set; }

        public string Tensan { get; set; }

    }
}