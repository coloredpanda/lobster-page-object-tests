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
		internal static HomePage LandingPage;

		internal static PageObjectModel.Dialogs.LogInDialog LogInDialog;

		[TestInitialize]
		public void Initialize()
		{
			Browser.Open();

			LandingPage = Pages.Home;
			LandingPage.Open();

			LogInDialog = LandingPage.OpenLogInDialog();
		}

		[TestMethod]
		public void UserEmptyPasswordTest()
		{
			// Act
			LogInDialog.Login(Users.EmptyPassword);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsFalse(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid password", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNotEmailTest()
		{
			// Act
			LogInDialog.Login(Users.NotEmail);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsTrue(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserNoOneTest()
		{
			// Act
			LogInDialog.Login(Users.NoOne);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsTrue(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserEmptyEmailTest()
		{
			// Act
			LogInDialog.Login(Users.EmptyEmail);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsTrue(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserWrongPasswordTest()
		{
			// Act
			LogInDialog.Login(Users.WrongPassword);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsTrue(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid password", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserRegisteredTest()
		{
			// Act
			LogInDialog.Login(Users.Registered);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsTrue(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("Your e-mail address is not confirmed. Please check your e-mail. Or click here to resend confirmation letter.", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserRegisteredResendTest()
		{
			// Arrange
			Gmail.DeleteUnread();

			// Act
			LogInDialog.Login(Users.Registered);
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			LogInDialog.Resend();
			var resend = Dialogs.Resend;

			// Assert UI
			LogInDialog.WaitForClose();
			resend.WaitElementToAppear(resend.Root);
			Assert.IsFalse(LogInDialog.Root.Displayed);
			Assert.IsTrue(resend.Root.Displayed);
			Gmail.WaitLetter();
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
			LogInDialog.Login(Users.Activated);

			// Assert
			LogInDialog.WaitForClose();
			Assert.AreEqual("My profile",LandingPage.MyProfile.Text);
		}

		[TestMethod]
		public void UserNotRegisteredTest()
		{
			// Act
			LogInDialog.Login(Users.NotRegistered);

			// Assert
			LogInDialog.WaitElementToAppear(LogInDialog.ErrorMessage);
			Assert.IsTrue(LogInDialog.ErrorMessage.Displayed);
			Assert.AreEqual("You are not registered", LogInDialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void UserFacebookActivatedFullTest()
		{
			// Act
			FacebookLogin(Users.FacebookFull);

			// Assert
			//Assert.IsTrue(Browser.HasNewWindow());
			LogInDialog.WaitForClose();
			Assert.AreEqual("Max Stern", LandingPage.MyProfile.Text);
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
			LogInDialog.Signup();
			var signupDialog = Dialogs.SignUp;

			// Assert
			LogInDialog.WaitForClose();
			signupDialog.WaitElementToAppear(signupDialog.Root);
			Assert.IsTrue(signupDialog.Root.Displayed);
			Assert.AreEqual("Sign up", signupDialog.Name.Text);
		}

		[TestMethod]
		public void ClickForgotPasswordTest()
		{
			// Act
			LogInDialog.ForgotPassword();
			var forgot = Dialogs.Forgot;

			// Assert
			LogInDialog.WaitForClose();
			Assert.IsFalse(LogInDialog.Root.Displayed);
			Assert.IsTrue(forgot.Root.Displayed);
			Assert.AreEqual("Forgot? Do not panic", forgot.Name.Text);
		}

		[TestMethod]
		public void CloseTest()
		{
			// Act
			LogInDialog.Close();

			// Assert
			LogInDialog.WaitForClose();
			Assert.IsFalse(LogInDialog.Root.Displayed);
		}

		[TestCleanup]
		public void CleanUp()
		{
			Browser.Quit();
		}

		private static void FacebookLogin(User user)
		{
			LogInDialog.ViaFacebook();
			var facebook = Pages.Facebook;
			Browser.GetNewWindow();
			facebook.Login(user.Email, user.Password);
			Browser.GetCurrentWindow();
		}
	}
}