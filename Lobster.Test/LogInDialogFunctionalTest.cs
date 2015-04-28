using System;
using System.Threading;
using Lobster.PageObjectModel;
using Lobster.PageObjectModel.Dialogs;
using Lobster.PageObjectModel.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lobster.Test
{
	[TestClass]
	public class LogInDialogFunctionalTest
	{
		public static HomePage HomePage;

		[TestInitialize]
		public void Initialize()
		{
			Browser.Open();

			HomePage = Pages.Home;
			HomePage.Open();
		}

		[TestMethod]
		public void LogInDialogFunctionalCloseTest()
		{
			// Arrange
			var dialog = HomePage.OpenLogInDialog();

			// Act
			dialog.Close();
			Wait(dialog);

			// Assert
			Assert.IsFalse(dialog.Root.Displayed);
		}

		[TestMethod]
		public void LogInDialogFunctionalEmptyUserPasswordTest()
		{
			// Arrange
			var dialog = HomePage.OpenLogInDialog();

			// Act
			dialog.Login(String.Empty, String.Empty);

			// Assert
			Assert.IsFalse(dialog.ErrorMessage.Displayed);
		}

		[TestMethod]
		public void LogInDialogFunctionalNotEmailTest()
		{
			// Arrange
			var dialog = HomePage.OpenLogInDialog();

			// Act
			dialog.Login("test", String.Empty);

			// Assert
			Wait(dialog.ErrorMessage);
			Assert.IsTrue(dialog.ErrorMessage.Displayed);
			Assert.AreEqual("Invalid email", dialog.ErrorMessage.Text);
		}

		[TestMethod]
		public void LogInDialogFunctionalWrongPasswordTest()
		{

		}

		[TestMethod]
		public void LogInDialogFunctionalNotActivatedUserTest()
		{

		}

		[TestMethod]
		public void LogInDialogFunctionalActivatedUserTest()
		{

		}

		[TestCleanup]
		public void CleanUp()
		{
			Browser.Quit();
		}

		private void Wait(LogInDialog dialog)
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

		private void Wait(IWebElement webElement)
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
	}
}