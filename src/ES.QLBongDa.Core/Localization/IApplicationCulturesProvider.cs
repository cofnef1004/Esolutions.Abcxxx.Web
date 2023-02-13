using System.Globalization;

namespace ES.QLBongDa.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}