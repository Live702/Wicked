namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;
using WickedSchema;

[Factory]
public class ChatsViewModel : LzItemsViewModelAuthNotifications<ChatViewModel, Chat, ChatModel>
{
    public ChatsViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    ISessionViewModel sessionViewModel,
    [FactoryInject] IChatViewModelFactory ChatViewModelFactory) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        ChatViewModelFactory = ChatViewModelFactory;
        //_DTOReadListAsync = sessionViewModel.Consumer.ListChatsAsync;

    }
    private ISessionViewModel _sessionViewModel;
    public IChatViewModelFactory? ChatViewModelFactory { get; init; }
    /// <inheritdoc/>
}
