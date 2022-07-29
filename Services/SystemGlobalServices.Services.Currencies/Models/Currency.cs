namespace SystemGlobalServices.Services.Currencies.Models;

public record Currency(
    string Id,
    string NumCode,
    string CharCode,
    int Nominal,
    string Name,
    decimal Value,
    decimal Previous);
