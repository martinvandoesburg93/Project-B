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

		public Dish(int dishcode, string name, DishTypes type, double price, string desc)
		{
			DishCode = dishcode;
			Name = name;
			Type = type;
			Price = price;
			Desc = desc;
			DishDict.Add(DishCode, this);
		}

		public static void InfoWithDishCode(int dishcode)
		{
			Dish dish = DishDict[dishcode];
			Write($"Code: {dish.DishCode}, Name: {dish.Name}, Desc: {dish.Desc}, Type: {dish.Type}, Price: {dish.Price}.");
		}


		public static void PrintAllDishes()
		{
			foreach (var item in DishDict)
				Write($"Code: {item.Key}, Name: {item.Value.Name}, Desc: {item.Value.Desc}, Type: {item.Value.Type}, Price: {item.Value.Price}.");
		}

		public static int GenerateDishCode()
		{
			int code = new Random().Next(1000, 9999);
			while (DishDict.ContainsKey(code))
				code = new Random().Next(1000, 9999);
			return code;
		}

		public static Dish CreateDishFromConsole()
		{
			int dishcode = GenerateDishCode();

			Write("Give the name of the new dish: ");
			string name = ReadLine();

			Write("Give the type of the new dish: ");
			DishTypes type = (DishTypes) Enum.Parse(typeof(DishTypes), ReadLine());

			double price = 0;
			do Write("Give the price of the new dish: ");
			while (!double.TryParse(ReadLine(), out price));

			Write("Give the description of the new dish: ");
			string desc = ReadLine();

			return new Dish(dishcode, name, type, price, desc);
		}

		public static void RemoveDish()
		{
			PrintAllDishes();
			Write("Enter the dishcode of the dish you want to remove: ");
			int dishcode = Int32.Parse(ReadLine());

			foreach (var item in DishDict)
				if (item.Key == dishcode)
					DishDict.Remove(dishcode);
		}

		public static void EditDish()
		{
			PrintAllDishes();
			Write("Enter the dishcode of the dish you want to edit: ");
			int code = Int32.Parse(ReadLine());
			Dish dish = DishDict[code];

			Write($"Change the current name: {dish.Name}, or leave blank to keep it unchanged: ");
			if (!(ReadLine() == null))
				dish.Name = ReadLine();

			Write($"Change the current type: {dish.Type}, or leave blank to keep it unchanged: ");
			if (!(ReadLine() == null))
				dish.Type = (DishTypes)Enum.Parse(typeof(DishTypes), ReadLine());

			double price = 0;
			do Write($"Change the current price: {dish.Price}, or leave blank to keep it unchanged: ");
			while (!double.TryParse(ReadLine(), out price));
			dish.Price = price;

			Write($"Change the current description: {dish.Desc}, or leave blank to keep it unchanged: ");
			if (!(ReadLine() == null))
				dish.Desc = ReadLine();
		}
	}
}

				





























