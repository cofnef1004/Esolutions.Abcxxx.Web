using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.Clubs.Dtos
{
    public class GetClubForEditOutput
    {
        public CreateOrEditClubDto Club { get; set; }

        public string StadiumTensan { get; set; }

        public string Vilagetentinh { get; set; }

    }
}