using System.Collections.Generic;
using ES.QLBongDa.Players.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Players.Exporting
{
    public interface IPlayersExcelExporter
    {
        FileDto ExportToFile(List<GetPlayerForViewDto> players);
    }
}