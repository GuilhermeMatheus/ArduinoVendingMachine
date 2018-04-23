using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Infrastructure.Actions;

namespace VendingMachine.Infrastructure.Request
{
    public abstract class RequestListenerBase : IDisposable, IRequestListener
    {
        private readonly IActionContextProvider _contextProvider;
        private readonly IActionHandlerProvider _actionHandlerProvider;
        private ILogger _logger;

        public RequestListenerBase(IActionContextProvider contextProvider, IActionHandlerProvider actionHandlerProvider, ILogger<RequestListenerBase> logger)
        {
            _contextProvider = contextProvider ?? throw new ArgumentNullException(nameof(contextProvider));
            _actionHandlerProvider = actionHandlerProvider ?? throw new ArgumentNullException(nameof(actionHandlerProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract void Start();

        public virtual void Listen()
        {
            try
            {
                ListenAsync().Wait();
            }
            catch (AggregateException e)
            {
                if (e.InnerExceptions.Count == 1)
                    throw e.InnerExceptions[0];

                throw;
            }
        }

        public virtual async Task ListenAsync()
        {
            var requestHandler = await GetRequestHandler();

            var requestBytes = requestHandler.GetRequestData();
            var context = GetContext(requestBytes);
            var handler = GetHandler(context);

            var response = handler.Process(context);

            await requestHandler.SendResponse(response);
        }

        protected abstract Task<IRequestHandler> GetRequestHandler();

        protected virtual ActionContext GetContext(RequestData data) =>
            _contextProvider.GetContext(data);

        protected virtual IActionHandler GetHandler(ActionContext actionContext) =>
            _actionHandlerProvider.GetActionHandler(actionContext);

        public abstract void Dispose();
    }
}
