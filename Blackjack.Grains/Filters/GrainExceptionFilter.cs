using System.Reflection;
using DannyGoodacre.Primitives;
using Microsoft.Extensions.Logging;

namespace Blackjack.Grains.Filters;

public sealed partial class GrainExceptionFilter(ILogger<GrainExceptionFilter> logger) : IIncomingGrainCallFilter
{

    public async Task Invoke(IIncomingGrainCallContext context)
    {
        try
        {
            await context.Invoke();
        }
        catch (OperationCanceledException)
        {
            LogGrainCallMethodCanceled(context.ImplementationMethod);

            context.Result = new Canceled();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Grain call '{Method}' threw an unhandled exception.", context.ImplementationMethod);

            context.Result = new InternalError(ex.Message);
        }
    }

    [LoggerMessage(LogLevel.Information, "Grain call '{Method}', was canceled.")]
    partial void LogGrainCallMethodCanceled(MethodInfo method);
}
