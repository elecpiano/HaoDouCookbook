using System;
using System.Threading.Tasks;

namespace Shared.Infrastructures
{
    public interface ILeechExecuter<TSucceess, TError>
    {
        Task RunAsync(Action<TSucceess> onSuccess, Action<TError> onFail);
    }
}
