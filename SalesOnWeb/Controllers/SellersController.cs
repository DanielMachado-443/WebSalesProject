﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesOnWeb.Services;
using SalesOnWeb.Models;
using SalesOnWeb.Models.ViewModels;

namespace SalesOnWeb.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index() {
            var list = _sellerService.FindAll(); // << MVC happening 
            return View(list);
        }

        public IActionResult Create() {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) { // MATCHING WITH THE asp-action "Create" of the Create view page in views
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
