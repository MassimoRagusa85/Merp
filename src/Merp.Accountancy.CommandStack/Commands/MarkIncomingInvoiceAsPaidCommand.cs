﻿using MementoFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.CommandStack.Commands
{
    public class MarkIncomingInvoiceAsPaidCommand : Command
    {
        public Guid InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }

        public MarkIncomingInvoiceAsPaidCommand(Guid invoiceId, DateTime paymentDate)
        {
            InvoiceId = invoiceId;
            PaymentDate = paymentDate;
        }
    }
}
