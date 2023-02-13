using System.Collections.Generic;
using ES.QLBongDa.Stadiums.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Stadiums.Exporting
{
    public interface IStadiumsExcelExporter
    {
        FileDto ExportToFile(List<GetStadiumForViewDto> stadiums);
    }
}