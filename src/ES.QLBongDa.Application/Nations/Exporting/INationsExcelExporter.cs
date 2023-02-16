using System.Collections.Generic;
using ES.QLBongDa.Nations.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Nations.Exporting
{
    public interface INationsExcelExporter
    {
        FileDto ExportToFile(List<GetNationForViewDto> nations);
    }
}