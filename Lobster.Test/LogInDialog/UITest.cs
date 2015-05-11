using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lobster.Test.LogInDialog
{
	[TestClass]
	public class UiTest
	{
		internal static HomePage HomePage;

		internal static PageObjectModel.Dialogs.LogInDialog LogInDialogDialog;
		
		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void SetupTest(TestContext testContext)
		{
			Browser.Open();

			HomePage = Pages.Home;
			HomePage.Open();

			LogInDialogDialog = HomePage.OpenLogInDialog();

			LogInDialogDialog.WaitElementToAppear(LogInDialogDialog.Root);
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
			Assert.IsTrue(LogInDialogDialog.Root.Displayed);
		}

		[TestMethod]
		public void LocationTest()
		{
			Assert.AreEqual("100", LogInDialogDialog.Root.GetCssValue("top"));
		}

		[TestMethod]
		public void NameTest()
		{
			Assert.AreEqual("Log In", LogInDialogDialog.Name.Text);
		}

		[TestMethod]
		public void SizeTest()
		{
			Assert.AreEqual(560, LogInDialogDialog.Root.Size.Height);
			Assert.AreEqual(430, LogInDialogDialog.Root.Size.Width);
		}

		[TestMethod]
		public void IsScrollableTest()
		{
			// Arrange
			var dialogLocationX = LogInDialogDialog.Root.Location.X;
			var dialogMarginTop = LogInDialogDialog.Root.GetCssValue("top");

			// Act
			Browser.GetDriver.Keyboard.SendKeys(Keys.PageDown);

			Assert.AreEqual(dialogLocationX, LogInDialogDialog.Root.Location.X);
			Assert.AreEqual(dialogMarginTop, LogInDialogDialog.Root.GetCssValue("top"));
		}

		#endregion

		#region Email

		[TestMethod]
		public void EmailIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.EmailTextField.Displayed);
		}

		[TestMethod]
		public void EmailTextTest()
		{
			Assert.AreEqual("E-mail", LogInDialogDialog.EmailTextField.GetAttribute("data-placeholder"));
		}

		[TestMethod]
		public void EmailSizeTest()
		{
			Assert.AreEqual(60, LogInDialogDialog.EmailTextField.Size.Height);
			Assert.AreEqual(370, LogInDialogDialog.EmailTextField.Size.Width);
		}

		[TestMethod]
		public void EmailIsEnabledTest()
		{
			Assert.IsTrue(LogInDialogDialog.EmailTextField.Enabled);
		}

		#endregion

		#region Password

		[TestMethod]
		public void PasswordIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.PasswordTextField.Displayed);
		}

		[TestMethod]
		public void PasswordTextTest()
		{
			Assert.AreEqual("Password", LogInDialogDialog.PasswordTextField.GetAttribute("data-placeholder"));
		}

		[TestMethod]
		public void PasswordSizeTest()
		{
			Assert.AreEqual(60, LogInDialogDialog.PasswordTextField.Size.Height);
			Assert.AreEqual(370, LogInDialogDialog.PasswordTextField.Size.Width);
		}

		[TestMethod]
		public void PasswordIsEnabledTest()
		{
			Assert.IsTrue(LogInDialogDialog.PasswordTextField.Enabled);
		}

		#endregion

		#region Login Button

		[TestMethod]
		public void LoginButtonIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.LoginButton.Displayed);
		}

		[TestMethod]
		public void LoginButtonIsEnabledTest()
		{
			Assert.IsTrue(LogInDialogDialog.LoginButton.Enabled);
		}

		[TestMethod]
		public void LoginButtonTextTest()
		{
			Assert.AreEqual("Login", LogInDialogDialog.LoginButton.GetAttribute("value"));
		}

		[TestMethod]
		public void LoginButtonSizeTest()
		{
			Assert.AreEqual(50, LogInDialogDialog.LoginButton.Size.Height);
			Assert.AreEqual(91, LogInDialogDialog.LoginButton.Size.Width);
		}

		#endregion

		#region Facebook Button

		[TestMethod]
		public void FacebookButtonIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.ViaFacebookButtton.Displayed);
		}

		[TestMethod]
		public void FacebookButtonIsEnabledTest()
		{
			Assert.IsTrue(LogInDialogDialog.ViaFacebookButtton.Enabled);
		}

		[TestMethod]
		public void FacebookButtonTextTest()
		{
			Assert.AreEqual("via Facebook", LogInDialogDialog.ViaFacebookButtton.Text);
		}

		[TestMethod]
		public void FacebookButtonSizeTest()
		{
			Assert.AreEqual(48, LogInDialogDialog.ViaFacebookButtton.Size.Height);
			Assert.AreEqual(155, LogInDialogDialog.ViaFacebookButtton.Size.Width);
		}

		#endregion

		#region Sign Up Link

		[TestMethod]
		public void SignUpLinkIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.SignUpLink.Displayed);
		}

		[TestMethod]
		public void SignUpLinkIsEnabledTestTest()
		{
			Assert.IsTrue(LogInDialogDialog.SignUpLink.Enabled);
		}

		[TestMethod]
		public void SignUpLinkTextTest()
		{
			Assert.AreEqual("Sign up", LogInDialogDialog.SignUpLink.Text);
		}

		#endregion

		#region Forgot password link

		[TestMethod]
		public void ForgotPasswordLinkIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.ForgotPasswordLink.Displayed);
		}

		[TestMethod]
		public void ForgotPasswordIsEnabledTest()
		{
			Assert.IsTrue(LogInDialogDialog.ForgotPasswordLink.Enabled);
		}

		[TestMethod]
		public void ForgotPasswordTextTest()
		{
			Assert.AreEqual("Forgot password", LogInDialogDialog.ForgotPasswordLink.Text);
		}

		#endregion

		#region Close button

		[TestMethod]
		public void CloseButtonIsVisibleTest()
		{
			Assert.IsTrue(LogInDialogDialog.CloseButton.Displayed);
		}

		[TestMethod]
		public void CloseButtonIsEnabledTest()
		{
			Assert.IsTrue(LogInDialogDialog.CloseButton.Enabled);
		}

		#endregion

		[ClassCleanup]
		public static void TestCleanup()
		{
			Browser.Quit();
		}
	}
}