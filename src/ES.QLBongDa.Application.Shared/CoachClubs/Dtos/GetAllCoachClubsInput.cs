using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.CoachClubs.Dtos
{
    public class GetAllCoachClubsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MahlvFilter { get; set; }

        public string MACLBFilter { get; set; }

        public string VaitroFilter { get; set; }

    }
}