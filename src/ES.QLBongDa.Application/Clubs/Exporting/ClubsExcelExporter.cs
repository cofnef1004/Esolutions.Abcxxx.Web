using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Clubs.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Clubs.Exporting
{
    public class ClubsExcelExporter : NpoiExcelExporterBase, IClubsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ClubsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetClubForViewDto> clubs)
        {
            return CreateExcelPackage(
                "Clubs.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Clubs"));

                    AddHeader(
                        sheet,
                        L("MACLB"),
                        L("TENCLB"),
                        (L("Stadium")) + L("Tensan"),
                        (L("Vilage")) + L("tentinh")
                        );

                    AddObjects(
                        sheet, clubs,
                        _ => _.Club.MACLB,
                        _ => _.Club.TENCLB,
                        _ => _.StadiumTensan,
                        _ => _.Vilagetentinh
                        );

                });
        }
    }
}