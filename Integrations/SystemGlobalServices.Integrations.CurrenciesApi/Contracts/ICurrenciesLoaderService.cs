using SystemGlobalServices.Integrations.CurrenciesApi.Models;

namespace SystemGlobalServices.Integrations.CurrenciesApi.Contracts;

public interface ICurrenciesLoaderService
{
    Task<CurrenciesModel> LoadCurrenciesAsync(CancellationToken cancellationToken = default);
}