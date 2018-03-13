using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Model
{
    public class Machine
    {
        [Key]
        public virtual int Id { get; set; }

        public virtual string Alias { get; set; }
        public virtual bool IsActivated { get; set; }
        public virtual string Address { get; set; }
    }
}
