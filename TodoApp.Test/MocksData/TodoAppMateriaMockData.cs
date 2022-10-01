using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Test.MocksData
{
    internal class TodoAppMateriaMockData
    {
        public static List<Materia> GetAllMaterias()
        {
            return new List<Materia>
            {
                new Materia
                {
                    Nombre = "Lengu",
                    Descripcion = "Tareas de la materia Lengu"
                },
                new Materia
                {
                    Nombre = "Matematicas",
                    Descripcion = "Tareas de la materia matematicas"
                }
            };
        }

        public static Materia GetMateria()
        {
            return new Materia
            {
                Nombre = "Lengu",
                Descripcion = "Tareas de la materia Lengu"
               
            };
        }
    }
}
