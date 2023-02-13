using ES.QLBongDa.Stadiums.Dtos;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Stadiums
{
    public class CreateOrEditStadiumViewModel
    {
        public CreateOrEditStadiumDto Stadium { get; set; }

        public bool IsEditMode => Stadium.Id.HasValue;
    }
}