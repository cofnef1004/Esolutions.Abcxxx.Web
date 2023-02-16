using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Managers.Dtos
{
    public class GetAllManagersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string MahlvFilter { get; set; }

        public string TenhlvFilter { get; set; }

        public int? MaxNamsinhFilter { get; set; }
        public int? MinNamsinhFilter { get; set; }

        public string NationtenqgFilter { get; set; }

    }
}