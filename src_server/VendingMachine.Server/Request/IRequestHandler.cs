using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Request
{
    public class RequestData
    {
        public byte[] RawBytes { get; }
        private readonly IDictionary<string, object> _data = new Dictionary<string, object>();

        public RequestData(byte[] rawBytes)
        {
            RawBytes = rawBytes;
        }

        public object this[string key]
        {
            get => _data[key];
            set => _data.Add(key, value);
        }
    }

    public interface IRequestHandler
    {
        RequestData GetRequestData();
        Task SendResponse(byte[] data);
    }
}
