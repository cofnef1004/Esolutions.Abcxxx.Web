using System.Collections.Generic;
using ES.QLBongDa.Authorization.Users.Importing.Dto;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
