using System.ComponentModel.DataAnnotations;

namespace TODOApplication.Models
{
    public class Kategori
    {

        public Kategori()
        {

        }

        public Kategori(KategoriCreateDTO dto)
        {

            Tittel = dto.Tittel;

        }


        [Key]
        public int Id { get; set; }

        public string Tittel { get; set; }

    }
}
