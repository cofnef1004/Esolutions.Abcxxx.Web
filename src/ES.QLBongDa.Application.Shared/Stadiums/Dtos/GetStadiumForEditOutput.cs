using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Stadiums.Dtos
{
    public class GetStadiumForEditOutput
    {
        public CreateOrEditStadiumDto Stadium { get; set; }

    }
}