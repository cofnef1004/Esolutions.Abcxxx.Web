using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}