using MediatR;

namespace Messaging.Abstractions
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        new Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
    }
}
