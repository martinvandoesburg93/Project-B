using AccountManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TableReservationManager
{
	public class Table
	{
		public int TableNumber { get; }
		public int Size { get; }
		public static List<Table> Tables = new List<Table>();

		public Table(int size)
		{
			TableNumber = Tables.Count + 1;
			Size = size;
			Tables.Add(this);
		}

        public override string ToString()
        {
			return $"Table {TableNumber} ({Size}p)";
        }

    }

	public enum TimeSlots
    {
		FirstSlot,
		SecondSlot
    }

	public static class TimeSlotsExtension
    {
		public static string Time(this TimeSlots TimeSlot)
        {
			return TimeSlot == TimeSlots.FirstSlot ? "16.00 - 19.00" : (TimeSlot == TimeSlots.SecondSlot ? "20.00 - 23.00" : "N/A");
		}
    }


	public class TableReservation
    {
		public Table Table;
		public Account Account;
		public DateTime Date;
        public TimeSlots TimeSlot;
		public static Dictionary<DateTime, List<TableReservation>> Reservations = new Dictionary<DateTime, List<TableReservation>>();

		public TableReservation(Table table, Account account, DateTime date, TimeSlots timeSlot)
        {
			Table = table;
			Account = account;
			Date = date;
			TimeSlot = timeSlot;
			if (!Reservations.ContainsKey(date))
				Reservations[date] = new List<TableReservation>();
			Reservations[date].Add(this);
        }

        public override string ToString()
        {
			return $"{Date:dd/MM} {TimeSlot.Time()}  - Table {Table.TableNumber} ({Table.Size}p)";
        }

		public static void RemoveTableReservation(TableReservation reservation)
        {
			Reservations[reservation.Date].Remove(reservation);
		}


        public static List<DateTime> GetNextNDays(int numberOfDays)
        {
			List<DateTime> result = new List<DateTime>();
			for (int i = 0; i < numberOfDays; i++)
				result.Add(DateTime.Today.AddDays(i));
			return result;
        }

		public static Dictionary<Table, List<TimeSlots>> TableAvailability(DateTime date)
        {
			Dictionary<Table, List<TimeSlots>> result = new Dictionary<Table, List<TimeSlots>>();

			foreach (Table table in Table.Tables)
            {
				result[table] = new List<TimeSlots> { TimeSlots.FirstSlot, TimeSlots.SecondSlot };

				if (Reservations.ContainsKey(date))
					foreach (TableReservation reservation in Reservations[date])
						if (table == reservation.Table)
							result[table].Remove(reservation.TimeSlot);
            } 
			return result;
		}

		public static Dictionary<int, int> TableAvailabilityByTableSize(DateTime date)
        {
			Dictionary<int, int> res = new Dictionary<int, int>();
			foreach (KeyValuePair<Table, List<TimeSlots>> entry in TableAvailability(date))
            {
				if (!res.ContainsKey(entry.Key.Size))
					res[entry.Key.Size] = 0;
				res[entry.Key.Size] += entry.Value.Count;
            }
			return res;
		}

		public static Dictionary<DateTime, Dictionary<int, int>> GetAvailabilityUpcomingDays(int numberOfDays)
        {
			Dictionary<DateTime, Dictionary<int, int>> result = new Dictionary<DateTime, Dictionary<int, int>>();
			foreach (DateTime day in GetNextNDays(numberOfDays))
				result[day] = TableAvailabilityByTableSize(day);
			return result;
		}

		public static List<TableReservation> GetReservationsByAccount(Account acc)
        {
			List<TableReservation> result = new List<TableReservation>();
			foreach (KeyValuePair<DateTime, List<TableReservation>> entry in Reservations)
				foreach (TableReservation reservation in entry.Value)
					if (reservation.Account == acc)
						result.Add(reservation);
			return result;
		}





	}
}
