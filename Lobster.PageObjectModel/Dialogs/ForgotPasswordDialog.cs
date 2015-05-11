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

		[FindsBy(How = How.Id, Using = "forgotemail")]
		public IWebElement EmailTextField { get; set; }

		[FindsBy(How = How.Id, Using = "login_return")]
		public IWebElement BackToLoginLink { get; set; }

		[FindsBy(How = How.Id, Using = "helpme")]
		public IWebElement HelpMeButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='forgotpassword']/div[2]")]
		public IWebElement ErrorMessage { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[3]/div[1]/a")]
		public IWebElement CloseButton { get; set; }

		public void HelpMe(string email)
		{
			Browser.Wait();
			EmailTextField.Clear();
			EmailTextField.SendKeys(email);

			Browser.Wait();
			HelpMeButton.Click();
		}

		public void BackToLogin()
		{
			Browser.Wait();

			BackToLoginLink.Click();
		}

		public void Close()
		{
			Browser.Wait();

			CloseButton.Click();
		}
	}
}