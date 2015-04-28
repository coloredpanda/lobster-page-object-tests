using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Lobster.PageObjectModel
{
	public static class Browser
	{
		private static RemoteWebDriver _webDriver;
		
		#region Internal Methods and properties
		//Here we wrap the web driver properties and methods 

		internal static string Title
		{
			get { return _webDriver.Title; }
		}

		internal static string PageSource
		{
			get { return _webDriver.PageSource; }
		}

		public static RemoteWebDriver GetDriver
		{
			get { return _webDriver; }
		}

		internal static ISearchContext Driver
		{
			get { return _webDriver; }
		}

		public static void Goto(string url)
		{
			_webDriver.Url = url;
		}

		internal static WebDriverWait Wait()
		{
			return new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
		}

		/// <summary>
		/// This method gets the inner html for the specified element
		/// </summary>
		/// <param name="element">The element to retrieve the inner html from</param>
		/// <returns>The inner html as string</returns>
		internal static string GetInnerHtml(IWebElement element)
		{
			var js = Driver as IJavaScriptExecutor;
			var result = "";

			try
			{
				if (js != null)
				{
					result = (string)js.ExecuteScript("return arguments[0].innerHTML;", element);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw;
			}

			return result;
		}

		/// <summary>
		/// Maximises the browser window
		/// </summary>
		internal static void MaximizeWindow()
		{
			_webDriver.Manage().Window.Maximize();
		}
		#endregion

		/// <summary>
		/// Quits the browser and all windows associated with it.
		/// </summary>
		public static void Quit()
		{
			_webDriver.Quit();
		}

		/// <summary>
		/// Opens a new browser window.
		/// </summary>
		public static void Open()
		{
			try
			{
				//Here we read the type of webdriver we want from the app settings, then create an instance of it
				_webDriver = (RemoteWebDriver)Activator.CreateInstance("WebDriver", Setup.Settings.Browser).Unwrap();				
			}
			catch (ArgumentNullException e1)
			{
				Console.WriteLine("Browser was not found. Check a browser has been chosen in the app.config file. " + e1.Message);
				throw;
			}
			catch (TargetInvocationException e2)
			{
				Console.WriteLine("Browser.Open() encountered an error. Check Driver location. " + e2.Message);
				throw;
			}
			catch (Exception e3)
			{
				Console.WriteLine("Browser.Open() encountered an error. " + e3.Message);
				throw;
			}

			_webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(3));

			MaximizeWindow();
		}

		public static bool HasNewWindow()
		{
			return _webDriver.WindowHandles.Count > 1;
		}

		public static IWebDriver GetNewWindow()
		{
			String parentWindow = _webDriver.CurrentWindowHandle;

			//get the current window handles 
			ReadOnlyCollection<string> windowHandles = _webDriver.WindowHandles;

			foreach (String handle in windowHandles)
			{
				if (handle != parentWindow)
				{
					String popupHandle = handle;

					return _webDriver.SwitchTo().Window(popupHandle);
				}
			}

			return null;
		}

		public static void GetCurrentWindow()
		{
			ReadOnlyCollection<string> windowHandles = _webDriver.WindowHandles;
			_webDriver.SwitchTo().Window(windowHandles[0]);
		}

		public static IWebDriver GetIFrame(IWebDriver window)
		{
			return window.SwitchTo().Frame(0);
		}

		public static void CloseNewWindow()
		{
			if (!HasNewWindow()) return;
		}

		public static void CloseNewWindow(IWebDriver window)
		{
			window.Close();
		}
	}
}