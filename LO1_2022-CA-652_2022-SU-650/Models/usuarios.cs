using System.ComponentModel.DataAnnotations;

namespace LO1_2022_CA_652_2022_SU_650.Models
{
    public class usuarios
    {
        [Key]

        public int usuarioId { get; set; }

        public int rolId { get; set; }

        public string nombreUsuario { get; set; }

        public string clave {  get; set; }

        public string nombre {  get; set; }

        public string apellido { get; set; }
    }
}
