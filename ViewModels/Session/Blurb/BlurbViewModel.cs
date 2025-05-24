using LazyMagic.Client.FactoryGenerator;
using WickedSchema;

namespace ViewModels;
[Factory]
public class BlurbViewModel : LzItemViewModelAuthNotifications<Blurb, BlurbModel>
{
    public BlurbViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        [FactoryInject] IPremisesViewModelFactory premisesViewModelFactory, //transient
        [FactoryInject] IChatsViewModelFactory chatsViewModelFactory,
        ISessionViewModel sessionViewModel,
        ILzParentViewModel parentViewModel,
        Blurb blurb,
        bool? isLoaded = null
        ) : base(loggerFactory, sessionViewModel, blurb, model: null, isLoaded)
    {
        _sessionViewModel = sessionViewModel;
        ParentViewModel = parentViewModel;
        _DTOReadAsync = sessionViewModel.Public.ReadBlurbByIdAsync;
        _DTOCreateAsync = sessionViewModel.Public.CreateBlurbAsync;
        _DTODeleteAsync = sessionViewModel.Public.DeleteBlurbAsync;

        PremisesViewModel = premisesViewModelFactory?.Create(sessionViewModel, this)
            ?? throw new ArgumentNullException(nameof(premisesViewModelFactory));

        ChatsViewModel = chatsViewModelFactory?.Create(sessionViewModel, blurbViewModel: this, premiseViewModel: null)
            ?? throw new ArgumentNullException(nameof(chatsViewModelFactory));
    }
    private ISessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;
    public PremisesViewModel PremisesViewModel { get; set; }
    public ChatsViewModel ChatsViewModel { get; private set; }

}   



