using Blackjack.Domain;
using DannyGoodacre.Cqrs;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Application.Commands;

public interface IHit
{
    Task<IResult> ExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default);
}

public record AddPlayerCommand(Guid Id, string Name) : ICommand;

public sealed class AddPlayerCommandHandler(ILogger logger, IStateUnit stateUnit, TableContext context)
    : StateCommandHandler<AddPlayerCommand>(logger, stateUnit), IHit
{
    protected override string CommandName => "Hit";

    protected override void Validate(ValidationState validationState, AddPlayerCommand command)
    {

    }

    protected override Task<IResult> InternalExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
    {
        TableAggregate table = context.Aggregate;

        IResult<IEnumerable<IDomainEvent>> result = table.AddPlayer(command.Id, command.Name);


    }

    protected override Task AfterSaveAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
    {
        // Emit signalR event to update clients.

        return Task.CompletedTask;
    }

    public new Task<IResult> ExecuteAsync(AddPlayerCommand command, CancellationToken cancellationToken = default)
        => base.ExecuteAsync(command, cancellationToken);
}
