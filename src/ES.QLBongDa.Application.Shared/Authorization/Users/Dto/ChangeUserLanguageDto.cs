using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
