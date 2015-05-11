using Lobster.PageObjectModel.Common;
using System;
using System.Threading;
using Lobster.PageObjectModel.Setup;
using OpenQA.Selenium;

namespace Lobster.PageObjectModel.Pages
{
	public abstract class Page : PageObject
	{
		public static string Url { get; set; }

		protected Page()
		{
			Url = EnvironmentReader.Get(GetType().Name).Url;
			PageTitle = EnvironmentReader.Get(GetType().Name).PageTitle;
		}

		/// <summary>
		/// Navigates to the page URL.
		/// </summary>
		public void Open()
		{
			try
			{
				Browser.Goto(Url);
				WaitForLoad();
			}
			catch (Exception e)
			{
				throw new Exception(GetType().Name + " could not be loaded. Check page url is correct in app.config." +
									" " + e.Message);
			}
		}

		public void WaitElementToAppear(IWebElement webElement)
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