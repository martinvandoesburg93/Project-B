using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using AccountManager;
using DishManager;


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


		public void Navigate(List<(string, Action)> options)
        {
			for (int i = 0; i < options.Count; i++)
            {
				string indicator = $"{i + 1}.".PadRight(3);
				WriteLine($"{indicator} {options[i].Item1}");
            }
			DisplayFooter();

			Write("\nEnter the number to navigate: ");
			
			// input >= 1 && input <= length
			
			try
            {
				string input = ReadLine();
				int chosenOption = Int32.Parse(input) - 1;
				if (chosenOption >= 0 && chosenOption < options.Count)
                {
					WriteLine();
				}
				Clear();
				options[chosenOption].Item2();
			} 
			catch (Exception)
            {
				WriteLine("Invalid input given.");
				Navigate(options); // If user doesn't give a digit, just display the options again
            }
		
			
		}



		public void DisplayFooter()
        {
			WriteLine(RepeatString("=", Width));
		}


		/* DISPLAY MENU METHODS */

		public void DisplayWelcomeScreen()
        {
			DisplayHeader("Login and Registration");
			Navigate(new List<(string, Action)> 
			{ 
				("Login", DisplayLoginScreen), 
				("Register", DisplayRegisterScreen)
			});
		}

		public void DisplayLoginScreen()
        {
			DisplayHeader("LOGIN SCREEN");

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


			Account acc = Accounts.SessionAccount;
			AccountPermissionTypes permission = Accounts.SessionAccount.PermissionType;

			var options = new List<(string, Action)>
			{
				("View dish menu", DisplayDishMenu),
				("Table reservations", Dummy),
				("Contact us", DisplayContactScreen)
			};

			if (permission == AccountPermissionTypes.Chef || permission == AccountPermissionTypes.Admin)
            {
				options.Add(("Manage dishes", Dummy));
				options.Add(("Update daily/monthly dish", Dummy));
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
			DisplayHeader("Dish Menu items");
			WriteLine("DishCode  Name  Type  Price  Description");
			foreach (var item in Dish.DishDict)
				WriteLine($"{item.Key}  {item.Value.Name}  {item.Value.Type}  {item.Value.Price}  {item.Value.Desc}");
			DisplayFooter();

			Write("Would you like to filter by (meat/fish/vega/drink)? leave blank to go back: ");
			string input = ReadLine();
			while (input != "")
            {
				if (string.Equals(input, "meat"))
				{
					Clear();
					DisplayHeader("Dish Menu items");
				}
				else if (string.Equals(input, "fish"))
				{

				}
				else if (string.Equals(input, "vega"))
				{

				}
				else if (string.Equals(input, "drink"))
				{

				}
				else
                {
					Write("Would you like to filter by (meat/fish/vega/drink)? leave blank to go back: ");
					input = ReadLine();
				}
			}
			DisplayMainScreen();
		}
		

		public void DisplayContactScreen()
        {
			DisplayHeader("Contact us");
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

		public void DisplayLogoutScreen()
		{
			Accounts.SignOut();
			DisplayWelcomeScreen();
		}

		public void Dummy() { }


















	}
}
