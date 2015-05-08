using System.Threading;
using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Dialogs;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lobster.Test.LogInDialog
{
	[TestClass]
	public class UiTest
	{
		private static HomePage _homePage;

		private static PageObjectModel.Dialogs.LogInDialog _dialog;
		
		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void SetupTest(TestContext testContext)
		{
			Browser.Open();

			_homePage = Pages.Home;
			_homePage.Open();

			_dialog = _homePage.OpenLogInDialog();

			Thread.Sleep(6000);
		}

		[TestInitialize]
		public void SetupTest()
		{
			//Console.WriteLine(
			//	"TextContext.TestName='{0}'  static _testContext.TestName='{1}'",
			//	TestContext.TestName);
		}

		#region Dialog General

		[TestMethod]
		public void LogInDialogIsOpenedTest()
		{
			Assert.IsTrue(_dialog.Root.Displayed);
		}

		[TestMethod]
		public void LogInDialogNameTest()
		{
			Assert.AreEqual(_dialog.Name.Text, "Log In");
		}

		[TestMethod]
		public void LogInDialogSizeTest()
		{
			Assert.AreEqual(_dialog.Root.Size.Height, 560);
			Assert.AreEqual(_dialog.Root.Size.Width, 430);
		}

		[TestMethod]
		public void LogInDialogIsScrollableTest()
		{
			// Arrange
			var dialogX = _dialog.Root.Location.X;
			var dialogMarginTop = _dialog.Root.GetCssValue("top");

			// Act
			Browser.GetDriver.Keyboard.SendKeys(Keys.PageDown);

			Assert.AreEqual(_dialog.Root.Location.X, dialogX);
			Assert.AreEqual(_dialog.Root.GetCssValue("top"), dialogMarginTop);
		}

		#endregion

		#region Email

		[TestMethod]
		public void LogInDialogEmailIsVisibleTest()
		{
			Assert.IsTrue(_dialog.EmailTextField.Displayed);
		}

		[TestMethod]
		public void LogInDialogEmailTextTest()
		{
			Assert.AreEqual(_dialog.EmailTextField.GetAttribute("data-placeholder"), "E-mail");
		}

		[TestMethod]
		public void LogInDialogEmailSizeTest()
		{
			Assert.AreEqual(_dialog.EmailTextField.Size.Height, 60);
			Assert.AreEqual(_dialog.EmailTextField.Size.Width, 370);
		}

		[TestMethod]
		public void LogInDialogEmailIsEnabledTest()
		{
			Assert.IsTrue(_dialog.EmailTextField.Enabled);
		}

		#endregion

		#region Password

		[TestMethod]
		public void LogInDialogPasswordIsVisibleTest()
		{
			Assert.IsTrue(_dialog.PasswordTextField.Displayed);
		}

		[TestMethod]
		public void LogInDialogPasswordTextTest()
		{
			Assert.AreEqual(_dialog.PasswordTextField.GetAttribute("data-placeholder"), "Password");
		}

		[TestMethod]
		public void LogInDialogPasswordSizeTest()
		{
			Assert.AreEqual(_dialog.PasswordTextField.Size.Height, 60);
			Assert.AreEqual(_dialog.PasswordTextField.Size.Width, 370);
		}

		[TestMethod]
		public void LogInDialogPasswordIsEnabledTest()
		{
			Assert.IsTrue(_dialog.PasswordTextField.Enabled);
		}

		#endregion

		#region Login Button

		[TestMethod]
		public void LogInDialogLoginButtonIsVisibleTest()
		{
			Assert.IsTrue(_dialog.LoginButton.Displayed);
		}

		[TestMethod]
		public void LogInDialogLoginButtonIsEnabledTest()
		{
			Assert.IsTrue(_dialog.LoginButton.Enabled);
		}

		[TestMethod]
		public void LogInDialogLoginButtonTextTest()
		{
			Assert.AreEqual(_dialog.LoginButton.GetAttribute("value"), "Login");
		}

		[TestMethod]
		public void LogInDialogLoginButtonSizeTest()
		{
			Assert.AreEqual(_dialog.LoginButton.Size.Height, 50);
			Assert.AreEqual(_dialog.LoginButton.Size.Width, 91);
		}

		#endregion

		#region Facebook Button

		[TestMethod]
		public void LogInDialogFacebookButtonIsVisibleTest()
		{
			Assert.IsTrue(_dialog.ViaFacebookButtton.Displayed);
		}

		[TestMethod]
		public void LogInDialogFacebookButtonIsEnabledTest()
		{
			Assert.IsTrue(_dialog.ViaFacebookButtton.Enabled);
		}

		[TestMethod]
		public void LogInDialogFacebookButtonTextTest()
		{
			Assert.AreEqual(_dialog.ViaFacebookButtton.Text, "via Facebook");
		}

		[TestMethod]
		public void LogInDialogFacebookButtonSizeTest()
		{
			Assert.AreEqual(_dialog.ViaFacebookButtton.Size.Height, 48);
			Assert.AreEqual(_dialog.ViaFacebookButtton.Size.Width, 155);
		}

		#endregion

		#region Sign Up Link

		[TestMethod]
		public void LogInDialogSignUpLinkIsVisibleTest()
		{
			Assert.IsTrue(_dialog.SignUpLink.Displayed);
		}

		[TestMethod]
		public void LogInDialogSignUpLinkIsEnabledTestTest()
		{
			Assert.IsTrue(_dialog.SignUpLink.Enabled);
		}

		[TestMethod]
		public void LogInDialogSignUpLinkTextTest()
		{
			Assert.AreEqual(_dialog.SignUpLink.Text, "Sign up");
		}

		#endregion

		#region Forgot password link

		[TestMethod]
		public void LogInDialogForgotPasswordLinkIsVisibleTest()
		{
			Assert.IsTrue(_dialog.ForgotPasswordLink.Displayed);
		}

		[TestMethod]
		public void LogInDialogForgotPasswordIsEnabledTest()
		{
			Assert.IsTrue(_dialog.ForgotPasswordLink.Enabled);
		}

		[TestMethod]
		public void LogInDialogForgotPasswordTextTest()
		{
			Assert.AreEqual(_dialog.ForgotPasswordLink.Text, "Forgot password");
		}

		#endregion

		#region Close button

		[TestMethod]
		public void LogInDialogCloseButtonIsVisibleTest()
		{
			Assert.IsTrue(_dialog.CloseButton.Displayed);
		}

		[TestMethod]
		public void LogInDialogCloseButtonIsEnabledTest()
		{
			Assert.IsTrue(_dialog.CloseButton.Enabled);
		}

		#endregion

		[ClassCleanup]
		public static void TestCleanup()
		{
			Browser.Quit();
		}
	}
}