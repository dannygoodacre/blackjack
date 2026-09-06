using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.InvalidSurrogate")]
public readonly struct InvalidSurrogate(ValidationState validationState)
{
    [Id(0)]
    public ValidationState ValidationState { get; } = validationState;
}

[RegisterConverter]
public sealed class InvalidSurrogateConverter : IConverter<Invalid, InvalidSurrogate>
{
    public Invalid ConvertFromSurrogate(in InvalidSurrogate surrogate) => new(surrogate.ValidationState);

    public InvalidSurrogate ConvertToSurrogate(in Invalid value) => new(value.ValidationState);
}

[RegisterCopier]
public sealed class InvalidCopier : IDeepCopier<Invalid>
{
    public Invalid DeepCopy(Invalid input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.InvalidSurrogate`1")]
public readonly struct InvalidSurrogate<T>(ValidationState validationState)
{
    [Id(0)]
    public ValidationState ValidationState { get; } = validationState;
}

[RegisterConverter]
public sealed class InvalidSurrogateConverter<T> : IConverter<Invalid<T>, InvalidSurrogate<T>>
{
    public Invalid<T> ConvertFromSurrogate(in InvalidSurrogate<T> surrogate) => new(surrogate.ValidationState);

    public InvalidSurrogate<T> ConvertToSurrogate(in Invalid<T> value) => new(value.ValidationState);
}

[RegisterCopier]
public sealed class InvalidCopier<T> : IDeepCopier<Invalid<T>>
{
    public Invalid<T> DeepCopy(Invalid<T> input, CopyContext context)
    {
        ValidationState copiedValidationState = context.DeepCopy(input.ValidationState)!;

        return new Invalid<T>(copiedValidationState);
    }
}
