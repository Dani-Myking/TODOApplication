using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace TODOApplication.Models
{
    public class TODO
    {

        public TODO()
        {

        }

        public TODO(TODOCreateDTO dto)
        {
            Tittel = dto.Tittel;
            Frist = dto.Frist;
            Kategori = dto.Kategori;
        }
        public TODO(TODOUpdateDTO dto)
        {
            Id = dto.Id;
            Tittel = dto.Tittel;
            Frist = dto.Frist;
            Kategori = dto.Kategori;
            Utfort = dto.Utfort;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(80)]
        public string Tittel { get; set; }
        public DateTime Opprettelse { get; set; }
        public DateTime Frist { get; set; }
        
        public Kategori Kategori { get; set; }
        public bool Utfort { get; set; }

    }
}
