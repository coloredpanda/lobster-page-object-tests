using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Lobster.PageObjectModel.Setup
{
	public static class EnvironmentReader
	{
		public static EnvironmentElement Get(string pageName)
		{
			var pageConfig = new List<EnvironmentElement>();

			var environmentInfo =
				ConfigurationManager.GetSection(EnvironmentsDataSection.SectionName) as EnvironmentsDataSection;

			if (environmentInfo != null)
			{
				pageConfig.AddRange(from EnvironmentElement environmentElement in environmentInfo.Environment
									select new EnvironmentElement()
											   {
												   Name = environmentElement.Name,
												   Url = environmentElement.Url,
												   PageTitle = environmentElement.PageTitle
											   });
			}

			return pageConfig.First(x => x.Name == pageName);
		}
	}
}