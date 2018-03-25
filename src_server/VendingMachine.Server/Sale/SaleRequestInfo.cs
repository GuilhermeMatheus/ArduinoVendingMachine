using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Sale
{
    internal struct SaleRequestInfo
    {
        public int MachineId { get; }
        public long ClientCardId { get; }
        public int ItemsCount { get; }
        public byte[] ItemsId { get; }
        public float Price { get; }

        public SaleRequestInfo(int machineId, long clientCardId, int itemsCount, byte[] itemsId, float price)
        {
            MachineId = machineId;
            ClientCardId = clientCardId;
            ItemsCount = itemsCount;
            ItemsId = itemsId;
            Price = price;
        }
    }
}
