using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Vilages.Dtos
{
    public class GetAllVilagesForExcelInput
    {
        public string Filter { get; set; }

        public string matinhFilter { get; set; }

        public string tentinhFilter { get; set; }

    }
}