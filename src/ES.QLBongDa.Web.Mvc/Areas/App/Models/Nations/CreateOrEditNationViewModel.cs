﻿using ES.QLBongDa.Nations.Dtos;

using Abp.Extensions;

namespace ES.QLBongDa.Web.Areas.App.Models.Nations
{
    public class CreateOrEditNationViewModel
    {
        public CreateOrEditNationDto Nation { get; set; }

        public bool IsEditMode => Nation.Id.HasValue;
    }
}