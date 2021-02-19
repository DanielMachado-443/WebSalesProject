using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesOnWeb.Models {
    public class Seller {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]        
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; } // << Forcing the framework to garantee that this Department property won't be null, since 'int' is a struct type
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() {
        }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department) {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr) {
            Sales.Add(sr);
        }

        public void RemoveSalres(SalesRecord sr) {
            Sales.Remove(sr);
        }

        public double totalSales(DateTime initial, DateTime final) {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount); // << NICE!!!
        }
    }
}
