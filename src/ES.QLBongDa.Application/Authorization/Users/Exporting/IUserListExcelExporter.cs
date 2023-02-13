using System.Collections.Generic;
using ES.QLBongDa.Authorization.Users.Dto;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}