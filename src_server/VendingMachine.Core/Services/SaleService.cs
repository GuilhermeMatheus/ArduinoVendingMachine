using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Repository;

namespace VendingMachine.Core.Services
{
    public class SaleService : ISaleService
    {
        private readonly IClientCardRepository _clientCardRepository;
        private readonly IMachineRepository _machineRepository;
        private readonly IProductRepository _productRepository;
        private readonly ITransactionRepository _transactionRepository;

        public SaleService(
            IClientCardRepository clientCardRepository,
            IMachineRepository machineRepository,
            IProductRepository productRepository,
            ITransactionRepository transactionRepository)
        {
            _clientCardRepository = clientCardRepository ?? throw new ArgumentNullException(nameof(clientCardRepository));
            _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public void Sale()
        {
            throw new NotImplementedException();
        }
    }
}
