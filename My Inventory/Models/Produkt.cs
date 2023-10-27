using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Inventory.Models
{
    public class Produkt
    {
        public int ID { get; set; }
        public string Produkt_Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Kategori_Name { get; set; }
    }
}
