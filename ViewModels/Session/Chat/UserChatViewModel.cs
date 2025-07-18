using LazyMagic.Client.FactoryGenerator;

namespace ViewModels;
[Factory]
public class UserChatViewModel : LzItemViewModelAuthNotifications<Chat, ChatModel>
{
    public UserChatViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    ISessionViewModel sessionViewModel,
    ILzParentViewModel parentViewModel,
    Chat chat,
    bool? isLoaded = null
    ) : base(loggerFactory, sessionViewModel, chat, model: null, isLoaded)
    {
        _sessionViewModel = sessionViewModel;
        ParentViewModel = parentViewModel;
        _DTOReadAsync = sessionViewModel.WickedAppApi.ReadChatByIdAsync;
        _DTOCreateAsync = sessionViewModel.WickedAppApi.CreateChatAsync;
    }
    private ISessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;
}