using ES.QLBongDa.Players.Dtos;
using System.Collections.Generic;
using System.Collections.Generic;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Players
{
    public class CreateOrEditPlayerViewModel
    {
        public CreateOrEditPlayerDto Player { get; set; }

        public string ClubMACLB { get; set; }

        public string Nationmaqg { get; set; }

        public List<PlayerClubLookupTableDto> PlayerClubList { get; set; }

        public List<PlayerNationLookupTableDto> PlayerNationList { get; set; }

        public bool IsEditMode => Player.Id.HasValue;
    }
}