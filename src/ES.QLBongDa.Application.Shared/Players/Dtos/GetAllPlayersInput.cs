using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Players.Dtos
{
    public class GetAllPlayersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string HotenFilter { get; set; }

        public string VitriFilter { get; set; }

        public int? MaxsoaoFilter { get; set; }
        public int? MinsoaoFilter { get; set; }

        public string ClubMACLBFilter { get; set; }

        public string NationmaqgFilter { get; set; }

    }
}