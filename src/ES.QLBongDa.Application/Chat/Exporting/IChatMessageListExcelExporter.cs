using System.Collections.Generic;
using Abp;
using ES.QLBongDa.Chat.Dto;
using ES.QLBongDa.Dto;

namespace ES.QLBongDa.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
