using System.Configuration;

namespace Lobster.PageObjectModel.Setup
{
	public class EnvironmentCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new EnvironmentElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((EnvironmentElement)element).Name;
		}
	}
}