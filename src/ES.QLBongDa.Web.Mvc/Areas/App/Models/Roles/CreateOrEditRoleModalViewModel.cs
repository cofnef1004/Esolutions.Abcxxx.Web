using Abp.AutoMapper;
using ES.QLBongDa.Authorization.Roles.Dto;
using ES.QLBongDa.Web.Areas.App.Models.Common;

namespace ES.QLBongDa.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}