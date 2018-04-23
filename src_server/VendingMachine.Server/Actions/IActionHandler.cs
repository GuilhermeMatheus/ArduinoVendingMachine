﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Actions
{
    public interface IActionHandler
    {
        byte[] Process(ActionContext context);
    }
}
