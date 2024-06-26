﻿using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.Configuration.Host.Dto;

namespace ES.QLBongDa.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
