using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesOnWeb.Services;
using SalesOnWeb.Models;

namespace SalesOnWeb.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService) {
            _sellerService = sellerService;
        }

        public IActionResult Index() {
            var list = _sellerService.FindAll(); // << MVC happening 
            return View(list);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) { // MATCHING WITH THE asp-action "Create" of the Create view page in views
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
