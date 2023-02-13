using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
