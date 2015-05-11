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
		internal static HomePage LandingPage;

		internal static PageObjectModel.Dialogs.LogInDialog LogInDialog;

		internal static PageObjectModel.Dialogs.ForgotPasswordDialog ForgotPasswordDialog;

		[TestInitialize]
		public void Initialize()
		{
			Browser.Open();

			LandingPage = Pages.Home;
			LandingPage.Open();

			LogInDialog = LandingPage.OpenLogInDialog();
			ForgotPasswordDialog = LogInDialog.ForgotPassword();

			LandingPage.WaitElementToAppear(ForgotPasswordDialog.Root);
		}

		[TestMethod]
		public void CloseTest()
		{
			// Act
			ForgotPasswordDialog.Close();

			// Assert
			ForgotPasswordDialog.WaitForClose();
			Assert.IsFalse(ForgotPasswordDialog.Root.Displayed);
		}

		[TestMethod]
		public void BackTest()
		{
			// Act
			ForgotPasswordDialog.BackToLogin();

			// Assert
			ForgotPasswordDialog.WaitForClose();
			Assert.IsFalse(ForgotPasswordDialog.Root.Displayed);
			Assert.IsTrue(LogInDialog.Root.Displayed);
		}

		[TestMethod]
		public void UserActivatedTest()
		{
			// Arrange
			Gmail.DeleteUnread();
			ForgotPasswordDialog.HelpMe(Users.Activated);
			ForgotPasswordDialog.WaitForClose();
			Gmail.WaitLetter();
			Gmail.GetLetterLinks();
			
			Assert.AreEqual(Users.Activated.Email, Gmail.LetterLinks["User"].Text);
			Browser.Goto(Gmail.LetterLinks["Reset"].Href);

			// Act
			var updateDialog = Dialogs.Update;
			updateDialog.UpdatePassword(Users.Activated.Password, Users.Activated.Password);
			var alertText = Browser.GetAlertText();
			Browser.AcceptAlert();
			var signedInPage = Pages.Home;
			signedInPage.Open();

			// Assert
			updateDialog.WaitForClose();
			Assert.IsFalse(ForgotPasswordDialog.Root.Displayed);
			Assert.AreEqual("http://lobster.media/marketplace", Browser.GetDriver.Url);
			Assert.AreEqual("Your password has been updated and you are now signed in!", alertText);
			Assert.IsTrue(signedInPage.AboutButton.Displayed);
			Assert.IsTrue(signedInPage.MyProfile.Displayed);
		}

		[TestMethod]
		public void UserActivatedPasswordMismatchTest()
		{
			// Arrange
			Gmail.DeleteUnread();
			ForgotPasswordDialog.HelpMe(Users.Activated);
			ForgotPasswordDialog.WaitForClose();
			Gmail.WaitLetter();
			Gmail.GetLetterLinks();

			Assert.AreEqual(Users.Activated.Email, Gmail.LetterLinks["User"].Text);
			Browser.Goto(Gmail.LetterLinks["Reset"].Href);

			// Act
			var updateDialog = Dialogs.Update;
			updateDialog.UpdatePassword(Users.Activated.Password, Users.WrongPassword.Password);

			// Assert
			ForgotPasswordDialog.WaitElementToAppear(ForgotPasswordDialog.ErrorMessage);
			Assert.IsTrue(ForgotPasswordDialog.ErrorMessage.Displayed);
			Assert.AreEqual("This email was not found in our system", ForgotPasswordDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserRegisteredTest()
		{
			// Arrange
			Gmail.DeleteUnread();
			ForgotPasswordDialog.HelpMe(Users.Registered);
			ForgotPasswordDialog.WaitForClose();
			Gmail.WaitLetter();
			Gmail.GetLetterLinks();

			Assert.AreEqual(Users.Registered.Email, Gmail.LetterLinks["User"].Text);
			Browser.Goto(Gmail.LetterLinks["Reset"].Href);

			// Act
			var updateDialog = Dialogs.Update;
			updateDialog.UpdatePassword(Users.Registered.Password, Users.Registered.Password);
			var alertText = Browser.GetAlertText();
			Browser.AcceptAlert();
			var signedInPage = Pages.Home;
			signedInPage.Open();

			// Assert
			updateDialog.WaitForClose();
			Assert.IsFalse(ForgotPasswordDialog.Root.Displayed);
			Assert.AreEqual("http://lobster.media/marketplace", Browser.GetDriver.Url);
			Assert.AreEqual("Your password has been updated and you are now signed in!", alertText);
			Assert.IsTrue(signedInPage.AboutButton.Displayed);
			Assert.IsTrue(signedInPage.MyProfile.Displayed);
		}

		[TestMethod]
		public void UserNotRegisteredTest()
		{
			// Arrange
			Gmail.DeleteUnread();

			// Act
			ForgotPasswordDialog.HelpMe(Users.NotRegistered);

			// Assert
			ForgotPasswordDialog.WaitElementToAppear(ForgotPasswordDialog.ErrorMessage);
			Assert.IsTrue(ForgotPasswordDialog.ErrorMessage.Displayed);
			Assert.AreEqual("This email was not found in our system", ForgotPasswordDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNoOneTest()
		{
			// Act
			ForgotPasswordDialog.HelpMe(Users.NoOne);

			// Assert
			ForgotPasswordDialog.WaitElementToAppear(ForgotPasswordDialog.ErrorMessage);
			Assert.IsTrue(ForgotPasswordDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Please enter valid email address", ForgotPasswordDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNotEmailTest()
		{
			// Act
			ForgotPasswordDialog.HelpMe(Users.NotEmail);

			// Assert
			ForgotPasswordDialog.WaitElementToAppear(ForgotPasswordDialog.ErrorMessage);
			Assert.IsTrue(ForgotPasswordDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Please enter valid email address", ForgotPasswordDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserFacebookActivatedFullTest()
		{
			// Act
			ForgotPasswordDialog.HelpMe(Users.FacebookFull);

			// Assert
			//Assert.IsTrue(Browser.HasNewWindow());
			LogInDialog.WaitForClose();
			Assert.AreEqual("Max Stern", LandingPage.MyProfile.Text);
		}

		[TestCleanup]
		public void CleanUp()
		{
			Browser.Quit();
		}
	}
}