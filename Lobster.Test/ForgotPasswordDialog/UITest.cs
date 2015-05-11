using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lobster.Test.ForgotPasswordDialog
{
	[TestClass]
	public class UiTest
	{
		internal static HomePage HomePage;

		internal static PageObjectModel.Dialogs.LogInDialog LoginDialog;

		internal static PageObjectModel.Dialogs.ForgotPasswordDialog ForgotPasswordDialog;

		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void SetupTest(TestContext testContext)
		{
			Browser.Open();

			HomePage = Pages.Home;
			HomePage.Open();

			LoginDialog = HomePage.OpenLogInDialog();
			ForgotPasswordDialog = LoginDialog.ForgotPassword();

			Helpers.Wait(ForgotPasswordDialog.Root);
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
			Assert.IsTrue(ForgotPasswordDialog.Root.Displayed);
		}

		[TestMethod]
		public void LocationTest()
		{
			Assert.AreEqual("100px", ForgotPasswordDialog.Root.GetCssValue("top"));
		}

		[TestMethod]
		public void NameTest()
		{
			Assert.AreEqual("Forgot? Do not panic", ForgotPasswordDialog.Name.Text);
		}

		[TestMethod]
		public void SizeTest()
		{
			Assert.AreEqual(258, ForgotPasswordDialog.Root.Size.Height);
			Assert.AreEqual(430, ForgotPasswordDialog.Root.Size.Width);
		}

		[TestMethod]
		public void IsScrollableTest()
		{
			// Arrange
			var dialogX = ForgotPasswordDialog.Root.Location.X;
			var dialogMarginTop = ForgotPasswordDialog.Root.GetCssValue("top");

			// Act
			Browser.GetDriver.Keyboard.SendKeys(Keys.PageDown);

			Assert.AreEqual(ForgotPasswordDialog.Root.Location.X, dialogX);
			Assert.AreEqual(ForgotPasswordDialog.Root.GetCssValue("top"), dialogMarginTop);
		}

		#endregion

		#region Email

		[TestMethod]
		public void EmailIsVisibleTest()
		{
			Assert.IsTrue(ForgotPasswordDialog.EmailTextField.Displayed);
		}

		[TestMethod]
		public void EmailTextTest()
		{
			Assert.AreEqual("E-mail", ForgotPasswordDialog.EmailTextField.GetAttribute("data-placeholder"));
		}

		[TestMethod]
		public void EmailSizeTest()
		{
			Assert.AreEqual(60, ForgotPasswordDialog.EmailTextField.Size.Height);
			Assert.AreEqual(370, ForgotPasswordDialog.EmailTextField.Size.Width);
		}

		[TestMethod]
		public void EmailIsEnabledTest()
		{
			Assert.IsTrue(ForgotPasswordDialog.EmailTextField.Enabled);
		}

		#endregion

		#region HelpMe Button

		[TestMethod]
		public void HelpMeButtonIsVisibleTest()
		{
			Assert.IsTrue(ForgotPasswordDialog.HelpMeButton.Displayed);
		}

		[TestMethod]
		public void HelpMeButtonIsEnabledTest()
		{
			Assert.IsTrue(ForgotPasswordDialog.HelpMeButton.Enabled);
		}

		[TestMethod]
		public void HelpMeButtonTextTest()
		{
			Assert.AreEqual("Help me", ForgotPasswordDialog.HelpMeButton.GetAttribute("value"));
		}

		[TestMethod]
		public void HelpMeButtonSizeTest()
		{
			Assert.AreEqual(50, ForgotPasswordDialog.HelpMeButton.Size.Height);
			Assert.AreEqual(111, ForgotPasswordDialog.HelpMeButton.Size.Width);
		}

		#endregion

		[ClassCleanup]
		public static void TestCleanup()
		{
			Browser.Quit();
		}
	}
}