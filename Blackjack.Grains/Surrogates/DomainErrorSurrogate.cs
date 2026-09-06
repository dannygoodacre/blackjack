using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.DomainErrorSurrogate")]
public readonly struct DomainErrorSurrogate(string message)
{
    [Id(0)]
    public string Message { get; } = message;
}

[RegisterConverter]
public sealed class DomainErrorSurrogateConverter : IConverter<DomainError, DomainErrorSurrogate>
{
    public DomainError ConvertFromSurrogate(in DomainErrorSurrogate surrogate) => new(surrogate.Message);

    public DomainErrorSurrogate ConvertToSurrogate(in DomainError value) => new(value.Message);
}

[RegisterCopier]
public sealed class DomainErrorCopier : IDeepCopier<DomainError>
{
    public DomainError DeepCopy(DomainError input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.DomainErrorSurrogate`1")]
public readonly struct DomainErrorSurrogate<T>(string message)
{
    [Id(0)]
    public string Message { get; } = message;
}

[RegisterConverter]
public sealed class DomainErrorSurrogateConverter<T> : IConverter<DomainError<T>, DomainErrorSurrogate<T>>
{
    public DomainError<T> ConvertFromSurrogate(in DomainErrorSurrogate<T> surrogate) => new(surrogate.Message);

    public DomainErrorSurrogate<T> ConvertToSurrogate(in DomainError<T> value) => new(value.Message);
}

[RegisterCopier]
public sealed class DomainErrorCopier<T> : IDeepCopier<DomainError<T>>
{
    public DomainError<T> DeepCopy(DomainError<T> input, CopyContext context) => input;
}
