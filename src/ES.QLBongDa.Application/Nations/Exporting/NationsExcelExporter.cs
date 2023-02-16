using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Nations.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Nations.Exporting
{
    public class NationsExcelExporter : NpoiExcelExporterBase, INationsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public NationsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetNationForViewDto> nations)
        {
            return CreateExcelPackage(
                "Nations.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Nations"));

                    AddHeader(
                        sheet,
                        L("maqg"),
                        L("tenqg")
                        );

                    AddObjects(
                        sheet, nations,
                        _ => _.Nation.maqg,
                        _ => _.Nation.tenqg
                        );

                });
        }
    }
}