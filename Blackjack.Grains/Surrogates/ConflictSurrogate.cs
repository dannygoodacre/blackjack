using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.ConflictSurrogate")]
public readonly struct ConflictSurrogate(string message)
{
    [Id(0)]
    public string Message { get; } = message;
}

[RegisterConverter]
public sealed class ConflictSurrogateConverter : IConverter<Conflict, ConflictSurrogate>
{
    public Conflict ConvertFromSurrogate(in ConflictSurrogate surrogate) => new(surrogate.Message);

    public ConflictSurrogate ConvertToSurrogate(in Conflict value) => new(value.Message);
}

[RegisterCopier]
public sealed class ConflictCopier : IDeepCopier<Conflict>
{
    public Conflict DeepCopy(Conflict input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.ConflictSurrogate`1")]
public readonly struct ConflictSurrogate<T>(string message)
{
    [Id(0)]
    public string Message { get; } = message;
}

[RegisterConverter]
public sealed class ConflictSurrogateConverter<T> : IConverter<Conflict<T>, ConflictSurrogate<T>>
{
    public Conflict<T> ConvertFromSurrogate(in ConflictSurrogate<T> surrogate) => new(surrogate.Message);

    public ConflictSurrogate<T> ConvertToSurrogate(in Conflict<T> value) => new(value.Message);
}

[RegisterCopier]
public sealed class ConflictCopier<T> : IDeepCopier<Conflict<T>>
{
    public Conflict<T> DeepCopy(Conflict<T> input, CopyContext context) => input;
}
