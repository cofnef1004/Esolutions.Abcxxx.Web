using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Matchs.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Matchs.Exporting
{
    public class MatchsExcelExporter : NpoiExcelExporterBase, IMatchsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MatchsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMatchForViewDto> matchs)
        {
            return CreateExcelPackage(
                "Matchs.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Matchs"));

                    AddHeader(
                        sheet,
                        L("Nam"),
                        L("Vong"),
                        L("Ketqua"),
                        (L("Club")) + L("TENCLB"),
                        (L("Club")) + L("TENCLB"),
                        (L("Stadium")) + L("Tensan")
                        );

                    AddObjects(
                        sheet, matchs,
                        _ => _.Match.Nam,
                        _ => _.Match.Vong,
                        _ => _.Match.Ketqua,
                        _ => _.ClubTENCLB,
                        _ => _.ClubTENCLB2,
                        _ => _.StadiumTensan
                        );

                });
        }
    }
}