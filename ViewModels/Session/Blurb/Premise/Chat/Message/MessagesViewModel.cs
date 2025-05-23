namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;

[Factory]
public class MessagesViewModel : LzItemsViewModelAuthNotifications<MessageViewModel, Message, MessageModel>
{
    public MessagesViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    ISessionViewModel sessionViewModel,
    [FactoryInject] IMessageViewModelFactory messageViewModelFactory) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        MessageViewModelFactory = messageViewModelFactory;
        _DTOReadListIdAsync = sessionViewModel.Public.ListMessagesByChatIdAsync;
    }
    private ISessionViewModel _sessionViewModel;
    public IMessageViewModelFactory? MessageViewModelFactory { get; init; }

    public override (MessageViewModel, string) NewViewModel(Message dto)
        => (MessageViewModelFactory!.Create(_sessionViewModel, this, dto), string.Empty);

    public override async Task<(bool, string)> ReadAsync(bool forceload = false)
       => await base.ReadAsync(forceload);

    /// <inheritdoc/>
}
