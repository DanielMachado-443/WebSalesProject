using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesOnWeb.Models;

namespace SalesOnWeb.Services {
    public class SellerService {
        private readonly SalesOnWebContext _context;

        public SellerService(SalesOnWebContext context) {
            _context = context;
        }

        public List<Seller> FindAll() {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj) {

            obj.Department = _context.Department.First(); // << in order to prevent the error that would occur in case of the obj (Seller) not having a Department on the instantiantion step

            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
