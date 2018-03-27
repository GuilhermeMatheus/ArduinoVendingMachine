using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;
using VendingMachine.Core.Operations;
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

        //TODO: Review floating points comparisons
        public OperationResult<SaleOperationResult> Sale(SaleOperation sale)
        {
            var result = default(SaleOperationResult);

            var decimalPrice = new decimal(sale.Price);
            var machine = _machineRepository.Get(sale.MachineId);
            var client = _clientCardRepository.Get(sale.ClientCardId);
            var products = _productRepository.GetMany(sale.ItemsId.Cast<int>()).ToList();

            if (client == null)
                result += SaleOperationResult.WithClientNotFound();
            else if (client.Credit < decimalPrice)
                result += SaleOperationResult.WithNotEnoughCredit();

            if (products.Count != sale.ItemsCount)
                result += SaleOperationResult.WithInvalidProduct();

            if (products.Sum(_ => _.Price) != decimalPrice)
                result += SaleOperationResult.WithInvalidPrice();

            if (products.Count != sale.ItemsCount)
                result += SaleOperationResult.WithInvalidProduct();

            var success = result.Equals(default);

            if (success)
                DoTransaction(client, machine, products, decimalPrice);

            return new OperationResult<SaleOperationResult>(success, result);
        }

        //TODO: we know its not a transaction
        private void DoTransaction(ClientCard clientCard, Machine machine, IEnumerable<Product> products, decimal value)
        {
            var transaction = new Transaction { Card = clientCard, Machine = machine };

            foreach (var item in products)
                transaction.Products.Add(item);

            clientCard.Credit -= value;

            _transactionRepository.Save();
            _clientCardRepository.Save();
        }
    }
}
