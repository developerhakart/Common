using System.Threading;
using System.Threading.Tasks;

namespace Common.Clients
{

    public interface IApiHttpClient
    {
        Task<TResponse> Get<TResponse>(string path, CancellationToken cancellationToken = default);

        Task<TResponse> Post<TRequest, TResponse>(string path, TRequest model, CancellationToken cancellationToken = default);

        Task<TResponse> Put<TRequest, TResponse>(string path, TRequest model, CancellationToken cancellationToken = default);

        Task<TResponse> Delete<TResponse>(string path, CancellationToken cancellationToken = default);

    }
}