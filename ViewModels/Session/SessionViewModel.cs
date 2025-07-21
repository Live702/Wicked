using Amazon;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.

namespace ViewModels;
/// <summary>
/// The SessionViewModel is the root viewModel for a user session.
/// This class maintains the "state" of the user session, which includes 
/// the data (in this case the PetsViewMode).
/// </summary>
[Factory]
public class SessionViewModel : BaseAppSessionViewModelAuthNotifications, ISessionViewModel
{
    public SessionViewModel(
        [FactoryInject] ILoggerFactory loggerFactory, // singleton
        [FactoryInject] ILzClientConfig clientConfig, // singleton
        [FactoryInject] IInternetConnectivitySvc internetConnectivity, // singleton
        [FactoryInject] ILzHost lzHost, // singleton
        [FactoryInject] ILzMessages messages, // singleton
        [FactoryInject] IAuthProcess authProcess, // transient
        [FactoryInject] IBlurbsViewModelFactory blurbsViewModelFactory,
        [FactoryInject] IUserChatsViewModelFactory chatsViewModelFactory,
        ISessionsViewModel sessionsViewModel
        ) 
        : base(loggerFactory, authProcess, clientConfig, internetConnectivity, messages)  
    {
        try
        {
            var tenantKey = (string?)clientConfig.TenancyConfig["tenantKey"] ?? "";
            TenantName = AppConfig.TenantName;
            authProcess.SetAuthenticator(clientConfig.AuthConfigs?["ConsumerAuth"]!);
            authProcess.SetSignUpAllowed(true);

            var sessionId = Guid.NewGuid().ToString(); 

            WickedAppApi = new WickedAppApi.WickedAppApi(new LzHttpClient(loggerFactory, authProcess.AuthProvider, lzHost, sessionId));

            BlurbsViewModel = blurbsViewModelFactory?.Create(this) 
                ?? throw new ArgumentNullException(nameof(blurbsViewModelFactory));

            UserChatsViewModel = chatsViewModelFactory?.Create(this)
                ?? throw new ArgumentNullException(nameof(chatsViewModelFactory));

        }
        catch (Exception ex)
        {
            Console.WriteLine($"SetAuthenticator failed. {ex.Message}");
            throw new Exception("oops");
        }
    }
    public IWickedAppApi WickedAppApi { get; set; }  

    public string TenantName { get; set; } = string.Empty;
    public BlurbsViewModel BlurbsViewModel { get; set; }

    public UserChatsViewModel UserChatsViewModel { get; set; }

    // Base class calls UnloadAsync () when IsSignedIn changes to false
    public override async Task UnloadAsync()
    {

        await Task.Delay(0);    
    }
}