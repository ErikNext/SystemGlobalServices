namespace SystemGlobalServices.Services.Currencies.Commands;

public record SearchCurrenciesCommand(
    int Take = 10, 
    int Skip = 0);