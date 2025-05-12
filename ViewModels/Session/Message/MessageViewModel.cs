using LazyMagic.Client.FactoryGenerator;

namespace ViewModels;
[Factory]
public class MessageViewModel : LzItemViewModelAuthNotifications<Message,MessageModel>
{
    public MessageViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        ISessionViewModel sessionViewModel,
        ILzParentViewModel parentViewModel,
        Message Message,
        bool? isLoaded = null
        ) : base(loggerFactory, sessionViewModel, Message, model: null, isLoaded)
    {
        _sessionViewModel = sessionViewModel;
        ParentViewModel = parentViewModel;
       _DTOReadAsync = sessionViewModel.Consumer.GetMessageByIdAsync;
    }
    private ISessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;
}
