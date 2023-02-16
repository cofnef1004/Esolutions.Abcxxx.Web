using System.Collections.Generic;
using ES.QLBongDa.Matchs.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Matchs.Exporting
{
    public interface IMatchsExcelExporter
    {
        FileDto ExportToFile(List<GetMatchForViewDto> matchs);
    }
}