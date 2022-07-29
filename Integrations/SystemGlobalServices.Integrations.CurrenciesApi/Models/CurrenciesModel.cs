using System.Text.Json.Serialization;

namespace SystemGlobalServices.Integrations.CurrenciesApi.Models;

public class CurrenciesModel
{
    [JsonPropertyName("Valute")]
    public Dictionary<string, CurrencyModel> Currencies { get; set; } = new();
}