using Blackjack.Domain;
using DannyGoodacre.Cqrs;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Application.Commands;

public sealed record CreateTableCommand(Guid StreamId, string Name) : ICommand;

public interface ICreateTable
{
    Task<IResult<Guid>> ExecuteAsync(CreateTableCommand command, CancellationToken cancellationToken = default);
}

internal sealed class CreateTableHandler(ILogger<CreateTableHandler> logger,
                                         IEventStateUnit stateUnit,
                                         IAggregateContext context)
    : StateCommandHandler<CreateTableCommand, Guid>(logger, stateUnit), ICreateTable
{
    protected override string CommandName => "Create Table";

    protected override void Validate(ValidationState validationState, CreateTableCommand command)
    {
        validationState.IsNonEmptyGuid(command.StreamId, nameof(command.StreamId));

        validationState.IsNotNullEmptyOrWhitespace(command.Name, nameof(command.Name));
    }

    protected override Task<IResult<Guid>> InternalExecuteAsync(CreateTableCommand command, CancellationToken cancellationToken = default)
    {
        TableAggregate? aggregate = context.Aggregate;

        if (aggregate is not null)
        {
            return Task.FromResult(Conflict($"A table with ID '{command.StreamId}' already exists."));
        }

        var createdEvent = new CreatedTableEvent(command.Name);

        stateUnit.Start(command.StreamId, createdEvent);

        return Task.FromResult(Success(command.StreamId));
    }

    public new Task<IResult<Guid>> ExecuteAsync(CreateTableCommand command, CancellationToken cancellationToken = default)
        => base.ExecuteAsync(command, cancellationToken);
}
