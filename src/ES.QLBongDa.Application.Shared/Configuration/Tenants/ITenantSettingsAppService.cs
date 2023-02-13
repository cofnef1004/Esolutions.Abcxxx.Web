﻿using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.Configuration.Tenants.Dto;

namespace ES.QLBongDa.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
