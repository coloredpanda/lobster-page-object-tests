

// ReSharper disable UnusedAutoPropertyAccessor.Local


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

		public static User InvalidEmail { get; private set; }

		public static User EmptyEmail { get; private set; }

		public static User EmptyPassword { get; private set; }

		public static User WrongPassword { get; private set; }

		public static User NoOne { get; private set; }

		static Users()
		{
			Initialize();
		}

		private static void Initialize()
		{
			foreach (var property in typeof(Users).GetProperties())
			{
				property.SetValue(property, CreateUser(property.Name));
			}
		}

		private static User CreateUser(string accountState)
		{
			switch (accountState)
			{
				case "EmptyPassword":
					return CreateUserSimple(accountState, string.Empty);
				case "NoOne":
					return CreateUserSimple(string.Empty, string.Empty);
				case "InvalidEmail":
					return CreateUserSimple(BaseMailPrefix, Password);
				case "WrongPassword":
					return CreateUserSimple(accountState, "wrong");
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
	}
}