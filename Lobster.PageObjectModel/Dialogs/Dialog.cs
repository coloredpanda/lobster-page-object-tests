using System;
using System.Threading;
using OpenQA.Selenium;

namespace Lobster.PageObjectModel.Dialogs
{
	public abstract class Dialog
	{
		public void WaitElementToAppear(IWebElement webElement)
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

		internal abstract bool IsDisplayed();

		public void WaitForClose()
		{
			var start = 0;
			const int finish = 5;

			while (IsDisplayed())
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