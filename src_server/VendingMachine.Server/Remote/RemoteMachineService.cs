using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.Core.Helpers;
using VendingMachine.Core.Model;
using VendingMachine.Core.Operations;
using VendingMachine.Core.Services;
using System.Net;
using VendingMachine.Infrastructure.Helpers;
using System.Threading;

namespace VendingMachine.Infrastructure.Remote
{
    public class RemoteMachineService : IRemoteMachineService
    {
        private const int PRODUCT_SIZE = 27;
        private const int MACHINE_TCP_PORT = 879;

        private readonly ILogger<RemoteMachineService> _logger;

        public RemoteMachineService(ILogger<RemoteMachineService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<OperationResult> UpdateRemoteMachinePriceTableAsync(Machine machine, IEnumerable<ProductRail> products)
        {
            //if (!IPAddress.TryParse(machine.IPEndPoint, out var ipAddr))
            if (!IPEndPointHelper.TryParse(machine.IPEndPoint, out var ipAddr))
                return OperationResult.Failed(OperationErrorFactory.FromWellKnowErrors(WellKnowErrors.InvalidMachineIPEndPoint));

            var productsArr = products.ToArray();

            using (var client = new TcpClient())
            {
                _logger.LogInformationIfEnabled(() => $"Connection to machine {machine}");
                _logger.LogInformationIfEnabled(() => $"Openning TCP connection in {ipAddr.Address}:{MACHINE_TCP_PORT}");
                await client.ConnectAsync(ipAddr.Address, MACHINE_TCP_PORT);


                var stream = client.GetStream();
                _logger.LogDebug("Got stream!");

                var productsCount = productsArr.Length;
                _logger.LogDebugIfEnabled(() => $"Sending {productsCount} products update.");

                var data = new byte[OFFSET.PRODUCTS + (productsCount * PRODUCT_SIZE)];

                data[OFFSET.INTENTION] = 0x02;
                data[OFFSET.COUNT] = (byte)productsCount;

                for (int i = 0; i < productsCount; i++)
                {
                    var p = productsArr[i];
                    ToBytes(p).CopyTo(data, OFFSET.PRODUCTS + (i * PRODUCT_SIZE));
                }

                _logger.LogInformationIfEnabled(() => $"Awaiting...");
                await Task.Delay(500);

                _logger.LogTraceIfEnabled(() => $"Sending bulk update data: '{ByteHelper.ByteArrayToString(data)}'.");
                stream.Write(data, 0, data.Length);

                _logger.LogDebug("Ignoring response....");
                // var buffer = new byte[255];
                // var result = await stream.ReadAsync(buffer, 0, 255);
            }

            _logger.LogDebug("Returning OperationResult.Success.");
            return OperationResult.Success;
        }


        // Always pair this struct with ArduinoVendingMachine/blob/master/src_arduino/entities/product.h
        private byte[] ToBytes(ProductRail product)
        {
            var result = new byte[PRODUCT_SIZE];
            var priceDouble = Convert.ToDouble(product.Product.Price);
            var priceBytes = BitConverter.GetBytes(priceDouble);
            var nameBytes = 
                product.Product.DisplayName
                    .PadRight(20)
                    .Substring(0, 20)
                    .Select(Convert.ToByte);
            
            //TODO: Review int type inconsistency
            result[0] = (byte)product.ProductId;
            result[1] = (byte)product.Helix;
            result[2] = (byte)product.Count;

            result[3] = priceBytes[0];
            result[4] = priceBytes[1];
            result[5] = priceBytes[2];
            result[6] = priceBytes[3];

            Array.Copy(
                sourceArray: nameBytes.ToArray(),
                sourceIndex: 0,
                destinationArray: result,
                destinationIndex: 7,
                length: 20);

            return result;
        }

        private static class OFFSET
        {
            internal const int INTENTION = 0;
            internal const int COUNT = 1;
            internal const int PRODUCTS = 2;
        }

        //[StructLayout(LayoutKind.Explicit)]
        //private unsafe struct InteropProduct
        //{
        //    [FieldOffset(0)]
        //    public byte id;

        //    [FieldOffset(1)]
        //    public byte helix;

        //    [FieldOffset(2)]
        //    public byte count;

        //    [FieldOffset(3)]
        //    public float price;

        //    [FieldOffset(7)]
        //    public fixed char name[20];

        //    [FieldOffset(0)]
        //    public fixed byte data[27];
        //}
    }
}
