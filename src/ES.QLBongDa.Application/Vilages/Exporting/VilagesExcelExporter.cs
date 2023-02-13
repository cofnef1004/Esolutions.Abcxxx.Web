using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using ES.QLBongDa.DataExporting.Excel.NPOI;
using ES.QLBongDa.Vilages.Dtos;
using ES.QLBongDa.Dto;
using ES.QLBongDa.Storage;

namespace ES.QLBongDa.Vilages.Exporting
{
    public class VilagesExcelExporter : NpoiExcelExporterBase, IVilagesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public VilagesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetVilageForViewDto> vilages)
        {
            return CreateExcelPackage(
                "Vilages.xlsx",
                excelPackage =>
                {

                    var sheet = excelPackage.CreateSheet(L("Vilages"));

                    AddHeader(
                        sheet,
                        L("matinh"),
                        L("tentinh")
                        );

                    AddObjects(
                        sheet, vilages,
                        _ => _.Vilage.matinh,
                        _ => _.Vilage.tentinh
                        );

                });
        }
    }
}