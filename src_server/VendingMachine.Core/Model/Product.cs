using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachine.Core.Model
{
    public class Product
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string DisplayName { get; set; }
        public virtual decimal Price { get; set; }
    }
}
