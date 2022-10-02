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
using Xunit;

namespace TodoApp.Test.Service
{
    public class TodoAppMateriaServiceTestCase : IDisposable
    {
        protected readonly TodoAppContext _context;

        public TodoAppMateriaServiceTestCase()
        {
            var options = new DbContextOptionsBuilder<TodoAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new TodoAppContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllMaterias()
        {
            /// Arrange
            _context.materias.AddRange(TodoAppMateriaMockData.GetAllMaterias());
            _context.SaveChanges();

            var service = new MateriaService(_context);

            /// Act
            var result = await service.GetAllMateriasAsync();

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(TodoAppMateriaMockData.GetAllMaterias().Count, result.Count);
        }

        [Fact]
        public async Task GetMateriaByID()
        {
            /// Arrange
            _context.materias.AddRange(TodoAppMateriaMockData.GetAllMaterias());
            _context.SaveChanges();

            var service = new MateriaService(_context);

            /// Act
            var result = await service.GetMateria(1);

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.MateriaId);
            Assert.Equal("Lengu", result.Nombre);
            Assert.Equal("Tareas de la materia Lengu", result.Descripcion);
        }

        [Fact]
        public async Task AddMateria()
        {
            /// Arrange
            _context.materias.AddRange(TodoAppMateriaMockData.GetAllMaterias());
            _context.SaveChanges();

            var service = new MateriaService(_context);

            /// Act
            await service.SaveMateria(TodoAppMateriaMockData.GetMateria());
            var result = await service.GetAllMateriasAsync();

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Last().MateriaId);
            Assert.Equal("Fisica", result.Last().Nombre);
            Assert.Equal("Tareas de la materia Fisica", result.Last().Descripcion);
        }

        [Fact]
        public async Task PutMateria()
        {
            /// Arrange
            _context.materias.AddRange(TodoAppMateriaMockData.GetAllMaterias());
            _context.SaveChanges();

            var service = new MateriaService(_context);

            Materia editMateria = await service.GetMateria(2);
            editMateria.Nombre = "Fisica";
            editMateria.Descripcion = "Tareas de la materia Fisica";

            /// Act
            await service.PutMateria(2, editMateria);
            var result = await service.GetMateria(2);

            /// Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.MateriaId);
            Assert.Equal("Fisica", result.Nombre);
            Assert.Equal("Tareas de la materia Fisica", result.Descripcion);
        }

        [Fact]
        public async Task DeleteMateria()
        {
            /// Arrange
            _context.materias.AddRange(TodoAppMateriaMockData.GetAllMaterias());
            _context.SaveChanges();

            var service = new MateriaService(_context);

            var result = await service.GetAllMateriasAsync();
            Assert.Equal(2, result.Count);

            /// Act
            await service.DeleteMateria(2);
            result = await service.GetAllMateriasAsync();

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
