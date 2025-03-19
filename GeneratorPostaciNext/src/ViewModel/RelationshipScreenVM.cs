using CommunityToolkit.Mvvm.ComponentModel;
using GeneratorPostaciNext.Database;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPostaciNext.ViewModel
{
    public partial class RelationshipScreenVM:ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> characterNames;
        [ObservableProperty]
        ObservableCollection<string> relatedCharacters;

        public RelationshipScreenVM()
        {
            characterNames = new ObservableCollection<string>();
            relatedCharacters = new ObservableCollection<string>();
            List<string> existingCharas = DatabaseManager.SelectAllChars();
            if (existingCharas != null) 
            { foreach (string chara in existingCharas)
                {
                    CharacterNames.Add(chara);
                }
             }
            RelatedCharacters.Add("<Nowa relacja>");
        }

        public void SelectAllRelationships(string name)
        {
            if (RelatedCharacters.Count > 1)
            {
                for (int i = 1; i < RelatedCharacters.Count; i++)
                {
                    RelatedCharacters.RemoveAt(RelatedCharacters.Count-i);
                }
            }
            List<string> relationships = DatabaseManager.SelectAllRelationships(name);
            if (!relationships.IsNullOrEmpty())
            {
                foreach (string chara in relationships)
                {
                    RelatedCharacters.Add(chara);
                }
            }
            int j = 0;
        }

        public bool AddRelationship(string name, string relatedName, relationshipType type, bool isFamily)
        {
            bool success=DatabaseManager.AddRelationship(name, relatedName, type, isFamily);
            if (success)
            { RelatedCharacters.Add(relatedName);}
            return success;   
        }

        public bool UpdateRelationship(string name, string relatedName, relationshipType type, bool isFamily)
        {
            bool success = DatabaseManager.UpdateRelationship(name, relatedName, type, isFamily);
            return success;
        }
    }
}
