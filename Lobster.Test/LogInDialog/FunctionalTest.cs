using Lobster.Mail;
using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Dialogs;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
			Helpers.Wait(_logInDialog.ErrorMessage);
			Assert.IsFalse(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid password", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNotEmailTest()
		{
			// Act
			Login(Users.NotEmail);

			// Assert
			Helpers.Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNoOneTest()
		{
			// Act
			Login(Users.NoOne);

			// Assert
			Helpers.Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserEmptyEmailTest()
		{
			// Act
			Login(Users.EmptyEmail);

			// Assert
			Helpers.Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserWrongPasswordTest()
		{
			// Act
			Login(Users.WrongPassword);

			// Assert
			Helpers.Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid password", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserRegisteredTest()
		{
			// Act
			Login(Users.Registered);

			// Assert
			Helpers.Wait(_logInDialog.ErrorMessage);
			Assert.IsTrue(_logInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Your e-mail address is not confirmed. Please check your e-mail. Or click here to resend confirmation letter.", _logInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserRegisteredResendTest()
		{
			// Arrange
			Gmail.DeleteUnread();

			// Act
			Login(Users.Registered);
			Helpers.Wait(_logInDialog.ErrorMessage);
			_logInDialog.Resend();
			var resend = Dialogs.Resend;

			// Assert UI
			Helpers.Wait(_logInDialog);
			Helpers.Wait(resend.Root);
			Assert.IsFalse(_logInDialog.Root.Displayed);
			Assert.IsTrue(resend.Root.Displayed);
			Helpers.WaitLetter();
			var letter = Gmail.GetMessageBody();

			// Assert Gmail
			Assert.AreEqual(1, Gmail.GetUnreadMessagesCount());
			var email = Gmail.GetEmailFromLetter(letter);
			Assert.AreEqual(Users.Registered.Email, email.Text);
		}

		[TestMethod]
		public void UserActivatedTest()
		{
			// Act
			Login(Users.Activated);

			// Assert
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("My profile",_landingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserNotRegisteredTest()
		{
			// Act
			Login(Users.NotRegistered);

			// Assert
			Helpers.Wait(_logInDialog.ErrorMessage);
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
			Helpers.Wait(_logInDialog);
			Assert.AreEqual("Max Stern", _landingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserFacebookActivatedNoEmailTest()
		{
			//// Act
			//FacebookLogin(Users.FacebookNoEmail);

			//// Assert
			////Assert.IsTrue(Browser.HasNewWindow());
			//Helpers.Wait(_logInDialog);
			//Assert.AreEqual("", _landingPage.MyPforfile.Text);
		}

		[TestMethod]
		public void ClickSignUpTest()
		{
			// Act
			_logInDialog.Signup();
			var signUp = Dialogs.SignUp;

			// Assert
			Helpers.Wait(_logInDialog);
			Helpers.Wait(signUp.Root);
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
			Helpers.Wait(_logInDialog);
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
			Helpers.Wait(_logInDialog);
			Assert.IsFalse(_logInDialog.Root.Displayed);
		}

		[TestCleanup]
		public void CleanUp()
		{
			Browser.Quit();
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