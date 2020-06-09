using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using AccountManager;
using DishManager;
using System.Globalization;

namespace TUI
{
	public class TextUserInterface
	{

		private int Width = 100;

		public TextUserInterface()
		{
		}

		/* AUXILIARY METHODS */
		private string RepeatString(string text, int n)
        {
			return string.Concat(Enumerable.Repeat(text, n));
		}

		public void DisplayHeader(string headerText)
        {
			WriteLine(RepeatString("=", Width));
			WriteLine(RepeatString(" ", Width/2 - headerText.Length/2) + headerText);
			WriteLine(RepeatString("=", Width));
		}
		public void DisplayFooter()
		{
			WriteLine(RepeatString("=", Width));
		}

		public void Navigate(List<(string, Action)> options)
        {
			for (int i = 0; i < options.Count; i++)
            {
				string indicator = $" {i + 1}.".PadRight(4);
				WriteLine($"{indicator} {options[i].Item1}");
            }
			DisplayFooter();

			Write("\nEnter the number to navigate: ");

			int chosenOption;
			while(true)
            {
				try
                {
					string input = ReadLine();
					chosenOption = Int32.Parse(input) - 1;
					if (chosenOption < 0 || chosenOption >= options.Count)
						throw new IndexOutOfRangeException();
					
					Clear();
					options[chosenOption].Item2();
					return;
				}
				catch (Exception)
                {
					Write("Invalid input given. Enter the number to navigate: ");
				}
            }
		}



		/* DISPLAY MENU METHODS */

		public void DisplayWelcomeScreen()
        {
			DisplayHeader("Login and Registration");
			Navigate(new List<(string, Action)> 
			{ 
				("Login", DisplayLoginScreen), 
				("Register", DisplayRegisterScreen),
				("Exit", Exit)
			});
		}

		public void DisplayLoginScreen()
        {
			DisplayHeader("Login");

			Write("Enter your username: ");
			string username = ReadLine();

			Write("Enter your password: ");
			string password = ReadLine();

			var account = Accounts.SignIn(username, password);

			if (account)
            {
				Clear();
				WriteLine($"Logged in as {Accounts.SessionAccount.Username}");
				DisplayMainScreen();
			} 
			else
            {
				Clear();
				WriteLine("Login unsuccessful");
				DisplayWelcomeScreen();
			}
		}

		public void DisplayRegisterScreen()
		{
			DisplayHeader("Registration Screen");

			Write("Enter a username: ");
			string username = ReadLine();

			Write("Enter a password: ");
			string password = ReadLine();

			if (Accounts.SignUp(username, password, AccountPermissionTypes.Customer))
            {
				Clear();
				WriteLine("Registration successful");
            }
			else
            {
				Clear();
				WriteLine("Registration unsuccesful - username is already taken");
            }
			DisplayWelcomeScreen();
		}

		public void DisplayMainScreen()
        {
			DisplayHeader("Main Menu");

			AccountPermissionTypes permission = Accounts.SessionAccount.PermissionType;

			var options = new List<(string, Action)>
			{
				("View dish menu", DisplayDishMenu),
				("View daily/monthly dish", DisplayDailyMonthlyDishScreen),
				("Table reservations", Dummy),
				("Contact us", DisplayContactScreen)
			};

			if (permission == AccountPermissionTypes.Chef || permission == AccountPermissionTypes.Admin)
            {
				options.Add(("Manage dishes", DisplayDishManagementScreen));
				options.Add(("Update daily/monthly dish", DisplayUpdateDailyMonthlyDishScreen));
			}
			if (permission == AccountPermissionTypes.Admin)
            {
				options.Add(("View transactions", Dummy));
            }
			options.Add(("Logout", DisplayLogoutScreen));

			Navigate(options);
		}

