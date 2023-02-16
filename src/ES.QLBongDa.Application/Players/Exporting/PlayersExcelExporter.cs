using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Players.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Players.Exporting
{
    public class PlayersExcelExporter : NpoiExcelExporterBase, IPlayersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public PlayersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetPlayerForViewDto> players)
        {
            return CreateExcelPackage(
                "Players.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Players"));

                    AddHeader(
                        sheet,
                        L("Hoten"),
                        L("Vitri"),
                        L("soao"),
                        (L("Club")) + L("MACLB"),
                        (L("Nation")) + L("maqg")
                        );

                    AddObjects(
                        sheet, players,
                        _ => _.Player.Hoten,
                        _ => _.Player.Vitri,
                        _ => _.Player.soao,
                        _ => _.ClubMACLB,
                        _ => _.Nationmaqg
                        );

                });
        }
    }
}