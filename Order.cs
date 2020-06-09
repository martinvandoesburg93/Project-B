using System;
using System.Collections.Generic;
using AccountManager;
using DishManager;


namespace OrderManager
{
	public class Order
	{
		private List<OrderItem> OrderItems;
		public DateTime DateOrdered { get; }
		public static Dictionary<Account, List<Order>> AllOrders = new Dictionary<Account, List<Order>>();

		public Order(List<OrderItem> orderItems, DateTime dateOrdered)
		{
			OrderItems = orderItems;
			DateOrdered = dateOrdered;
		}

        public override string ToString()
        {
			string text = "";
			foreach (OrderItem item in OrderItems)
				text += $"{item}\n";
			return text;
        }

		public string Info()
        {
			string text = $"({ComputePrice()}€) [";
			foreach (OrderItem item in OrderItems)
				text += $"{item}, ";
			return text + "]";
		}

        public double ComputePrice()
        {
			double total = 0;
			foreach (OrderItem item in OrderItems)
				total += item.Amount * item.Dish.Price;
			return total;
        }

		public void MakeOrder(Account acc)
        {
			if (!AllOrders.ContainsKey(acc))
				AllOrders[acc] = new List<Order>();
			AllOrders[acc].Add(this);
        }

	}

	public class OrderItem
    {
		public Dish Dish { get; }
		public int Amount { get; } 

		public OrderItem(Dish dish, int amount)
        {
			Dish = dish;
			Amount = amount;
        }

        public override string ToString()
        {
			return $"{Amount}x {Dish.Name}";
        }
    }

}
