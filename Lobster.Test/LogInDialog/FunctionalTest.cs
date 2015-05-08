using System;
using System.Threading;
using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Dialogs;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lobster.Test.LogInDialog
{
	[TestClass]
	public class FunctionalTest
	{
		private static HomePage _landingPage;

		private static PageObjectModel.Dialogs.LogInDialog _logInDialog;

		[TestInitialize]
		public void Initialize()
		{
			Browser.Open();

			_landingPage = Pages.Home;
			_landingPage.Open();

			_logInDialog = _landingPage.OpenLogInDialog();
		}

		[TestMethod]
		public void LogInDialogFunctionalCloseTest()
		{
			// Act
			_logInDialog.Close();

			// Assert
			Wait(_logInDialog);
			Assert.IsFalse(_logInDialog.Root.Displayed);
		}

		[TestMethod]
		public void LogInDialogFunctionalEmptyUserPasswordTest()
		{
			// Act
			_logInDialog.Login(String.Empty, String.Empty);

			// Assert
			Assert.IsFalse(_logInDialog.ErrorMessage.Displayed);
		}

		[TestMethod]
		public void LogInDialogFunctionalNotEmailTest()
		{
			// Act
			_logInDialog.Login("test", String.Empty);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalWrongPasswordTest()
		{
			// Act
			_logInDialog.Login("test", String.Empty);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalNotActivatedUserTest()
		{
			// Act
			_logInDialog.Login(Users.Registered["email"], Users.Registered["password"]);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Your e-mail address is not confirmed. Please check your e-mail. Or click here to resend confirmation letter.", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalActivatedUserTest()
		{
			// Act
			_logInDialog.Login(Users.Activated["email"], Users.Activated["password"]);

			// Assert
			Wait(_logInDialog);
			Assert.AreEqual("My profile",_landingPage.MyPforfile.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalClickSignUpTest()
		{
			// Act
			_logInDialog.Signup();
			var signUp = Dialogs.SignUp;

			// Assert
			Wait(_logInDialog);
			Wait(signUp.Root);
			Assert.IsTrue(signUp.Root.Displayed);
			Assert.AreEqual("Sign up", signUp.Name.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalClickForgotPasswordTest()
		{
			// Act
			_logInDialog.ForgotPassword();
			var forgot = Dialogs.Forgot;

			// Assert
			Wait(_logInDialog);
			Assert.IsFalse(_logInDialog.Root.Displayed);
			Assert.IsTrue(forgot.Root.Displayed);
			Assert.AreEqual("Forgot? Do not panic", forgot.Name.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalClickFacebookTest()
		{
			// Act
			_logInDialog.ViaFacebook();
			var facebook = Pages.Facebook;
			Browser.GetNewWindow();
			facebook.Login(Users.Facebook["email"], Users.Facebook["password"]);
			Browser.GetCurrentWindow();

			// Assert
			//Assert.IsTrue(Browser.HasNewWindow());
			Wait(_logInDialog);
			Assert.AreEqual("Sergey Ok", _landingPage.MyPforfile.Text);
		}

		[TestCleanup]
		public void CleanUp()
		{
			Browser.Quit();
		}

		private static void Wait(PageObjectModel.Dialogs.LogInDialog dialog)
		{
			var start = 0;
			const int finish = 5;

			while (dialog.Root.Displayed)
			{
				Thread.Sleep(1000);

				start++;

				if (start == finish)
				{
					throw new Exception("Timed out");
				}
			}
		}

		private static void Wait(IWebElement webElement)
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