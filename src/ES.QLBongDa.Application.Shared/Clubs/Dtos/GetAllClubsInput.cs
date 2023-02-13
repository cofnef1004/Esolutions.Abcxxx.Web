using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Clubs.Dtos
{
    public class GetAllClubsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MACLBFilter { get; set; }

        public string TENCLBFilter { get; set; }

        public string StadiumTensanFilter { get; set; }

        public string VilagetentinhFilter { get; set; }

    }
}