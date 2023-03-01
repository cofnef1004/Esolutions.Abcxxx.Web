



using ES.QLBongDa.Rankings.Dtos;

public class ProfileFootballViewModel
{
    public RankingDto Ranking { get; set; }
    public int TotalMatch { get; set; }
    public int MatchDone { get; set; }
    public int MatchAhead { get; set; }
    public string MostGoalsClub { get; set; }
    public string LeastGoalsClub { get; set; }
    public string BestSave { get; set; }
    public string LeastSave { get; set; }

}
