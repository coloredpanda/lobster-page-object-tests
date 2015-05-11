using System;
using System.Threading;
using Lobster.Mail;
using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Dialogs;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lobster.Test.ForgotPasswordDialog
{
	[TestClass]
	public class FunctionalTest
	{
		private static HomePage _landingPage;

		private static PageObjectModel.Dialogs.LogInDialog _logInDialog;

		private static PageObjectModel.Dialogs.ForgotPasswordDialog _forgotPasswordDialog;

		[TestInitialize]
		public void Initialize()
		{
			Browser.Open();

			_landingPage = Pages.Home;
			_landingPage.Open();

			_logInDialog = _landingPage.OpenLogInDialog();

			_logInDialog.ForgotPassword();
			_forgotPasswordDialog = Dialogs.Forgot;

			Helpers.Wait(_forgotPasswordDialog.Root);
		}

		[TestMethod]
		public void CloseTest()
		{
			// Act
			_forgotPasswordDialog.Close();

			// Assert
			Wait(_forgotPasswordDialog);
			Assert.IsFalse(_forgotPasswordDialog.Root.Displayed);
		}

		[TestMethod]
		public void UserActivatedTest()
		{
			// Arrange
			Gmail.DeleteUnread();
			HelpMe(Users.Activated);
			Helpers.WaitLetter();
			var letter = Gmail.GetMessageBody();
			var links = Gmail.Find(letter);
			Wait(_forgotPasswordDialog);
			Assert.AreEqual(Users.Activated.Email, links[1].Text);
			Browser.Goto(links[2].Href);

			// Act
			var updateDialog = Dialogs.Update;
			updateDialog.UpdatePassword(Users.Activated.Password);
			var alertText = Browser.GetAlertText();
			Browser.AcceptAlert();
			var signedInPage = Pages.Home;
			signedInPage.Open();

			// Assert
			Wait(updateDialog);
			Assert.IsFalse(_forgotPasswordDialog.Root.Displayed);
			Assert.AreEqual("http://lobster.media/marketplace", Browser.GetDriver.Url);
			Assert.AreEqual("Your password has been updated and you are now signed in!", alertText);
			Assert.IsTrue(signedInPage.AboutButton.Displayed);
			Assert.IsTrue(signedInPage.MyProfile.Displayed);
		}

		[TestMethod]
		public void UserRegisteredTest()
		{
			// Act
			HelpMe(Users.Registered);

			// Assert
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("My profile", _landingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserNotRegisteredTest()
		{
			// Act
			HelpMe(Users.NotRegistered);

			// Assert
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("My profile", _landingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserNoOneTest()
		{
			// Act
			HelpMe(Users.NoOne);

			// Assert
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("My profile", _landingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserNotEmailTest()
		{
			// Act
			HelpMe(Users.NotEmail);

			// Assert
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("My profile", _landingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserFacebookActivatedFullTest()
		{
			// Act
			HelpMe(Users.FacebookFull);

			// Assert
			//Assert.IsTrue(Browser.HasNewWindow());
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("Max Stern", _landingPage.MyProfile.Text);
		}

		[TestCleanup]
		public void CleanUp()
		{
			Browser.Quit();
		}

		private static void HelpMe(User user)
		{
			_forgotPasswordDialog.HelpMe(user.Email);
		}

		private static void Wait(PageObjectModel.Dialogs.ForgotPasswordDialog dialog)
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

		private static void Wait(PageObjectModel.Dialogs.UpdatePasswordDialog dialog)
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
	}
}