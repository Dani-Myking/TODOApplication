using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TODOApplication.Models
{
    public class TODOUpdateDTO
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Tittel { get; set; }

        [Required]
        public DateTime Frist { get; set; }

        [Required]
        public Kategori Kategori { get; set; }

        [Required]
        public bool Utfort { get; set; }

    }
}
