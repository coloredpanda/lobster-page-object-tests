using System;
using System.Collections.Generic;
using System.Configuration;

namespace Lobster.PageObjectModel.Setup
{
	public static class Settings
	{
		public static readonly string Browser = ConfigurationManager.AppSettings["Browser"];
		public static readonly string CaptureLocation = ConfigurationManager.AppSettings["ErrorCaptureLocation"];
	}
}