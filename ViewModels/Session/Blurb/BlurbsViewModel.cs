namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;
using System.Runtime.CompilerServices;
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
        _DTOReadListAsync = sessionViewModel.Public.ListBlurbsAsync;

    }
    private ISessionViewModel _sessionViewModel;
    public IBlurbViewModelFactory? BlurbViewModelFactory { get; init; }

    public override (BlurbViewModel, string) NewViewModel(Blurb dto)
        => (BlurbViewModelFactory!.Create(_sessionViewModel, this, dto), string.Empty);

    public override async Task<(bool, string)> ReadAsync(bool forceload = false)
        => await base.ReadAsync(forceload);

    /// <inheritdoc/>
}
