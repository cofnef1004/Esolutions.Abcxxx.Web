using System.Threading.Tasks;
using Abp.Application.Services;
using ES.QLBongDa.Install.Dto;

namespace ES.QLBongDa.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}