using GeneratorPostaciNext.Windows;

namespace GeneratorPostaciNext
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CharacterScreen), typeof(CharacterScreen));
            Routing.RegisterRoute(nameof(GeneratorScreen), typeof(GeneratorScreen));
            Routing.RegisterRoute(nameof(OtherDBScreen), typeof(OtherDBScreen));
            Routing.RegisterRoute(nameof(RelationshipScreen), typeof(RelationshipScreen));
        }
    }
}
