using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Operations
{
    public class SaleOperation
    {
        public int MachineId { get; }
        public long ClientCardId { get; }
        public int ItemsCount { get; }
        public byte[] ItemsId { get; }
        public float Price { get; }

        public SaleOperation(int machineId, long clientCardId, int itemsCount, byte[] itemsId, float price)
        {
            MachineId = machineId;
            ClientCardId = clientCardId;
            ItemsCount = itemsCount;
            ItemsId = itemsId;
            Price = price;
        }
    }
}
