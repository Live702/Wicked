namespace ViewModels;
using LazyMagic.Client.FactoryGenerator; // do not put in global using. Causes runtime error.
using LazyMagic.Client.ViewModels;

[Factory]
public class PremisesViewModel : LzItemsViewModelAuthNotifications<PremiseViewModel, Premise, PremiseModel>
{
    public PremisesViewModel(
        [FactoryInject] ILoggerFactory loggerFactory,
        [FactoryInject] IPremiseViewModelFactory premiseViewModelFactory,
        ISessionViewModel sessionViewModel,
        BlurbViewModel blurbViewModel
        )
        : base(loggerFactory, sessionViewModel)
    {
        _sessionViewModel = sessionViewModel;
        PremiseViewModelFactory = premiseViewModelFactory;
        _DTOReadListIdAsync = sessionViewModel.Public.ListPremisesByBlurbIdAsync;

        BlurbViewModel = blurbViewModel;
    }
    private ISessionViewModel _sessionViewModel;
    public IPremiseViewModelFactory? PremiseViewModelFactory { get; init; }
    public BlurbViewModel BlurbViewModel { get; init; }

    public override (PremiseViewModel, string) NewViewModel(Premise dto)
        => (PremiseViewModelFactory!.Create(_sessionViewModel, this, dto), string.Empty);

    public override async Task<(bool, string)> ReadAsync(bool forceload = false)
        => await base.ReadAsync(forceload);


}
