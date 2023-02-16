using ES.QLBongDa.Managers.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Managers
{
    public class CreateOrEditManagerViewModel
    {
        public CreateOrEditManagerDto Manager { get; set; }

        public string Nationtenqg { get; set; }

        public List<ManagerNationLookupTableDto> ManagerNationList { get; set; }

        public bool IsEditMode => Manager.Id.HasValue;
    }
}