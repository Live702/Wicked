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
    [FactoryInject] IChatViewModelFactory chatViewModelFactory) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        ChatViewModelFactory = chatViewModelFactory;
        _DTOReadListIdAsync = sessionViewModel.Consumer.ListChatsByBlurbIdAsync;
    }
    private ISessionViewModel _sessionViewModel;
    public IChatViewModelFactory? ChatViewModelFactory { get; init; }
    /// <inheritdoc/>
}
