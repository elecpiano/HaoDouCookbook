using System;
using System.Threading.Tasks;

namespace HaoDouCookBook.Infrastructures
{
    public interface ILeechExecuter<TSucceess, TError>
    {
        Task RunAsync(Action<TSucceess> onSuccess, Action<TError> onFail);
    }
}
