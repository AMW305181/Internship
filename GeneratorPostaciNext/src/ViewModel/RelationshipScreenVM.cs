using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPostaciNext.ViewModel
{
    public partial class RelationshipScreenVM:ObservableObject
    {
        [ObservableProperty]
        List<string> characterNames;
        [ObservableProperty]
        List<string> relatedCharacters;

        public RelationshipScreenVM()
        {
            characterNames = new List<string>();
            relatedCharacters = new List<string>();
            List<string> existingCharas = DatabaseManager.SelectAllChars();
            if (existingCharas != null) { characterNames.AddRange(existingCharas); }
            relatedCharacters.Add("<Nowa relacja>");
        }

        public void SelectAllRelationships(string name)
        {
            RelatedCharacters.RemoveRange(1, RelatedCharacters.Count);
            List<string> relationships = DatabaseManager.SelectAllRelationships(name);
            if (relationships != null) 
            {
                RelatedCharacters.AddRange(relationships);
            }
        }

        public void AddRelationship(string name)
        {
            bool success=DatabaseManager.AddRelationship(name);
            if (success)
            { RelatedCharacters.Add(name); }
           
        }
    }
}
