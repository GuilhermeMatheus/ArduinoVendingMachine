using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Core.Model
{
    public class ClientCard
    {
        [Key]
        public virtual int Rfid { get; set; }

        public virtual string Alias { get; set; }
        public virtual decimal Credit { get; set; }
    }
}
