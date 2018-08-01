using System;
using System.IO;
using Genyman.Core;
using Genyman.Core.Handlebars;
using Genyman.Core.MSBuild;
using HandlebarsDotNet;

namespace Stefandevo.Genyman.XamarinViewModel.Implementation
{
	internal class Generator : GenymanGenerator<Configuration>
	{
		public override void Execute()
		{
			// ViewModel
			var viewModelFile = Path.Combine(TemplatePath, "{{ Name }}.cs");
			var viewModelTargetFile = Path.Combine(WorkingDirectory, TargetFileName(viewModelFile, TemplatePath));

			viewModelTargetFile = FluentHandlebars.Create(this)
				.WithAllHelpers()
				.HavingModel(Configuration)
				.UsingFileTemplate(viewModelFile)
				.OutputFile(viewModelTargetFile, false); //DO NOT OVERWRITE ViewModel

			FluentMSBuild.Use(viewModelTargetFile)
				.WithBuildAction(BuildAction.Compile)
				.AddToProject();

			// generated ViewModel
			var generatedViewModelFile = Path.Combine(TemplatePath, "{{ Name }}.generated.cs");
			var generatedViewModelTargetFile = Path.Combine(WorkingDirectory, TargetFileName(generatedViewModelFile, TemplatePath));

			generatedViewModelTargetFile = FluentHandlebars.Create(this)
				.WithAllHelpers()
				.WithCustomHelper(Helpers)
				.HavingModel(Configuration)
				.UsingFileTemplate(generatedViewModelFile)
				.OutputFile(generatedViewModelTargetFile, true);

			FluentMSBuild.Use(generatedViewModelTargetFile)
				.WithBuildAction(BuildAction.Compile)
				.WithDependencyOn(viewModelTargetFile)
				.AddToProject();

			FluentMSBuild.Use(InputFileName)
				.WithBuildAction(BuildAction.None)
				.WithDependencyOn(viewModelTargetFile)
				.AddToProject();
		}

		internal void Helpers(IHandlebars handlebars)
		{
			handlebars.RegisterHelper("command-ctor", (writer, context, parameters) => { RenderCommandConstructor(parameters, context, writer); });
			handlebars.RegisterHelper("command-exec", (writer, context, parameters) => { RenderCommandFunctions(parameters, writer); });
			handlebars.RegisterHelper("command-can-exec", (writer, context, parameters) => { RenderCommandCanExecute(parameters, writer); });
			handlebars.RegisterHelper("command-type", (writer, context, parameters) => { RenderCommandProperty(writer); });
			handlebars.RegisterHelper("base", (writer, context, parameters) => { RenderBaseClass(parameters, writer); });
		}

		static void RenderBaseClass(object[] parameters, TextWriter writer)
		{
			var baseType = parameters[0].ToString();
			var useNPC = (bool) parameters[1];
			if (!string.IsNullOrEmpty(baseType) && useNPC)
				baseType += ", "; //"INotifyPropertyChanged";
			if (useNPC)
				baseType += "INotifyPropertyChanged";
			writer.WriteSafeString(baseType);
		}

		void RenderCommandProperty(TextWriter writer)
		{
			switch (Configuration.Framework)
			{
				case Framework.MugenMvvmToolkit:
					writer.WriteSafeString("IRelayCommand");
					break;
				case Framework.XamarinForms:
					writer.WriteSafeString("ICommand");
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		void RenderCommandCanExecute(object[] parameters, TextWriter writer)
		{
			var name = parameters[0].ToString();
			var type = parameters[1]?.ToString();

			if (Configuration.Framework == Framework.XamarinForms && string.IsNullOrEmpty(type))
				type = "object";

			if (!string.IsNullOrEmpty(type))
				type = $"{type} value";

			writer.WriteSafeString($"{name}({type})");
		}

		void RenderCommandFunctions(object[] parameters, TextWriter writer)
		{
			var name = parameters[0].ToString();
			var type = parameters[1]?.ToString();

			if (Configuration.Framework == Framework.XamarinForms && string.IsNullOrEmpty(type))
				type = "object";

			if (!string.IsNullOrEmpty(type))
				type = $"{type} value";

			writer.WriteSafeString($"Task {name}({type})");
		}

		void RenderCommandConstructor(object[] parameters, dynamic context, TextWriter writer)
		{
			var name = parameters[0].ToString();
			var type = parameters[1]?.ToString();

			if (!string.IsNullOrEmpty(type))
				type = $"<{type}>";

			Command command = context as Command;
			switch (Configuration.Framework)
			{
				case Framework.MugenMvvmToolkit:
				{
					var output = $"{name}Command = RelayCommandBase.FromAsyncHandler{type}({name}";
					if (command.CheckCanExecute)
						output += $", CanExecute{name}";
					output += ", false);";
					writer.WriteSafeString(output);
					break;
				}

				case Framework.XamarinForms:
				{
					var param = string.IsNullOrEmpty(type) ? "" : "value";
					var output = $"{name}Command = new Command{type}(async ({param}) => await {name}({param})";
					if (command.CheckCanExecute)
						output += $", CanExecute{name}";
					output += ");";
					writer.WriteSafeString(output);
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}