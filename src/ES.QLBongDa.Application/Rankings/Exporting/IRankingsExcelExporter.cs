using System.Collections.Generic;
using ES.QLBongDa.Rankings.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Rankings.Exporting
{
    public interface IRankingsExcelExporter
    {
        FileDto ExportToFile(List<GetRankingForViewDto> rankings);
    }
}