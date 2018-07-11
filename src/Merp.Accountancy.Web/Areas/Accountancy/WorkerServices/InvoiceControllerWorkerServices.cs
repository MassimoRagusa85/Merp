﻿using Merp.Accountancy.CommandStack.Commands;
using Merp.Accountancy.QueryStack;
using Merp.Web.Site.Areas.Accountancy.Models.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using Rebus.Bus;
using static Merp.Web.Site.Areas.Accountancy.AccountancyBoundedContextConfigurator;
using Merp.Accountancy.CommandStack.Model;
using static Merp.Accountancy.CommandStack.Model.Invoice;
using Merp.Accountancy.Web.Areas.Accountancy.Models.Invoice;

namespace Merp.Web.Site.Areas.Accountancy.WorkerServices
{
    public class InvoiceControllerWorkerServices
    {
        public IBus Bus { get; private set; }
        public IDatabase Database { get; set; }
        public InvoicingSettings Settings { get; set; }


        public InvoiceControllerWorkerServices(IBus bus, IDatabase database, InvoicingSettings invoicingSettings)
        {
            this.Bus = bus ?? throw new ArgumentNullException(nameof(bus));
            this.Database = database ?? throw new ArgumentNullException(nameof(database));
            this.Settings = invoicingSettings ?? throw new ArgumentNullException(nameof(invoicingSettings));
        }
        public IssueViewModel GetIssueViewModel()
        {
            var model = new IssueViewModel();
            model.Date = DateTime.Now;
            model.LastInvoice = Database.OutgoingInvoices.Max(x => x.Date);
            return model;
        }
        public RegisterViewModel GetRegisterViewModel()
        {
            var model = new RegisterViewModel();
            model.Date = DateTime.Now;
            return model;
        }
        public void Issue(IssueViewModel model)
        {
            var amount = new Money(model.Amount.Amount, model.Currency);
            var taxes = new Money(model.Taxes.Amount, model.Currency);
            var totalPrice = new Money(model.TotalPrice.Amount, model.Currency);
            List<InvoiceRow> Rows = new List<InvoiceRow>(); 

            foreach (var item in model.InvoiceRows)
            {
                Rows.Add(new InvoiceRow(
                    item.Description,
                    item.Code,
                    item.Quantity,
                    item.UnitPrice.Amount,
                    item.Amount.Amount,
                    item.Taxes.Amount,
                    item.TaxRate,
                    item.TotalPrice.Amount));
            }
            var command = new IssueInvoiceCommand(
                model.Date,
                model.Currency,
                amount,
                taxes,
                totalPrice,
                model.Description,
                model.PaymentTerms,
                model.PurchaseOrderNumber,
                model.Customer.OriginalId,
                model.Customer.Name,
                Settings.Address,
                Settings.City,
                Settings.PostalCode,
                Settings.Country,
                Settings.TaxId,
                Settings.TaxId,
                Settings.CompanyName,
                Settings.Address,
                Settings.City,
                Settings.PostalCode,
                Settings.Country,
                Settings.TaxId,
                string.Empty,
                Rows
                
                );
            Bus.Send(command);
        }
        public void Register(RegisterViewModel model)
        {
            var amount = new Money(model.Amount.Amount, model.Currency);
            var taxes = new Money(5, model.Currency);
            var totalPrice = new Money(10, model.Currency);
            List<InvoiceRow> Rows = new List<InvoiceRow>();

            foreach (var item in model.InvoiceRows)
            {
                Rows.Add(new InvoiceRow(
                    item.Description,
                    item.Code,
                    item.Quantity,
                    item.UnitPrice.Amount,
                    item.Amount.Amount,
                    item.Taxes.Amount,
                    item.TaxRate,
                    item.TotalPrice.Amount));
            }

            var command = new RegisterIncomingInvoiceCommand(
                model.InvoiceNumber,
                model.Date,
                model.DueDate,
                model.Currency,
                amount,
                taxes,
                totalPrice,
                model.Description,
                model.PaymentTerms,
                model.PurchaseOrderNumber,
                Guid.Empty,
                Settings.CompanyName,
                Settings.Address,
                Settings.City,
                Settings.PostalCode,
                Settings.Country,
                Settings.TaxId,
                string.Empty,
                model.Supplier.OriginalId,
                model.Supplier.Name,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                Rows
                );
            Bus.Send(command);
        }

