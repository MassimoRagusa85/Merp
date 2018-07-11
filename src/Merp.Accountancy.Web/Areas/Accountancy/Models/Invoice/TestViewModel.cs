using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merp.Accountancy.Web.Areas.Accountancy.Models.Invoice
{
    public class TestViewModel
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public int Anni { get; set; }

        public Indirizzo Address { get; set; }
        public List<Item> Items { get; set; }

        public class Indirizzo
        {
            public string Via { get; set; }
            public int Civico { get; set; }
        }
        public class Item
        {
            public string Id { get; set; }
            public int Qty { get; set; }
        }
    }
}
