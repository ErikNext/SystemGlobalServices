using LazyCache;
using SystemGlobalServices.Integrations.CurrenciesApi.Contracts;
using SystemGlobalServices.Integrations.CurrenciesApi.Models;

namespace SystemGlobalServices.Integrations.CurrenciesApi.Services;

public class CachedCurrenciesLoaderService : ICurrenciesLoaderService
{
    private readonly ICurrenciesLoaderService _service;
    private readonly IAppCache _memoryCache;

    public CachedCurrenciesLoaderService(
        ICurrenciesLoaderService service, 
        IAppCache memoryCache)
    {
        _memoryCache = memoryCache;
        _service = service;
    }

    public async Task<CurrenciesModel> LoadCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrAddAsync("key",
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return await _service.LoadCurrenciesAsync(cancellationToken);
            });
    }
}