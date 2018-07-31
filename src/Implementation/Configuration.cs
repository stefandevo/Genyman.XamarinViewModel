using System;
using System.Collections.Generic;
using Genyman.Core;

namespace Stefandevo.Genyman.XamarinViewModel.Implementation
{
	public class Configuration
	{
		public string Namespace { get; set; }
		
		public string Name { get; set; }
		public string BaseClass { get; set; }
		public bool UseNPC { get; set; }
		
		public string[] InjectedClasses { get; set; }
		public string[] Usings { get; set; }
		
		public Framework Framework { get; set; }
		
		public List<Property> Properties { get; set; }
		public List<Command> Commands { get; set; }

		[GenymanIgnore]
		public bool IsMugenMvvmToolkit => Framework == Framework.MugenMvvmToolkit;
		
		[GenymanIgnore]
		public bool IsXamarinForms => Framework == Framework.XamarinForms;

	}
	
	public class Property
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string[] AlsoNotifyFor { get; set; }
		public bool DoNotNotify { get; set; }
		public bool DoNotCheckEquality { get; set; }

		[GenymanIgnore]
		public string Indent => (DoNotCheckEquality) ? "" : "\t";
	}

	public class Command
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool CheckCanExecute { get; set; }
	}
	
	public enum Framework
	{
		XamarinForms,
		MugenMvvmToolkit
	}
}