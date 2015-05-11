using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lobster.Mail;
using OpenQA.Selenium;

namespace Lobster.Test
{
	public static class Helpers
	{
		public static void Wait(PageObjectModel.Dialogs.LogInDialog dialog)
		{
			var start = 0;
			const int finish = 5;

			while (dialog.Root.Displayed)
			{
				Thread.Sleep(1000);

				start++;

				if (start == finish)
				{
					throw new Exception("Timed out");
				}
			}
		}

		public static void Wait(IWebElement webElement)
		{
			var start = 0;
			const int finish = 5;

			while (!webElement.Displayed)
			{
				Thread.Sleep(1000);

				start++;

				if (start == finish)
				{
					throw new Exception("Timed out");
				}
			}
		}
	}
}