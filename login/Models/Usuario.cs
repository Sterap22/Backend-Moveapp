using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace login.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int tipoDoc { get; set; }        
        [Column(TypeName = "varchar(max)")]
        public string documento { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string correo { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string telefono { get; set; }
        public string perfil { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string clave { get; set; }
        public bool vigente { get; set; }
        public DateTime fechaCreacion { get; set; }
    }
}
