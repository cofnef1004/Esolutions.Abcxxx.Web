using System.Threading.Tasks;
using ES.QLBongDa.Sessions.Dto;

namespace ES.QLBongDa.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
