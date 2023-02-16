using ES.QLBongDa.Tables.Dtos;
using System.Collections.Generic;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Tables
{
    public class CreateOrEditTableModalViewModel
    {
        public CreateOrEditTableDto Table { get; set; }

        public string ClubTENCLB { get; set; }

        public List<TableClubLookupTableDto> TableClubList { get; set; }

        public bool IsEditMode => Table.Id.HasValue;
    }
}