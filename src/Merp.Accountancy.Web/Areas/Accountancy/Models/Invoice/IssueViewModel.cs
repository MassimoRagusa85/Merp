
using Merp.Accountancy.Web.Areas.Accountancy.Models.CustomValidator;
using Merp.Web.Site.Areas.Registry.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Merp.Web.Site.Areas.Accountancy.Models.Invoice
{
    public class IssueViewModel
    {
        public PartyInfo Customer { get; set; }

        [DisplayName("Date of issue")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [OutgoingInvoiceDateValidator(ErrorMessage = "Errore")]
        public DateTime Date { get; set; }

        public DateTime LastInvoice { get; set; }

        [DisplayName("Currency")]
        [Required]
        public string Currency { get; set; }

        [DisplayName("Amount")]
        //[Required]
        public Money Amount { get; set; }

        [DisplayName("Taxes")]
        //[Required]
        public Money Taxes { get; set; }

        [DisplayName("Total price")]
        //[Required]
        public Money TotalPrice { get; set; }

        [DisplayName("Description")]
        [Required]
        
        public string Description { get; set; }

        [DisplayName("Payment terms")]
        public string PaymentTerms { get; set; }

        [DisplayName("PO #")]
        public string PurchaseOrderNumber { get; set; }
        [Required]
        public List<InvoiceRow> InvoiceRows { get; set; }

        public decimal TestNumber { get; set; }

        public class Money
        {
            public decimal Amount { get; set; }
            public string Currency { get; set; }
        }

        public class InvoiceRow
        {
            public Guid Id { get; set; }
            [RegularExpression("^[-_, @.A-Za-z0-9]*$", ErrorMessage = "Non lettera")]
            public string Description { get; set; }
            public string Code { get; set; }
            [Required]
            public decimal Quantity { get; set; }
            public Money UnitPrice { get; set; }
            public Money Amount { get; set; }
            public Money Taxes { get; set; }
            public decimal TaxRate { get; set; }
            public Money TotalPrice { get; set; }
        }
    }
}