
using System.ComponentModel;

namespace ViewModels;

public interface ISessionViewModel : IBaseAppSessionViewModelAuthNotifications
{

    IWickedAppApi WickedAppApi { get; set; } 
    BlurbsViewModel BlurbsViewModel { get; }
    UserChatsViewModel UserChatsViewModel { get; }
    public string TenantName { get; set; }
}