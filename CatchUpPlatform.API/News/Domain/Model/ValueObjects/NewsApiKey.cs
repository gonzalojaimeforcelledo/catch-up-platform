namespace CatchUpPlatform.API.news.domain.model.ValueObjects;

public sealed record NewsApiKey
{
    public string Value { get; }

    public NewsApiKey(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
        if (value.Length > MaxLength)
            throw new ArgumentExceptions($"News API key cannot");
        Value= value
    }
}