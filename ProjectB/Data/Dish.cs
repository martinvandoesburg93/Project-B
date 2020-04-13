using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB.Data
{
	class Dish
	{
        public int DishCode;
        public string Name;
        public string Desc;
        public Type Type;
        public decimal Price;

        public Dish() { }

        public Dish(int dishcode, string name, string desc, Type type, decimal price)
        {
            DishCode = dishcode;
            Name = name;
            Desc = desc;
            Type = type;
            Price = price;
        }
    }

    enum Type
    {
        Vega,
        Vis,
        Vlees,
    }
}
