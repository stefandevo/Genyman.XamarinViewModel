using System.Collections.Generic;

namespace Stefandevo.Genyman.XamarinViewModel.Implementation
{
	public class NewTemplate : Configuration
	{
		public NewTemplate()
		{
			Namespace = "Your.ClassNamespace";
			Name = "YourViewModel";
			BaseClass = "";
			
			Framework = Framework.MugenMvvmToolkit;
			
			Properties = new List<Property>()
			{
				new Property()
				{
					Name = "FirstName",
					Type = "string"
				},
				new Property()
				{
					Name = "LastName",
					Type = "string"
				}
			};
			
			Commands = new List<Command>()
			{
				new Command()
				{
					Name = "Save",
				},
				new Command()
				{
					Name = "Delete",
					CheckCanExecute = true
				}
			};
		}
	}
}