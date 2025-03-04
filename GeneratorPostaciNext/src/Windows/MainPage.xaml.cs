


using GeneratorPostaciNext.ViewModel;

namespace GeneratorPostaciNext.Windows
{

    /*todo
     * design each window (inserting+editing+viewing characters, editing+viewing relationships and kanji, character randomizer)
     * viewing each table 
     * inserting
     * editing
     * randomizing
     * add pictures
     * multiple characters to randomize?
     * remember to use test data
     * csv parsing?
    */
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainPageVM vm)
        {
            InitializeComponent();
            BindingContext=vm;
        }
        /*
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
           // DatabaseManager.testSelect(); 
           

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        */
        private void CharacterBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void RelationshipBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void GeneratorBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void OtherDbtn_Clicked(object sender, EventArgs e)
        {

        }

        private void CSVImportBtn_Clicked(object sender, EventArgs e)
        {

        }
    }

}
