using System;
using System.Threading;
using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Dialogs;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lobster.Test.ForgotPasswordDialog
{
	[TestClass]
	public class UiTest
	{
		private static HomePage _homePage;

		private static PageObjectModel.Dialogs.LogInDialog _loginDialog;

		private static PageObjectModel.Dialogs.ForgotPasswordDialog _forgotPasswordDialog;

		public TestContext TestContext { get; set; }

		[ClassInitialize]
		public static void SetupTest(TestContext testContext)
		{
			Browser.Open();

			_homePage = Pages.Home;
			_homePage.Open();

			_loginDialog = _homePage.OpenLogInDialog();

			_loginDialog.ForgotPassword();

			_forgotPasswordDialog = Dialogs.Forgot;

			Helpers.Wait(_forgotPasswordDialog.Root);
		}

		[TestInitialize]
		public void SetupTest()
		{
			//Console.WriteLine(
			//	"TextContext.TestName='{0}'  static _testContext.TestName='{1}'",
			//	TestContext.TestName);
		}

		[TestMethod]
		public void TestMethod1()
		{
			// Assert
			Assert.IsTrue(_forgotPasswordDialog.Root.Displayed);
		}

		#region Dialog General

		[TestMethod]
		public void IsOpenedTest()
		{
			Assert.IsTrue(_forgotPasswordDialog.Root.Displayed);
		}

		[TestMethod]
		public void LocationTest()
		{
			Assert.AreEqual("100px", _forgotPasswordDialog.Root.GetCssValue("top"));
		}

		[TestMethod]
		public void NameTest()
		{
			Assert.AreEqual("Forgot? Do not panic", _forgotPasswordDialog.Name.Text);
		}

		[TestMethod]
		public void SizeTest()
		{
			Assert.AreEqual(258, _forgotPasswordDialog.Root.Size.Height);
			Assert.AreEqual(430, _forgotPasswordDialog.Root.Size.Width);
		}

		[TestMethod]
		public void IsScrollableTest()
		{
			// Arrange
			var dialogX = _forgotPasswordDialog.Root.Location.X;
			var dialogMarginTop = _forgotPasswordDialog.Root.GetCssValue("top");

			// Act
			Browser.GetDriver.Keyboard.SendKeys(Keys.PageDown);

			Assert.AreEqual(_forgotPasswordDialog.Root.Location.X, dialogX);
			Assert.AreEqual(_forgotPasswordDialog.Root.GetCssValue("top"), dialogMarginTop);
		}

		#endregion

		#region Email

		[TestMethod]
		public void EmailIsVisibleTest()
		{
			Assert.IsTrue(_forgotPasswordDialog.EmailTextField.Displayed);
		}

		[TestMethod]
		public void EmailTextTest()
		{
			Assert.AreEqual("E-mail", _forgotPasswordDialog.EmailTextField.GetAttribute("data-placeholder"));
		}

		[TestMethod]
		public void EmailSizeTest()
		{
			Assert.AreEqual(60, _forgotPasswordDialog.EmailTextField.Size.Height);
			Assert.AreEqual(370, _forgotPasswordDialog.EmailTextField.Size.Width);
		}

		[TestMethod]
		public void EmailIsEnabledTest()
		{
			Assert.IsTrue(_forgotPasswordDialog.EmailTextField.Enabled);
		}

		#endregion

		#region Login Button

		[TestMethod]
		public void LoginButtonIsVisibleTest()
		{
			Assert.IsTrue(_forgotPasswordDialog.HelpMeButton.Displayed);
		}

		[TestMethod]
		public void LoginButtonIsEnabledTest()
		{
			Assert.IsTrue(_forgotPasswordDialog.HelpMeButton.Enabled);
		}

		[TestMethod]
		public void LoginButtonTextTest()
		{
			Assert.AreEqual("Help me", _forgotPasswordDialog.HelpMeButton.GetAttribute("value"));
		}

		[TestMethod]
		public void LoginButtonSizeTest()
		{
			Assert.AreEqual(50, _forgotPasswordDialog.HelpMeButton.Size.Height);
			Assert.AreEqual(111, _forgotPasswordDialog.HelpMeButton.Size.Width);
		}

		#endregion

		[ClassCleanup]
		public static void TestCleanup()
		{
			Browser.Quit();
		}
	}
}