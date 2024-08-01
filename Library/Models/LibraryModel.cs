using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    [Index(nameof(Ganre), IsUnique = true)]

    public class LibraryModel
    {
        public long Id { get; set; }

        [Required, StringLength(50, MinimumLength =4)]
        public string Ganre { get; set; } = string.Empty;
        public List<ShelfModel> Shelves { get; set; } = [];
    }
}
