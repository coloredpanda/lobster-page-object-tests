using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lobster.PageObjectModel.Dialogs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Pages
{
	public class HomePage : Page
	{
		#region Buttons

		[FindsBy(How = How.XPath, Using = "//*[@id='m-navigation']/ul/li[4]/a")]
		public IWebElement SingInButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='m-navigation']/ul/li[3]/a")]
		public IWebElement SignUpButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='m-navigation']/ul/li[2]/a")]
		public IWebElement BlogButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='m-navigation']/ul/li[1]/a")]
		public IWebElement AboutButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='m-navigation']/ul/li[1]/a")]
		public IWebElement MarketplaceButton { get; set; }

		#endregion

		public LogInDialog OpenLogInDialog()
		{
			SingInButton.Click();
			
			return Dialogs.Dialogs.LogIn;
		}
	}
}