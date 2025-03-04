using GeneratorPostaciNext.ViewModel;

namespace GeneratorPostaciNext.Windows;

public partial class RelationshipScreen : ContentPage
{
	public RelationshipScreen(RelationshipScreenVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}