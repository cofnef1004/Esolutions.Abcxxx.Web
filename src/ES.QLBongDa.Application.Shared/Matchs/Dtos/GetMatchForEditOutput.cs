using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Matchs.Dtos
{
    public class GetMatchForEditOutput
    {
        public CreateOrEditMatchDto Match { get; set; }

        public string ClubTENCLB { get; set; }

        public string ClubTENCLB2 { get; set; }

        public string StadiumTensan { get; set; }

    }
}