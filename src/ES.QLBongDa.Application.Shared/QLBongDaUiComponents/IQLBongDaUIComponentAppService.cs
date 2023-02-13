using System;
using System.Collections.Generic;
using Abp;
using Abp.Application.Services;
using ES.QLBongDa.QLBongDaUiComponents.Dto;

namespace ES.QLBongDa.QLBongDaUiComponents
{
    public interface IQLBongDaUiComponentsAppService: IApplicationService
    {
        DateToStringOutput SendAndGetDate(DateTime? date);

        DateToStringOutput SendAndGetDateTime(DateTime? date);

        DateToStringOutput SendAndGetDateRange(DateTime? startDate, DateTime? endDate);

        List<NameValue<string>> GetCountries(string searchTerm);

        List<NameValue<string>> SendAndGetSelectedCountries(List<NameValue<string>> selectedCountries);

        StringOutput SendAndGetValue(string input);
    }
}