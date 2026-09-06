using Blackjack.Domain;
using DannyGoodacre.Cqrs;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Application;

public abstract record AggregateCommand(Guid StreamId) : ICommand;

internal abstract class AggregateCommandHandler<TEventCommand>(ILogger logger, IEventStateUnit stateUnit, IAggregateContext context)
    : StateCommandHandler<TEventCommand>(logger, stateUnit)
    where TEventCommand : AggregateCommand
{
    protected readonly IEventStateUnit EventStateUnit = stateUnit;

    protected TableAggregate Aggregate = null!;

    protected new Task<IResult> ExecuteAsync(TEventCommand command, CancellationToken cancellationToken = default)
    {
        if (context.Aggregate is null)
        {
            return Task.FromResult(DomainError($"A stream with ID '{command.StreamId}' does not exist."));
        }

        Aggregate = context.Aggregate;

        return base.ExecuteAsync(command, cancellationToken);
    }
}
