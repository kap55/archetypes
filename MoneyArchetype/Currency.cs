using Common;
using System.Reflection.Metadata;

namespace MoneyArchetype;
public record struct Currency
{
    public string Name { get; }
    public string ISOCode { get; }
    public int NumericCode { get; }

    public Currency() => throw new NotSupportedException();

    private Currency(string name, string isoCode, int numericCode)
    {
        Name = name;
        ISOCode = isoCode;
        NumericCode = numericCode;
    }

    public static Currency GetByISOCode(string isoCode)
    {
        if (HasISOCodeError(isoCode))
            throw new NotSupportedException($"currency {isoCode} is not supported");

        return _currenciesByISOCode[isoCode.ToUpper()];
    }

    public static Currency GetByNumericCode(int numericCode)
    {
        if (HasNumericCodeError(numericCode))
            throw new NotSupportedException($"currency {numericCode} is not supported");

        return _currenciesByNumericCode[numericCode];
    }

    public static bool IsISOCodeValid(string isoCode) => isoCode is not null && _currenciesByISOCode.ContainsKey(isoCode.ToUpper());
    public static bool HasISOCodeError(string isoCode) => !IsISOCodeValid(isoCode);
    public static bool IsNumericCodeValid(int numericCode) => _currenciesByNumericCode.ContainsKey(numericCode);
    public static bool HasNumericCodeError(int numericCode) => !IsNumericCodeValid(numericCode);

    private static Dictionary<string, Currency> _currenciesByISOCode = new()
    {
        { PLN.ISOCode, PLN },
        { USD.ISOCode, USD },
        { EUR.ISOCode, EUR }
    };

    private static Dictionary<int, Currency> _currenciesByNumericCode = new()
    {
        { PLN.NumericCode, PLN },
        { USD.NumericCode, USD },
        { EUR.NumericCode, EUR }
    };

    public static Currency PLN => new Currency("Polish zloty", "PLN", 985);
    public static Currency USD => new Currency("United States dollar", "USD", 840);
    public static Currency EUR => new Currency("Euro", "EUR", 978);
}
