using ES.QLBongDa.ListHLVs.Dtos;
using ES.QLBongDa.Managers.Dtos;

namespace ES.QLBongDa.Clubs.Dtos
{
    public class GetClubForViewDto
    {
        public ClubDto Club { get; set; }

        public string StadiumTensan { get; set; }

        public string Vilagetentinh { get; set; }
        public ListHLVDto List { get; set; }

        public ManagerDto coach { get; set; }

    }
}