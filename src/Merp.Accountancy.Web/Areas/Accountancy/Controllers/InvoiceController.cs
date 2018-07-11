using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Merp.Web.Site.Areas.Accountancy.Models.Invoice;
using Merp.Web.Site.Areas.Accountancy.WorkerServices;
using Merp.Accountancy.Web.Areas.Accountancy.Models.Invoice;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Merp.Web.Site.Areas.Accountancy.Controllers
{
    [Area("Accountancy")]
    [Authorize(Roles = "Accountancy")]
    public class InvoiceController : Controller
    {
        public InvoiceControllerWorkerServices WorkerServices { get; private set; }

        public InvoiceController(InvoiceControllerWorkerServices workerServices)
        {
            WorkerServices = workerServices ?? throw new ArgumentNullException(nameof(workerServices));
        }

        [HttpGet]
        public ActionResult Issue()
        {
            var Currency = new List<SelectListItem>
            {
                new SelectListItem{Text = "EUR", Value = "EUR"},
                new SelectListItem{Text = "USD", Value = "USD"}
            };
            //var Iva = new List<SelectListItem>
            //{
            //    new SelectListItem{Text = "22%", Value = "22"},
            //    new SelectListItem{Text = "10%", Value = "10"},
            //    new SelectListItem{Text = "4%", Value = "4"}
            //};
            ViewBag.CurrentNumberFormat = new System.Globalization.CultureInfo("fr-FR", false).NumberFormat;
            ViewBag.CurrentNumberFormat.NumberDecimalDigits = 2;
            ViewBag.CurrentNumberFormat.NumberDecimalSeparator = ",";
            ViewBag.CurrentNumberFormat.NumberGroupSeparator = ".";

            ViewData["CurrencyList"] = Currency;
            var model = WorkerServices.GetIssueViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult IssueCopia()
        {
            var model = WorkerServices.GetIssueViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult IssueCopia([FromBody] IssueViewModel model)
        {

            return Json(new { });
        }

        [HttpPost]
        public ActionResult Issue(IssueViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            WorkerServices.Issue(model);
            return Redirect("/Accountancy/");
        }

        [HttpGet]
        public ActionResult Register()
        {
            var model = WorkerServices.GetRegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            WorkerServices.Register(model);
            return Redirect("/Accountancy/");
        }

        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Search_GetInvoiceList(SearchViewModel.InvoiceKind kind, SearchViewModel.InvoiceStatus status)
        {
            var model = WorkerServices.Search_GetInvoiceListViewModel(kind, status);
            return Json(model);
        }
        [HttpGet]
        public JsonResult ModalDetail(Guid uid)
        {
            var model = WorkerServices.GetInvoiceDetail(uid);
            return Json(model);
        }

        #region LinkIncomingInvoiceToJobOrder
        [HttpGet]
        public ActionResult ListOfIncomingInvoicesNotLinkedToAJobOrder()
        {
            var model = WorkerServices.GetListOfIncomingInvoicesNotLinkedToAJobOrderViewModel();
            return Json(model);
        }
        [HttpGet]
        public ActionResult IncomingInvoicesNotLinkedToAJobOrder()
        {
            var model = new IncomingInvoicesNotLinkedToAJobOrderViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult LinkIncomingInvoiceToJobOrder(Guid? id)
        {
            var model = WorkerServices.GetLinkIncomingInvoiceToJobOrderViewModel(id.Value);
            return View(model);
        }

        [HttpPost]
        public ActionResult LinkIncomingInvoiceToJobOrder(LinkIncomingInvoiceToJobOrderViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            WorkerServices.LinkIncomingInvoiceToJobOrder(model);
            return Redirect("/Accountancy/");
        }
        #endregion

        #region LinkOutgoingInvoiceToJobOrder
        [HttpGet]
        public ActionResult ListOfOutgoingInvoicesNotLinkedToAJobOrder()
        {
            var model = WorkerServices.GetListOfOutgoingInvoicesNotLinkedToAJobOrderViewModel();
            return Json(model);
        }
        [HttpGet]
        public ActionResult OutgoingInvoicesNotLinkedToAJobOrder()
        {
            var model = new OutgoingInvoicesNotLinkedToAJobOrderViewModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult LinkOutgoingInvoiceToJobOrder(Guid? id)
        {
            var model = WorkerServices.GetLinkOutgoingInvoiceToJobOrderViewModel(id.Value);
            return View(model);
        }

        [HttpPost]
        public ActionResult LinkOutgoingInvoiceToJobOrder(LinkOutgoingInvoiceToJobOrderViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }
            WorkerServices.LinkOutgoingInvoiceToJobOrder(model);
            return Redirect("/Accountancy/");
        }
        #endregion
    }
}