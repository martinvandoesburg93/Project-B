using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Security.Principal;

namespace ProjectB.Data
{
	static class Data
	{
		public static event EventHandler<EventArgs> LanguageChanged;

		private static Language _Lang = Language.eng;

		public static Language Lang
		{
			//get = lamba
			get => _Lang;
			//set kan _lang wel veranderen
			set
			{
				_Lang = value;
				LanguageChanged?.Invoke(null, null);
			}
		}

		public static List<Dish> Dishes = new List<Dish>()
		{
			            /// Voor Gerechten ///          
        new Dish(101, "Bread Plate", "Warm bread with garlic butter, avioli and tapenade", "Vega", 5.50m, 24),
		new Dish(102, "French Onion Soup", "Homemade, gratinated with cheese", "Vega", 6.75m, 24),
		new Dish(103, "Supreme Nacho's", "From the oven with jalapeno's, cheddar, tomato, onion, crême fraiche, homemade guacemole and spicy salsa", "Vega", 10.75m, 24),
		new Dish(104, "Avocado Royale", "Shrimp cocktail from Dutch shrimps, coktailsauce and 1 / 2 avocado", "Vis", 10.90m, 24),
		new Dish(105, "Smokey Salmon", "Norwegian smoked salmon, spiced cheese and bread", "Vis", 11.50m, 24),
		new Dish(106, "Buffalo Wings", "Chicken wings marinated in a smokey barbecue sauce", "Vlees", 12.50m, 24),
		new Dish(107, "Carpaccio", "Dressing of truffle mayonaise, lettuce, sundried tomato, parmesan and pine nuts", "Vlees", 11.50m, 24),
		new Dish(108, "Chorizo", "Two spicy chorizo sausages with garlic sauce", "Vlees", 9.90m, 24),

		/// Vlees Gerechten ///
		new Dish(201, "Entrecote", "Sirloin 300gr with a characteristic grease rim", "Vlees", 25.00m, 24),
		new Dish(202, "1/2 Chicken", "Grilled in the oven with the flavours: Spicy | Ketjap | BBQ", "Vlees", 12.00m, 24),
		new Dish(203, "1/1 Chicken", "Grilled in the oven with the flavours: Spicy | Ketjap | BBQ", "Vlees", 19.50m, 24),
		new Dish(204, "Mixed Grill", "Entrecote, spicy ribs, chicken fillet, chorizo and a rack of lamb", "Vlees", 25.00m, 24),
		new Dish(205, "Tenderloin", "Tournedos 300gr, the most tender part of the bovine", "Vlees", 29.50m, 24),
		new Dish(206, "Ribeye", "Beef 300gr from the ribs, grained in fat", "Vlees", 26.00m, 24),
		new Dish(207, "Satans Ribs", "Made in the charcoal grill with the flavours: Spicy | Ketjap | BBQ | Sweet", "Vlees", 17.50m, 24),
		new Dish(208, "Tenderloin Skewer", "With home made saté sauce", "Vlees", 23.50m, 24),
		new Dish(209, "Chicken Skewer", "With home made saté sauce", "Vlees", 16.00m, 24),
		new Dish(210, "Silence of the Lambs", "Rack of lamb 300gr from New-Zealand", "Vlees", 23.50m, 24),
		new Dish(211, "T-Bone", "T-Bone steak 600gr, for the real carnivores", "Vlees", 31.50m, 24),
		new Dish(212, "Royale with Cheese", "200gr Black angus burger with cheddar, lettuce, tomato, pickles and crispy bacon", "Vlees", 13.50m, 24),

		/// Vis Gerechten ///
		new Dish(213, "Atlantic Salmon", "Salmon fillet out of the charcoal grill", "Vis", 19.00m, 24),
		new Dish(214, "Gamba's", "Gamba's out of the charcoal grill with garlic sauce", "Vis", 20.50m, 24),
		new Dish(215, "Mixed Fish Grill", "Gamba's, salmon fillet, tuna steak and Norwegian smoked salmon", "Vis", 25.00m, 24),
		new Dish(216, "Ocean King", "Tuna steak out of the charcoal grill", "Vis", 19.00m, 24),

		/// Vegetarische Gerechten ///
		new Dish(217, "Green Wrap", "Baked vegetables and cheddar cheese in a tortilla wrap", "Vega", 14.00m, 24),
		new Dish(218, "Ravioli Ricotta Spinach", "Ravioli filled with ricotta cheese, spinach and pesto-cream sauce", "Vega", 13.00m, 24),

		/// Na Gerechten ///
		new Dish(301, "Pure Chocolate Brownie", "With passion fruit sorbet and salted caramel", "Vega", 8.50m, 24),
		new Dish(302, "Cheese Board", "Varied cheeseboard with 3 kinds of cheese, fruit loaf and homemade chutney", "Vega", 8.50m, 24),
		new Dish(303, "Ice Fondue", "With pineapple and banana, sugar loaf, marshmellows, vanilla mascarpone cream, chocolate sauce and nuts", "Vega", 19.00m, 24),
		new Dish(304, "Dame Blanche", "3 scoops of vanilla ice cream with hot chocolate sauce", "Vega", 6.50m, 24),
		new Dish(305, "Children's Ice Cream", "2 scoops of vanilla ice cream with decoration and a surprise", "Vega", 4.00m, 24),
		new Dish(306, "Cheesecake & Milkshake", "New York style cheesecake with curd and a homemade milkshake", "Vega", 8.50m, 24),
		new Dish(307, "Coffee with Applepie", "A cup of coffee with homemade applepie", "Vega", 6.50m, 24),


		/// Frisdranken ///
		new Dish(401, "Cola", "Regular/Light/Zero", "Fris", 2.50m, 50),
		new Dish(402, "Sprite", "", "Fris", 2.20m, 50),
		new Dish(403, "Fanta", "", "Fris", 2.20m, 50),
		new Dish(404, "Cassis", "", "Fris", 2.20m, 50),
		new Dish(405, "Ice Tea", "Peach/Lemon", "Fris", 2.20m, 50),
		new Dish(406, "Jus d'Orange", "", "Fris", 2.80m, 50),
		new Dish(407, "Water", "", "Fris", 1.50m, 50),
		new Dish(408, "Tea", "Many different flavours", "Fris", 2.20m, 50),

		/// Koffie ///
		new Dish(501, "Black Coffee", "", "Koffie", 2.30m, 50),
		new Dish(502, "Cappuccino", "", "Koffie", 3.00m, 50),
		new Dish(503, "Espresso", "", "Koffie", 2.75m, 50),
		new Dish(504, "Double Espresso", "", "Koffie", 4.25m, 50),
		new Dish(505, "Caffé Latte", "", "Koffie", 3.75m, 50),
		new Dish(506, "Latte Macchiato", "", "Koffie", 3.75m, 50),
		new Dish(507, "Irish Coffee", "", "Koffie", 6.95m, 50),
		new Dish(508, "Spanish Coffee", "", "Koffie", 6.95m, 50),
		new Dish(509, "Italian Coffee", "", "Koffie", 6.95m, 50),
		new Dish(510, "French Coffee", "", "Koffie", 6.95m, 50),
		new Dish(511, "Baileys Coffee", "", "Koffie", 6.95m, 50),

		/// Alcoholische Dranken ///
		new Dish(601, "Red Wine", "", "Alcohol", 2.60m, 50),
		new Dish(602, "White Wine", "", "Alcohol", 2.50m, 50),
		new Dish(603, "Rosé Wine", "", "Alcohol", 2.30m, 50),
		new Dish(604, "Grolsch Amsterdammer 0,25L", "", "Alcohol", 2.75m, 50),
		new Dish(605, "Grolsch Amsterdammer 0,5L", "", "Alcohol", 5.50m, 50),
		new Dish(606, "Grolsch Weizen 0.3L", "", "Alcohol", 4.75m, 50),
		new Dish(607, "Grolsch Weizen 0.5L", "", "Alcohol", 7.85m, 50),
		new Dish(608, "Grimmbergen Blond", "", "Alcohol", 4.35m, 50),
		new Dish(609, "Grimmbergen Double", "", "Alcohol", 4.35m, 50),
		new Dish(610, "Guiness", "", "Alcohol", 4.55m, 50),
		new Dish(611, "Hertog Jan", "", "Alcohol", 3.40m, 50),
		new Dish(612, "Heineken", "", "Alcohol", 3.50m, 50),
		new Dish(613, "Jupiler", "", "Alcohol", 3.40m, 50),
		new Dish(614, "Kornuit", "", "Alcohol", 2.75m, 50),

		/// Sterke Drank ///
		new Dish(701, "Rum", "", "Sterk", 6.00m, 50),
		new Dish(702, "Vodka", "", "Sterk", 5.50m, 50),
		new Dish(703, "Bacardi", "", "Sterk", 5.50m, 50),
		new Dish(704, "Whiskey", "", "Sterk", 5.50m, 50),
		new Dish(705, "Ouzo", "", "Sterk", 4.25m, 50),
		new Dish(706, "Safari", "", "Sterk", 3.85m, 50),
		new Dish(707, "Malibu", "", "Sterk", 3.85m, 50),
		new Dish(708, "Pina Colada", "", "Sterk", 3.85m, 50),
		new Dish(709, "Amaretto di Saronno", "", "Sterk", 4.25m, 50),
		new Dish(710, "Baileys", "", "Sterk", 4.25m, 50),
		new Dish(711, "Sambuca", "", "Sterk", 3.85m, 50),
		new Dish(712, "Tia Maria", "", "Sterk", 4.25m, 50),
		new Dish(713, "Boswandeling", "", "Sterk", 3.85m, 50),
		new Dish(714, "Dropshot", "", "Sterk", 3.85m, 50),
		new Dish(715, "Blueberry", "", "Sterk", 3.85m, 50),
		new Dish(716, "Licor 43", "", "Sterk", 4.25m, 50),

	};
	}

	enum Language
	{
		eng,
		nl
	}
}
