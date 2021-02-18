using System.Linq;
using System.Collections.Generic;
using SalesOnWeb.Models;

namespace SalesOnWeb.Services {
    public class DepartmentService {
        private readonly SalesOnWebContext _context;

        public DepartmentService(SalesOnWebContext context) {
            _context = context;
        }

        public List<Department> FindAll() {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
