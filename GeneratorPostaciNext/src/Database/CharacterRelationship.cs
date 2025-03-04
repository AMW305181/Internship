using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace GeneratorPostaciNext.Database
{
    public enum relationshipType { Spouse, Parent, Child, Sibling, OherFamily}
    public class CharacterRelationship
    {
        public required int characterId {  get; set; }
        [Required]
        public Character character { get; set; }
        
        public int relatedCharacterId { get; set; }
        [Required]
        public Character relatedCharacter { get; set; }

        [Required]
        public relationshipType type { get; set; }

        bool isFamily { get; set; }
    }
}
