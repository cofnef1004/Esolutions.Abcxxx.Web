using Abp.Application.Services.Dto;
using System;

namespace ES.QLBongDa.Rankings.Dtos
{
    public class GetAllRankingsForExcelInput
    {
        public string Filter { get; set; }

        public int? MaxnamFilter { get; set; }
        public int? MinnamFilter { get; set; }

        public int? MaxvongFilter { get; set; }
        public int? MinvongFilter { get; set; }

        public int? MaxtranFilter { get; set; }
        public int? MintranFilter { get; set; }

        public int? MaxthangFilter { get; set; }
        public int? MinthangFilter { get; set; }

        public int? MaxhoaFilter { get; set; }
        public int? MinhoaFilter { get; set; }

        public int? MaxthuaFilter { get; set; }
        public int? MinthuaFilter { get; set; }

        public int? MaxhieusoFilter { get; set; }
        public int? MinhieusoFilter { get; set; }

        public int? MaxdiemFilter { get; set; }
        public int? MindiemFilter { get; set; }

        public string ClubTENCLBFilter { get; set; }

    }
}