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
    /// <inheritdoc/>
    public override async Task<(bool, string)> ReadAsync(bool forceload = false)
    => await base.ReadAsync(forceload);

    public async Task SeedPremises()
    {
        await ReadAsync(true);
         if( ViewModels.Count == 0)
        {
            var premise = new Premise()
            {
                Id = Guid.NewGuid().ToString(),
                Body = "Premise1"
            };
            var premiseViewModel = PremiseViewModelFactory!.Create(
                _sessionViewModel,
                this,
                premise
                );
            premiseViewModel.State = LzItemViewModelState.New;
            var result = await premiseViewModel.CreateAsync();

            var premise2 = new Premise()
            {
                Id = Guid.NewGuid().ToString(),
                Body = "Premise1"
            };
            var premiseViewModel2 = PremiseViewModelFactory!.Create(
                _sessionViewModel,
                this,
                premise2
                );
            premiseViewModel2.State = LzItemViewModelState.New;
            var result2 = await premiseViewModel2.CreateAsync();

        }

    }

}
