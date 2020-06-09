using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using static System.Console;

namespace DishManager
{
	public enum DishTypes
	{
		Meat,
		Fish,
		Vega,
		Drink
	}

	public class Dish
	{
		public int DishCode;
		public string Name;
		public DishTypes Type;
		public double Price;
		public string Desc;
		public static Dictionary<int, Dish> DishDict = new Dictionary<int, Dish>();
		public static Dish DailyDish { get; set; }
		public static Dish MonthlyDish { get; set; }

		public Dish(int dishcode, string name, DishTypes type, double price, string desc)
		{
			DishCode = dishcode;
			Name = name;
			Type = type;
			Price = price;
			Desc = desc;
			DishDict.Add(DishCode, this);
		}

		public Dish(string name, DishTypes type, double price, string desc) : this(GenerateDishCode(), name, type, price, desc) { }

        public override string ToString()
        {
			return $"Dish<{Name},{Type},({Price}€),{(Desc.Length < 20 ? Desc : Desc.Substring(0, 20) + "...")}>";
        }

		public string Info()
        {
			return $" {DishCode,-6}{Name,-30}{Type,-10}{Price,-10:F2}{Desc}";
		}

		public static void PrintDishes(DishTypes? filter = null)
        {
			WriteLine($" {"Code",-6}{"Name",-30}{"Type",-10}{"Price",-10}Description");
			foreach (Dish dish in DishDict.Values)
            {
				if (filter == null || dish.Type == filter)
					WriteLine(dish.Info());
            }
        }

		public static int GenerateDishCode()
		{
			int code = new Random().Next(1000, 9999);
			while (DishDict.ContainsKey(code))
				code = new Random().Next(1000, 9999);
			return code;
		}

		public static void RemoveDish(int dishcode)
		{
			foreach (var item in DishDict)
				if (item.Key == dishcode)
					DishDict.Remove(dishcode);
		}

		public void EditDish(string name = "", DishTypes? type = null, double price = -1, string desc = null)
		{
			if (name != "")
				Name = name;
			if (type != null)
				Type = (DishTypes) type;
			if (price != -1)
				Price = price;
			if (desc != null)
				Desc = desc;
		}
	}
}
