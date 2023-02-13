using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}