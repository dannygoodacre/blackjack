using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.SuccessSurrogate")]
public readonly struct SuccessSurrogate;

[RegisterConverter]
public sealed class SuccessSurrogateConverter : IConverter<Success, SuccessSurrogate>
{
    public Success ConvertFromSurrogate(in SuccessSurrogate surrogate) => new();

    public SuccessSurrogate ConvertToSurrogate(in Success value) => new();
}

[RegisterCopier]
public sealed class SuccessCopier : IDeepCopier<Success>
{
    public Success DeepCopy(Success input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.SuccessSurrogate`1")]
public readonly struct SuccessSurrogate<T>(T value)
{
    [Id(0)]
    public T Value { get; } = value;
}

[RegisterConverter]
public sealed class SuccessSurrogateConverter<T> : IConverter<Success<T>, SuccessSurrogate<T>>
{
    public Success<T> ConvertFromSurrogate(in SuccessSurrogate<T> surrogate) => new(surrogate.Value);

    public SuccessSurrogate<T> ConvertToSurrogate(in Success<T> value) => new(value.Value);
}

[RegisterCopier]
public sealed class SuccessCopier<T> : IDeepCopier<Success<T>>
{
    public Success<T> DeepCopy(Success<T> input, CopyContext context)
    {
        T copiedValue = context.DeepCopy(input.Value)!;

        return new Success<T>(copiedValue);
    }
}
