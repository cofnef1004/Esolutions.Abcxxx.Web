using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Managers.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Managers.Exporting
{
    public class ManagersExcelExporter : NpoiExcelExporterBase, IManagersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ManagersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetManagerForViewDto> managers)
        {
            return CreateExcelPackage(
                "Managers.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Managers"));

                    AddHeader(
                        sheet,
                        L("Mahlv"),
                        L("Tenhlv"),
                        L("Namsinh"),
                        (L("Nation")) + L("tenqg")
                        );

                    AddObjects(
                        sheet, managers,
                        _ => _.Manager.Mahlv,
                        _ => _.Manager.Tenhlv,
                        _ => _.Manager.Namsinh,
                        _ => _.Nationtenqg
                        );

                });
        }
    }
}