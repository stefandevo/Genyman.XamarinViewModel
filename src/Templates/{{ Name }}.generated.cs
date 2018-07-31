using System;
using System.Threading.Tasks;
{{#if UseNPC}}
using System.ComponentModel;
{{/if}}
{{#if IsMugenMvvmToolkit}}
using MugenMvvmToolkit.Interfaces.Models;
using MugenMvvmToolkit.Models;
{{/if}}
{{#if IsXamarinForms}}
using System.Windows.Input;
{{/if}}

namespace {{ Namespace }}
{
	public abstract class {{ Name }}Base : {{ base BaseClass UseNPC }}
	{
	
		{{ #each InjectedClasses }}
		protected readonly {{ this }} {{ csharp-membervar this }};
		{{ /each }}
	
		public {{ Name }}Base({{ csharp-parameters InjectedClasses }})
		{
			{{ #each InjectedClasses }}
			{{ csharp-membervar this }} = {{ csharp-var this }};
			{{ /each }}
			{{ #Commands }}
			{{ command-ctor Name Type }}
			{{ /Commands }}
		}
	
		{{#if UseNPC}}
		public event PropertyChangedEventHandler PropertyChanged;
		{{/if}}
			
		{{ #Commands }}
		public {{ command-type this }} {{ Name }}Command { get; }
		protected abstract {{ command-exec Name Type }};
		{{#if CheckCanExecute}}
		protected abstract bool CanExecute{{ command-can-exec Name Type }};
		{{/if}}
		
		{{ /Commands }}		
		
		{{ #Properties }}
		{{ Type }} {{ csharp-membervar Name }};
		public {{ Type }} {{ Name }}
		{
			get => {{ csharp-membervar Name }};
			set
			{
				{{ #unless DoNotCheckEquality }}
				if (value != {{ csharp-membervar Name }})
				{
				{{ /unless }}
				{{ Indent }}{{ csharp-membervar Name }} = value;
				{{ #unless DoNotNotify }}
				{{ Indent }}OnPropertyChanged("{{ Name }}");
				{{ /unless }}
				{{ #each AlsoNotifyFor }}
				{{ Indent }}OnPropertyChanged("{{ this }}");
				{{ /each }}
				{{ #unless DoNotCheckEquality }}
				}
				{{ /unless }}
			}
		}

		{{ /Properties }}
	
		{{#if UseNPC}}
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		{{/if}}

	}
}