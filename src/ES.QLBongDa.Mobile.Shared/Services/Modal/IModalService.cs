using System.Threading.Tasks;
using ES.QLBongDa.Views;
using Xamarin.Forms;

namespace ES.QLBongDa.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
