using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}