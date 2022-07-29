using SystemGlobalServices.Integrations.CurrenciesApi.Contracts;
using SystemGlobalServices.Integrations.CurrenciesApi.Models;
using SystemGlobalServices.Services.Currencies.Commands;
using SystemGlobalServices.Services.Currencies.Models;

namespace SystemGlobalServices.Services.Currencies.Services;

public class CurrenciesService : ICurrenciesService
{
    private readonly ICurrenciesLoaderService _currenciesLoaderService;

    public CurrenciesService(ICurrenciesLoaderService currenciesLoaderService)
    {
        _currenciesLoaderService = currenciesLoaderService;
    }

    public async Task<CurrenciesScrollableCollection> GetCurrenciesAsync(
        SearchCurrenciesCommand command,
        CancellationToken cancellationToken = default)
    {
        var currenciesModel = await _currenciesLoaderService
            .LoadCurrenciesAsync(cancellationToken)
            .ConfigureAwait(false);

        var currencies = currenciesModel?.Currencies.Values
            .Skip(command.Skip)
            .Take(command.Take)
            .Select(MapToDto)
            .ToList();

        return new(
            currencies,
            command.Take,
            command.Skip,
            currenciesModel.Currencies.Count);
    }

    public async Task<Currency> GetCurrencyAsync(
        string id,
        CancellationToken cancellationToken)
    {
        var currenciesModel = await _currenciesLoaderService
            .LoadCurrenciesAsync(cancellationToken)
            .ConfigureAwait(false);

        var currency = currenciesModel.Currencies.FirstOrDefault(x => x.Value.ID == id).Value;

        if (currency == null)
        {
            throw new InvalidOperationException($"The currency {id} is not found");
        }

        return MapToDto(currency);
    }

    private static Currency MapToDto(CurrencyModel model)
    {
        return new Currency(
            model.ID,
            model.NumCode,
            model.CharCode,
            model.Nominal,
            model.Name,
            model.Value,
            model.Previous);
    }
}