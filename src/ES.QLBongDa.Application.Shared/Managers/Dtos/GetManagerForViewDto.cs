using System.Collections.Generic;
using ES.QLBongDa.CoachClubs.Dtos;

namespace ES.QLBongDa.Managers.Dtos
{
    public class GetManagerForViewDto
    {
        public ManagerDto Manager { get; set; }

        public string Nationtenqg { get; set; }

        public GetCoachClubForViewDto ClubForViewDto { get; set; }
    }
}