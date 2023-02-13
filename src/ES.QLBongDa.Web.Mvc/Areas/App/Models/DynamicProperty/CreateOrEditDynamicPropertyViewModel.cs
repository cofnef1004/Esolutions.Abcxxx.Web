using System.Collections.Generic;
using ES.QLBongDa.DynamicEntityProperties.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
