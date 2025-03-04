
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeneratorPostaciNext.Windows;

namespace GeneratorPostaciNext.ViewModel
{
    public partial class MainPageVM:ObservableObject
    {
        [RelayCommand]
        async Task NavigateCharacter()
        {
            await Shell.Current.GoToAsync(nameof(CharacterScreen));
        }
        [RelayCommand]
        async Task NavigateRelationship()
        {
            await Shell.Current.GoToAsync(nameof(RelationshipScreen));
        }
        [RelayCommand]
        async Task NavigateGenerator()
        {
            await Shell.Current.GoToAsync(nameof(GeneratorScreen));
        }
        [RelayCommand]
        async Task NavigateOtherDB()
        {
            await Shell.Current.GoToAsync(nameof(OtherDBScreen));
        }
       
    }
}
