using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.ListHLVs.Dtos
{
    public class ListHLVDto : EntityDto
    {
        public string Mahlv { get; set; }

        public string MACLB { get; set; }

        public string VAITRO { get; set; }

    }
}