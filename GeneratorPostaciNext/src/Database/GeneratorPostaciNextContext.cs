using Microsoft.EntityFrameworkCore;
using System;
//using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorPostaciNext.Database
{
    public class CharGenContext: DbContext
    {
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterKanji> Kanjis { get; set; }
        public virtual DbSet<CharacterRelationship> CharacterRelationships { get; set; }
        public CharGenContext() : base()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=CharGenNext;ConnectRetryCount=1");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja wielu do wielu z tabelą przejściową
            modelBuilder.Entity<CharacterRelationship>()
                .HasKey(cr => new { cr.characterId, cr.relatedCharacterId })
                ;

            modelBuilder.Entity<CharacterRelationship>()
                .HasOne(cr => cr.character)
                .WithMany(c => c.relationships)
                .HasForeignKey(cr => cr.characterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CharacterRelationship>()
                .HasOne(cr => cr.relatedCharacter)
                .WithMany()
                .HasForeignKey(cr => cr.relatedCharacterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Character>()
                 .HasKey(c => new { c.characterId });

            modelBuilder.Entity<Character>()
                .HasOne(c => c.characterKanji)
                .WithOne(ck => ck.character)
                .HasForeignKey<CharacterKanji>("characterId")
                .IsRequired();

           


        }
       
    }
}
