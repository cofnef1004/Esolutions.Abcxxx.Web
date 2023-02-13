using System.Collections.Generic;
using ES.QLBongDa.Auditing.Dto;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
