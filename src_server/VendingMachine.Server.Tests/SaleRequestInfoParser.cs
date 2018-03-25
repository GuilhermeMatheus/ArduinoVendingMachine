using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VendingMachine.Server.Sale;

namespace VendingMachine.Server.Tests
{
    [TestClass]
    public class SaleRequestInfoParser
    {
        [TestMethod]
        public void TestSaleRequestInfoParser()
        {
            byte[] incommingData = {
                0x00, //Action: Sale
                0x00, 0x24, //VendingMachineID: 36
                0xFF, 0xFF, 0xFF, 0xFF, // ClientCardId: 4294967295L
                0x02, // ItemsCount: 2
                0x12, 0x13, // ItemsId: 18 and 19
                0x00, 0x00, 0x20, 0x40 // Price: 2.5
            };

            var result = Sale.SaleRequestInfoParser.Parse(incommingData);

            Assert.AreEqual(36, result.MachineId);
            Assert.AreEqual(4294967295L, result.ClientCardId);
            Assert.AreEqual(2, result.ItemsCount);
            CollectionAssert.AreEqual(new byte[] { 18, 19 }, result.ItemsId);
            Assert.AreEqual(2.5f, result.Price);
        }
    }
}
