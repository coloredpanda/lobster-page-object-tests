using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lobster.Mail;

namespace Lobster.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			PrintUnreadMessagesCount();

			Gmail.DeleteAllUnreadMessages();

			PrintUnreadMessagesCount();
		}

		private static void PrintUnreadMessagesCount()
		{
			System.Console.WriteLine(Gmail.GetUnreadMessagesCount());

			foreach (var scope in Preparation.Scopes)
			{
				System.Console.WriteLine(scope);
			}

			System.Console.ReadLine();
		}
	}
}