using Microsoft.Extensions.Configuration;

namespace ES.QLBongDa.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
