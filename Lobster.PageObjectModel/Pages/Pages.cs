using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Pages
{
	public static class Pages
	{
		public static HomePage Home
		{
			get
			{
				var page = new HomePage();
				PageFactory.InitElements(Browser.Driver, page);
				return page;
			}
		}
	}
}