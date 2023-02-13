using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using ES.QLBongDa.Clubs;
using ES.QLBongDa.Tables.Dtos;
using IdentityServer4.Extensions;
using Stripe;

namespace ES.QLBongDa.Tables
{
    public class TablesAppService : QLBongDaAppServiceBase, ITablesAppService
    {
        private readonly IRepository<Table> _tableRepository;

        public TablesAppService(IRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public ListResultDto<PersonListDto> GetPeople(GetTablesInput input)
        {
            var tables = _tableRepository
                .GetAll()
                .WhereIf(
                    !input.Filter.IsNullOrEmpty(),
                    p => p.tenclb.Contains(input.Filter) ||
                            p.nam.Equals(input.Filter) ||
                            p.vong.Equals(input.Filter)||
                            p.tran.Equals(input.Filter) ||
                            p.thang.Equals(input.Filter) ||
                            p.hoa.Equals(input.Filter) ||
                            p.thua.Equals(input.Filter) ||
                            p.hieuso.Equals(input.Filter) ||
                            p.diem.Equals(input.Filter) 

                );

            return new ListResultDto<PersonListDto>(ObjectMapper.Map<List<PersonListDto>>(tables));
        }
    }
}
