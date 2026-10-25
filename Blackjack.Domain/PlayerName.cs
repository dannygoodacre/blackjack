using DannyGoodacre.Primitives;

namespace Blackjack.Domain;

public readonly record struct PlayerName
{
    private const int MinLength = 2;

    private const int MaxLength = 15;

    private string Value { get; }

    private PlayerName(string value) => Value = value;

    public static IResult<PlayerName> Create(string? name)
    {
        var state = new ValidationState();

        if (string.IsNullOrWhiteSpace(name))
        {
            state.AddError(nameof(name), "Must not be null, empty, or whitespace.");

            return new Invalid<PlayerName>(state);
        }

        string trimmedName = name.Trim();

        // ReSharper disable once InvertIf
        if (trimmedName.Length is < MinLength or > MaxLength)
        {
            state.AddError(nameof(name), $"Player name must be between {MinLength} and {MaxLength} characters.");

            return new Invalid<PlayerName>(state);
        }

        return new Success<PlayerName>(new PlayerName(trimmedName));
    }

    public static implicit operator string(PlayerName name) => name.Value;

    public override string ToString() => Value;
}
