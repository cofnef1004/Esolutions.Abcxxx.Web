using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace ES.QLBongDa.CoachClubs.Dtos
{
    public class GetCoachClubForEditOutput
    {
        public CreateOrEditCoachClubDto CoachClub { get; set; }

    }
}