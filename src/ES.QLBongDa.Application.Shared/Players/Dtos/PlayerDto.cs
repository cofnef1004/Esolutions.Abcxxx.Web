using System;
using Abp.Application.Services.Dto;

namespace ES.QLBongDa.Players.Dtos
{
    public class PlayerDto : EntityDto
    {
        public string Hoten { get; set; }

        public string Vitri { get; set; }

        public int soao { get; set; }

        public int? ClubId { get; set; }

        public int? NationId { get; set; }

    }
}