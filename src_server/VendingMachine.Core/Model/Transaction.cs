using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachine.Core.Model
{
    public class Transaction
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual ClientCard Card { get; set; }
        public virtual Machine Machine { get; set; }
        public virtual IList<Product> Products { get; set; }

        public Transaction()
        {
            Products = new List<Product>();
        }
    }
}
