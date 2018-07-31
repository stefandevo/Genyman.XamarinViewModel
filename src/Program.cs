using Stefandevo.Genyman.XamarinViewModel.Implementation;
using Genyman.Core;

namespace Stefandevo.Genyman.XamarinViewModel
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			GenymanApplication.Run<Configuration, NewTemplate, Generator>(args);
		}
	}
}