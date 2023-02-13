using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Stadiums.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Stadiums.Exporting
{
    public class StadiumsExcelExporter : NpoiExcelExporterBase, IStadiumsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public StadiumsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetStadiumForViewDto> stadiums)
        {
            return CreateExcelPackage(
                "Stadiums.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Stadiums"));

                    AddHeader(
                        sheet,
                        L("Masan"),
                        L("Tensan")
                        );

                    AddObjects(
                        sheet, stadiums,
                        _ => _.Stadium.Masan,
                        _ => _.Stadium.Tensan
                        );

                });
        }
    }
}