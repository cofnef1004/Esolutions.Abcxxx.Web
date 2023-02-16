using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Rankings.Dtos
{
    public class GetRankingForEditOutput
    {
        public CreateOrEditRankingDto Ranking { get; set; }

        public string ClubTENCLB { get; set; }

    }
}