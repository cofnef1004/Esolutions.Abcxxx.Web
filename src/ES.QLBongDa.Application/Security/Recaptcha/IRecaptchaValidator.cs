using System.Threading.Tasks;

namespace ES.QLBongDa.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}