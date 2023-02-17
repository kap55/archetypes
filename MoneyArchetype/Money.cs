using Common;

namespace MoneyArchetype;
public record struct Money
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    public Money() => throw new NotSupportedException();

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = Currency.GetByISOCode(currency);
    }

    private Money(decimal amount, int currency)
    {
        Amount = amount;
        Currency = Currency.GetByNumericCode(currency);
    }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency)
    {
        IsCurrencyValid(currency).ThrowIfInvalid(nameof(currency));

        return new Money(amount, currency);
    }

    public static Money Create(decimal amount, int currency)
    {
        IsCurrencyValid(currency).ThrowIfInvalid(nameof(currency));

        return new Money(amount, currency);
    }

    public static bool IsCurrencyValid(string currency) => Currency.IsISOCodeValid(currency);
    public static bool IsCurrencyValid(int currency) => Currency.IsNumericCodeValid(currency);
    public static bool HasCurrencyError(string currency) => !IsCurrencyValid(currency);
    public static bool HasCurrencyError(int currency) => !IsCurrencyValid(currency);

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new FormatException($"left currency {left.Currency.ISOCode} does not equal right currency {right.Currency.ISOCode}");

        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new FormatException($"left currency {left.Currency.ISOCode} does not equal right currency {right.Currency.ISOCode}");

        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money left, int factor)
    {
        return new Money(left.Amount * factor, left.Currency);
    }

    public static Money operator /(Money dividend, int divider) 
    {
        if (divider == 0)
            throw new DivideByZeroException();

        return new Money(dividend.Amount / divider, dividend.Currency);
    }

    public static bool operator >(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new FormatException($"left currency {left.Currency.ISOCode} does not equal right currency {right.Currency.ISOCode}");

        return left.Amount > right.Amount;
    }

    public static bool operator <(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new FormatException($"left currency  {left.Currency.ISOCode}  does not equal right currency {right.Currency.ISOCode}");

        return left.Amount < right.Amount;
    }

    public static bool operator >=(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new FormatException($"left currency  {left.Currency.ISOCode}  does not equal right currency {right.Currency.ISOCode}");

        return left.Amount >= right.Amount;
    }

    public static bool operator <=(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new FormatException($"left currency  {left.Currency.ISOCode}  does not equal right currency {right.Currency.ISOCode}");

        return left.Amount <= right.Amount;
    }
}
