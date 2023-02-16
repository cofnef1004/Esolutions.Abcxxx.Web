using System.Collections.Generic;
using ES.QLBongDa.Managers.Dtos;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Managers.Exporting
{
    public interface IManagersExcelExporter
    {
        FileDto ExportToFile(List<GetManagerForViewDto> managers);
    }
}