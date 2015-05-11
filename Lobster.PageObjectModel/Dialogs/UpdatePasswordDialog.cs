using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Dialogs
{
	public class UpdatePasswordDialog
	{
		[FindsBy(How = How.XPath, Using = "/html/body/div[5]")]
		public IWebElement Root { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[5]/div[1]/span")]
		public IWebElement Name { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='forgotpassword_edit']/input[2]")]
		public IWebElement NewPasswordTextField { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='forgotpassword_edit']/input[3]")]
		public IWebElement ConfirmPasswordTextField { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='forgotpassword_edit']/div[3]/div[2]")]
		public IWebElement HelpMeButton { get; set; }

		[FindsBy(How = How.Id, Using = "login_return")]
		public IWebElement BackToLoginLink { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[5]/div[1]/a")]
		public IWebElement CloseButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='forgotpassword_edit']/div[2]")]
		public IWebElement ErrorMessageText { get; set; }

		public void UpdatePassword(string newPassword, string confirmPassword)
		{
			Browser.Wait();

			NewPasswordTextField.Clear();
			NewPasswordTextField.SendKeys(newPassword);

			ConfirmPasswordTextField.Clear();
			ConfirmPasswordTextField.SendKeys(newPassword);

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