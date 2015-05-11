using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Lobster.Mail
{
	public class Preparation
	{
		public static string[] Scopes = { GmailService.Scope.MailGoogleCom };

		public static string ApplicationName = "Gmail API Quickstart";

		public static UserCredential GetUserCredential()
		{
			UserCredential credential;

			using (var stream = new FileStream("client_secrets_desktop.json", FileMode.Open, FileAccess.Read))
			{
				var credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				credPath = Path.Combine(credPath, ".credentials");

				credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
					GoogleClientSecrets.Load(stream).Secrets,
					Scopes,
					"user",
					CancellationToken.None,
					new FileDataStore(credPath, true)).Result;

				Console.WriteLine("Credential file saved to: " + credPath);
			}

			return credential;
		}

		public static GmailService GetService(UserCredential credential)
		{
			// Create Gmail Service.
			return new GmailService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = credential,
				ApplicationName = ApplicationName
			});
		}
	}
}