using System;
using System.Collections.Generic;
using Genyman.Core;

namespace Stefandevo.Genyman.XamarinViewModel.Implementation
{
	[Documentation(Remarks = "https://github.com/stefandevo/Genyman.XamarinViewModel")]
	public class Configuration
	{
		[Description("The namespace to use for the viewmodel")]
		[Required]
		public string Namespace { get; set; }

		[Description("The name of the viewmodel")]
		[Required]
		public string Name { get; set; }

		[Description("The base class for the viewmodel")]
		public string BaseClass { get; set; }

		[Description("Whether you want explicit INotifyPropertyChanged implemented")]
		public bool UseNPC { get; set; }

		[Description("A list of classes that are injected in the constructor of the viewmodel")]
		public string[] InjectedClasses { get; set; }

		[Description("For what mvvm framework this is used")]
		public Framework Framework { get; set; }

		[Description("A list of properties")]
		public List<Property> Properties { get; set; }

		[Description("A list of commands")]
		public List<Command> Commands { get; set; }

		[Ignore]
		public bool IsMugenMvvmToolkit => Framework == Framework.MugenMvvmToolkit;

		[Ignore]
		public bool IsXamarinForms => Framework == Framework.XamarinForms;
	}


	[Documentation]
	public class Property
	{
		[Description("The name of the property")]
		[Required]
		public string Name { get; set; }

		[Description("The type of the property")]
		[Required]
		public string Type { get; set; }

		[Description("A list of properties that also are notified when this property changes")]
		public string[] AlsoNotifyFor { get; set; }

		[Description("Do not generate a propertychanged when this property is changed")]
		public bool DoNotNotify { get; set; }

		[Description("Do not check equality; always sends a propertychanged")]
		public bool DoNotCheckEquality { get; set; }

		[Ignore]
		public string Indent => (DoNotCheckEquality) ? "" : "\t";
	}

	[Documentation]
	public class Command
	{
		[Description("Name of the command")]
		[Required]
		public string Name { get; set; }

		[Description("An optional type to associate to this command")]
		public string Type { get; set; }

		[Description("Whether to generate a CanExecute clause for this command")]
		public bool CheckCanExecute { get; set; }
	}

	[Documentation]
	public enum Framework
	{
		[Description("Xamarin Forms")] XamarinForms,
		[Description("Mugen MVVM Toolkit")] MugenMvvmToolkit
	}
}