using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.NotFoundSurrogate")]
public readonly struct NotFoundSurrogate;

[RegisterConverter]
public sealed class NotFoundSurrogateConverter : IConverter<NotFound, NotFoundSurrogate>
{
    public NotFound ConvertFromSurrogate(in NotFoundSurrogate surrogate) => new();

    public NotFoundSurrogate ConvertToSurrogate(in NotFound value) => new();
}

[RegisterCopier]
public sealed class NotFoundCopier : IDeepCopier<NotFound>
{
    public NotFound DeepCopy(NotFound input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.NotFoundSurrogate`1")]
public readonly struct NotFoundSurrogate<T>;

[RegisterConverter]
public sealed class NotFoundSurrogateConverter<T> : IConverter<NotFound<T>, NotFoundSurrogate<T>>
{
    public NotFound<T> ConvertFromSurrogate(in NotFoundSurrogate<T> surrogate) => new();

    public NotFoundSurrogate<T> ConvertToSurrogate(in NotFound<T> value) => new();
}

[RegisterCopier]
public sealed class NotFoundCopier<T> : IDeepCopier<NotFound<T>>
{
    public NotFound<T> DeepCopy(NotFound<T> input, CopyContext context) => input;
}
