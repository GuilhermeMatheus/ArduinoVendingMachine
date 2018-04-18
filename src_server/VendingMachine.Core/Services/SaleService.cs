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
        public OperationResult Sale(SaleOperation sale)
        {
            var decimalPrice = new decimal(sale.Price);
            var machine = _machineRepository.Get(sale.MachineId);
            var client = _clientCardRepository.Get(sale.ClientCardId);
            var products = _productRepository.GetMany(sale.ItemsId.Cast<int>()).ToList();

            var errors = new List<OperationError>();

            if (client == null)
                errors.Add(OperationErrorFactory.FromWellKnowErrors(WellKnowErrors.ClientNotFound));

            if (client != null && client.Credit < decimalPrice)
                errors.Add(OperationErrorFactory.FromWellKnowErrors(WellKnowErrors.ClientWithNoEnoughCredit));

            if (products.Count != sale.ItemsCount)
                errors.Add(OperationErrorFactory.FromWellKnowErrors(WellKnowErrors.InvalidProduct));

            if (products.Sum(_ => _.Price) != decimalPrice)
                errors.Add(OperationErrorFactory.FromWellKnowErrors(WellKnowErrors.InvalidPrice));

            if (products.Count != sale.ItemsCount)
                errors.Add(OperationErrorFactory.FromWellKnowErrors(WellKnowErrors.InvalidProduct));

            if(errors.Any())
                return OperationResult.Failed(errors.ToArray());

            DoTransaction(client, machine, products, decimalPrice);
            return OperationResult.Success;
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
