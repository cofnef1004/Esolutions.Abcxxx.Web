using Abp.AutoMapper;
using ES.QLBongDa.Authorization.Users;
using ES.QLBongDa.Authorization.Users.Dto;
using ES.QLBongDa.Web.Areas.App.Models.Common;

namespace ES.QLBongDa.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}