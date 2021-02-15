using System;
using System.Linq;
using System.Collections.Generic;

namespace SalesOnWeb.Models {
    public class Department {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department() {
        }

        public Department(int id, string name) {
            Id = id;
            Name = name;
        }

        public void addSeller(Seller Seller) {
            Sellers.Add(Seller);
        }
        
        public double totalSales(DateTime initial, DateTime final) {
            return Sellers.Sum(seller => seller.totalSales(initial, final)); // <<< CRAZY!!! BEAULTIFUL
        }                                                                    // The Seller object of the Sellers List will call its method responsible for finding the totalSales in a given time(initial and final)
                                                                            //  And then will retain its result in the Linq Sum method to find all department sellers sales in THIS given time
    }
}
