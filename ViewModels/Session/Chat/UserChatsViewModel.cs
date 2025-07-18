namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.

/// <summary>
/// UserChatsViewModel is similar to UserChatsViewModel, but it doesn't require  Blurb and Premise 
/// parent objects. 
/// </summary>
[Factory]
public class UserChatsViewModel : LzItemsViewModelAuthNotifications<UserChatViewModel, Chat, ChatModel>
{
    public UserChatsViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    [FactoryInject] IUserChatViewModelFactory chatViewModelFactory,
    ISessionViewModel sessionViewModel
    ) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        UserChatViewModelFactory = chatViewModelFactory;

        // The UserId is drawn from the CallerInfo on the server side, so we don't need to pass it in.
        _DTOReadListAsync = sessionViewModel.WickedAppApi.ListChatsByUserIdAsync;

    }
    private ISessionViewModel _sessionViewModel;
    public IUserChatViewModelFactory? UserChatViewModelFactory { get; init; }
    public override (UserChatViewModel, string) NewViewModel(Chat dto)
        => (UserChatViewModelFactory!.Create(_sessionViewModel, this, dto), string.Empty);
    public override async Task<(bool, string)> ReadAsync(bool forceload = false)
        => await base.ReadAsync(forceload);
}
