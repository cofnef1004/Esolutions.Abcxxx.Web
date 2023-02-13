using System.Collections.Generic;
using Abp.Localization;
using ES.QLBongDa.Install.Dto;

namespace ES.QLBongDa.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
