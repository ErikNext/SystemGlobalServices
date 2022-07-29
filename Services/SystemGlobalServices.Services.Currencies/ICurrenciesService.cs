using SystemGlobalServices.Services.Currencies.Commands;
using SystemGlobalServices.Services.Currencies.Models;

namespace SystemGlobalServices.Services.Currencies;

public interface ICurrenciesService
{
    Task<CurrenciesScrollableCollection> GetCurrenciesAsync(
        SearchCurrenciesCommand command,
        CancellationToken cancellationToken);

    Task<Currency> GetCurrencyAsync(
        string id,
        CancellationToken cancellationToken);
}