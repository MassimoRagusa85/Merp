﻿using System;
using Xunit;
using Merp.Accountancy.CommandStack.Commands;
using Merp.Accountancy.CommandStack.Model;
using static Merp.Accountancy.CommandStack.Model.Invoice;
using System.Collections.Generic;

namespace Merp.Accountancy.CommandStack.Tests.Commands
{
    
    public class IssueInvoiceCommandFixture
    {
        [Fact]
        public void Ctor_should_set_properties_according_to_parameters()
        {
            DateTime invoiceDate = new DateTime(1990, 11, 11);
            string currency = "EUR";
            Money amount = new Money(101, "Eur");
            Money taxes = new Money(42, "Eur");
            Money totalPrice = new Money(143, "Eur");
            string description = "fake";
            string paymentTerms = "none";
            string purchaseOrderNumber = "42";
            Guid customerId = Guid.NewGuid();
            string customerName = "Managed Designs S.r.l.";
            string customerAddress = "Via Torino 51";
            string customerCity = "Milan";
            string customerPostalCode = "20123";
            string customerCountry ="Italy";
            string customerVatIndex = "04358780965";
            string customerNationalIdentificationNumber = "04358780965";
            string supplierName = "Mastreeno ltd";
            string supplierAddress = "8, Leman street";
            string supplierCity = "London";
            string supplierPostalCode = "";
            string supplierCountry = "England - United Kingdom";
            string supplierVatIndex = "XYZ";
            string supplierNationalIdentificationNumber = "04358780965";
            IEnumerable<InvoiceRow> invoiceRows = new List<InvoiceRow>();
            var sut = new IssueInvoiceCommand(
                invoiceDate,
                currency,
                amount,
                taxes,
                totalPrice,
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
                supplierName,
                supplierAddress,
                supplierCity,
                supplierPostalCode,
                supplierCountry,
                supplierVatIndex,
                supplierNationalIdentificationNumber,
                invoiceRows
                );
            Assert.Equal(invoiceDate, sut.InvoiceDate);
            Assert.Equal(currency, sut.Currency);
            Assert.Equal(amount, sut.TaxableAmount);
            Assert.Equal(taxes, sut.Taxes);
            Assert.Equal(totalPrice, sut.TotalPrice);
            Assert.Equal(description, sut.Description);
            Assert.Equal(paymentTerms, sut.PaymentTerms);
            Assert.Equal(purchaseOrderNumber, sut.PurchaseOrderNumber);
            Assert.Equal(customerId, sut.Customer.Id);
            Assert.Equal(customerName, sut.Customer.Name);
            Assert.Equal(customerAddress, sut.Customer.StreetName);
            Assert.Equal(customerCity, sut.Customer.City);
            Assert.Equal(customerPostalCode, sut.Customer.PostalCode);
            Assert.Equal(customerCountry, sut.Customer.Country);
            Assert.Equal(customerVatIndex, sut.Customer.VatIndex);
            Assert.Equal(customerNationalIdentificationNumber, sut.Customer.NationalIdentificationNumber);

            Assert.Equal(supplierName, sut.Supplier.Name);
            Assert.Equal(supplierAddress, sut.Supplier.StreetName);
            Assert.Equal(supplierCity, sut.Supplier.City);
            Assert.Equal(supplierPostalCode, sut.Supplier.PostalCode);
            Assert.Equal(supplierCountry, sut.Supplier.Country);
            Assert.Equal(supplierVatIndex, sut.Supplier.VatIndex);
            Assert.Equal(supplierNationalIdentificationNumber, sut.Supplier.NationalIdentificationNumber);
        }
    }
}
