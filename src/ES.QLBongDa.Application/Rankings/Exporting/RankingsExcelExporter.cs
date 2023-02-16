using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Rankings.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Rankings.Exporting
{
    public class RankingsExcelExporter : NpoiExcelExporterBase, IRankingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public RankingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetRankingForViewDto> rankings)
        {
            return CreateExcelPackage(
                "Rankings.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Rankings"));

                    AddHeader(
                        sheet,
                        L("nam"),
                        L("vong"),
                        L("tran"),
                        L("thang"),
                        L("hoa"),
                        L("thua"),
                        L("hieuso"),
                        L("diem"),
                        (L("Club")) + L("TENCLB")
                        );

                    AddObjects(
                        sheet, rankings,
                        _ => _.Ranking.nam,
                        _ => _.Ranking.vong,
                        _ => _.Ranking.tran,
                        _ => _.Ranking.thang,
                        _ => _.Ranking.hoa,
                        _ => _.Ranking.thua,
                        _ => _.Ranking.hieuso,
                        _ => _.Ranking.diem,
                        _ => _.ClubTENCLB
                        );

                });
        }
    }
}