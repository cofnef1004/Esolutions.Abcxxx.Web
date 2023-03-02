using ES.QLBongDa.ListHLVs.Dtos;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.ListHLVs
{
    public class CreateOrEditListHLVModalViewModel
    {
        public CreateOrEditListHLVDto ListHLV { get; set; }

        public bool IsEditMode => ListHLV.Id.HasValue;
    }
}