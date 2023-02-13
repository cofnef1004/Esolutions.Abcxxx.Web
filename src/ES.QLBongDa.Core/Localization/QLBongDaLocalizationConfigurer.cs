using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ES.QLBongDa.Localization
{
    public static class QLBongDaLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    QLBongDaConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(QLBongDaLocalizationConfigurer).GetAssembly(),
                        "ES.QLBongDa.Localization.QLBongDa"
                    )
                )
            );
        }
    }
}