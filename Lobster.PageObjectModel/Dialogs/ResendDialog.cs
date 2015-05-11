using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Dialogs
{
	public class ResendDialog : Dialog
	{
		[FindsBy(How = How.XPath, Using = "/html/body/div[8]")]
		public IWebElement Root { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[8]/div[1]/span")]
		public IWebElement NameElement { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[8]/div[1]/a")]
		public IWebElement CloseButton { get; set; }

		[FindsBy(How = How.Id, Using = "register-success-close")]
		public IWebElement GotItButton { get; set; }

		[FindsBy(How = How.XPath, Using = "/html/body/div[8]/div[2]/div/div/text()")]
		public IWebElement MessageElement { get; set; }

		public void Close()
		{
			Browser.Wait();
			CloseButton.Click();
		}

		public void GotIt()
		{
			Browser.Wait();
			GotItButton.Click();
		}

		public string Message()
		{
			Browser.Wait();
			return MessageElement.Text;
		}

		public string Name()
		{
			Browser.Wait();
			return NameElement.Text;
		}
	}
}