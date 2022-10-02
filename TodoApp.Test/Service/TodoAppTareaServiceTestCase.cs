using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Context;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Test.MocksData;

namespace TodoApp.Test.Service
{
    public class TodoAppTareaServiceTestCase : IDisposable
    {
        protected readonly TodoAppContext _context;

        public TodoAppTareaServiceTestCase()
        {
            var options = new DbContextOptionsBuilder<TodoAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new TodoAppContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllTareas()
        {
            /// Arrange
            _context.materias.Add(TodoAppMateriaMockData.GetMateria());
            _context.tareas.AddRange(TodoAppTareaMockData.GetAllTareas());
            _context.SaveChanges();

            var service = new TareaService(_context);

            /// Act
            var result = await service.GetAllTareasAsync();

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(TodoAppTareaMockData.GetAllTareas().Count, result.Count);
        }

        [Fact]
        public async Task GetTareaByID()
        {
            /// Arrange
            _context.materias.Add(TodoAppMateriaMockData.GetMateria());
            _context.tareas.AddRange(TodoAppTareaMockData.GetAllTareas());
            _context.SaveChanges();

            var service = new TareaService(_context);

            /// Act
            var result = await service.GetTarea(1);

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.MateriaId);
            Assert.Equal("Tarea 9", result.Nombre);
            Assert.Equal("Estudiar acentos", result.Titulo);
            Assert.Equal("Estudiar todos los acentos enseñados en clase", result.Descripcion);
            Assert.Equal(5, result.Puntuacion_Dificultad);
            Assert.Equal(1, result.MateriaId);
        }

        [Fact]
        public async Task AddTarea()
        {
            /// Arrange
            _context.materias.Add(TodoAppMateriaMockData.GetMateria());
            _context.tareas.AddRange(TodoAppTareaMockData.GetAllTareas());
            _context.SaveChanges();

            var service = new TareaService(_context);

            /// Act
            await service.SaveTarea(TodoAppTareaMockData.GetTarea());
            var result = await service.GetAllTareasAsync();

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Last().TareaId);
            Assert.Equal("Tarea 2", result.Last().Nombre);
            Assert.Equal("Estudiar", result.Last().Titulo);
            Assert.Equal("Estudiar algo", result.Last().Descripcion);
            Assert.Equal(8, result.Last().Puntuacion_Dificultad);
            Assert.Equal(1, result.Last().MateriaId);
        }

        [Fact]
        public async Task PutTarea()
        {
            /// Arrange
            _context.materias.Add(TodoAppMateriaMockData.GetMateria());
            _context.tareas.AddRange(TodoAppTareaMockData.GetAllTareas());
            _context.SaveChanges();

            var service = new TareaService(_context);

            Tarea tarea = await service.GetTarea(2);
            tarea.Nombre = "edit1";
            tarea.Titulo = "edit2";
            tarea.Descripcion = "edit3";

            /// Act
            await service.PutTarea(2, tarea);
            var result = await service.GetTarea(2);

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TareaId);
            Assert.Equal("edit1", result.Nombre);
            Assert.Equal("edit2", result.Titulo);
            Assert.Equal("edit3", result.Descripcion);
        }

        [Fact]
        public async Task DeleteTarea()
        {
            /// Arrange
            _context.materias.Add(TodoAppMateriaMockData.GetMateria());
            _context.tareas.AddRange(TodoAppTareaMockData.GetAllTareas());
            _context.SaveChanges();

            var service = new TareaService(_context);

            var result = await service.GetAllTareasAsync();
            Assert.Equal(2, result.Count);

            /// Act
            await service.DeleteTarea(2);
            result = await service.GetAllTareasAsync();

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
