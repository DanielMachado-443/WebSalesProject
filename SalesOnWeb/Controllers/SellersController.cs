using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesOnWeb.Services;
using SalesOnWeb.Models;
using SalesOnWeb.Models.ViewModels;

namespace SalesOnWeb.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService; // <<<<<< Dependency Injection
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index() {
            var list = await _sellerService.FindAllAsync(); // << MVC happening 
            return View(list);
        }

        public async Task<IActionResult> Create() {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller) { // MATCHING WITH THE asp-action "Create" of the Create view page in views
            if (!ModelState.IsValid) { // <<< C# VALIDATION PROCESS TO THE CASE OF JS BEING DEACTIVATED ON THE CLIENT SIDE
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }; // <<< HERE WE CANT JUST PASS THE Seller to the View METHOD
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) { // optional parameter
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller) {
            if (!ModelState.IsValid) { // <<< C# VALIDATION PROCESS TO THE CASE OF JS BEING DEACTIVATED ON THE CLIENT SIDE
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments }; // <<< HERE WE CANT JUST PASS THE Seller to the View METHOD
                return View(viewModel);                
            }

            if (id != seller.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }

            try {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }            
        }

        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }; // << I didnt understand this step
            return View(viewModel);
        }
    }
}
