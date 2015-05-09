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
		public void UserEmptyPasswordTest()
		{
			// Act
			Login(Users.EmptyPassword);

			// Assert
			Assert.IsFalse(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid password", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNotEmailTest()
		{
			// Act
			Login(Users.NotEmail);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNoOneTest()
		{
			// Act
			Login(Users.NoOne);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserEmptyEmailTest()
		{
			// Act
			Login(Users.EmptyEmail);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserWrongPasswordTest()
		{
			// Act
			Login(Users.WrongPassword);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid password", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserRegisteredTest()
		{
			// Act
			Login(Users.Registered);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Your e-mail address is not confirmed. Please check your e-mail. Or click here to resend confirmation letter.", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserActivatedTest()
		{
			// Act
			Login(Users.Activated);

			// Assert
			Wait(_logInDialog);
			Assert.AreEqual("My profile",_landingPage.MyPforfile.Text);
		}

		[TestMethod]
		public void UserNotRegisteredTest()
		{
			// Act
			Login(Users.NotRegistered);

			// Assert
			Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("You are not registered", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserFacebookActivatedFullTest()
		{
			// Act
			FacebookLogin(Users.FacebookFull);

			// Assert
			//Assert.IsTrue(Browser.HasNewWindow());
			Wait(_logInDialog);
			Assert.AreEqual("Sergey Ok", _landingPage.MyPforfile.Text);
		}

		[TestMethod]
		public void UserFacebookActivatedNoEmailTest()
		{
			// Act
			FacebookLogin(Users.FacebookNoEmail);

			// Assert
			//Assert.IsTrue(Browser.HasNewWindow());
			Wait(_logInDialog);
			Assert.AreEqual("Sergey Ok", _landingPage.MyPforfile.Text);
		}

		[TestMethod]
		public void ClickSignUpTest()
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
		public void ClickForgotPasswordTest()
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
		public void CloseTest()
		{
			// Act
			_logInDialog.Close();

			// Assert
			Wait(_logInDialog);
			Assert.IsFalse(_logInDialog.Root.Displayed);
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

		private static void Login(User user)
		{
			_logInDialog.Login(user.Email, user.Password);
		}

		private static void FacebookLogin(User user)
		{
			_logInDialog.ViaFacebook();
			var facebook = Pages.Facebook;
			Browser.GetNewWindow();
			facebook.Login(user.Email, user.Password);
			Browser.GetCurrentWindow();
		}
	}
}