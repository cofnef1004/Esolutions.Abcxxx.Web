using System.Collections.Generic;
using MvvmHelpers;
using ES.QLBongDa.Models.NavigationMenu;

namespace ES.QLBongDa.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}