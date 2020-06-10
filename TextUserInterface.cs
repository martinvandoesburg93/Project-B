using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using AccountManager;
using DishManager;
using System.Globalization;
using TableReservationManager;
using OrderManager;
using PaymentManager;

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



		/* WELCOME LOGIN/REGISTER SCREEN */

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



		/* MAIN MENU SCREEN */
		
		public void DisplayMainScreen()
        {
			DisplayHeader("Main Menu");

			AccountPermissionTypes permission = Accounts.SessionAccount.PermissionType;

			var options = new List<(string, Action)>
			{
				("View dish menu", DisplayDishMenu),
				("View daily/monthly dish", DisplayDailyMonthlyDishScreen),
				("Table reservations", DisplayReservationsMenuScreen),
				("Order takeaway", DisplayOrderMenuScreen),
				("Contact us", DisplayContactScreen)
			};

			if (permission == AccountPermissionTypes.Chef || permission == AccountPermissionTypes.Admin)
            {
				options.Add(("Manage dishes", DisplayDishManagementScreen));
				options.Add(("Update daily/monthly dish", DisplayUpdateDailyMonthlyDishScreen));
			}
			if (permission == AccountPermissionTypes.Admin)
            {
				options.Add(("View all transactions", DisplayAllReservationsScreen));
				options.Add(("View all orders", DisplayAllOrdersScreen));
            }
			options.Add(("Logout", DisplayLogoutScreen));

			Navigate(options);
		}



		/* DISH MENU */

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



		/* DAILY/MONTHLY DISH */ 

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



		/* CONTACT PAGE */

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



		/* TABLE RESERVATIONS */

		public void DisplayReservationsMenuScreen()
        {
			DisplayHeader("Table reservations");
			Navigate(new List<(string, Action)>
			{
				("Reserve a table", DisplayReserveTableScreen),
				("View my reservations", DisplayMyReservationsScreen),
				("Cancel my reservation", DisplayCancelReservationsScreen),
				("Return to main menu", DisplayMainScreen)
			});
		}

		public void DisplayReserveTableScreen()
        {
			DisplayHeader("Reserve a Table");
			Dictionary<DateTime, Dictionary<int, int>> spots = TableReservation.GetAvailabilityUpcomingDays(14);
			List<DateTime> days = TableReservation.GetNextNDays(14);

			int choiceCounter = 0;
			foreach(KeyValuePair<DateTime, Dictionary<int, int>> entry in spots)
            {
				string text = $"{++choiceCounter}. {entry.Key.ToString("dd/MM")}\t";
				foreach (KeyValuePair<int, int> innerEntry in entry.Value)
					text += $"{innerEntry.Value} spots (Table for {innerEntry.Key})\t";
				WriteLine(text);
            }
			DisplayFooter();
			WriteLine();

			int choice = -1;
			while (!(choice >= 1 && choice <= spots.Count))
            {
				Write("Choose a day (using the numbering): ");
				Int32.TryParse(ReadLine(), out choice);
			}

			DateTime chosenDay = days[choice - 1];

			Clear();
			DisplayHeader($"Reserve a Table ({chosenDay:dd/MM})");



			
			choiceCounter = 0;
			Dictionary<Table, List<TimeSlots>> tableSpots = TableReservation.TableAvailability(chosenDay);

			List<(Table, TimeSlots)> tableChoices = new List<(Table, TimeSlots)>();
			foreach(KeyValuePair<Table, List<TimeSlots>> entry in tableSpots)
            {
				string text = $"Table {entry.Key.TableNumber} ({entry.Key.Size}p)\t\t";
				foreach(TimeSlots slot in entry.Value)
                {
					tableChoices.Add((entry.Key, slot));
					text += $"{++choiceCounter}. {slot.Time()}\t";
                }
				WriteLine(text);
            }
			DisplayFooter();
			WriteLine();
			
			choice = -1;
			while (!(choice >= 1 && choice <= tableChoices.Count))
			{
				Write("Choose a timeslot (using the numbering): ");
				Int32.TryParse(ReadLine(), out choice);
			}
			(Table, TimeSlots) chosenTableSlot = tableChoices[choice - 1];
			WriteLine($"{chosenTableSlot.Item1} - {chosenTableSlot.Item2.Time()}");

			Clear();
			DisplayHeader("Reservation confirmation");
			WriteLine($"You have chosen Table {chosenTableSlot.Item1.TableNumber} ({chosenTableSlot.Item1.Size}p) " +
				$"at {chosenDay:dd/MM} {chosenTableSlot.Item2.Time()}");
			DisplayFooter();
			
			string inputConfirmation = "";
			while (inputConfirmation != "yes" && inputConfirmation != "no")
            {
				Write("Are you sure you would like to make the reservation? (yes/no): ");
				inputConfirmation = ReadLine();
			}
				

			if (inputConfirmation == "yes")
            {
				new TableReservation(chosenTableSlot.Item1, Accounts.SessionAccount, chosenDay, chosenTableSlot.Item2);
				Clear();
				WriteLine($"Reservation made for {chosenDay:dd/MM} at {chosenTableSlot.Item2.Time()}");
				DisplayReservationsMenuScreen();
            }
            else
            {
				Clear();
				WriteLine($"Reservation aborted");
				DisplayReservationsMenuScreen();
			}

		}

		public void DisplayMyReservationsScreen()
        {
			DisplayHeader("View my Reservations");

			List<TableReservation> reservations = TableReservation.GetReservationsByAccount(Accounts.SessionAccount);

			if (reservations.Count > 0)
            {
				foreach (TableReservation reservation in reservations)
					WriteLine(reservation);
			}
			else
            {
				WriteLine("You currently have made no reservations.");
            }
			DisplayFooter();

			Write("\nPress enter to return...");
			ReadLine();
			Clear();
			DisplayReservationsMenuScreen();
		}
		
		public void DisplayCancelReservationsScreen()
        {
			DisplayHeader("Cancel my Reservations");
			int numbering = 0;
			List<TableReservation> reservations = TableReservation.GetReservationsByAccount(Accounts.SessionAccount);
			foreach (TableReservation reservation in reservations)
				WriteLine($"{++numbering}. {reservation}");
			DisplayFooter();

			int chosenOption = 0;
			while (!(chosenOption > 0 && chosenOption <= reservations.Count))
            {
				Write("Select the reservation you would like to cancel (leave blank to abort): ");
				string input = ReadLine();
				if (input == "")
                {
					Clear();
					WriteLine($"No reservation has been cancelled");
					DisplayReservationsMenuScreen();
				}
				Int32.TryParse(input, out chosenOption);
            }
			DisplayFooter();
			WriteLine();
			TableReservation chosenReservation = reservations[chosenOption - 1];
			
			string inputConfirmation = "";
			while (inputConfirmation != "yes" && inputConfirmation != "no")
			{
				Write($"Are you sure you want to remove reservation {chosenReservation} (yes/no): ");
				inputConfirmation = ReadLine();
			}

			if (inputConfirmation == "yes")
			{
				string text = chosenReservation.ToString();
				TableReservation.RemoveTableReservation(chosenReservation);
				Clear();
				WriteLine($"Successfully cancelled reservation: {text}");
				DisplayReservationsMenuScreen();
			}
			else
			{
				Clear();
				WriteLine($"No reservation has been cancelled");
				DisplayReservationsMenuScreen();
			}
		}



		/* DISH ORDERING SCREEN */

		public void DisplayOrderMenuScreen()
        {
			DisplayHeader("Order Menu");
			Navigate(new List<(string, Action)>
			{
				("Place an order", DisplayOrderScreen),
				("View my orders", DisplayViewOrderScreen),
				("Return to main menu", DisplayMainScreen)
			});
        }

		public void DisplayOrderScreen()
        {
			DisplayHeader("Order TakeAway");
			Dish.PrintDishes();
			DisplayFooter();

			string inputDish = "";
			string inputAmount = "";

			int dishcode = 0;
			int amount = 0;
			List<OrderItem> orderItems = new List<OrderItem>();

			WriteLine("\nSelect the dishes you want to order. Type pay to continue, type exit to abort.\n");
			while (true)
            {
				Write("Select dish by code: ");
				inputDish = ReadLine();

				if (inputDish == "pay")
					break;
				else if (inputDish == "exit")
                {
					Clear();
					WriteLine("Order process aborted.");
					DisplayOrderMenuScreen();
                }

				Write("Enter amount: ");
				inputAmount = ReadLine();
				if (inputAmount == "pay")
					break;
				else if (inputAmount == "exit")
				{
					Clear();
					WriteLine("Order process aborted.");
					DisplayOrderMenuScreen();
				}

				Int32.TryParse(inputDish, out dishcode);
				Int32.TryParse(inputAmount, out amount);
				if (!Dish.DishDict.ContainsKey(dishcode) || amount == 0)
					continue;
				orderItems.Add(new OrderItem(Dish.DishDict[dishcode], amount));
			}
			Order order = new Order(orderItems, DateTime.Today);
			WriteLine($"\nYou have chosen the dishes:\n{order}\nThe total price is {order.ComputePrice()}€\n");


			string inputConfirmation = "";
			while (inputConfirmation != "yes" && inputConfirmation != "no")
			{
				Write($"Would you like to proceed to payment (yes/no): ");
				inputConfirmation = ReadLine();
			}

			if (inputConfirmation == "no")
			{
				Clear();
				WriteLine($"Order process aborted.");
				DisplayMainScreen();
			}
			DisplayFooter();

			Clear();
			DisplayHeader("Finishing up Order");

			WriteLine(" 1. Cash\n 2. Online\n");
			DisplayFooter();
			
			string input = "";
			while (input != "1" && input != "2")
            {
				Write("How would you like to pay?: ");
				input = ReadLine();
			}

			if (input == "2")
            {
				Clear();
				DisplayHeader("Finishing up Order - Online Payment");
				WriteLine(" 1. Master Card\n 2. Visa Card\n 3. IDeal");
				DisplayFooter();
				WriteLine();


				Dictionary<string, Payment> paymentMapping = new Dictionary<string, Payment>
				{
					{ "1", Payment.MasterCard },
					{ "2", Payment.Visa },
					{ "3", Payment.IDeal }
				};
				input = "";
				while (input != "1" && input != "2" && input != "3")
				{
					Write("Choose your payment option: ");
					input = ReadLine();
				}

				Payment paymentOption = paymentMapping[input];

				string paymentInfo = "";
				while (!paymentOption.IsValid(paymentInfo))
                {
					Write("Carefully enter your payment information (exit to abort): ");
					paymentInfo = ReadLine();

					if (paymentInfo == "exit")
                    {
						Clear();
						WriteLine("Order process aborted.");
						DisplayMainScreen();
					}

				}

			}

			input = "";
			while (input != "yes" && input != "no")
			{
				Write("Would you like to confirm your order (yes/no): ");
				input = ReadLine();
			}

			if (input == "yes")
            {
				order.MakeOrder(Accounts.SessionAccount);
				Clear();
				WriteLine("Order succesfully placed.");
				DisplayMainScreen();
            }
			else
            {
				Clear();
				WriteLine("Order process aborted.");
				DisplayMainScreen();
            }
		}


		public void DisplayViewOrderScreen()
        {
			DisplayHeader("My Orders");

			if (Order.AllOrders.ContainsKey(Accounts.SessionAccount))
				foreach (Order order in Order.AllOrders[Accounts.SessionAccount])
					WriteLine($"{order.DateOrdered:dd/MM} - {order.Info()}");
			else
				WriteLine("You have no orders");
			
				
			DisplayFooter();

			Write("\nPress enter to return...");
			ReadLine();
			Clear();
			DisplayOrderMenuScreen();
		}






		/* DISH MANAGEMENT SCREEN */

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



		/* ADMIN PAGE */

		public void DisplayAllReservationsScreen()
        {
			DisplayHeader("All Transactions");
			if (TableReservation.Reservations.Count > 0)
				foreach (KeyValuePair<DateTime, List<TableReservation>> entry in TableReservation.Reservations)
					foreach (TableReservation reservation in entry.Value)
						WriteLine($"{entry.Key:dd/MM} (by {reservation.Account.Username}) - {reservation}");
			else
				WriteLine("Currently no reservations have been made.");
			DisplayFooter();

			Write("\n\nPress enter to return...");
			ReadLine();
			Clear();
			DisplayMainScreen();
		}

		public void DisplayAllOrdersScreen()
        {
			DisplayHeader("All Orders");
			if (Order.AllOrders.Count > 0)
				foreach (KeyValuePair<Account, List<Order>> entry in Order.AllOrders)
					foreach (Order order in entry.Value)
						WriteLine($"Order by {entry.Key.Username} - {order.Info()}");
			else
				WriteLine("Currently no orders have been made.");
			DisplayFooter();

			Write("\n\nPress enter to return...");
			ReadLine();
			Clear();
			DisplayMainScreen();
		}



		/* LOGOUT */
		
		public void DisplayLogoutScreen()
		{
			Accounts.SignOut();
			DisplayWelcomeScreen();
		}

		public void Exit()
        {
			// TODO: Savely exit, storing every changed and added dish, reservations etc
        }
	}
}
