using GeneratorPostaciNext.ViewModel;
using Microsoft.IdentityModel.Tokens;

namespace GeneratorPostaciNext.Windows;

public partial class RelationshipScreen : ContentPage
{
	RelationshipScreenVM rvm;
	public RelationshipScreen(RelationshipScreenVM vm)
	{
		InitializeComponent();
		rvm = vm;
        BindingContext = rvm;
    }

    private void CharacterList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        string name = CharacterList.SelectedItem.ToString();
        if (!name.IsNullOrEmpty())
        { rvm.SelectAllRelationships(name); }
    }
}