using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaDane
{
    public class Movie
    {
        private string name;
        private decimal price;
        private List<Category> categories;    

        public decimal Price
        {
            get { return price; }
            set { if (value < 0) throw new ArgumentException("Price must be greater than zero!"); else price = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public List<Category> Categories
        {
            get { return categories; }
        }

        public Movie(string name, decimal price)
        {
            this.name = name;
            this.Price = price;
            this.categories = new List<Category>();
        }

        public override string ToString()
        {
            return String.Format("{0}", name);
        }
    }
}
