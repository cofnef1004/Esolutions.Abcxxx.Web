using Xamarin.Forms.Internals;

namespace ES.QLBongDa.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}