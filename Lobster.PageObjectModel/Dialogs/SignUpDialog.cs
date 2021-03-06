﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Dialogs
{
	public class SignUpDialog : Dialog
	{
		[FindsBy(How = How.XPath, Using = "/html/body/div[7]")]
		public IWebElement Root { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[7]/div[1]/span")]
		public IWebElement Name { get; set; }

		internal override bool IsDisplayed()
		{
			return Root.Displayed;
		}
	}
}