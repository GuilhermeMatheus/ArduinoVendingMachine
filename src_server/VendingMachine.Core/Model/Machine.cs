using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VendingMachine.Core.Model
{
    public class Machine
    {
        [Key]
        public virtual int Id { get; set; }

        [Display(Name = "Apelido")]
        public virtual string Alias { get; set; }

        [Display(Name = "Ativada")]
        public virtual bool IsActivated { get; set; }

        [Display(Name = "Endereço")]
        public virtual string Address { get; set; }

        [Display(Name = "Client IPEndPoint")]
        public virtual string IPEndPoint { get; set; }

        [Display(Name = "Access Point IP")]
        public virtual string AccessPointIp { get; set; }

        public override string ToString() =>
            $"{Id} - {Alias}";
    }
}
