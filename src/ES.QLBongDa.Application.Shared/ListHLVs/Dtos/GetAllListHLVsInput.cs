using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.ListHLVs.Dtos
{
    public class GetAllListHLVsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MahlvFilter { get; set; }

        public string MACLBFilter { get; set; }

        public string VAITROFilter { get; set; }

    }
}