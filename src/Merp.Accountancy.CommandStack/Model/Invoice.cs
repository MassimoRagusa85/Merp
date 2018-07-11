using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MementoFX;
using MementoFX.Domain;

namespace Merp.Accountancy.CommandStack.Model
{
    public abstract class Invoice : Aggregate
    {
        public string Number { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Amount { get; protected set; }
        public decimal Taxes { get; protected set; }
        public decimal TotalPrice { get; protected set; }
        public string Description { get; protected set; }
        public string PaymentTerms { get; protected set; }
        public string PurchaseOrderNumber { get; protected set; }
        public DateTime? DueDate { get; protected set; }
        public DateTime? PaymentDate { get; protected set; }
        public string Currency { get; protected set; }
        public bool IsOverdue { get; protected set; }

        public class PartyInfo
        {
            public Guid Id { get; private set; }
            public string Name { get; private set; }
            public string StreetName { get; private set; }
            public string City { get; private set; }
            public string PostalCode { get; private set; }
            public string Country { get; private set; }
            public string VatIndex { get; private set; }
            public string NationalIdentificationNumber { get; private set; }

            public PartyInfo(Guid customerId, string customerName, string streetName, string city, string postalCode, string country, string vatIndex, string nationalIdentificationNumber)
            {
                City = city;
                Name=customerName;
                Country = country;
                Id = customerId;
                NationalIdentificationNumber = nationalIdentificationNumber;
                PostalCode = postalCode;
                StreetName=streetName;
                VatIndex = vatIndex;
            }
        }

        public class InvoiceRow
        {
            public Guid Id { get; private set; }
            public string Description { get; private set; }
            public string Code { get; private set; }
            public decimal Quantity { get; private set; }
            public decimal UnitPrice { get; private set; }
            public decimal Amount { get; private set; }
            public decimal Taxes { get; private set; }
            public decimal TaxRate { get; private set; }
            public decimal TotalPrice { get; private set; }

            public InvoiceRow(string description, string code, decimal quantity, decimal unitPrice, decimal amount, decimal taxes, decimal taxRate, decimal totalPrice)
            {

                Id = Guid.NewGuid();
                Description = description;
                Code = code;
                Quantity = quantity;
                UnitPrice = unitPrice;
                Amount = amount;
                Taxes = taxes;
                TaxRate = taxRate;
                TotalPrice = totalPrice;
            }
        }
    }
}
