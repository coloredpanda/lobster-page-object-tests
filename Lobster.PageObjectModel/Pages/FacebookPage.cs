using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Pages
{
	public class FacebookPage
	{
		[FindsBy(How = How.Id, Using = "email")]
		private IWebElement EmailTextField { get; set; }

		[FindsBy(How = How.Id, Using = "pass")]
		private IWebElement PasswordTextField { get; set; }

		[FindsBy(How = How.Id, Using = "u_0_2")]
		private IWebElement LoginButton { get; set; }

		public void Login(String email, String password)
		{
			Browser.Wait();

			EmailTextField.Clear();
			EmailTextField.SendKeys(email);

			PasswordTextField.Clear();
			PasswordTextField.SendKeys(password);

			LoginButton.Click();
		}
	}
}