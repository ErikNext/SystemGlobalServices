namespace SystemGlobalServices.Services.Currencies.Models;

public record CurrenciesScrollableCollection(
    ICollection<Currency> Collection,
    int Take,
    int Skip,
    int Total);