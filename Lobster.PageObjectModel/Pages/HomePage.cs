using Lobster.PageObjectModel.Dialogs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Pages
{
	public class HomePage : Page
	{
		#region Buttons

		[FindsBy(How = How.XPath, Using = "//*[@class='m-navigation js-mobile-navigation']/ul/li[1]/a")]
		public IWebElement AboutButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@class='m-navigation js-mobile-navigation']/ul/li[2]/a")]
		public IWebElement BlogButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@class='m-navigation js-mobile-navigation']/ul/li[3]/a")]
		public IWebElement SignUpButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@class='m-navigation js-mobile-navigation']/ul/li[4]/a")]
		public IWebElement SignInButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@class='m-navigation m-navigation--banner']/ul/li[1]/a")]
		public IWebElement MarketplaceButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@class='m-navigation js-mobile-navigation']/ul/li[3]/a")]
		public IWebElement MyProfile { get; set; }

		#endregion

		public LogInDialog OpenLogInDialog()
		{
			SignInButton.Click();
			
			return Dialogs.Dialogs.LogIn;
		}
	}
}