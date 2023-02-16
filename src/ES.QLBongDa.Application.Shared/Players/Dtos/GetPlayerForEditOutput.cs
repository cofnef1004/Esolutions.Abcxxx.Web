using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Players.Dtos
{
    public class GetPlayerForEditOutput
    {
        public CreateOrEditPlayerDto Player { get; set; }

        public string ClubMACLB { get; set; }

        public string Nationmaqg { get; set; }

    }
}