
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeneratorPostaciNext.Database;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GeneratorPostaciNext.ViewModel
{
    public partial class CharacterScreenVM:ObservableObject
    {
        [ObservableProperty]
        private List<string> charactersList;

        [ObservableProperty]
        string birthYear;

        private int? convertedBirthYear = null;


        public CharacterScreenVM()
        {
            charactersList = new List<string>();
            charactersList.Add("Nowa postać");
            List<string> existingCharas =DatabaseManager.SelectAllChars();
            if (existingCharas != null) { charactersList.AddRange(existingCharas); }
      
        }
        public bool AddCharacter(string name, List<int> worlds, Gender gender, Sexuality sexuality, Generation generation)
        {
            bool success=DatabaseManager.AddCharacter(name, worlds, gender, sexuality, generation, convertedBirthYear);
            if (success) {CharactersList.Add(name);}
            return success;
        }

        public bool UpdateCharacter(string name, List<int> worlds, Gender gender, Sexuality sexuality, Generation generation) 
        {
            bool success=DatabaseManager.CharacterUpdated(name, worlds, gender, sexuality, generation, convertedBirthYear);
            return success;
        }
        
        public bool convertBirthYear()
        {
            try 
            {
                if (!this.BirthYear.IsNullOrEmpty())
                {
                    convertedBirthYear = Convert.ToInt32(this.BirthYear);
                    return true;
                }
                return true;
            }
            catch { return false; }
        }
        
        
        public Character selectCharacter(int index, string name)
        {
            Character character = null;
            if(index==0)
            {
                return character;
            }
            else 
            {
                character = DatabaseManager.SelectCharacter(name);
                if (character.birthYear == null)
                {
                    BirthYear = string.Empty;
                }
                else 
                {
                    BirthYear = character.birthYear.ToString();
                }
                return character;
            }
        }
        
        
        /* private string _selectedText = "Wybierz postać";
        private bool _isOpened;
        public string SelectedText
        { get { return _selectedText; }
            set { SetProperty(ref _selectedText, value); }
        }
        public bool IsOpened
        {
            get { return _isOpened; }
            set { SetProperty(ref _isOpened, value); }
        }

        private ObservableCollection<string> _dataList;
        public ObservableCollection<string> DataList
        {
            get { return _dataList; }
            set { SetProperty(ref _dataList, value); }
        }

        public Command SelectedItem 
        { get { return new Command((obj)=> SelectedItemAction(obj)); } }

        private void SelectedItemAction(object obj)
        {
            IsOpened = false;
            SelectedText = obj.ToString();
        }
        public ICommand SelectionCommand { private set; get; }
        public CharacterScreenVM()
        {
            DataList = new ObservableCollection<string>();
            DataList.Add("Nowa postać");
            SelectionCommand = new Command(
                 execute: () =>
                 {
                     IsOpened = false;
                     RefreshCanExecutes();
                 },
                 canExecute: () =>
                 {
                     IsOpened = true;
                     return true;
                 });
        }
        void RefreshCanExecutes()
        {
            ((Command)SelectionCommand).ChangeCanExecute();

        }*/
    }
}
