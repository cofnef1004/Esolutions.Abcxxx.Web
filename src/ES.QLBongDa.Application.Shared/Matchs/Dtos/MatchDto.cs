using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Matchs.Dtos
{
    public class MatchDto : EntityDto
    {
        public int Nam { get; set; }

        public int Vong { get; set; }

        public string Ketqua { get; set; }

        public int? Maclb1 { get; set; }

        public int? Maclb2 { get; set; }

        public int? Masan { get; set; }

    }
}