using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Nations.Dtos
{
    public class GetAllNationsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string maqgFilter { get; set; }

        public string tenqgFilter { get; set; }

    }
}