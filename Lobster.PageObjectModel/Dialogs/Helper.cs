using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Lobster.PageObjectModel.Dialogs
{
	class Helper
	{
		internal static void WaitFor(By elementDesc)
		{
			var wait = Browser.Wait();

			wait.Until(e => e.FindElement(elementDesc));
		}
	}
}