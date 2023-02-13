using ES.QLBongDa.Clubs.Dtos;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Clubs
{
    public class CreateOrEditClubViewModel
    {
        public CreateOrEditClubDto Club { get; set; }

        public string StadiumTensan { get; set; }

        public string Vilagetentinh { get; set; }

        public bool IsEditMode => Club.Id.HasValue;
    }
}