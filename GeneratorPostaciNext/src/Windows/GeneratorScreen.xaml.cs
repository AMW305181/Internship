using GeneratorPostaciNext.ViewModel;

namespace GeneratorPostaciNext.Windows;

public partial class GeneratorScreen : ContentPage
{
	public GeneratorScreen(GeneratorScreenVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}