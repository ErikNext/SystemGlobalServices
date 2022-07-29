using SystemGlobalServices.Core.Common;
using SystemGlobalServices.Integrations.CurrenciesApi.Contracts;
using SystemGlobalServices.Integrations.CurrenciesApi.Models;

namespace SystemGlobalServices.Integrations.CurrenciesApi.Services;

public class CurrenciesLoaderService : ICurrenciesLoaderService
{
    public async Task<CurrenciesModel> LoadCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        string url = "https://www.cbr-xml-daily.ru/daily_json.js";

        using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url, cancellationToken))
        {
            if (response.IsSuccessStatusCode)
            {
                var responseResult = await response.Content.ReadAsStringAsync(cancellationToken);
                var currenciesModel = System.Text.Json.JsonSerializer.Deserialize<CurrenciesModel>(responseResult);

                return currenciesModel;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}