﻿namespace SystemGlobalServices.Integrations.CurrenciesApi.Models;

public class CurrencyModel
{
    public string ID { get; set; }
    public string NumCode { get; set; }
    public string CharCode { get; set; }
    public int Nominal { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public decimal Previous { get; set; }
}