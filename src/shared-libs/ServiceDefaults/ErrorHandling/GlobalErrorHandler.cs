using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ServiceDefaults.ErrorHandling.ExpectionMapper;

namespace ServiceDefaults.ErrorHandling
{
    public sealed partial class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly IExceptionProblemDetailsMapper _mapper;
        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            IProblemDetailsService problemDetailsService,
            IExceptionProblemDetailsMapper mapper)
        {
            _logger = logger;
            _problemDetailsService = problemDetailsService;
            _mapper = mapper;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            LogUnhandledException(_logger, exception.Message);

            var problemDetailsException = _mapper.Map(exception);

            httpContext.Response.StatusCode = problemDetailsException.Status ?? StatusCodes.Status500InternalServerError;

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = problemDetailsException
            });
        }

        [LoggerMessage(Level = LogLevel.Error, Message = "Unhandled exception occurred: {Message}")]
        public static partial void LogUnhandledException(ILogger logger, string message);
    }
}
