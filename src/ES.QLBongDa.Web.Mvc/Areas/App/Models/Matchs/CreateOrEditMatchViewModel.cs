using ES.QLBongDa.Matchs.Dtos;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Generic;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Matchs
{
    public class CreateOrEditMatchViewModel
    {
        public CreateOrEditMatchDto Match { get; set; }

        public string ClubTENCLB { get; set; }

        public string ClubTENCLB2 { get; set; }

        public string StadiumTensan { get; set; }

        public List<MatchClubLookupTableDto> MatchClubList { get; set; }

        public List<MatchStadiumLookupTableDto> MatchStadiumList { get; set; }

        public bool IsEditMode => Match.Id.HasValue;
    }
}