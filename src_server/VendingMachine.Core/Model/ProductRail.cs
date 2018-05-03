using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachine.Core.Model
{
    public class ProductRail
    {
        [Required]
        public virtual Product Product { get; set; }
        public virtual int ProductId { get; set; }

        [Required]
        public virtual Machine Machine { get; set; }
        public virtual int MachineId { get; set; }
        
        public virtual int Helix { get; set; }
        public virtual int Count { get; set; }
    }
}
