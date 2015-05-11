using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Dialogs
{
	public class LogInDialog : Dialog
	{
		[FindsBy(How = How.XPath, Using = "/html/body/div[5]")]
		public IWebElement Root { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[5]/div[1]/span")]
		public IWebElement Name { get; set; }

		[FindsBy(How = How.Id, Using = "user_email")]
		public IWebElement EmailTextField { get; set; }

		[FindsBy(How = How.Id, Using = "user_password")]
		public IWebElement PasswordTextField { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='sign_in_user']/div[7]/label")]
		public IWebElement RememberMeCheckBox { get; set; }

		[FindsBy(How = How.Id, Using = "sign_in_user_submit")]
		public IWebElement LoginButton { get; set; }

		[FindsBy(How = How.Id, Using = "facebook-login-link")]
		public IWebElement ViaFacebookButtton { get; set; }

		[FindsBy(How = How.Id, Using = "register-login")]
		public IWebElement SignUpLink { get; set; }

		[FindsBy(How = How.Id, Using = "forgot-password-login")]
		public IWebElement ForgotPasswordLink { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[5]/div[1]/a")]
		public IWebElement CloseButton { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='sign_in_user']/div[4]")]
		public IWebElement ErrorMessage { get; set; }

		[FindsBy(How = How.XPath, Using = "//*[@id='sign_in_user']/div[4]/a")]
		public IWebElement ResendLink { get; set; }

		public void Close()
		{
			while (!Root.Displayed)
			{
				Thread.Sleep(1000);
			}

			CloseButton.Click();
		}

		public void Login(User user)
		{
			Browser.Wait();

			EmailTextField.Clear();
			EmailTextField.SendKeys(user.Email);
			
			PasswordTextField.Clear();
			PasswordTextField.SendKeys(user.Password);

			LoginButton.Click();
		}

		public ForgotPasswordDialog ForgotPassword()
		{
			Browser.Wait();
			ForgotPasswordLink.Click();

			return Dialogs.Forgot;
		}

		public void Signup()
		{
			Browser.Wait();
			SignUpLink.Click();
		}

		public void RememberMe()
		{
			Browser.Wait();
			RememberMeCheckBox.Click();
		}

		public void ViaFacebook()
		{
			Browser.Wait();
			ViaFacebookButtton.Click();
		}

		public void Resend()
		{
			Browser.Wait();
			ResendLink.Click();
		}

		internal override bool IsDisplayed()
		{
			return Root.Displayed;
		}
	}
}