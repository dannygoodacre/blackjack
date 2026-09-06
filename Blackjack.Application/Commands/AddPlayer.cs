using Blackjack.Domain;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Application.Commands;

public interface IAddPlayer
{
    Task<IResult> ExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default);
}

public record AddPlayerCommand(Guid StreamId, Guid PlayerId, string Name) : AggregateCommand(StreamId);

internal sealed class AddPlayerCommandHandler(ILogger<AddPlayerCommandHandler> logger, IEventStateUnit stateUnit, IAggregateContext context)
    : AggregateCommandHandler<AddPlayerCommand>(logger, stateUnit, context), IAddPlayer
{
    protected override string CommandName => "Hit";

    protected override void Validate(ValidationState validationState, AddPlayerCommand command)
    {
        // TODO
    }

    protected override Task<IResult> InternalExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
    {
        IResult<List<IDomainEvent>> result = Aggregate.AddPlayer(command.PlayerId, command.Name);

        if (result is not Success<List<IDomainEvent>>(var events))
        {
            return Task.FromResult<IResult>(result);
        }

        EventStateUnit.Append(Aggregate.Id, events);

        return Task.FromResult(Success());
    }

    protected override Task AfterSaveAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
    {
        // Emit signalR event to update clients.

        return Task.CompletedTask;
    }

    public new Task<IResult> ExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
        => base.ExecuteAsync(command, cancellationToken);
}
