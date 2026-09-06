using DannyGoodacre.Primitives;
using Orleans.Serialization.Cloning;

namespace Blackjack.Grains.Surrogates;

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.SuccessSurrogate")]
public readonly struct CanceledSurrogate;

[RegisterConverter]
public sealed class CanceledSurrogateConverter : IConverter<Canceled, CanceledSurrogate>
{
    public Canceled ConvertFromSurrogate(in CanceledSurrogate surrogate) => new();

    public CanceledSurrogate ConvertToSurrogate(in Canceled value) => new();
}

[RegisterCopier]
public sealed class CanceledCopier : IDeepCopier<Canceled>
{
    public Canceled DeepCopy(Canceled input, CopyContext context) => input;
}

[GenerateSerializer]
[Alias("Blackjack.Grains.Surrogates.SuccessSurrogate`1")]
public readonly struct CanceledSurrogate<T>;

[RegisterConverter]
public sealed class CanceledSurrogateConverter<T> : IConverter<Canceled<T>, CanceledSurrogate<T>>
{
    public Canceled<T> ConvertFromSurrogate(in CanceledSurrogate<T> surrogate) => new();

    public CanceledSurrogate<T> ConvertToSurrogate(in Canceled<T> value) => new();
}

[RegisterCopier]
public sealed class CanceledCopier<T> : IDeepCopier<Canceled<T>>
{
    public Canceled<T> DeepCopy(Canceled<T> input, CopyContext context) => input;
}
