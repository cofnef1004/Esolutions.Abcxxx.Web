using ES.QLBongDa.Vilages.Dtos;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Vilages
{
    public class CreateOrEditVilageModalViewModel
    {
        public CreateOrEditVilageDto Vilage { get; set; }

        public bool IsEditMode => Vilage.Id.HasValue;
    }
}