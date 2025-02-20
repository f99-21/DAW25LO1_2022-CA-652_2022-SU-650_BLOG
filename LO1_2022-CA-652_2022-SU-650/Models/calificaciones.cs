using System.ComponentModel.DataAnnotations;

namespace LO1_2022_CA_652_2022_SU_650.Models
{
    public class calificaciones
    {
        [Key]
        public int calificacionId { get; set; }
        public int publicacionId { get; set; }
        public int usuarioId { get; set; }
        public int calificacion {  get; set; }
    }
}
