using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Matchs.Dtos
{
    public class GetAllMatchsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public int? MaxNamFilter { get; set; }
        public int? MinNamFilter { get; set; }

        public int? MaxVongFilter { get; set; }
        public int? MinVongFilter { get; set; }

        public string KetquaFilter { get; set; }

        public string ClubTENCLBFilter { get; set; }

        public string ClubTENCLB2Filter { get; set; }

        public string StadiumTensanFilter { get; set; }

    }
}