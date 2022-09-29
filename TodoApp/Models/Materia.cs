using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MateriaId { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "El nombre no puede tener mas de 15 caracteres.")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La descripcion no puede tener mas de 50 caracteres.")]
        public string Descripcion { get; set; }
        public List<Tarea>? Tareas { get; set; }
    }
}
