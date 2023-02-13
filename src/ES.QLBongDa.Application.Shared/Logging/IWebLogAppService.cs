using Abp.Application.Services;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Logging.Dto;

namespace ES.QLBongDa.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
