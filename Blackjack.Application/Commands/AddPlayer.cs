using Blackjack.Domain;
using DannyGoodacre.Cqrs;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Application.Commands;

public interface IAddPlayer
{
    Task<IResult> ExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default);
}

public record AddPlayerCommand(Guid PlayerId, string Name) : ICommand;

internal sealed class AddPlayerCommandHandler(ILogger<AddPlayerCommandHandler> logger, IEventStateUnit eventStateUnit, TableContext context)
    : StateCommandHandler<AddPlayerCommand>(logger, eventStateUnit), IAddPlayer
{
    protected override string CommandName => "Hit";

    private readonly TableAggregate _aggregate = context.Aggregate;

    protected override void Validate(ValidationState validationState, AddPlayerCommand command)
    {

    }

    protected override Task<IResult> InternalExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
    {
        IResult<List<IDomainEvent>> result = _aggregate.AddPlayer(command.PlayerId, command.Name);

        if (result is not Success<List<IDomainEvent>>(var events))
        {
            return Task.FromResult<IResult>(result);
        }

        eventStateUnit.Append(_aggregate.Id, events);

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
