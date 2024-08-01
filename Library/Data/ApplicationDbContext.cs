using System;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options
           ) : base(options)
        {
            Seed();
        }

        private void Seed()
        {
            if (Library.IsNullOrEmpty())
            {
                List<LibraryModel> librarys = [
                    new ()
                    {
                        Ganre = "Torah",
                        Shelves = [
                            new()
                            {
                                Hight = 30,
                                Width = 80,
                                Sets = [
                                    new()
                                    {
                                        SetName ="Humash",
                                        Books = [
                                            new()
                                            {
                                                Title = "Bereshit",
                                                Ganre = "Torah",
                                                Hight = 25,
                                                Width = 3
                                            },

                                            new()
                                            {
                                                Title = "Shmot",
                                                Ganre = "Torah",
                                                Hight = 25,
                                                Width = 3
                                            },

                                            new()
                                            {
                                                Title = "Vaickra",
                                                Ganre = "Torah",
                                                Hight = 25,
                                                Width = 3
                                            },

                                            new()
                                            {
                                                Title = "Bamidbar",
                                                Ganre = "Torah",
                                                Hight = 25,
                                                Width = 3
                                            },

                                            new()
                                            {
                                                Title = "Dvarim",
                                                Ganre = "Torah",
                                                Hight = 25,
                                                Width = 3
                                            }
                                            ]
                                    }
                                    ]
                            }
                            ]
                    }
                ];
                Library.AddRange( librarys );
                SaveChanges();
            }
        }

        public DbSet<LibraryModel> Library { get; set; }
        public DbSet<ShelfModel> Shelves { get; set; }
        public DbSet<SetModel> Sets { get; set; }
        public DbSet<BookModel> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibraryModel>()
               .HasMany(library => library.Shelves)
               .WithOne(shelf => shelf.Library)
               .HasForeignKey(shelf => shelf.LibraryId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShelfModel>()
               .HasMany(shelf => shelf.Sets)
               .WithOne(set => set.Shelf)
               .HasForeignKey(set => set.ShelfId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SetModel>()
               .HasMany(set => set.Books)
               .WithOne(book => book.Set)
               .HasForeignKey(book => book.SetId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
