using System;
using System.Collections.Generic;


namespace AccountManager
{
	public enum AccountPermissionTypes
	{
		Customer,
		Chef,
		Admin
	}


	public static class Accounts
	{
		public static Account SessionAccount = null;

		public static Dictionary<string, Account> AccountsDict = new Dictionary<string, Account>()
		{
			{ "admin", new Account("admin", "admin", AccountPermissionTypes.Admin) },
			{ "chef", new Account("chef", "chef", AccountPermissionTypes.Chef) }
		};

		public static bool SignIn(string username, string password)
		{
			if (AccountsDict.ContainsKey(username))
            {
				Account acc = AccountsDict[username];
				if (acc.CheckLoginCredentials(username, password))
                {
					SessionAccount = acc;
					return true;
                }
			}
			return false;
            //return AccountsDict.ContainsKey(username) ? AccountsDict[username].CheckLoginCredentials(username, password) : false;
        }

        public static bool SignUp(string username, string password, AccountPermissionTypes permissionType)
		{
			if (!AccountsDict.ContainsKey(username))
			{
				Account newAccount = new Account(username, password, permissionType);
				AccountsDict.Add(username, newAccount);
				return true;
			}
			return false;
		}

		public static void SignOut()
        {
			SessionAccount = null;
        }
	}


	public class Account
	{
		public string Username { get; }
		private string Password;
		public AccountPermissionTypes PermissionType { get; }

		public Account(string username, string password, AccountPermissionTypes permissionType)
		{
			Username = username;
			Password = password;
			PermissionType = permissionType;
		}

		public bool CheckLoginCredentials(string username, string password)
		{
			return Username == username && Password == password;
		}
	}
}