using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeneratorPostaciNext.Database
{
    public enum Generation {Universal, Elder, Mature, Young, Child}
    public enum Gender { F, M, X}
    public enum Sexuality { Ace, Hetero, Homo, Bi, Pan}
    public class Character
    {
       
        public int characterId { get; set; }
        public required string name { get; set; }
        public List<int> worlds { get; set; }=new List<int>();
        public Gender gender { get; set; }
        public Sexuality sexuality { get; set; }
        public Generation generation { get; set; }
        public int? birthYear { get; set; }
        public virtual ICollection<CharacterRelationship> relationships { get; set; }= new List<CharacterRelationship>();
        public virtual CharacterKanji? characterKanji { get; set; }
    }
}
