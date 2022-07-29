using Microsoft.AspNetCore.Mvc;
using SystemGlobalServices.Services.Currencies;
using SystemGlobalServices.Services.Currencies.Models;

namespace SystemGlobalServices.Core.App.Controllers;

public class CurrenciesController : Controller
{
    private readonly ICurrenciesService _service;

    public CurrenciesController(ICurrenciesService service)
    {
        _service = service;
    }

    [HttpGet("Currencies")]
    public async Task<CurrenciesScrollableCollection> GetCurrencies(
        [FromQuery] int take,
        [FromQuery] int skip,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetCurrenciesAsync(
                new(take, skip), 
                cancellationToken)
            .ConfigureAwait(false);

        return result;
    }

    [HttpGet("Currency/{id}")]
    public async Task<Currency> GetCurrencies(
        [FromRoute] string id,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetCurrencyAsync(
                id,
                cancellationToken)
            .ConfigureAwait(false);

        return result;
    }
}