using Lobster.PageObjectModel.Common;
using System;
using Lobster.PageObjectModel.Setup;

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
	}
}