		public void DisplayDishMenu()
		{
			string input = "";

			while (input != "exit")
            {
				if (string.Equals(input, ""))
				{
					Clear();
					DisplayHeader("Dish Menu Items");
					Dish.PrintDishes();
					DisplayFooter();
				}
				else if (string.Equals(input.ToLower(), "meat"))
				{
					Clear();
					DisplayHeader("Dish Menu Items (Meat)");
					Dish.PrintDishes(DishTypes.Meat);
					DisplayFooter();
				}
				else if (string.Equals(input.ToLower(), "fish"))
				{
					Clear();
					DisplayHeader("Dish Menu Items (Fish)");
					Dish.PrintDishes(DishTypes.Fish);
					DisplayFooter();
				}
				else if (string.Equals(input.ToLower(), "vega"))
				{
					Clear();
					DisplayHeader("Dish Menu Items (Vega)");
					Dish.PrintDishes(DishTypes.Vega);
					DisplayFooter();
				}
				else if (string.Equals(input.ToLower(), "drink"))
				{
					Clear();
					DisplayHeader("Dish Menu Items (Drink)");
					Dish.PrintDishes(DishTypes.Drink);
					DisplayFooter();
				}
				Write("\nWould you like to filter by (meat/fish/vega/drink)? leave blank for all, exit to return: ");
				input = ReadLine();
			}
			Clear();
			DisplayMainScreen();
		}

		public void DisplayDailyMonthlyDishScreen()
        {
			DisplayHeader("Daily and Monthly Dish");
			WriteLine(Dish.DailyDish != null ? $"The daily dish is: {Dish.DailyDish}" : "There is currently no daily dish.");
			WriteLine();
			WriteLine(Dish.MonthlyDish != null ? $"The monthly dish is: {Dish.MonthlyDish}" : "There is currently no monthly dish.");
			DisplayFooter();

			Write("\nPress enter to return...");
			ReadLine();
			Clear();
			DisplayMainScreen();
		}

		public void DisplayContactScreen()
        {
			DisplayHeader("Contact Us");
			WriteLine("You can contact using the following methods:");
			WriteLine("Phone: 06-12345678");
			WriteLine("Email: test@domain.com");
			WriteLine("Street: Wijnhaven 107");
			DisplayFooter();

			WriteLine ("Press enter to return to the main menu...");
			ReadLine();
			Clear();
			DisplayMainScreen();
        }

		public void DisplayDishManagementScreen()
        {
			DisplayHeader("Dish Management");
			Navigate(new List<(string, Action)>
			{
				("Add new dish", DisplayAddDishScreen),
				("Edit existing dish", DisplayEditDishScreen),
				("Remove existing dish", DisplayRemoveDishScreen),
				("Back to main menu", DisplayMainScreen)
			});
		}

		public void DisplayAddDishScreen()
        {
			DisplayHeader("Add a New Dish");
			Write("Give the name of the new dish: ");
			string name = ReadLine();

			Write("Give the type of the new dish (meat/fish/vega/drink): ");
			DishTypes type = (DishTypes)Enum.Parse(typeof(DishTypes), ReadLine(), true);

			double price = 0;
			do Write("Give the price of the new dish (ex. 1.99): ");
			while (!double.TryParse(ReadLine(), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out price));

			Write("Give the description of the new dish: ");
			string desc = ReadLine();

			Dish dish = new Dish(name, type, price, desc);

			Clear();
			WriteLine($"Succesfully added {dish}");
			DisplayDishManagementScreen();
		}

