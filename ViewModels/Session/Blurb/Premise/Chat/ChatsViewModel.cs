namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;
using WickedSchema;

[Factory]
public class ChatsViewModel : LzItemsViewModelAuthNotifications<ChatViewModel, Chat, ChatModel>
{
    public ChatsViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    [FactoryInject] IChatViewModelFactory chatViewModelFactory,
    ISessionViewModel sessionViewModel,
    BlurbViewModel? blurbViewModel = null,
    PremiseViewModel? premiseViewModel = null
    ) : base(loggerFactory, sessionViewModel)
    {
        if (blurbViewModel == null && premiseViewModel == null)
            throw new ArgumentNullException("BlurbViewModel and PremiseViewModel both null");

        if (blurbViewModel != null && premiseViewModel != null)
            throw new ArgumentException("Only one of BlurbViewModel or PremiseViewModel should be provided.");

        _sessionViewModel = sessionViewModel;
        ChatViewModelFactory = chatViewModelFactory;
        _DTOReadListIdAsync = sessionViewModel.Consumer.ListChatsByBlurbIdAsync;

        BlurbViewModel = blurbViewModel;
        PremiseViewModel = premiseViewModel;
    }
    private ISessionViewModel _sessionViewModel;
    public IChatViewModelFactory? ChatViewModelFactory { get; init; }
    public BlurbViewModel? BlurbViewModel { get; init; }
    public PremiseViewModel? PremiseViewModel { get; init; }
    /// <inheritdoc/>
}
