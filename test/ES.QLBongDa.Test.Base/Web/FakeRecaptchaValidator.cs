using System.Threading.Tasks;
using ES.QLBongDa.Security.Recaptcha;

namespace ES.QLBongDa.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
