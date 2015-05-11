using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using HtmlAgilityPack;
using Thread = System.Threading.Thread;

namespace Lobster.Mail
{
	public class Gmail
	{
		public static UserCredential Credential;

		public static GmailService Service;

		public static string User = "me";

		public static Dictionary<string, LinkItem> LetterLinks; 

		static Gmail()
		{
			Credential = Preparation.GetUserCredential();
			Service = Preparation.GetService(Credential);
		}

		public static int GetUnreadMessagesCount()
		{
			var messages = Messages.ListMessages(Service, User, "in:inbox is:unread");

			return messages.Count;
		}

		public static Message GetUnreadMessage()
		{
			var messages = Messages.ListMessages(Service, User, "in:inbox is:unread");

			return messages.Any() ? Messages.GetMessage(Service, User, messages.First().Id) : null;
		}

		public static void DeleteAllUnreadMessages()
		{
			Credential = Preparation.GetUserCredential();

			var messages = Messages.ListMessages(Preparation.GetService(Credential), User, "in:inbox is:unread");

			if (!messages.Any()) 
				return;

			foreach (var message in messages)
			{
				Messages.DeleteMessage(Service, User, message.Id);
			}
		}

		public static void DeleteUnread()
		{
			if (GetUnreadMessagesCount() > 0)
			{
				DeleteAllUnreadMessages();
			}
		}

		public static void TrashAllMessages()
		{
			var messages = Messages.ListMessages(Service, User, "in:inbox is:unread");

			if (!messages.Any())
				return;

			foreach (var message in messages)
			{
				Messages.TrashMessage(Service, User, message.Id);
			}
		}

		public static string GetMessageBody()
		{
			var unreadMessage = GetUnreadMessage();

			if (unreadMessage != null)
			{
				var body = unreadMessage.Payload.Body.Data;

				string converted = body.Replace('-', '+');
				converted = converted.Replace('_', '/');

				byte[] data = Convert.FromBase64String(converted);
				string decodedString = Encoding.UTF8.GetString(data);

				return decodedString;
			}

			return null;
		}

		public static HtmlDocument GetMessageBodyHtml(string letter)
		{
			var htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(letter);

			return htmlDocument;
		}

		public static LinkItem GetEmailFromLetter(string letter)
		{
			var list = Find(letter);

			return list[1];
		}

		public static LinkItem GetResetPasswordLinkFromLetter(string letter)
		{
			var list = Find(letter);

			return list[2];
		}

		public struct LinkItem
		{
			public string Href;
			public string Text;

			public override string ToString()
			{
				return Href + "\n\t" + Text;
			}
		}

		public static List<LinkItem> Find(string file)
		{
			List<LinkItem> list = new List<LinkItem>();

			// 1.
			// Find all matches in file.
			MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
				RegexOptions.Singleline);

			// 2.
			// Loop over each match.
			foreach (Match m in m1)
			{
				string value = m.Groups[1].Value;
				LinkItem i = new LinkItem();

				// 3.
				// Get href attribute.
				Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
				RegexOptions.Singleline);
				if (m2.Success)
				{
					i.Href = m2.Groups[1].Value;
				}

				// 4.
				// Remove inner tags from text.
				string t = Regex.Replace(value, @"\s*<.*?>\s*", "",
				RegexOptions.Singleline);
				i.Text = t;

				list.Add(i);
			}
			return list;
		}

		public static void WaitLetter()
		{
			var start = 0;
			const int finish = 5;

			while (GetUnreadMessagesCount() <= 0)
			{
				Thread.Sleep(1000);

				start++;

				if (start == finish)
				{
					throw new Exception("Timed out");
				}
			}
		}

		public static void GetLetterLinks()
		{
			var letter = GetMessageBody();
			var links = Find(letter);

			LetterLinks = new Dictionary<string, LinkItem>
			{
				{"Header", links[0]},
				{"User", links[1]},
				{"Reset", links[2]},
				{"Suport", links[3]},
				{"Footer", links[4]},
			};
		} 
	}
}