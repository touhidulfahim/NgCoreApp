namespace Application.Common.Helper;

public interface IHttpRequest : IRequest<IResult>
{

}

public interface IHttpRequest<out TResponse> : IRequest<TResponse>
{

}