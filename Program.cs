using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;
using TUI;
using DishManager;
using System.Runtime.InteropServices;
using System.Linq;
using TableReservationManager;
using AccountManager;
using PaymentManager;
using OrderManager;

namespace Restaurant_Web_app
{
	public class Program
	{
		
		static void Main(string[] args)
		{
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            // FILL THE RESERVATION TABLE TIMESLOTS AND DISH MENU
            FillTables();
            FillDishMenu();


            // SET THE DAILY AND MONTHLY DISH RANDOMLY
            Random rand = new Random();
            var dishes = Dish.DishDict.Values.ToList();
            Dish.DailyDish = dishes[rand.Next(0, dishes.Count)];
            Dish.MonthlyDish = dishes[rand.Next(0, dishes.Count)];


            // INPUT FAKE RESERVATION
            Account acc = Accounts.AccountsDict["admin"];
            Table tab = Table.Tables[0];
            DateTime time = TableReservation.GetNextNDays(1)[0];
            new TableReservation(tab, acc, time, TimeSlots.FirstSlot);


            // INPUT FAKE ORDER
            Order order = new Order(new List<OrderItem>
            {
                new OrderItem(Dish.DishDict[2006], 2),
                new OrderItem(Dish.DishDict[4001], 1),
                new OrderItem(Dish.DishDict[6011], 1),
            }, DateTime.Today);
            order.MakeOrder(acc);


            // RUN TUI
            TextUserInterface TUI = new TextUserInterface();
            TUI.DisplayWelcomeScreen();
        }

        public static void FillTables()
        {
            for (int i = 0; i < 8; i++)
                new Table(2);
            for (int i = 0; i < 5; i++)
                new Table(4);
            for (int i = 0; i < 2; i++)
                new Table(6);
        }

