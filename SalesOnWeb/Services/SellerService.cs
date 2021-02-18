using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesOnWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesOnWeb.Services {
    public class SellerService {
        private readonly SalesOnWebContext _context;

        public SellerService(SalesOnWebContext context) {
            _context = context;
        }

        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }

        public Seller FindById(int id) {
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); // << added include in order to pick the Deparment from the obj
        }

        public void Remove(int id) {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Insert(Seller obj) {            

            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
