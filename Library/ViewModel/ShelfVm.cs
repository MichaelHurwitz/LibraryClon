using System.ComponentModel.DataAnnotations;

namespace Library.ViewModel
{
    public class ShelfVm
    {
        public long Id { get; set; }

        [Required]
        public int Hight { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public long LibraryId { get; set; }
    }
}
