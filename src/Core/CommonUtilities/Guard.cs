namespace LargeScaleSolution.CommonUtilities;

public static class Guard
{
    public static void NotNull<T>(T? value, string paramName) where T : class
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static void NotNullOrEmpty(string? value, string paramName)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Value cannot be null or empty.", paramName);
    }
}
