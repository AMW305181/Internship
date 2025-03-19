using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorPostaciNext.Database;
using System.Windows;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using System.Data.Entity;

namespace GeneratorPostaciNext
{
    public static class DatabaseManager
    {
        private static CharGenContext db = new CharGenContext();

        public static bool AddCharacter(string name, List<int> worlds, Gender gender, Sexuality sexuality, Generation generation, int? birthYear = null)
        {
            try
            {
                Character character = new Character { name = name, worlds = worlds, gender = gender, sexuality = sexuality, generation = generation, birthYear = birthYear };
                db.Characters.Add(character);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public static bool CharacterExists(string name)
        {
            try
            {
                if (db.Characters.Where(c => c.name == name).FirstOrDefault() != null) { return true; }
                return false;
            }
            catch { return false; }
        }

        public static bool CharacterUpdated(string name, List<int> worlds, Gender gender, Sexuality sexuality, Generation generation, int? birthYear = null)
        {
            try
            {
                Character character = db.Characters.Where(c => c.name == name).FirstOrDefault();
                character.name = name;
                character.worlds = worlds;
                character.gender = gender;
                character.sexuality = sexuality;
                character.generation = generation;
                character.birthYear = birthYear;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        public static List<string> SelectAllChars()
        {
            try
            {
                var characters = db.Characters
                .OrderBy(c => c.name)
                .Select(c => c.name)
                .ToList();
                return characters;
            }
            catch { return null; }
        }
        public static Character SelectCharacter(string name)
        {
            try
            {
                Character character = db.Characters.Where(c => c.name == name).FirstOrDefault();
                return character;
            }
            catch { return null; }
        }

        public static List<string> SelectAllRelationships(string name)
        {
            try 
            {
                var relationships = db.CharacterRelationships.Where(r => r.character.name == name).Select(r=>r.relatedCharacter.name).ToList();
                return relationships;
            }
            catch { return null; }
        }

        public static bool AddRelationship(string name, string relatedName, relationshipType type, bool isFamily)
        {
            try {
                int charID = db.Characters.Where(c=>c.name==name).Select(c=>c.characterId).FirstOrDefault();
                int relatedId=db.Characters.Where(c=>c.name==relatedName).Select(c=>c.characterId).FirstOrDefault();
                CharacterRelationship relationship = new CharacterRelationship { characterId = charID, relatedCharacterId = relatedId, type = type, isFamily = isFamily };
                db.CharacterRelationships.Add(relationship);
                if (type==relationshipType.Parent)
                { 
                    CharacterRelationship relationshipReversed = new CharacterRelationship { characterId = relatedId, relatedCharacterId = charID, type = relationshipType.Child, isFamily = isFamily };
                    db.CharacterRelationships.Add(relationshipReversed);    
                }
                else if(type==relationshipType.Child)
                {
                    CharacterRelationship relationshipReversed = new CharacterRelationship { characterId = relatedId, relatedCharacterId = charID, type = relationshipType.Parent, isFamily = isFamily };
                    db.CharacterRelationships.Add(relationshipReversed);
                }
                else 
                { 
                    CharacterRelationship relationshipReversed = new CharacterRelationship { characterId = relatedId, relatedCharacterId = charID, type = type, isFamily = isFamily };
                    db.CharacterRelationships.Add(relationshipReversed);  
                }
                db.SaveChanges();
                return true; }
            catch { return false; }
        }

        public static bool UpdateRelationship(string name, string relatedName, relationshipType type, bool isFamily)
        {
            try {
                int charID = db.Characters.Where(c => c.name == name).Select(c => c.characterId).FirstOrDefault();
                int relatedId = db.Characters.Where(c => c.name == relatedName).Select(c => c.characterId).FirstOrDefault();
                CharacterRelationship relationship=db.CharacterRelationships.Where(r=>r.characterId == charID && r.relatedCharacterId==relatedId).FirstOrDefault();
                CharacterRelationship relationshipReversed = db.CharacterRelationships.Where(r => r.characterId == relatedId && r.relatedCharacterId == charID).FirstOrDefault();
                if (relationship == null) { return false; }
                relationship.type= type;
                relationship.isFamily= isFamily;
                relationshipReversed.isFamily = isFamily;
                if (type == relationshipType.Parent) { relationshipReversed.type = relationshipType.Child; }
                else if (type == relationshipType.Child) { relationshipReversed.type = relationshipType.Parent; }
                else { relationshipReversed.type = type; }
                db.SaveChanges();
                return true; }
            catch { return false; }
        }
    }

}
