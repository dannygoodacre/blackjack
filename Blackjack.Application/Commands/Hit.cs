using DannyGoodacre.Cqrs;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Application.Commands;

// Should contain table aggregate object itself
public record HitCommand(Guid TableId, Guid PlayerId) : ICommand;

public sealed class HitCommandHandler(ILogger logger, IStateUnit stateUnit) : StateCommandHandler<HitCommand>(logger, stateUnit)
{
    protected override string CommandName => "Hit";

    protected override void Validate(ValidationState validationState, HitCommand command)
    {

    }

    protected override Task<IResult> InternalExecuteAsync(HitCommand command, CancellationToken cancellationToken = default)
    {

    }

    protected override Task AfterSaveAsync(HitCommand command, IResult result, CancellationToken cancellationToken = default)
    {
        // Emit signalR event to update clients.
        // Need to figure out the best way to cleanly call signalR from here

        return Task.CompletedTask;
    }
}