		public void DisplayEditDishScreen()
        {
			DisplayHeader("Edit a Dish");
			Dish.PrintDishes();
			DisplayFooter();

			Write("\nEnter the dishcode of the dish you want to edit: ");
			int code = -1;
			Int32.TryParse(ReadLine(), out code);

			if (!Dish.DishDict.ContainsKey(code))
            {
				Clear();
				WriteLine($"Invalid dish code");
				DisplayDishManagementScreen();
				return;
			}

			Dish dish = Dish.DishDict[code];
			bool changeFlag = false;

			Write($"\nChange the current name: {dish.Name}, or leave blank to keep it unchanged: ");
			string inputName = ReadLine();
			if (inputName != "")
            {
				dish.EditDish(name: inputName);
				WriteLine($"Changed the name to {inputName}");
				changeFlag = true;
			}

			Write($"\nChange the current type: {dish.Type} to (Meat/Fish/Vega/Drink), or leave blank to keep it unchanged: ");
			string inputType = ReadLine();
			if (inputType != "")
            {
				dish.EditDish(type: (DishTypes)Enum.Parse(typeof(DishTypes), inputType, true));
				WriteLine($"Changed the type to {inputType}");
				changeFlag = true;
			}

			Write($"\nChange the current price: {dish.Price} (ex. 1.99), or leave blank to keep it unchanged: ");
			double price = 0;
			double.TryParse(ReadLine(), out price);
			if (price != 0)
            {
				dish.EditDish(price: price);
				WriteLine($"Changed the price to {price}€");
				changeFlag = true;
			}

			Write($"\nChange the current description: {dish.Desc}, or leave blank to keep it unchanged: ");
			string inputDesc = ReadLine();
			if (inputDesc != "")
            {
				dish.EditDish(desc: inputDesc);
				WriteLine($"Changed the description to {inputDesc}");
				changeFlag = true;
			}

			Clear();
			WriteLine(changeFlag ? $"Succesfully edited {dish}" : $"{dish} remains unchanged");
			DisplayDishManagementScreen();
		}

		public void DisplayRemoveDishScreen()
        {
			DisplayHeader("Remove a Dish");
			Dish.PrintDishes();

			Write("Enter the dishcode of the dish you want to remove: ");
			int code = -1; 
			Int32.TryParse(ReadLine(), out code);
			if (Dish.DishDict.ContainsKey(code))
            {
				string dishInfo = Dish.DishDict[code].ToString();
				Dish.RemoveDish(code);
				Clear();
				WriteLine($"Removed item: {dishInfo}");
				DisplayDishManagementScreen();
            }
			else
            {
				Clear();
				WriteLine($"Invalid dish code");
				DisplayDishManagementScreen();
			}
		}

		public void DisplayUpdateDailyMonthlyDishScreen()
        {
			DisplayHeader("Update Daily and Monthly Dish");
			Dish.PrintDishes();
			DisplayFooter();

			WriteLine($"\n{(Dish.DailyDish is null ? "Daily dish has not been set yet." : "The daily dish is: " + Dish.DailyDish)}");
			Write("Enter the dish code to change the daily dish: ");
			int inputDaily = -1;
			Int32.TryParse(ReadLine(), out inputDaily);
			if (Dish.DishDict.ContainsKey(inputDaily))
            {
				Dish.DailyDish = Dish.DishDict[inputDaily];
				WriteLine($"Succesfully changed daily dish to: {Dish.DailyDish}");
            }
			else
            {
				WriteLine("Daily dish not altered.");
            }

			WriteLine($"\n{(Dish.MonthlyDish is null ? "Monthly dish has not been set yet." : "The monthly dish is: " + Dish.MonthlyDish)}");
			Write("Enter the dish code to change the monthly dish: ");
			int inputMonthly = -1;
			Int32.TryParse(ReadLine(), out inputMonthly);
			if (Dish.DishDict.ContainsKey(inputMonthly))
			{
				Dish.MonthlyDish = Dish.DishDict[inputMonthly];
				WriteLine($"Succesfully changed monthly dish to: {Dish.MonthlyDish}");
			}
			else
			{
				WriteLine("Monthly dish not altered.");
			}

			Write("\n\nPress enter to return...");
			ReadLine();
			Clear();
			DisplayMainScreen();
		}

		public void DisplayLogoutScreen()
		{
			Accounts.SignOut();
			DisplayWelcomeScreen();
		}

		public void Exit()
        {
			// Savely exit, storing every changed and added dish, reservations etc
        }

		public void Dummy() { }


















	}
}
