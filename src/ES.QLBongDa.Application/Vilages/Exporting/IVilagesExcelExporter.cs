using System.Collections.Generic;
using ES.QLBongDa.Vilages.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Vilages.Exporting
{
    public interface IVilagesExcelExporter
    {
        FileDto ExportToFile(List<GetVilageForViewDto> vilages);
    }
}