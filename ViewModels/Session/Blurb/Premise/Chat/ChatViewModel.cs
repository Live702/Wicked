using LazyMagic.Client.FactoryGenerator;
using WickedSchema;

namespace ViewModels;
[Factory]
public class ChatViewModel : LzItemViewModelAuthNotifications<Chat, ChatModel>
{
    public ChatViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    ISessionViewModel sessionViewModel,
    ILzParentViewModel parentViewModel,
    Chat chat,
    bool? isLoaded = null
    ) : base(loggerFactory, sessionViewModel, chat, model: null, isLoaded)
    {
        _sessionViewModel = sessionViewModel;
        ParentViewModel = parentViewModel;
        _DTOReadAsync = sessionViewModel.Public.ReadChatByIdAsync;
        _DTOCreateAsync = sessionViewModel.Public.CreateChatAsync;
    }
    private ISessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;
}