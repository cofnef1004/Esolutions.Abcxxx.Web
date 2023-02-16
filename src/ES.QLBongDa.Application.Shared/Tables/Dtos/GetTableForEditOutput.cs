using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Tables.Dtos
{
    public class GetTableForEditOutput
    {
        public CreateOrEditTableDto Table { get; set; }

        public string ClubTENCLB { get; set; }

    }
}