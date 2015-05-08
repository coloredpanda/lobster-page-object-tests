using OpenQA.Selenium.Support.PageObjects;

namespace Lobster.PageObjectModel.Dialogs
{
	public static class Dialogs
	{
		public static LogInDialog LogIn
		{
			get
			{
				var dialog = new LogInDialog();
				PageFactory.InitElements(Browser.Driver, dialog);
				return dialog;
			}
		}

		public static SignUpDialog SignUp
		{
			get
			{
				var dialog = new SignUpDialog();
				PageFactory.InitElements(Browser.Driver, dialog);
				return dialog;
			}
		}

		public static ForgotPasswordDialog Forgot
		{
			get
			{
				var dialog = new ForgotPasswordDialog();
				PageFactory.InitElements(Browser.Driver, dialog);
				return dialog;
			}
		}
	}
}