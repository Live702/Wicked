using LazyMagic.Client.FactoryGenerator;

namespace ViewModels;
[Factory]
public class PremiseViewModel : LzItemViewModelAuthNotifications<Premise, PremiseModel>
{
    public PremiseViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        [FactoryInject] IChatsViewModelFactory chatsViewModelFactory,
        ISessionViewModel sessionViewModel,
        ILzParentViewModel parentViewModel,
        Premise Premise,
        bool? isLoaded = null
        ) : base(loggerFactory, sessionViewModel, Premise, model: null, isLoaded)
    {
        _sessionViewModel = sessionViewModel;
        ParentViewModel = parentViewModel;
        _DTOReadAsync = sessionViewModel.Public.ReadPremiseByIdAsync;
        _DTOCreateAsync = sessionViewModel.Public.CreatePremiseAsync;
        _DTOUpdateAsync = sessionViewModel.Public.UpdatePremiseAsync;

        ChatsViewModel = chatsViewModelFactory?.Create(sessionViewModel, blurbViewModel: null, premiseViewModel: this)
            ?? throw new ArgumentNullException(nameof(chatsViewModelFactory));
    }
    private ISessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;
    public ChatsViewModel ChatsViewModel { get; set; }
}
