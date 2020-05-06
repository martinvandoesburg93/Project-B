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
		public int Amount;
		public static decimal Earned = 0;
		static Dictionary<string, decimal> EarnedDict = new Dictionary<string, decimal>();

		public Dish() { }

		public Dish(int dishcode, string name, string desc, Type type, decimal price, int amount)
		{
			DishCode = dishcode;
			Name = name;
			Desc = desc;
			Type = type;
			Price = price;
			Amount = amount;
		}

		public Dish(int dishcode, string name, string desc, string type, decimal price, int amount)
		{
			DishCode = dishcode;
			Name = name;
			Desc = desc;
			Type = (Type)Enum.Parse(typeof(Type), type);
			Price = price;
			Amount = amount;
		}
	}

    enum Type
    {

        Vega,
        Vis,
        Vlees,
		Fris,
		Koffie,
		Alcohol,
		Sterk
    }
}
