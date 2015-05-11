using System.Threading;
using Lobster.PageObjectModel;
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
		public void IsOpenedTest()
		{
			Assert.IsTrue(_dialog.Root.Displayed);
		}

		[TestMethod]
		public void LocationTest()
		{
			Assert.AreEqual("100", _dialog.Root.GetCssValue("top"));
		}

		[TestMethod]
		public void NameTest()
		{
			Assert.AreEqual(_dialog.Name.Text, "Log In");
		}

		[TestMethod]
		public void SizeTest()
		{
			Assert.AreEqual(_dialog.Root.Size.Height, 560);
			Assert.AreEqual(_dialog.Root.Size.Width, 430);
		}

		[TestMethod]
		public void IsScrollableTest()
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
		public void EmailIsVisibleTest()
		{
			Assert.IsTrue(_dialog.EmailTextField.Displayed);
		}

		[TestMethod]
		public void EmailTextTest()
		{
			Assert.AreEqual(_dialog.EmailTextField.GetAttribute("data-placeholder"), "E-mail");
		}

		[TestMethod]
		public void EmailSizeTest()
		{
			Assert.AreEqual(_dialog.EmailTextField.Size.Height, 60);
			Assert.AreEqual(_dialog.EmailTextField.Size.Width, 370);
		}

		[TestMethod]
		public void EmailIsEnabledTest()
		{
			Assert.IsTrue(_dialog.EmailTextField.Enabled);
		}

		#endregion

		#region Password

		[TestMethod]
		public void PasswordIsVisibleTest()
		{
			Assert.IsTrue(_dialog.PasswordTextField.Displayed);
		}

		[TestMethod]
		public void PasswordTextTest()
		{
			Assert.AreEqual(_dialog.PasswordTextField.GetAttribute("data-placeholder"), "Password");
		}

		[TestMethod]
		public void PasswordSizeTest()
		{
			Assert.AreEqual(_dialog.PasswordTextField.Size.Height, 60);
			Assert.AreEqual(_dialog.PasswordTextField.Size.Width, 370);
		}

		[TestMethod]
		public void PasswordIsEnabledTest()
		{
			Assert.IsTrue(_dialog.PasswordTextField.Enabled);
		}

		#endregion

		#region Login Button

		[TestMethod]
		public void LoginButtonIsVisibleTest()
		{
			Assert.IsTrue(_dialog.LoginButton.Displayed);
		}

		[TestMethod]
		public void LoginButtonIsEnabledTest()
		{
			Assert.IsTrue(_dialog.LoginButton.Enabled);
		}

		[TestMethod]
		public void LoginButtonTextTest()
		{
			Assert.AreEqual(_dialog.LoginButton.GetAttribute("value"), "Login");
		}

		[TestMethod]
		public void LoginButtonSizeTest()
		{
			Assert.AreEqual(_dialog.LoginButton.Size.Height, 50);
			Assert.AreEqual(_dialog.LoginButton.Size.Width, 91);
		}

		#endregion

		#region Facebook Button

		[TestMethod]
		public void FacebookButtonIsVisibleTest()
		{
			Assert.IsTrue(_dialog.ViaFacebookButtton.Displayed);
		}

		[TestMethod]
		public void FacebookButtonIsEnabledTest()
		{
			Assert.IsTrue(_dialog.ViaFacebookButtton.Enabled);
		}

		[TestMethod]
		public void FacebookButtonTextTest()
		{
			Assert.AreEqual(_dialog.ViaFacebookButtton.Text, "via Facebook");
		}

		[TestMethod]
		public void FacebookButtonSizeTest()
		{
			Assert.AreEqual(_dialog.ViaFacebookButtton.Size.Height, 48);
			Assert.AreEqual(_dialog.ViaFacebookButtton.Size.Width, 155);
		}

		#endregion

		#region Sign Up Link

		[TestMethod]
		public void SignUpLinkIsVisibleTest()
		{
			Assert.IsTrue(_dialog.SignUpLink.Displayed);
		}

		[TestMethod]
		public void SignUpLinkIsEnabledTestTest()
		{
			Assert.IsTrue(_dialog.SignUpLink.Enabled);
		}

		[TestMethod]
		public void SignUpLinkTextTest()
		{
			Assert.AreEqual(_dialog.SignUpLink.Text, "Sign up");
		}

		#endregion

		#region Forgot password link

		[TestMethod]
		public void ForgotPasswordLinkIsVisibleTest()
		{
			Assert.IsTrue(_dialog.ForgotPasswordLink.Displayed);
		}

		[TestMethod]
		public void ForgotPasswordIsEnabledTest()
		{
			Assert.IsTrue(_dialog.ForgotPasswordLink.Enabled);
		}

		[TestMethod]
		public void ForgotPasswordTextTest()
		{
			Assert.AreEqual(_dialog.ForgotPasswordLink.Text, "Forgot password");
		}

		#endregion

		#region Close button

		[TestMethod]
		public void CloseButtonIsVisibleTest()
		{
			Assert.IsTrue(_dialog.CloseButton.Displayed);
		}

		[TestMethod]
		public void CloseButtonIsEnabledTest()
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