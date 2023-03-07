using ES.QLBongDa.Stadiums.Dtos;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Stadiums
{
    public class CreateOrEditStadiumModalViewModel
    {
        public CreateOrEditStadiumDto Stadium { get; set; }

        public bool IsEditMode => Stadium.Id.HasValue;
    }
}