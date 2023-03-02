using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.ListHLVs.Dtos
{
    public class GetListHLVForEditOutput
    {
        public CreateOrEditListHLVDto ListHLV { get; set; }

    }
}