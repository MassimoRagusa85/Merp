using Merp.Accountancy.QueryStack;
using Merp.Accountancy.QueryStack.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.Web.Areas.Accountancy.Models.CustomValidator
{
    public sealed class OutgoingInvoiceDateValidator : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DbContextOptions<AccountancyDbContext> ctx = (DbContextOptions<AccountancyDbContext>)validationContext.GetService(typeof(DbContextOptions<AccountancyDbContext>));
            var context = new AccountancyDbContext(ctx);
            var max = context.OutgoingInvoices.Max(m => m.Date);
            if (value != null)
            {
                DateTime InvoiceDate = Convert.ToDateTime(value);
                if (InvoiceDate > DateTime.Now)
                {
                    return new ValidationResult("La data non può essere nel futuro");
                }
                if (InvoiceDate < max)
                {
                    return new ValidationResult("L'ultima fattura è stata emessa in data " + max.Date + ", non puoi emettere una fattura in data antecedente");
                }
            }
            return ValidationResult.Success;              
        }       
    }
}