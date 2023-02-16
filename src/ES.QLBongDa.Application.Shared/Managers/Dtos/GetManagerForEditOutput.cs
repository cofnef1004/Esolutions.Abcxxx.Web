using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Managers.Dtos
{
    public class GetManagerForEditOutput
    {
        public CreateOrEditManagerDto Manager { get; set; }

        public string Nationtenqg { get; set; }

    }
}