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
			get => _Lang;
			set
			{
				_Lang = value;
				LanguageChanged?.Invoke(null, null);
			}
		}

		public static Dish GetDishByCode(int code) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load("Dishes.xml");
			XmlNode node = doc.SelectSingleNode($@"./Dishes/Dish[@type=""{code}""]");
			return new Dish(code, node.SelectSingleNode($"./Name/{_Lang}").InnerText, node.SelectSingleNode($"./Desc/{_Lang}").InnerText, (Type)Enum.Parse(typeof(Type), node.Attributes["type"].Value), decimal.Parse(node.Attributes["price"].Value));
		}
	}

	enum Language
	{
		eng,
		nl
	}
}
