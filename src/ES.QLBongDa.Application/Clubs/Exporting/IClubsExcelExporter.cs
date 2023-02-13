using System.Collections.Generic;
using ES.QLBongDa.Clubs.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Clubs.Exporting
{
    public interface IClubsExcelExporter
    {
        FileDto ExportToFile(List<GetClubForViewDto> clubs);
    }
}