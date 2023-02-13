using System.Threading.Tasks;

namespace ES.QLBongDa.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}