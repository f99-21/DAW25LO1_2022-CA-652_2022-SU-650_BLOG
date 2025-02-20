using System.ComponentModel.DataAnnotations;

namespace LO1_2022_CA_652_2022_SU_650.Models
{
    public class publicaciones
    {
        [Key]
        public int publicacionId { get; set; }
        public string titulo {  get; set; }
        public string descripcion { get; set; }
        public int usuarioId { get; set; }
    }
}
