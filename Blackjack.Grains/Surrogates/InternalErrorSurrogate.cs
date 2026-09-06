using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.InternalErrorSurrogate")]
public readonly struct InternalErrorSurrogate(Error error)
{
    [Id(0)]
    public Error Error { get; } = error;
}

[RegisterConverter]
public sealed class InternalErrorSurrogateConverter : IConverter<InternalError, InternalErrorSurrogate>
{
    public InternalError ConvertFromSurrogate(in InternalErrorSurrogate surrogate) => new(surrogate.Error);

    public InternalErrorSurrogate ConvertToSurrogate(in InternalError value) => new(value.Error);
}

[RegisterCopier]
public sealed class InternalErrorCopier : IDeepCopier<InternalError>
{
    public InternalError DeepCopy(InternalError input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.InternalErrorSurrogate`1")]
public readonly struct InternalErrorSurrogate<T>(Error error)
{
    [Id(0)]
    public Error Error { get; } = error;
}

[RegisterConverter]
public sealed class InternalErrorSurrogateConverter<T> : IConverter<InternalError<T>, InternalErrorSurrogate<T>>
{
    public InternalError<T> ConvertFromSurrogate(in InternalErrorSurrogate<T> surrogate) => new(surrogate.Error);

    public InternalErrorSurrogate<T> ConvertToSurrogate(in InternalError<T> value) => new(value.Error);
}

[RegisterCopier]
public sealed class InternalErrorCopier<T> : IDeepCopier<InternalError<T>>
{
    public InternalError<T> DeepCopy(InternalError<T> input, CopyContext context) => input;
}
