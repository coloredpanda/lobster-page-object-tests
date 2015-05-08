using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Dialogs
{
	public class ForgotPasswordDialog
	{
		[FindsBy(How = How.XPath, Using = "/html/body/div[3]")]
		public IWebElement Root { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[3]/div[1]/span")]
		public IWebElement Name { get; set; }
	}
}