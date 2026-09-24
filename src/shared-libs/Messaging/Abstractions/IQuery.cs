using MediatR;

namespace Messaging.Abstractions
{
    public interface IQuery<out TResponse> : IRequest<TResponse> { }
}
