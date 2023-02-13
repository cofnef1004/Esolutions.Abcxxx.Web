using System.Collections.Generic;
using ES.QLBongDa.Authorization.Delegation;
using ES.QLBongDa.Authorization.Users.Delegation.Dto;

namespace ES.QLBongDa.Web.Areas.App.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }

        public List<UserDelegationDto> UserDelegations { get; set; }

        public string CssClass { get; set; }
    }
}
