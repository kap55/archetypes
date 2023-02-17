namespace Common;
public static class BoolExtensionMethods
{
    public static void ThrowIfInvalid(this bool value, string name)
    {
        if (value == false)
            throw new ArgumentException($" field {name} invalid");
    }
}
