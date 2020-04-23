using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class Dish 
    {
        static Dictionary<int, Dish> DishesDict = new Dictionary<int, Dish>();
        public int DishCode;
        public string Name;
        public string Desc;
        public string Type;
        public decimal Price;
        public int Amount;
        public static decimal Earned = 0;
        static Dictionary<string, decimal> EarnedDict = new Dictionary<string, decimal>();

        public Dish(int dishcode, string name, string desc, string type, decimal price, int amount)
        {
            DishCode = dishcode;
            Name = name;
            Desc = desc;
            Type = type;
            Price = price;
            Amount = amount;
            DishesDict.Add(dishcode, this);
        }


        // Enter the dishcode and get the name, type, price and code //
		public static void GetInfo(int dishcode)
        {
            Dish dish = DishesDict[dishcode];
            Console.WriteLine("Name: " + dish.Name + ", Type: " + dish.Type + ", Price: €" + dish.Price + ", Code: " + dishcode);
            if(dish.Desc != "")
                Console.WriteLine("Description: " + dish.Desc);
        }


        // Enter the type and get the names and codes //
        public static void GetTypeList(string type)
		{
            foreach(var item in DishesDict)
                if(item.Value.Type == type)
                    Console.WriteLine("Name: " + item.Value.Name.PadRight(30) + "Type: " + item.Value.Type);
		}


        // Get codes from every dish //
        public static void GetCodes()
        {
            foreach (var item in DishesDict)
                Console.WriteLine(item.Key);
        }


        // Get stocks from every dish //
        public static void GetStocks()
        {
            foreach (var item in DishesDict)
                Console.WriteLine("Name: " + item.Value.Name.PadRight(30) + ", Code: " + item.Value.DishCode + ", Amount: " + item.Value.Amount);
        }


        // Enter dishcode and amount and check if there is enough stock, update money earned, the amount left //
        public static Dish BuyDish(int dishcode, int amount)
		{
            CheckStock(dishcode);
            Dish dish = DishesDict[dishcode];
            if(dish.Amount - amount > 0)
			{
                dish.Amount -= amount;
                Dish.Earned += OrderPrice(dishcode, amount);
                return dish;
			}
            Console.WriteLine("There are only " + dish.Amount + " units to sell of " + dish.Name);
            return dish;
        }


        // Enter dishcode and check if new stock has to be bought or not //
        public static void CheckStock(int dishcode)
        {
            Dish dish = DishesDict[dishcode];
            if (dish.Amount <= 8)
                Console.WriteLine("There are only " + dish.Amount + " units left of " + dish.Name + ". Buy new units soon!");
            else
                Console.WriteLine("There are still " + dish.Amount + " units left of " + dish.Name);
        }


        // Gets called to calculate the price of the order and returns the total price //
        public static decimal OrderPrice(int dishcode, int amount)
		{
            Dish dish = DishesDict[dishcode];
            decimal TotalPrice = (dish.Price * amount);
            return TotalPrice;
		}

        // Enter a date and save the money earned that day in a dictionary with key:value pair date:money and prints the entire dict //
        public static void SaveEarned(string date)
		{
            EarnedDict.Add(date, Dish.Earned);
            Dish.Earned = 0.00m;
            foreach (var item in EarnedDict)
                Console.WriteLine("Date: " + item.Key + ", Earned: " + item.Value);
		}
            
    }


    public class Program
	{
        public static void Main()
		{

            /// Voor Gerechten ///          
            Dish BreadPlate = new Dish(101, "Bread Plate", "Warm bread with garlic butter, avioli and tapenade", "Vega", 5.50m, 24);
            Dish OnionSoup = new Dish(102, "French Onion Soup", "Homemade, gratinated with cheese", "Vega", 6.75m, 24);
            Dish SupremeNacho = new Dish(103, "Supreme Nacho's", "From the oven with jalapeno's, cheddar, tomato, onion, crême fraiche, homemade guacemole and spicy salsa", "Vega", 10.75m, 24);
            Dish AvocadoRoyale = new Dish(104, "Avocado Royale", "Shrimp cocktail from Dutch shrimps, coktailsauce and 1 / 2 avocado", "Vis", 10.90m, 24);
            Dish SmokeySalmon = new Dish(105, "Smokey Salmon", "Norwegian smoked salmon, spiced cheese and bread", "Vis", 11.50m, 24);
            Dish BuffaloWings = new Dish(106, "Buffalo Wings", "Chicken wings marinated in a smokey barbecue sauce", "Vlees", 12.50m, 24);
            Dish Carpaccio = new Dish(107, "Carpaccio", "Dressing of truffle mayonaise, lettuce, sundried tomato, parmesan and pine nuts", "Vlees", 11.50m, 24);
            Dish Chorizo = new Dish(108, "Chorizo", "Two spicy chorizo sausages with garlic sauce", "Vlees", 9.90m, 24);

            /// Vlees Gerechten ///
            Dish Entrecote = new Dish(201, "Entrecote", "Sirloin 300gr with a characteristic grease rim", "Vlees", 25.00m, 24);
            Dish HalfChicken = new Dish(202, "1/2 Chicken", "Grilled in the oven with the flavours: Spicy | Ketjap | BBQ", "Vlees", 12.00m, 24);
            Dish Chicken = new Dish(203, "1/1 Chicken", "Grilled in the oven with the flavours: Spicy | Ketjap | BBQ", "Vlees", 19.50m, 24);
            Dish MixedGrill = new Dish(204, "Mixed Grill", "Entrecote, spicy ribs, chicken fillet, chorizo and a rack of lamb", "Vlees", 25.00m, 24);
            Dish Tenderloin = new Dish(205, "Tenderloin", "Tournedos 300gr, the most tender part of the bovine", "Vlees", 29.50m, 24);
            Dish Ribeye = new Dish(206, "Ribeye", "Beef 300gr from the ribs, grained in fat", "Vlees", 26.00m, 24);
            Dish SatansRibs = new Dish(207, "Satans Ribs", "Made in the charcoal grill with the flavours: Spicy | Ketjap | BBQ | Sweet", "Vlees", 17.50m, 24);
            Dish TenderloinSkewer = new Dish(208, "Tenderloin Skewer", "With home made saté sauce", "Vlees", 23.50m, 24);
            Dish ChickenSkewer = new Dish(209, "Chicken Skewer", "With home made saté sauce", "Vlees", 16.00m, 24);
            Dish SilenceOfTheLambs = new Dish(210, "Silence of the Lambs", "Rack of lamb 300gr from New-Zealand", "Vlees", 23.50m, 24);
            Dish TBone = new Dish(211, "T-Bone", "T-Bone steak 600gr, for the real carnivores", "Vlees", 31.50m, 24);
            Dish RoyaleWithCheese = new Dish(212, "Royale with Cheese", "200gr Black angus burger with cheddar, lettuce, tomato, pickles and crispy bacon", "Vlees", 13.50m, 24);

            /// Vis Gerechten ///
            Dish AtlanticSalmon = new Dish(213, "Atlantic Salmon", "Salmon fillet out of the charcoal grill", "Vis", 19.00m, 24);
            Dish Gamba = new Dish(214, "Gamba's", "Gamba's out of the charcoal grill with garlic sauce", "Vis", 20.50m, 24);
            Dish MixedFishGrill = new Dish(215, "Mixed Fish Grill", "Gamba's, salmon fillet, tuna steak and Norwegian smoked salmon", "Vis", 25.00m, 24);
            Dish OceanKing = new Dish(216, "Ocean King", "Tuna steak out of the charcoal grill", "Vis", 19.00m, 24);

            /// Vegetarische Gerechten ///
            Dish GreenWrap = new Dish(217, "Green Wrap", "Baked vegetables and cheddar cheese in a tortilla wrap", "Vega", 14.00m, 24);
            Dish Ravioli = new Dish(218, "Ravioli Ricotta Spinach", "Ravioli filled with ricotta cheese, spinach and pesto-cream sauce", "Vega", 13.00m, 24);

            /// Na Gerechten ///
            Dish ChocolateBrownie = new Dish(301, "Pure Chocolate Brownie", "With passion fruit sorbet and salted caramel", "Vega", 8.50m, 24);
            Dish CheeseBoard = new Dish(302, "Cheese Board", "Varied cheeseboard with 3 kinds of cheese, fruit loaf and homemade chutney", "Vega", 8.50m, 24);
            Dish IceFondue = new Dish(303, "Ice Fondue", "With pineapple and banana, sugar loaf, marshmellows, vanilla mascarpone cream, chocolate sauce and nuts", "Vega", 19.00m, 24);
            Dish DameBlanche = new Dish(304, "Dame Blanche", "3 scoops of vanilla ice cream with hot chocolate sauce", "Vega", 6.50m, 24);
            Dish ChildrenIceCream = new Dish(305, "Children's Ice Cream", "2 scoops of vanilla ice cream with decoration and a surprise", "Vega", 4.00m, 24);
            Dish CheesecakeMilkshake = new Dish(306, "Cheesecake & Milkshake", "New York style cheesecake with curd and a homemade milkshake", "Vega", 8.50m, 24);
            Dish CoffeeApplepie = new Dish(307, "Coffee with Applepie", "A cup of coffee with homemade applepie", "Vega", 6.50m, 24);


            /// Frisdranken ///
            Dish Cola = new Dish(401, "Cola", "Regular/Light/Zero", "Fris", 2.50m, 50);
            Dish Sprite = new Dish(402, "Sprite", "", "Fris", 2.20m, 50);
            Dish Fanta = new Dish(403, "Fanta", "", "Fris", 2.20m, 50);
            Dish Cassis = new Dish(404, "Cassis", "", "Fris", 2.20m, 50);
            Dish IceTea = new Dish(405, "Ice Tea", "Peach/Lemon", "Fris", 2.20m, 50);
            Dish JusdOrange = new Dish(406, "Jus d'Orange", "", "Fris", 2.80m, 50);
            Dish Water = new Dish(407, "Water", "", "Fris", 1.50m, 50);
            Dish Tea = new Dish(408, "Tea", "Many different flavours", "Fris", 2.20m, 50);

            /// Koffie ///
            Dish Black = new Dish(501, "Black Coffee", "", "Koffie", 2.30m, 50);
            Dish Cappuccino = new Dish(502, "Cappuccino", "", "Koffie", 3.00m, 50);
            Dish Espresso = new Dish(503, "Espresso", "", "Koffie", 2.75m, 50);
            Dish DoubleEspresso = new Dish(504, "Double Espresso", "", "Koffie", 4.25m, 50);
            Dish CaffeLatte = new Dish(505, "Caffé Latte", "", "Koffie", 3.75m, 50);
            Dish LatteMacchiato = new Dish(506, "Latte Macchiato", "", "Koffie", 3.75m, 50);
            Dish IrishCoffee = new Dish(507, "Irish Coffee", "", "Koffie", 6.95m, 50);
            Dish SpanishCoffee = new Dish(508, "Spanish Coffee", "", "Koffie", 6.95m, 50);
            Dish ItalianCoffee = new Dish(509, "Italian Coffee", "", "Koffie", 6.95m, 50);
            Dish FrenchCoffee = new Dish(510, "French Coffee", "", "Koffie", 6.95m, 50);
            Dish BaileysCoffee = new Dish(511, "Baileys Coffee", "", "Koffie", 6.95m, 50);

            /// Alcoholische Dranken ///
            Dish RedWine = new Dish(601, "Red Wine", "", "Alcohol", 2.60m, 50);
            Dish WhiteWine = new Dish(602, "White Wine", "", "Alcohol", 2.50m, 50);
            Dish RoseWine = new Dish(603, "Rosé Wine", "", "Alcohol", 2.30m, 50);
            Dish GrolschAmsterdamSmall = new Dish(604, "Grolsch Amsterdammer 0,25L", "", "Alcohol", 2.75m, 50);
            Dish GrolschAmsterdam = new Dish(605, "Grolsch Amsterdammer 0,5L", "", "Alcohol", 5.50m, 50);
            Dish GrolschWeizenSmall = new Dish(606, "Grolsch Weizen 0.3L", "", "Alcohol", 4.75m, 50);
            Dish GrolschWeizen = new Dish(607, "Grolsch Weizen 0.5L", "", "Alcohol", 7.85m, 50);
            Dish GrimmbergenBlond = new Dish(608, "Grimmbergen Blond", "", "Alcohol", 4.35m, 50);
            Dish GrimmbergenDouble = new Dish(609, "Grimmbergen Double", "", "Alcohol", 4.35m, 50);
            Dish Guiness = new Dish(610, "Guiness", "", "Alcohol", 4.55m, 50);
            Dish HertogJan = new Dish(611, "Hertog Jan", "", "Alcohol", 3.40m, 50);
            Dish Heineken = new Dish(612, "Heineken", "", "Alcohol", 3.50m, 50);
            Dish Jupiler = new Dish(613, "Jupiler", "", "Alcohol", 3.40m, 50);
            Dish Kornuit = new Dish(614, "Kornuit", "", "Alcohol", 2.75m, 50);

            /// Sterke Drank ///
            Dish Rum = new Dish(701, "Rum", "", "Sterk", 6.00m, 50);
            Dish Vodka = new Dish(702, "Vodka", "", "Sterk", 5.50m, 50);
            Dish Bacardi = new Dish(703, "Bacardi", "", "Sterk", 5.50m, 50);
            Dish Whiskey = new Dish(704, "Whiskey", "", "Sterk", 5.50m, 50);
            Dish Ouzo = new Dish(705, "Ouzo", "", "Sterk", 4.25m, 50);
            Dish Safari = new Dish(706, "Safari", "", "Sterk", 3.85m, 50);
            Dish Malibu = new Dish(707, "Malibu", "", "Sterk", 3.85m, 50);
            Dish PinaColada = new Dish(708, "Pina Colada", "", "Sterk", 3.85m, 50);
            Dish Amaretto = new Dish(709, "Amaretto di Saronno", "", "Sterk", 4.25m, 50);
            Dish Baileys = new Dish(710, "Baileys", "", "Sterk", 4.25m, 50);
            Dish Sambuca = new Dish(711, "Sambuca", "", "Sterk", 3.85m, 50);
            Dish TiaMaria = new Dish(712, "Tia Maria", "", "Sterk", 4.25m, 50);
            Dish Boswandeling = new Dish(713, "Boswandeling", "", "Sterk", 3.85m, 50);
            Dish Dropshot = new Dish(714, "Dropshot", "", "Sterk", 3.85m, 50);
            Dish Blueberry = new Dish(715, "Blueberry", "", "Sterk", 3.85m, 50);
            Dish Licor43 = new Dish(716, "Licor 43", "", "Sterk", 4.25m, 50);


            // Enter the dishcode and get the name, type, price and code //
            Dish.GetInfo(204);

            // Enter the type and get the names and codes //
            Dish.GetTypeList("Vlees");

            // Get codes from every dish //
            Dish.GetCodes();

            // Get stocks from every dish //
            Dish.GetStocks();

            // Enter dishcode and amount and check if there is enough stock, update money earned, the amount left //
            Dish.BuyDish(204, 3);

            // Enter dishcode and check if new stock has to be bought or not //
            Dish.CheckStock(204);

            // Enter a date (dd-mm-yyyy) and save the money earned that day in a dictionary with key:value pair date:money //
            Dish.SaveEarned("21-04-2020");



        }
    }



} 