        public Search_GetInvoiceListViewModel Search_GetInvoiceListViewModel(SearchViewModel.InvoiceKind kind, SearchViewModel.InvoiceStatus status)
        {

            var invoices = new List<Search_GetInvoiceListViewModel.Invoice>();
            var incomingInvoices = Database.IncomingInvoices;
            if(kind == SearchViewModel.InvoiceKind.Any || kind == SearchViewModel.InvoiceKind.Incoming)
            {
                if (status == SearchViewModel.InvoiceStatus.Overdue)
                    incomingInvoices = incomingInvoices.Where(i => i.IsOverdue == true);
                else if (status == SearchViewModel.InvoiceStatus.Outstanding)
                    incomingInvoices = incomingInvoices.Where(i => i.IsPaid == false);
                else if (status == SearchViewModel.InvoiceStatus.Paid)
                    incomingInvoices = incomingInvoices.Where(i => i.IsPaid == true);
                invoices.AddRange(incomingInvoices.Select(i => new Search_GetInvoiceListViewModel.Invoice { Uid = i.OriginalId, DocumentType="Incoming invoice", Number = i.Number, Date = i.Date, DueDate = i.DueDate, CustomerName = i.Customer.Name, SupplierName = i.Supplier.Name, TotalPrice = i.TotalPrice, Currency = i.Currency }).OrderByDescending(i => i.Date).Take(20));
            }

            if (kind == SearchViewModel.InvoiceKind.Any || kind == SearchViewModel.InvoiceKind.Outgoing)
            {
                var outgoingInvoices = Database.OutgoingInvoices;
                if (status == SearchViewModel.InvoiceStatus.Overdue)
                    outgoingInvoices = outgoingInvoices.Where(i => i.IsOverdue == true);
                else if (status == SearchViewModel.InvoiceStatus.Outstanding)
                    outgoingInvoices = outgoingInvoices.Where(i => i.IsPaid == false);
                else if (status == SearchViewModel.InvoiceStatus.Paid)
                    outgoingInvoices = outgoingInvoices.Where(i => i.IsPaid == true);
                invoices.AddRange(outgoingInvoices.Select(i => new Search_GetInvoiceListViewModel.Invoice { Uid = i.OriginalId, DocumentType = "Outgoing invoice", Number = i.Number, Date = i.Date, DueDate = i.DueDate, CustomerName = i.Customer.Name, SupplierName = i.Supplier.Name, TotalPrice = i.TotalPrice, Currency = i.Currency }).OrderByDescending(i => i.Date).Take(20));
            }
            var model = new Search_GetInvoiceListViewModel()
            {
                Invoices = invoices.OrderByDescending(i => i.Date).Take(20)
            };
            return model;
            
        }

        public InvoiceDetailViewModel GetInvoiceDetail(Guid uid)
        {
            var model = new InvoiceDetailViewModel();
            var Invoice = Database.OutgoingInvoices.Where(i => i.OriginalId == uid);
            model = Invoice.Select(i => new InvoiceDetailViewModel()
            {
                Date = i.Date,
                Currency = i.Currency,
                Description = i.Description,
                PaymentTerms = "",
                PurchaseOrderNumber = i.PurchaseOrderNumber,
                Total = i.TotalPrice
            }).SingleOrDefault();
            model.Rows = Invoice
                .SelectMany(i => i.Rows.Select(j => new InvoiceDetailViewModel.Row()
                {
                    Description = j.Description,
                    Quantity = j.Quantity,
                    TotalPrice = j.TotalAmount
                })).ToList();
            model.Client = Invoice
                .Select(i => new InvoiceDetailViewModel.Customer()
                {
                    City = i.Customer.City,
                    CodiceFiscale = i.Customer.NationalIdentificationNumber,
                    Country = i.Customer.Country,
                    Name = i.Customer.Name,
                    PostalCode = i.Customer.PostalCode,
                    StreetName = i.Customer.StreetName
                }).SingleOrDefault();
            
            return model;
        }
    //    Select(i => new InvoiceDetailViewModel.Row()
    //            {
    //                Description = i.,
    //                Quantity = i.Rows.,
    //                TotalPrice = i.TotalAmount
    //}).ToList();

