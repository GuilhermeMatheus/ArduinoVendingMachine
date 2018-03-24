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

        [Display(Name ="Apelido")]
        public virtual string Alias { get; set; }

        [Display(Name = "Crédito")]
        [DataType(DataType.Currency)]
        public virtual decimal Credit { get; set; }
    }
}
