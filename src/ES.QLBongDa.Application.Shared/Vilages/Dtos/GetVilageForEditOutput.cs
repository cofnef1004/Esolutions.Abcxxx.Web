using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Vilages.Dtos
{
    public class GetVilageForEditOutput
    {
        public CreateOrEditVilageDto Vilage { get; set; }

    }
}