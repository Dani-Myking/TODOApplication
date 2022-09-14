using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TODOApplication.Models
{
    public class TODOCreateDTO
    {

        [Required]
        public string Tittel { get; set; }

        [Required]
        public DateTime Frist { get; set; }

        public Kategori Kategori { get; set; }

    }

}
