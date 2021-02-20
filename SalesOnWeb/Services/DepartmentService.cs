using System.Linq;
using System.Collections.Generic;
using SalesOnWeb.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesOnWeb.Services {
    public class DepartmentService {
        private readonly SalesOnWebContext _context;

        public DepartmentService(SalesOnWebContext context) {
            _context = context;
        }

        public async Task<List<Department>> FindAllAsync() {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
