using System.Configuration;

namespace Lobster.PageObjectModel.Setup
{
	public class EnvironmentsDataSection : ConfigurationSection
	{
		public const string SectionName = "Environments";
		public const string EnvironmentsCollectionName = "Environment";

		[ConfigurationProperty(EnvironmentsCollectionName)]
		[ConfigurationCollection(typeof(EnvironmentCollection), AddItemName = "add")]
		public EnvironmentCollection Environment
		{
			get { return (EnvironmentCollection)base[EnvironmentsCollectionName]; }
		}
	}
}