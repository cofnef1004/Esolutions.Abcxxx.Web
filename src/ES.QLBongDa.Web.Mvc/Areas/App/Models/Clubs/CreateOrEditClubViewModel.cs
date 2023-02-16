using ES.QLBongDa.Clubs.Dtos;
using System.Collections.Generic;
using System.Collections.Generic;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Clubs
{
    public class CreateOrEditClubViewModel
    {
        public CreateOrEditClubDto Club { get; set; }

        public string StadiumTensan { get; set; }

        public string Vilagetentinh { get; set; }

        public List<ClubStadiumLookupTableDto> ClubStadiumList { get; set; }

        public List<ClubVilageLookupTableDto> ClubVilageList { get; set; }

        public bool IsEditMode => Club.Id.HasValue;
    }
}