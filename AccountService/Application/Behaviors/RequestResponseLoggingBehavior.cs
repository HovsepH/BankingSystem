using MediatR;
using System.Text.Json;

namespace AccountService.Application.Behaviors
{
    public class RequestResponseLoggingBehavior<TRequest, TResoinse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResoinse>> logger)
        : IPipelineBehavior<TRequest, TResoinse>
        where TRequest :class
    {
        public async Task<TResoinse> Handle(TRequest request, RequestHandlerDelegate<TResoinse> next, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();

            var requestJson=JsonSerializer.Serialize(request);

            logger.LogInformation("Handling request {CorrelationID}: {Request}", correlationId, requestJson);

            var response = await next();

            var responseJson = JsonSerializer.Serialize(response);

            logger.LogInformation("Response for {Correlation}: {Response}", correlationId, responseJson);

            return response;
        }
    }

}
