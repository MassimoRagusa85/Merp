using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.Web.Areas.Accountancy.Models.Invoice
{
    public class InvoiceDetailViewModel
    {

        public decimal Total { get; set; }
        public string PaymentTerms { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Customer Client { get; set; }
        public List<Row> Rows { get; set; }

        public class Row
        {
            public string Description { get; set; }
            public decimal Quantity { get; set; }
            public decimal TotalPrice { get; set; }
        }
        public class Customer
        {
            public string Name { get; set; }
            public string CodiceFiscale { get; set; }
            public string City { get; set; }
            public string StreetName { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
        }
    }
}