		public static void FillDishMenu()
        {
            /// Voor Gerechten ///
            Dish BreadPlate = new Dish(1001, "Bread Plate", DishTypes.Vega, 5.50, "Warm bread with garlic butter, avioli and tapenade");
            Dish OnionSoup = new Dish(1002, "French Onion Soup", DishTypes.Vega, 6.75, "Homemade, gratinated with cheese");
            Dish SupremeNacho = new Dish(1003, "Supreme Nacho's", DishTypes.Vega, 10.75, "From the oven with jalapeno's, cheddar, tomato, onion, crême fraiche, homemade guacemole and spicy salsa");
            Dish AvocadoRoyale = new Dish(1004, "Avocado Royale", DishTypes.Fish, 10.90, "Shrimp cocktail from Dutch shrimps, coktailsauce and 1 / 2 avocado");
            Dish SmokeySalmon = new Dish(1005, "Smokey Salmon", DishTypes.Fish, 11.50, "Norwegian smoked salmon, spiced cheese and bread");
            Dish BuffaloWings = new Dish(1006, "Buffalo Wings", DishTypes.Meat, 12.50, "Chicken wings marinated in a smokey barbecue sauce");
            Dish Carpaccio = new Dish(1007, "Carpaccio", DishTypes.Meat, 11.50, "Dressing of truffle mayonaise, lettuce, sundried tomato, parmesan and pine nuts");
            Dish Chorizo = new Dish(1008, "Chorizo", DishTypes.Meat, 9.90, "Two spicy chorizo sausages with garlic sauce");

            /// Vlees Gerechten ///
            Dish Entrecote = new Dish(2001, "Entrecote", DishTypes.Meat, 25.00, "Sirloin 300gr with a characteristic grease rim");
            Dish HalfChicken = new Dish(2002, "1/2 Chicken", DishTypes.Meat, 12.00, "Grilled in the oven with the flavours: Spicy | Ketjap | BBQ");
            Dish Chicken = new Dish(2003, "1/1 Chicken", DishTypes.Meat, 19.50, "Grilled in the oven with the flavours: Spicy | Ketjap | BBQ");
            Dish MixedGrill = new Dish(2004, "Mixed Grill", DishTypes.Meat, 25.00, "Entrecote, spicy ribs, chicken fillet, chorizo and a rack of lamb");
            Dish Tenderloin = new Dish(2005, "Tenderloin", DishTypes.Meat, 29.50, "Tournedos 300gr, the most tender part of the bovine");
            Dish Ribeye = new Dish(2006, "Ribeye", DishTypes.Meat, 26.00, "Beef 300gr from the ribs, grained in fat");
            Dish SatansRibs = new Dish(2007, "Satans Ribs", DishTypes.Meat, 17.50, "Made in the charcoal grill with the flavours: Spicy | Ketjap | BBQ | Sweet");
            Dish TenderloinSkewer = new Dish(2008, "Tenderloin Skewer", DishTypes.Meat, 23.50, "With home made saté sauce");
            Dish ChickenSkewer = new Dish(2009, "Chicken Skewer", DishTypes.Meat, 16.00, "With home made saté sauce");
            Dish SilenceOfTheLambs = new Dish(2010, "Silence of the Lambs", DishTypes.Meat, 23.50, "Rack of lamb 300gr from New-Zealand");
            Dish TBone = new Dish(2011, "T-Bone", DishTypes.Meat, 31.50, "T-Bone steak 600gr, for the real carnivores");
            Dish RoyaleWithCheese = new Dish(2012, "Royale with Cheese", DishTypes.Meat, 13.50, "200gr Black angus burger with cheddar, lettuce, tomato, pickles and crispy bacon");

            /// Vis Gerechten ///
            Dish AtlanticSalmon = new Dish(2013, "Atlantic Salmon", DishTypes.Fish, 19.00, "Salmon fillet out of the charcoal grill");
            Dish Gamba = new Dish(2014, "Gamba's", DishTypes.Fish, 20.50, "Gamba's out of the charcoal grill with garlic sauce");
            Dish MixedFishGrill = new Dish(2015, "Mixed Fish Grill", DishTypes.Fish, 25.00, "Gamba's, salmon fillet, tuna steak and Norwegian smoked salmon");
            Dish OceanKing = new Dish(2016, "Ocean King", DishTypes.Fish, 19.00, "Tuna steak out of the charcoal grill");

            /// Vegetarische Gerechten ///
            Dish GreenWrap = new Dish(2017, "Green Wrap", DishTypes.Vega, 14.00, "Baked vegetables and cheddar cheese in a tortilla wrap");
            Dish Ravioli = new Dish(2018, "Ravioli Ricotta Spinach", DishTypes.Vega, 13.00, "Ravioli filled with ricotta cheese, spinach and pesto-cream sauce");

            /// Na Gerechten ///
            Dish ChocolateBrownie = new Dish(3001, "Pure Chocolate Brownie", DishTypes.Vega, 8.50, "With passion fruit sorbet and salted caramel");
            Dish CheeseBoard = new Dish(3002, "Cheese Board", DishTypes.Vega, 8.50, "Varied cheeseboard with 3 kinds of cheese, fruit loaf and homemade chutney");
            Dish IceFondue = new Dish(3003, "Ice Fondue", DishTypes.Vega, 19.00, "With pineapple and banana, sugar loaf, marshmellows, vanilla mascarpone cream, chocolate sauce and nuts");
            Dish DameBlanche = new Dish(3004, "Dame Blanche", DishTypes.Vega, 6.50, "3 scoops of vanilla ice cream with hot chocolate sauce");
            Dish ChildrenIceCream = new Dish(3005, "Children's Ice Cream", DishTypes.Vega, 4.00, "2 scoops of vanilla ice cream with decoration and a surprise");
            Dish CheesecakeMilkshake = new Dish(3006, "Cheesecake & Milkshake", DishTypes.Vega, 8.50, "New York style cheesecake with curd and a homemade milkshake");
            Dish CoffeeApplepie = new Dish(3007, "Coffee with Applepie", DishTypes.Vega, 6.50, "A cup of coffee with homemade applepie");


            /// Frisdranken ///
            Dish Cola = new Dish(4001, "Cola", DishTypes.Drink, 2.50, "Regular/Light/Zero");
            Dish Sprite = new Dish(4002, "Sprite", DishTypes.Drink, 2.20, "");
            Dish Fanta = new Dish(4003, "Fanta", DishTypes.Drink, 2.20, "");
            Dish Cassis = new Dish(4004, "Cassis", DishTypes.Drink, 2.20, "");
            Dish IceTea = new Dish(4005, "Ice Tea", DishTypes.Drink, 2.20, "Peach/Lemon");
            Dish JusdOrange = new Dish(4006, "Jus d'Orange", DishTypes.Drink, 2.80, "");
            Dish Water = new Dish(4007, "Water", DishTypes.Drink, 1.50, "");
            Dish Tea = new Dish(4008, "Tea", DishTypes.Drink, 2.20, "Many different flavours");

            /// Koffie ///
            Dish Black = new Dish(5001, "Black Coffee", DishTypes.Drink, 2.30, "");
            Dish Cappuccino = new Dish(5002, "Cappuccino", DishTypes.Drink, 3.00, "");
            Dish Espresso = new Dish(5003, "Espresso", DishTypes.Drink, 2.75, "");
            Dish DoubleEspresso = new Dish(5004, "Double Espresso", DishTypes.Drink, 4.25, "");
            Dish CaffeLatte = new Dish(5005, "Caffé Latte", DishTypes.Drink, 3.75, "");
            Dish LatteMacchiato = new Dish(5006, "Latte Macchiato", DishTypes.Drink, 3.75, "");
            Dish IrishCoffee = new Dish(5007, "Irish Coffee", DishTypes.Drink, 6.95, "");
            Dish SpanishCoffee = new Dish(5008, "Spanish Coffee", DishTypes.Drink, 6.95, "");
            Dish ItalianCoffee = new Dish(5009, "Italian Coffee", DishTypes.Drink, 6.95, "");
            Dish FrenchCoffee = new Dish(5010, "French Coffee", DishTypes.Drink, 6.95, "");
            Dish BaileysCoffee = new Dish(5011, "Baileys Coffee", DishTypes.Drink, 6.95, "");

            /// Alcoholische Dranken ///
            Dish RedWine = new Dish(6001, "Red Wine", DishTypes.Drink, 2.60, "");
            Dish WhiteWine = new Dish(6002, "White Wine", DishTypes.Drink, 2.50, "");
            Dish RoseWine = new Dish(6003, "Rosé Wine", DishTypes.Drink, 2.30, "");
            Dish GrolschAmsterdamSmall = new Dish(6004, "Grolsch Amsterdammer 0,25L", DishTypes.Drink, 2.75, "");
            Dish GrolschAmsterdam = new Dish(6005, "Grolsch Amsterdammer 0,5L", DishTypes.Drink, 5.50, "");
            Dish GrolschWeizenSmall = new Dish(6006, "Grolsch Weizen 0.3L", DishTypes.Drink, 4.75, "");
            Dish GrolschWeizen = new Dish(6007, "Grolsch Weizen 0.5L", DishTypes.Drink, 7.85, "");
            Dish GrimmbergenBlond = new Dish(6008, "Grimmbergen Blond", DishTypes.Drink, 4.35, "");
            Dish GrimmbergenDouble = new Dish(6009, "Grimmbergen Double", DishTypes.Drink, 4.35, "");
            Dish Guiness = new Dish(6010, "Guiness", DishTypes.Drink, 4.55, "");
            Dish HertogJan = new Dish(6011, "Hertog Jan", DishTypes.Drink, 3.40, "");
            Dish Heineken = new Dish(6012, "Heineken", DishTypes.Drink, 3.50, "");
            Dish Jupiler = new Dish(6013, "Jupiler", DishTypes.Drink, 3.40, "");
            Dish Kornuit = new Dish(6014, "Kornuit", DishTypes.Drink, 2.75, "");

            /// Sterke Drank ///
            Dish Rum = new Dish(7001, "Rum", DishTypes.Drink, 6.00, "");
            Dish Vodka = new Dish(7002, "Vodka", DishTypes.Drink, 5.50, "");
            Dish Bacardi = new Dish(7003, "Bacardi", DishTypes.Drink, 5.50, "");
            Dish Whiskey = new Dish(7004, "Whiskey", DishTypes.Drink, 5.50, "");
            Dish Ouzo = new Dish(7005, "Ouzo", DishTypes.Drink, 4.25, "");
            Dish Safari = new Dish(7006, "Safari", DishTypes.Drink, 3.85, "");
            Dish Malibu = new Dish(7007, "Malibu", DishTypes.Drink, 3.85, "");
            Dish PinaColada = new Dish(7008, "Pina Colada", DishTypes.Drink, 3.85, "");
            Dish Amaretto = new Dish(7009, "Amaretto di Saronno", DishTypes.Drink, 4.25, "");
            Dish Baileys = new Dish(7010, "Baileys", DishTypes.Drink, 4.25, "");
            Dish Sambuca = new Dish(7011, "Sambuca", DishTypes.Drink, 3.85, "");
            Dish TiaMaria = new Dish(7012, "Tia Maria", DishTypes.Drink, 4.25, "");
            Dish Boswandeling = new Dish(7013, "Boswandeling", DishTypes.Drink, 3.85, "");
            Dish Dropshot = new Dish(7014, "Dropshot", DishTypes.Drink, 3.85, "");
            Dish Blueberry = new Dish(7015, "Blueberry", DishTypes.Drink, 3.85, "");
            Dish Licor43 = new Dish(7016, "Licor 43", DishTypes.Drink, 4.25, "");
        }
	}

	
}

