using LazyMagic.Client.FactoryGenerator;
using WickedSchema;

namespace ViewModels;
[Factory]
public class BlurbViewModel : LzItemViewModelAuthNotifications<Blurb, BlurbModel>
{
    public BlurbViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
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
    }
    private ISessionViewModel _sessionViewModel;
    public override string Id => Data?.Id ?? string.Empty;
    public override long UpdatedAt => Data?.UpdateUtcTick ?? long.MaxValue;
}