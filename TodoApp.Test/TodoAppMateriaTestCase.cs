using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoApp.Context;
using TodoApp.Controllers;
using TodoApp.Models;
using TodoApp.Test.MocksData;

namespace TodoApp.Test
{
    public class TodoAppMateriaTestCase
    {
        private Fixture _fixture;
        protected readonly TodoAppContext _context;

        public TodoAppMateriaTestCase()
        {
            var options = new DbContextOptionsBuilder<TodoAppContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new TodoAppContext(options);
            _context.Database.EnsureCreated();

            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn200Status()
        {
            _context.materias.Add(TodoAppMateriaMockData.GetMateria());

        //    MateriasController controller = new MateriasController(_context);
           // Task<ActionResult<IEnumerable<Materia>>> materias = controller.Getmateria();
           // Assert.NotEmpty((System.Collections.IEnumerable)materias);
            /*
            /// Arrange
            var todoService = new Mock<TodoAppContext>();
            todoService.Setup(p => p.materias.ToListAsync()).ReturnsAsync(TodoAppMateriaMockData.GetAllMaterias());
            var sut = new MateriasController(todoService.);

            /// Act
            var result = (OkObjectResult)await sut.GetAllAsync();


            // /// Assert
            result.StatusCode.Should().Be(200);
            */
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}