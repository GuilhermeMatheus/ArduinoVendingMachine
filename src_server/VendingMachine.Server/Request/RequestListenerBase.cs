using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Actions;

namespace VendingMachine.Server.Request
{
    public abstract class RequestListenerBase : IDisposable
    {
        private readonly IActionContextProvider _contextProvider;
        private readonly IActionHandlerProvider _actionHandlerProvider;

        public RequestListenerBase(IActionContextProvider contextProvider, IActionHandlerProvider actionHandlerProvider)
        {
            _contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
            _actionHandlerProvider = actionHandlerProvider ?? throw new ArgumentNullException(nameof(actionHandlerProvider));
        }

        public virtual async Task Start()
        {
            OnPreStart();
            var requestHandler = await GetRequestHandler();

            var requestBytes = requestHandler.GetRequestBytes();
            var context = GetContext(requestBytes);
            var handler = GetHandler(context);

            var response = handler.Process(context);

            await requestHandler.SendResponse(response);
        }

        protected virtual void OnPreStart() { }

        protected abstract Task<IRequestHandler> GetRequestHandler();

        protected virtual ActionContext GetContext(byte[] bytes) =>
            _contextProvider.GetContext(bytes);

        protected virtual IActionHandler GetHandler(ActionContext actionContext) =>
            _actionHandlerProvider.GetActionHandler(actionContext);

        public abstract void Dispose();
    }
}
