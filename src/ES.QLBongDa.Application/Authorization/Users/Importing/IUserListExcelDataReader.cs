using System.Collections.Generic;
using ES.QLBongDa.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace ES.QLBongDa.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
