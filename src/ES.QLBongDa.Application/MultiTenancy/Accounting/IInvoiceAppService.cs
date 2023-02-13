using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using ES.QLBongDa.MultiTenancy.Accounting.Dto;

namespace ES.QLBongDa.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
