using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.Infrastructures
{
    public interface ILeechExecuter<TSucceess, TError>
    {
        Task RunAsync(Action<TSucceess> onSuccess, Action<TError> onFail);
    }
}