    #region LinkIncomingInvoiceToJobOrder
    public IEnumerable<IncomingInvoicesNotLinkedToAJobOrderViewModel.Invoice> GetListOfIncomingInvoicesNotLinkedToAJobOrderViewModel()
        {
            var model = (from i in Database.IncomingInvoices.NotAssociatedToAnyJobOrder()
                        orderby i.Date
                        select new IncomingInvoicesNotLinkedToAJobOrderViewModel.Invoice
                        {
                            Amount = i.TaxableAmount,
                            Number = i.Number,
                            SupplierName = i.Supplier.Name,
                            OriginalId = i.OriginalId
                        }).ToArray();
            return model;
        }

        public LinkIncomingInvoiceToJobOrderViewModel GetLinkIncomingInvoiceToJobOrderViewModel(Guid invoiceId)
        {
            var model = (from i in Database.IncomingInvoices
                        where i.OriginalId == invoiceId
                        select new LinkIncomingInvoiceToJobOrderViewModel
                        {
                            Amount = i.TaxableAmount,
                            DateOfLink = i.Date,
                            InvoiceNumber = i.Number,
                            InvoiceOriginalId = i.OriginalId,
                            SupplierName = i.Supplier.Name
                        }).Single();
            return model;
        }

        public void LinkIncomingInvoiceToJobOrder(LinkIncomingInvoiceToJobOrderViewModel model)
        {
            var jobOrderId = (from j in Database.JobOrders
                              where j.Number == model.JobOrderNumber
                                 select j.OriginalId).Single();
            var command = new LinkIncomingInvoiceToJobOrderCommand(model.InvoiceOriginalId, jobOrderId, model.DateOfLink, model.Amount);
            Bus.Send(command);
        }
        #endregion

        #region LinkOutgoingInvoiceToJobOrder
        public IEnumerable<OutgoingInvoicesNotLinkedToAJobOrderViewModel.Invoice> GetListOfOutgoingInvoicesNotLinkedToAJobOrderViewModel()
        {
            var model = (from i in Database.OutgoingInvoices.NotAssociatedToAnyJobOrder()
                         orderby i.Date
                         select new OutgoingInvoicesNotLinkedToAJobOrderViewModel.Invoice
                         {
                             Amount = i.TaxableAmount,
                             Number = i.Number,
                             CustomerName = i.Customer.Name,
                             OriginalId = i.OriginalId
                         }).ToArray();
            return model;
        }

        public LinkOutgoingInvoiceToJobOrderViewModel GetLinkOutgoingInvoiceToJobOrderViewModel(Guid invoiceId)
        {
            var model = (from i in Database.OutgoingInvoices
                         where i.OriginalId == invoiceId
                         select new LinkOutgoingInvoiceToJobOrderViewModel
                         {
                             Amount = i.TaxableAmount,
                             DateOfLink = i.Date,
                             InvoiceNumber = i.Number,
                             InvoiceOriginalId = i.OriginalId,
                             CustomerName = i.Customer.Name
                         }).Single();
            return model;
        }

        public void LinkOutgoingInvoiceToJobOrder(LinkOutgoingInvoiceToJobOrderViewModel model)
        {
            var jobOrderId = (from j in Database.JobOrders
                              where j.Number == model.JobOrderNumber
                              select j.OriginalId).Single();
            var command = new LinkOutgoingInvoiceToJobOrderCommand(model.InvoiceOriginalId, jobOrderId, model.DateOfLink, model.Amount);
            Bus.Send(command);
        }
        #endregion
    }
}