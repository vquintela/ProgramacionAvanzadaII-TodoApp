using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Models
{
    public class Tarea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TareaId { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "El nombre no puede tener mas de 8 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El nombre no puede tener mas de 30 caracteres.")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "El nombre no puede tener mas de 250 caracteres.")]
        public string Descripcion { get; set; }

        [Range(0, 10, ErrorMessage = "El valor ingresado {0}, debe de estar entre {1} y {2}.")]
        public int  Puntuacion_Dificultad { get; set; }

        [Required(ErrorMessage ="El estado es requerido")]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "Una tarea debe de estar asociada a una materia")]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }
    }
}
