using System.Threading.Tasks;

namespace ES.QLBongDa.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
