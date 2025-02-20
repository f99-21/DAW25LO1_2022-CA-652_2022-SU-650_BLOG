using System.ComponentModel.DataAnnotations;

namespace LO1_2022_CA_652_2022_SU_650.Models
{
    public class comentarios
    {
        [Key]
        public int cometarioId { get; set; }
        public int publicacionId { get; set; }
        public string comentario { get; set; }
        public int usuarioId { get; set; }

    }
}
