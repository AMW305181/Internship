using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPostaciNext.Database
{
    public class CharacterKanji
    {
        [Key]
        
        public int characterId { get; set; }
        [Required]
        public required Character character { get; set; }
        public string? nameKanji { get; set; }
    }
}
