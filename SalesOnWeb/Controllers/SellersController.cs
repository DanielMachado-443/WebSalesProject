using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesOnWeb.Services;

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
    }
}
