

// ReSharper disable UnusedAutoPropertyAccessor.Local
using System.Collections.Generic;
using System.Diagnostics;

namespace Lobster.Test
{
	public static class Users
	{
		private const string BaseMailPrefix = "";

		private const string EmailProvider = "";

		private const string Password = "";

		public static User Activated { get; private set; }

		public static User Registered { get; private set; }

		public static User FacebookFull { get; private set; }

		public static User FacebookNoEmail { get; private set; }

		public static User NotRegistered { get; private set; }

		public static User NotEmail { get; private set; }

		public static User EmptyEmail { get; private set; }

		public static User EmptyPassword { get; private set; }

		public static User WrongPassword { get; private set; }

		public static User NoOne { get; private set; }

		private static List<User> _users; 

		static Users()
		{
			Initialize();
		}

		private static void Initialize()
		{
			_users = new List<User>();

			foreach (var property in typeof(Users).GetProperties())
			{
				property.SetValue(property, CreateUser(property.Name));
				_users.Add((User)property.GetValue(property));
			}
		}

		private static User CreateUser(string accountState)
		{
			switch (accountState)
			{
				case "EmptyPassword":
					return CreateUserSimple(Activated.Email, string.Empty);
				case "NoOne":
					return CreateUserSimple(string.Empty, string.Empty);
				case "NotEmail":
					return CreateUserSimple(BaseMailPrefix, Password);
				case "WrongPassword":
					return CreateUserSimple(Activated.Email, "wrong");
				case "EmptyEmail":
					return CreateUserSimple(string.Empty, Password);
			}

			return CreateUserSimple(ConstructEmail(accountState), Password);
		}

		private static User CreateUserSimple(string accountState, string password)
		{
			return new User
			{
				Email = accountState,
				Password = password
			};
		}

		private static string ConstructEmail(string accountState)
		{
			return BaseMailPrefix + "+" + accountState + "@" + EmailProvider;
		}

		public static void PrintUsers()
		{
			foreach (var user in _users)
			{
				Debug.WriteLine(user.Email, user.Password);
			}
		}
	}
}