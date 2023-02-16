using ES.QLBongDa.Rankings.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Rankings
{
    public class CreateOrEditRankingViewModel
    {
        public CreateOrEditRankingDto Ranking { get; set; }

        public string ClubTENCLB { get; set; }

        public List<RankingClubLookupTableDto> RankingClubList { get; set; }

        public bool IsEditMode => Ranking.Id.HasValue;
    }
}