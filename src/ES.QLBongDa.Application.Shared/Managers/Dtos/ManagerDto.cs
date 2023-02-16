using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Managers.Dtos
{
    public class ManagerDto : EntityDto
    {
        public string Mahlv { get; set; }

        public string Tenhlv { get; set; }

        public int Namsinh { get; set; }

        public int? NationId { get; set; }

    }
}