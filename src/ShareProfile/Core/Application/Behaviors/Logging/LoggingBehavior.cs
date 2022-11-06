using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Application.Behaviors.Logging
{
    public class LoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ILoggableRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Serilog.ILogger _logger;



        public LoggingBehavior(IHttpContextAccessor httpContextAccessor, Serilog.ILogger logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
                                      RequestHandlerDelegate<TResponse> next)
        {
            List<LogParameter> logParameters = new();
            logParameters.Add(new LogParameter
            {
                Type = request.GetType().Name,
                Value = request
            });

            LogDetail logDetail = new()
            {
                MethodName = next.Method.Name,
                Parameters = logParameters,
                User = _httpContextAccessor.HttpContext == null ||
                       _httpContextAccessor.HttpContext.User.Identity.Name == null
                           ? "?"
                           : _httpContextAccessor.HttpContext.User.Identity.Name
            };

            _logger.Information(JsonConvert.SerializeObject(logDetail));

            return next();
        }
    }
}
