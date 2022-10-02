using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Test.MocksData
{
    internal class TodoAppTareaMockData
    {
        public static List<Tarea> GetAllTareas()
        {
            return new List<Tarea>
            {
                new Tarea
                {
                    Nombre = "Tarea 9",
                    Titulo = "Estudiar acentos",
                    Descripcion = "Estudiar todos los acentos enseñados en clase",
                    Puntuacion_Dificultad = 5,
                    Estado = 0,
                    MateriaId = 1
                },
                new Tarea
                {
                    Nombre = "Tarea 10",
                    Titulo = "Estudiar algo",
                    Descripcion = "Estudiar algode todo",
                    Puntuacion_Dificultad = 2,
                    Estado = 0,
                    MateriaId = 1
                }
            };
        }

        public static Tarea GetTarea()
        {
            return new Tarea
            {
                Nombre = "Tarea 2",
                Titulo = "Estudiar",
                Descripcion = "Estudiar algo",
                Puntuacion_Dificultad = 8,
                Estado = 0,
                MateriaId = 1
            };
        }

        public static Tarea GetTareaId()
        {
            return new Tarea
            {
                TareaId = 1,
                Nombre = "Tarea 9",
                Titulo = "Estudiar acentos",
                Descripcion = "Estudiar todos los acentos enseñados en clase",
                Puntuacion_Dificultad = 5,
                Estado = 0,
                MateriaId = 1
            };
        }
    } 
}
