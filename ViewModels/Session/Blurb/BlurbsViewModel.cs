namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;
using WickedSchema;

[Factory]
public class BlurbsViewModel : LzItemsViewModelAuthNotifications<BlurbViewModel, Blurb, BlurbModel>
{
    public BlurbsViewModel(
    [FactoryInject] ILoggerFactory loggerFactory,
    ISessionViewModel sessionViewModel,
    [FactoryInject] IBlurbViewModelFactory blurbViewModelFactory) : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        BlurbViewModelFactory = blurbViewModelFactory;
        _DTOReadListAsync = sessionViewModel.Consumer.ListBlurbsAsync;

    }
    private ISessionViewModel _sessionViewModel;
    public IBlurbViewModelFactory? BlurbViewModelFactory { get; init; }
    /// <inheritdoc/>
}
