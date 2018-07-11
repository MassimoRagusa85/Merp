using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoFX;
using MementoFX.Domain;
using Merp.Accountancy.CommandStack.Events;

namespace Merp.Accountancy.CommandStack.Model
{
    public class IncomingInvoice : Invoice,
        IApplyEvent<IncomingInvoiceRegisteredEvent>,
        IApplyEvent<IncomingInvoiceGotOverdueEvent>
    {
        public PartyInfo Supplier { get; protected set; }
        public IEnumerable<InvoiceRow> InvoiceRows { get; protected set; }

        protected IncomingInvoice()
        {

        }

        public void ApplyEvent([AggregateId(nameof(IncomingInvoiceRegisteredEvent.InvoiceId))] IncomingInvoiceRegisteredEvent evt)
        {
            Id = evt.InvoiceId;
            IsOverdue = false;
            Number = evt.InvoiceNumber;
            Date = evt.InvoiceDate;
            DueDate = evt.DueDate;
            Currency = evt.Currency;
            Amount = evt.TaxableAmount;
            Taxes = evt.Taxes;
            TotalPrice = evt.TotalPrice;
            Description = evt.Description;
            PaymentTerms = evt.PaymentTerms;
            PurchaseOrderNumber = evt.PurchaseOrderNumber;
            InvoiceRows = evt.InvoiceRows;
            Supplier = new PartyInfo(evt.Supplier.Id, evt.Supplier.Name, evt.Supplier.StreetName, evt.Supplier.City, evt.Supplier.PostalCode, evt.Supplier.Country, evt.Supplier.VatIndex, evt.Supplier.NationalIdentificationNumber);
        }

        public void ApplyEvent([AggregateId(nameof(IncomingInvoicePaidEvent.InvoiceId))] IncomingInvoicePaidEvent evt)
        {
            PaymentDate = evt.PaymentDate;
        }

        public void ApplyEvent([AggregateId(nameof(IncomingInvoiceGotOverdueEvent.InvoiceId))] IncomingInvoiceGotOverdueEvent evt)
        {
            IsOverdue = true;
        }

        public void MarkAsOverdue()
        {
            if (!DueDate.HasValue)
                throw new InvalidOperationException("An invoice must have a due date for it to be marked as expired.");

            var evt = new IncomingInvoiceGotOverdueEvent(this.Id, DueDate.Value);
            RaiseEvent(evt);
        }

        public void MarkAsPaid(DateTime paymentDate)
        {
            var evt = new IncomingInvoicePaidEvent(this.Id, paymentDate);
            RaiseEvent(evt);
        }

        public static class Factory
        {
            public static IncomingInvoice Register(string invoiceNumber, DateTime invoiceDate, DateTime? dueDate, string currency, Money amount, Money taxes, Money totalPrice, string description, string paymentTerms, string purchaseOrderNumber,
            Guid customerId, string customerName, string customerAddress, string customerCity, string customerPostalCode, string customerCountry, string customerVatIndex, string customerNationalIdentificationNumber,
            Guid supplierId, string supplierName, string supplierAddress, string supplierCity, string supplierPostalCode, string supplierCountry, string supplierVatIndex, string supplierNationalIdentificationNumber,
            IEnumerable<InvoiceRow> invoiceRows)
            {
                var @event = new IncomingInvoiceRegisteredEvent(
                        Guid.NewGuid(),
                        invoiceNumber,
                        invoiceDate,
                        dueDate,
                        currency,
                        amount.Amount,
                        taxes.Amount,
                        totalPrice.Amount,
                        description,
                        paymentTerms,
                        purchaseOrderNumber,
                        customerId,
                        customerName,
                        customerAddress,
                        customerCity,
                        customerPostalCode,
                        customerCountry,
                        customerVatIndex,
                        customerNationalIdentificationNumber,
                        supplierId,
                        supplierName,
                        supplierAddress,
                        supplierCity,
                        supplierPostalCode,
                        supplierCountry,
                        supplierVatIndex,
                        supplierNationalIdentificationNumber,
                        invoiceRows
                    );
                var invoice = new IncomingInvoice();
                invoice.RaiseEvent(@event);
                return invoice;
            }

            public static IncomingInvoice Import(Guid invoiceId, string invoiceNumber, DateTime invoiceDate, DateTime? dueDate, string currency, Money amount, Money taxes, Money totalPrice, string description, string paymentTerms, string purchaseOrderNumber,
             Guid customerId, string customerName, string customerAddress, string customerCity, string customerPostalCode, string customerCountry, string customerVatIndex, string customerNationalIdentificationNumber,
             Guid supplierId, string supplierName, string supplierAddress, string supplierCity, string supplierPostalCode, string supplierCountry, string supplierVatIndex, string supplierNationalIdentificationNumber,
             IEnumerable<InvoiceRow> invoiceRows)
            {
                var @event = new IncomingInvoiceRegisteredEvent(
                        invoiceId,
                        invoiceNumber,
                        invoiceDate,
                        dueDate,
                        currency,
                        amount.Amount,
                        taxes.Amount,
                        totalPrice.Amount,
                        description,
                        paymentTerms,
                        purchaseOrderNumber,
                        customerId,
                        customerName,
                        customerAddress,
                        customerCity,
                        customerPostalCode,
                        customerCountry,
                        customerVatIndex,
                        customerNationalIdentificationNumber,
                        supplierId,
                        supplierName,
                        supplierAddress,
                        supplierCity,
                        supplierPostalCode,
                        supplierCountry,
                        supplierVatIndex,
                        supplierNationalIdentificationNumber,
                        invoiceRows
                    );
                var invoice = new IncomingInvoice();
                invoice.RaiseEvent(@event);
                return invoice;
            }
        }
    }
}
