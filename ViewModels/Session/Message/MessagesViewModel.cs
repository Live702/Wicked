namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;

[Factory]
public class MessagesViewModel : LzItemsViewModelAuthNotifications<MessageViewModel, Message, MessageModel>
{
    public MessagesViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    ISessionViewModel sessionViewModel,
    [FactoryInject] IMessageViewModelFactory MessageViewModelFactory) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        MessageViewModelFactory = MessageViewModelFactory;
        _DTOReadListAsync = sessionViewModel.Consumer.ListMessagesAsync;

    }
    private ISessionViewModel _sessionViewModel;
    public IPetViewModelFactory? MessageViewModelFactory { get; init; }
    /// <inheritdoc/>
}
