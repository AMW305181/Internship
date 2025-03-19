using GeneratorPostaciNext.Database;
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
    //testing needed
    private async void UpdateRelButton_Clicked(object sender, EventArgs e)
    {
        string name = CharacterList.SelectedItem.ToString();
        string? relatedName;
        relationshipType type = CheckRelationshipType();
        bool isFamily = type == relationshipType.Spouse ? false : true;
        if (RelationshipsList.SelectedItem.ToString()=="<Nowa relacja>") //adding relationship
        {
            relatedName=NewRelationshipPicker.SelectedItem.ToString();
            if (relatedName.IsNullOrEmpty())
            {
                await DisplayAlert("Błąd", "Nie wybrano postaci.", "OK");
                return;
            }
            else
            {
                bool success = rvm.AddRelationship(name, relatedName, type, isFamily);
                if (success)
                {
                    await DisplayAlert("Komunikat", "Relacja została dodana do bazy.", "OK");
                    return;
                }
                else {
                    await DisplayAlert("Błąd", "Nie udało się dodać relacji.", "OK");
                    return;
                }
            }
        }
        else 
        { 
            relatedName = RelationshipsList.SelectedItem.ToString(); //updating relationship
            bool success=rvm.UpdateRelationship(name, relatedName, type, isFamily);
            if (success) 
            {
                await DisplayAlert("Komunikat", "Relacja została zaktualizowana.", "OK");
                return;
            }
            else
            {
                await DisplayAlert("Błąd", "Nie udało się zaktualizować relacji.", "OK");
                return;
            }
        } 

    }

    private relationshipType CheckRelationshipType()
    {
        relationshipType type = 0;
        if (RSButton.IsChecked) {type = relationshipType.Spouse; }
        else if (RPButton.IsChecked)  { type = relationshipType.Parent; }
        else if (RCButton.IsChecked) { type=relationshipType.Child; }
        else if (RSibButton.IsChecked) { type=relationshipType.Sibling; }
        else if (RFButton.IsChecked){ type=relationshipType.OherFamily; }
        return type;
    }

    private void RelationshipsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        string relatedName=RelationshipsList.SelectedItem.ToString();
        if(relatedName!="<Nowa relacja>")
        {
            NewRelationshipPicker.IsVisible = false;
            //pobierz typ relacji i ustaw guzik
        }
        else
        {
            NewRelationshipPicker.IsVisible = true;
        }

    }

    private void SetRelationshipType(relationshipType type)
    {
        if (type == relationshipType.Spouse) { RSButton.IsChecked = true; }
        else if(type == relationshipType.Parent) { RPButton.IsChecked = true; }
        else if (type == relationshipType.Child) { RCButton.IsChecked = true; }
        else if (type == relationshipType.Sibling) { RSibButton.IsChecked = true; }
        else if (type == relationshipType.OherFamily) { RFButton.IsChecked = true; }
    }

}