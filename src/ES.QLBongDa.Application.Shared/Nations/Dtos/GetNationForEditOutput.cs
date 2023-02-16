using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Nations.Dtos
{
    public class GetNationForEditOutput
    {
        public CreateOrEditNationDto Nation { get; set; }

    }
}