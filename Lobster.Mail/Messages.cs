using System;
using System.Collections.Generic;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;

// ReSharper disable BuiltInTypeReferenceStyle

namespace Lobster.Mail
{
	public class Messages
	{
		/// <summary>
		/// List all Messages of the user's mailbox matching the query.
		/// </summary>
		/// <param name="service">Gmail API service instance.</param>
		/// <param name="userId">User's email address. The special value "me"
		/// can be used to indicate the authenticated user.</param>
		/// <param name="query">String used to filter Messages returned.</param>
		public static List<Message> ListMessages(GmailService service, String userId, String query)
		{
			List<Message> result = new List<Message>();
			UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
			request.Q = query;

			do
			{
				try
				{
					ListMessagesResponse response = request.Execute();
					result.AddRange(response.Messages);
					request.PageToken = response.NextPageToken;
				}
				catch (Exception e)
				{
					Console.WriteLine("An error occurred: " + e.Message);
				}
			} while (!String.IsNullOrEmpty(request.PageToken));

			return result;
		}

		/// <summary>
		/// Retrieve a Message by ID.
		/// </summary>
		/// <param name="service">Gmail API service instance.</param>
		/// <param name="userId">User's email address. The special value "me"
		/// can be used to indicate the authenticated user.</param>
		/// <param name="messageId">ID of Message to retrieve.</param>
		public static Message GetMessage(GmailService service, String userId, String messageId)
		{
			try
			{
				return service.Users.Messages.Get(userId, messageId).Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}

			return null;
		}

		/// <summary>
		/// Delete a Message.
		/// </summary>
		/// <param name="service">Gmail API service instance.</param>
		/// <param name="userId">User's email address. The special value "me"
		/// can be used to indicate the authenticated user.</param>
		/// <param name="messageId">ID of the Message to delete.</param>
		public static void DeleteMessage(GmailService service, String userId, String messageId)
		{
			try
			{
				service.Users.Messages.Delete(userId, messageId).Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}
		}

		public static void TrashMessage(GmailService service, String userId, String messageId)
		{
			try
			{
				service.Users.Messages.Trash(userId, messageId).Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}
		}

		/// <summary>
		/// Insert an email Message into the user's mailbox.
		/// </summary>
		/// <param name="service">Gmail API service instance.</param>
		/// <param name="userId">User's email address. The special value "me"
		/// can be used to indicate the authenticated user.</param>
		/// <param name="email">Email to be inserted.</param>
		public static Message InsertMessage(GmailService service, String userId, Message email)
		{
			try
			{
				return service.Users.Messages.Insert(email, userId).Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}

			return null;
		}

		/// <summary>
		/// Modify the Labels a Message is associated with.
		/// </summary>
		/// <param name="service">Gmail API service instance.</param>
		/// <param name="userId">User's email address. The special value "me"
		/// can be used to indicate the authenticated user.</param>
		/// <param name="messageId">ID of Message to modify.</param>
		/// <param name="labelsToAdd">List of label ids to add.</param>
		/// <param name="labelsToRemove">List of label ids to remove.</param>
		public static Message ModifyMessage(GmailService service, String userId,
			String messageId, List<String> labelsToAdd, List<String> labelsToRemove)
		{
			ModifyMessageRequest mods = new ModifyMessageRequest();
			mods.AddLabelIds = labelsToAdd;
			mods.RemoveLabelIds = labelsToRemove;

			try
			{
				return service.Users.Messages.Modify(mods, userId, messageId).Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}

			return null;
		}

		/// <summary>
		/// Send an email from the user's mailbox to its recipient.
		/// </summary>
		/// <param name="service">Gmail API service instance.</param>
		/// <param name="userId">User's email address. The special value "me"
		/// can be used to indicate the authenticated user.</param>
		/// <param name="email">Email to be sent.</param>
		public static Message SendMessage(GmailService service, String userId, Message email)
		{
			try
			{
				return service.Users.Messages.Send(email, userId).Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine("An error occurred: " + e.Message);
			}

			return null;
		}
	}
}