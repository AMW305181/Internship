using GeneratorPostaciNext.ViewModel;

namespace GeneratorPostaciNext.Windows;

public partial class OtherDBScreen : ContentPage
{
	public OtherDBScreen(OtherDBScreenVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}