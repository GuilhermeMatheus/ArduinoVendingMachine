using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace VendingMachine.Core.Helpers
{
    public static class IPEndPointHelper
    {
        public static bool TryParse(string ipEndPoint, out IPEndPoint result)
        {
            result = default;

            var ipAddressLength = ipEndPoint.LastIndexOf(':');
            if (ipAddressLength < 0)
                return false;

            var ipString = ipEndPoint.Substring(0, ipAddressLength);
            if(!IPAddress.TryParse(ipString, out var ipAddr))
                return false;

            var portString = ipEndPoint.Substring(ipAddressLength + 1);
            if (!Int32.TryParse(portString, out var port))
                return false;

            result = new IPEndPoint(ipAddr, port);
            return true;
        }
    }
}
