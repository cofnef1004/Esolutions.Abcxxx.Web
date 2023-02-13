using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Vilages.Dtos
{
    public class VilageDto : EntityDto
    {
        public string matinh { get; set; }

        public string tentinh { get; set; }

    }
}