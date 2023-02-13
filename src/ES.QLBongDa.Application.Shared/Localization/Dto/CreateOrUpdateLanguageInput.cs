using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}