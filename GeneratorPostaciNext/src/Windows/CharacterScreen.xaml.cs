using CommunityToolkit.Mvvm.Input;
using GeneratorPostaciNext.Database;
using GeneratorPostaciNext.ViewModel;
using Microsoft.IdentityModel.Tokens;

namespace GeneratorPostaciNext.Windows;

public partial class CharacterScreen : ContentPage
{
    CharacterScreenVM cvm;
	public CharacterScreen(CharacterScreenVM vm)
	{
		InitializeComponent();
        cvm = vm;
        BindingContext = cvm;
        
	}
    
    async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        if (CharacterPicker.SelectedItem == null)
        {
            await DisplayAlert("Błąd", "Nie wybrano postaci.", "OK");
            return;
        }
        if (!cvm.convertBirthYear()) 
        {
            await DisplayAlert("Błąd", "Błędny rok urodzenia. Upewnij się, że wpisano wyłącznie cyfry.", "OK");
            return;
        }
        List<int> worlds=CheckWorlds(); //check if not empty
        if (worlds.IsNullOrEmpty()) 
        {
            await DisplayAlert("Błąd", "Nie wybrano żadnego świata. Przed dodaniem lub zmianą danych postaci wybierz co najmniej jeden świat, do którego należy.", "OK");
            return;
        }//error
        Gender gender=CheckGender();
        Sexuality sexuality=CheckSexuality();
        Generation generation=CheckGeneration();
        if (CharacterPicker.SelectedIndex == 0) //adding character
        {
            string name = await DisplayPromptAsync("Dodawanie nowej postaci", "Wpisz imię postaci");
            if (name.IsNullOrEmpty())
            {
                await DisplayAlert("Błąd", "Nie nadano imienia postaci. Wpisz imię postaci, aby dodać ją do bazy.", "OK");
                return;
            }
            else if (DatabaseManager.CharacterExists(name))
            {
                await DisplayAlert("Błąd", "Postać o podanym imieniu istnieje już w bazie. Spróbuj zapisać imię tak, by postacie były rozróżnialne, np. dodając pierwszą literę nazwiska.", "OK");
                return;
            }
            else
            {
                bool success = cvm.AddCharacter(name, worlds, gender, sexuality, generation);
                if (success)
                {
                    await DisplayAlert("Komunikat", "Postać została dodana do bazy.", "OK");
                    return;
                }
                else
                {
                    await DisplayAlert("Błąd", "Nie udało się dodać postaci. Spróbuj jeszcze raz.", "OK");
                    return;
                }
            }
        }
        else //updating existing character
        {
            string name=CharacterPicker.SelectedItem.ToString();
            bool success=cvm.UpdateCharacter(name,worlds,gender,sexuality,generation);
            if (success)
            {
                await DisplayAlert("Komunikat", "Zaktualizowano dane postaci.", "OK");
                return;
            }
            else 
            {
                await DisplayAlert("Błąd", "Nie udało się zaktualizować danych postaci. Spróbuj jeszcze raz.", "OK");
                return;
            }
        }
        
    }

	private List<int> CheckWorlds()
	{ List<int> worlds = new List<int>();
		if (World1Check.IsChecked) { worlds.Add(1); }
        if (World2Check.IsChecked) { worlds.Add(2); }
        if (World3Check.IsChecked) { worlds.Add(3); }
        if (World4Check.IsChecked) { worlds.Add(4); }
        if (World5Check.IsChecked) { worlds.Add(5); }
        return worlds;
	}

	private Gender CheckGender()
	{
		Gender gender=0;
        if(GFButton.IsChecked)
        { gender = Gender.F; }
        else if (GMButton.IsChecked)
        { gender = Gender.M; }
        else if (GXButton.IsChecked)
        { gender = Gender.X; }
        return gender;
	}
    private Sexuality CheckSexuality()
    {
        Sexuality sexuality = 0;
        if(SAButton.IsChecked)
        { sexuality = Sexuality.Ace; }
        else if (SHetButton.IsChecked)
        { sexuality = Sexuality.Hetero; }
        else if (SHoButton.IsChecked)
        { sexuality = Sexuality.Homo; }
        else if (SBButton.IsChecked)
        { sexuality = Sexuality.Bi; }
        else if (SPButton.IsChecked)
        { sexuality = Sexuality.Pan; }
        return sexuality;
    }
    private Generation CheckGeneration()
    {
        Generation generation = 0;
        if(GenUButton.IsChecked)
        { generation=Generation.Universal; }
        else if (GenEButton.IsChecked)
        { generation = Generation.Elder; }
        else if (GenMButton.IsChecked)
        { generation = Generation.Mature; }
        else if (GenYButton.IsChecked)
        { generation = Generation.Young; }
        else if (GenBButton.IsChecked)
        { generation = Generation.Child; }
        return generation;
    }

    private void SetWorlds(List<int> worlds) 
    {
        for (int i=1; i<6; i++)
        {
            if (worlds.Contains(i))
            {
                switch (i)
                    {
                    case 1: World1Check.IsChecked = true; break;
                    case 2: World2Check.IsChecked = true; break;
                    case 3: World3Check.IsChecked = true; break;
                    case 4: World4Check.IsChecked = true; break;
                    case 5: World5Check.IsChecked = true; break;
                }
            }
            else 
            {
                switch (i)
                {
                    case 1: World1Check.IsChecked = false; break;
                    case 2: World2Check.IsChecked = false; break;
                    case 3: World3Check.IsChecked = false; break;
                    case 4: World4Check.IsChecked = false; break;
                    case 5: World5Check.IsChecked = false; break;
                }
            }
        }
        
    }
    private void SetGender(Gender gender) 
    {
        switch (gender) 
            {
            case Gender.F: GFButton.IsChecked = true; break;
            case Gender.M: GMButton.IsChecked = true; break;
            case Gender.X: GFButton.IsChecked = true; break;
        }
    }
    private void SetSexuality(Sexuality sexuality) 
    {
        switch (sexuality) 
        {
            case Sexuality.Ace: SAButton.IsChecked = true; break;
            case Sexuality.Hetero: SHetButton.IsChecked = true; break;
            case Sexuality.Homo: SHoButton.IsChecked = true; break;
            case Sexuality.Bi: SBButton.IsChecked = true; break;
            case Sexuality.Pan: SPButton.IsChecked = true; break;
        }
    }
    private void SetGeneration(Generation generation)
    {
        switch (generation) 
        {
            case Generation.Universal: GenUButton.IsChecked = true; break;
            case Generation.Elder: GenEButton.IsChecked = true; break;
            case Generation.Mature: GenMButton.IsChecked = true; break;
            case Generation.Young: GenYButton.IsChecked = true; break;
            case Generation.Child: GenBButton.IsChecked = true; break;

        }
    }

    private async void CharacterPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        Character character=cvm.selectCharacter(CharacterPicker.SelectedIndex, CharacterPicker.SelectedItem.ToString());
        if (character != null)
        {
            SetWorlds(character.worlds);
            SetGender(character.gender);
            SetSexuality(character.sexuality);
            SetGeneration(character.generation);
        }
        else if (CharacterPicker.SelectedIndex != 0)
        {
            await DisplayAlert("Błąd", "Nie znaleziono postaci. Upewnij się, że postać istnieje w bazie.", "OK");
            return;
        }

    }
}