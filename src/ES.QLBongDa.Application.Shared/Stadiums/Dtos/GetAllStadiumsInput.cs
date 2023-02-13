using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Stadiums.Dtos
{
    public class GetAllStadiumsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MasanFilter { get; set; }

        public string TensanFilter { get; set; }

    }
}