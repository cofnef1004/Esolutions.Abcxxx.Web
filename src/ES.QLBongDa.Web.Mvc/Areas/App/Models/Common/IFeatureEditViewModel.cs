using System.Collections.Generic;
using Abp.Application.Services.Dto;
using ES.QLBongDa.Editions.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}