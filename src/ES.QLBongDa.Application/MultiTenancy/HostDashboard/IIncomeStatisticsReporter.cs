using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ES.QLBongDa.MultiTenancy.HostDashboard.Dto;

namespace ES.QLBongDa.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}