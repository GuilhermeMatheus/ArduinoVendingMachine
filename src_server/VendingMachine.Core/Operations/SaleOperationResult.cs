using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Operations
{
    // TODO: Do it fucking better
    public struct SaleOperationResult
    {
        public bool NotEnoughCredit { get; }
        public bool InvalidPrice { get; }
        public bool ClientNotFound { get; }
        public bool InvalidProduct { get; }

        public SaleOperationResult(
            bool notEnoughCredit = false,
            bool invalidPrice = false,
            bool clientNotFound = false,
            bool invalidProduct = false)
        {
            NotEnoughCredit = notEnoughCredit;
            InvalidPrice = invalidPrice;
            ClientNotFound = clientNotFound;
            InvalidProduct = invalidProduct;
        }

        public static SaleOperationResult WithNotEnoughCredit()
            => new SaleOperationResult(notEnoughCredit: true);

        public static SaleOperationResult WithInvalidPrice()
            => new SaleOperationResult(invalidPrice: true);

        public static SaleOperationResult WithClientNotFound()
            => new SaleOperationResult(clientNotFound: true);

        public static SaleOperationResult WithInvalidProduct()
            => new SaleOperationResult(invalidProduct: true);

        public static SaleOperationResult operator +(SaleOperationResult left, SaleOperationResult right)
        {
            return new SaleOperationResult(
                notEnoughCredit: left.NotEnoughCredit || right.NotEnoughCredit,
                invalidPrice: left.InvalidPrice || right.InvalidPrice,
                clientNotFound: left.ClientNotFound || right.ClientNotFound,
                invalidProduct: left.InvalidProduct || right.InvalidProduct
            );
        }
    }
}
