using Xunit;

namespace MoneyArchetype;
public class MoneyUnitTests
{
    [Theory]
    [InlineData(10.12, "pln")]
    [InlineData(10.12, "Pln")]
    [InlineData(10.12, "PLN")]
    [InlineData(-10.12, "PLN")]
    [InlineData(0, "PLN")]
    public void Create_Returns_Money_By_Currency_ISOCode(decimal amount, string currency)
    {
        var money = Money.Create(amount, currency);

        Assert.Equal(amount, money.Amount);
        Assert.Equal(Currency.PLN, money.Currency);
    }

    [Theory]
    [InlineData(10.12, 840)]
    [InlineData(-10.12, 840)]
    [InlineData(0, 840)]
    public void Create_Returns_Money_By_Currency_NumericCode(decimal amount, int currency)
    {
        var money = Money.Create(amount, currency);

        Assert.Equal(amount, money.Amount);
        Assert.Equal(Currency.USD, money.Currency);
    }

    [Fact]
    public void Default_Constructor_Throws_NotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => { new Money(); });
    }

    [Theory]
    [InlineData("PLNN")]
    [InlineData("aaa")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_Throws_ArgumentException_Because_Wrong_Currency_ISOCode(string currency)
    {
        Assert.Throws<ArgumentException>(() => { var money = Money.Create(10M, currency); });
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(999)]
    public void Create_Throws_ArgumentException_Because_Wrong_Currency_NumericCode(int currency)
    {
        Assert.Throws<ArgumentException>(() => { var money = Money.Create(10M, currency); });
    }

    [Fact]
    public void Compare_Two_Moneys_Returns_True()
    {
        var left = Money.Create(10.12M, "pln");
        var right = Money.Create(10.12M, "PLN");

        var result1 = left.Equals(right);
        var result2 = left == right;

        Assert.True(result1);
        Assert.True(result2);
    }

    [Fact]
    public void Compare_Two_Moneys_Returns_False()
    {
        var left = Money.Create(10.12M, "usd");
        var right = Money.Create(11.13M, "Usd");

        var result1 = left.Equals(right);
        var result2 = left == right;

        Assert.False(result1);
        Assert.False(result2);
    }

    [Fact]
    public void Greater_Than_Operator_Returns_True()
    {
        var left = Money.Create(11.12M, "pln");
        var right = Money.Create(10.13M, "PLN");

        var result = left > right;

        Assert.True(result);
    }

    [Theory]
    [InlineData(10.12, 10.12)]
    [InlineData(10.13, 11.13)]
    public void Greater_Than_Operator_Returns_False(decimal amountLeft, decimal amountRight)
    {
        var left = Money.Create(amountLeft, "pln");
        var right = Money.Create(amountRight, "PLN");

        var result = left > right;

        Assert.False(result);
    }

    [Fact]
    public void Greater_Than_Operator_Throws_FormatException_Because_Currencies_Are_Not_The_Same()
    {
        var left = Money.Create(11.12M, "PLN");
        var right = Money.Create(10.13M, "USD");

        Assert.Throws<FormatException>(() => { var result = left > right; });
    }

    [Theory]
    [InlineData(10.12, 10.12)]
    [InlineData(11.12, 10.13)]
    public void Greater_Than_Or_Equal_Operator_Returns_True(decimal amountLeft, decimal amountRight)
    {
        var left = Money.Create(amountLeft, "pln");
        var right = Money.Create(amountRight, "PLN");

        var result = left >= right;

        Assert.True(result);
    }

    [Fact]
    public void Greater_Than_Or_Equal_Operator_Returns_False()
    {
        var left = Money.Create(10.12M, "pln");
        var right = Money.Create(11.13M, "PLN");

        var result = left >= right;

        Assert.False(result);
    }

    [Theory]
    [InlineData(10.12, 10.12)]
    [InlineData(11.12, 10.12)]
    public void Greater_Than_Or_Equal_Operator_Throws_FormatException_Because_Currencies_Are_Not_The_Same(decimal amountLeft, decimal amountRight)
    {
        var left = Money.Create(amountLeft, "PLN");
        var right = Money.Create(amountRight, "USD");

        Assert.Throws<FormatException>(() => { var result = left >= right; });
    }

    [Fact]
    public void Less_Than_Operator_Returns_True()
    {
        var left = Money.Create(10.12M, "pln");
        var right = Money.Create(11.13M, "PLN");

        var result = left < right;

        Assert.True(result);
    }

    [Theory]
    [InlineData(10.12, 10.12)]
    [InlineData(11.13, 10.13)]
    public void Less_Than_Operator_Returns_False(decimal amountLeft, decimal amountRight)
    {
        var left = Money.Create(amountLeft, "pln");
        var right = Money.Create(amountRight, "PLN");

        var result = left < right;

        Assert.False(result);
    }

    [Fact]
    public void Less_Than_Operator_Throws_FormatException_Because_Currencies_Are_Not_The_Same()
    {
        var left = Money.Create(10.12M, "PLN");
        var right = Money.Create(11.12M, "USD");

        Assert.Throws<FormatException>(() => { var result = left < right; });
    }

    [Theory]
    [InlineData(10.12, 10.12)]
    [InlineData(10.12, 11.13)]
    public void Less_Than_Or_Equal_Operator_Returns_True(decimal amountLeft, decimal amountRight)
    {
        var left = Money.Create(amountLeft, "pln");
        var right = Money.Create(amountRight, "PLN");

        var result = left <= right;

        Assert.True(result);
    }

    [Fact]
    public void Less_Than_Or_Equal_Operator_Returns_False()
    {
        var left = Money.Create(11.12M, "pln");
        var right = Money.Create(10.13M, "PLN");

        var result = left <= right;

        Assert.False(result);
    }

    [Theory]
    [InlineData(10.12, 10.12)]
    [InlineData(10.12, 11.12)]
    public void Less_Than_Or_Equal_Operator_Throws_FormatException_Because_Currencies_Are_Not_The_Same(decimal amountLeft, decimal amountRight)
    {
        var left = Money.Create(amountLeft, "PLN");
        var right = Money.Create(amountRight, "USD");

        Assert.Throws<FormatException>(() => { var result = left <= right; });
    }

    [Fact]
    public void Plus_Operator_Adds_Two_Moneys()
    {
        var left = Money.Create(10.12M, "PLN");
        var right = Money.Create(20.13M, "pln");

        var result = left + right;

        var expected = Money.Create(30.25M, "PLN");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Plus_Operator_Throws_FormatException_Because_Currencies_Are_Not_The_Same()
    {
        var left = Money.Create(10.12M, "PLN");
        var right = Money.Create(20.13M, "USD");

        Assert.Throws<FormatException>(() => { var result = left + right; });
    }

    [Fact]
    public void Minus_Operator_Subtracts_Two_Moneys()
    {
        var left = Money.Create(20.13M, "PLN");
        var right = Money.Create(10.12M, "pln");

        var result = left - right;

        var expected = Money.Create(10.01M, "PLN");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Minus_Operator_Throws_FormatException_Because_Currencies_Are_Not_The_Same()
    {
        var left = Money.Create(20.13M, "PLN");
        var right = Money.Create(10.12M, "USD");

        Assert.Throws<FormatException>(() => { var result = left - right; });
    }

    [Fact]
    public void Multiplication_Operator_Multiplies_Money_By_Number()
    {
        var left = Money.Create(2.13M, "PLN");
        var factor = 3;

        var result = left * factor;

        var expected = Money.Create(6.39M, "PLN");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Division_Operator_Divides_Money_By_Number()
    {
        var dividend = Money.Create(9.15M, "PLN");
        var divider = 3;

        var result = dividend / divider;

        var expected = Money.Create(3.05M, "PLN");
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Division_Operator_Throws_DivideByZeroException()
    {
        var dividend = Money.Create(9.15M, "PLN");
        var divider = 0;

        Assert.Throws<DivideByZeroException>(() => { var result = dividend / divider; });
    }
}
