using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Context;
using TodoApp.Controllers;
using TodoApp.Services;
using TodoApp.Test.MocksData;

namespace TodoApp.Test.Controller
{
    public class TodoAppMateriaControllerTestCase
    {
        [Fact]
        public async Task GetMaterias()
        {
            /// Arrange
            var todoService = new Mock<IMateriaService>();
            todoService.Setup(_ => _.GetAllMateriasAsync()).ReturnsAsync(TodoAppMateriaMockData.GetAllMaterias());
            var controller = new MateriasController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await controller.GetMaterias();

            /// Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetMateria()
        {
            /// Arrange
            var todoService = new Mock<IMateriaService>();
            todoService.Setup(_ => _.GetMateria(1)).ReturnsAsync(TodoAppMateriaMockData.GetMateriaId());
            var controller = new MateriasController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await controller.GetMateria(1);

            /// Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AddMateria()
        {
            /// Arrange
            var todoService = new Mock<IMateriaService>();
            var materia = TodoAppMateriaMockData.GetMateria();
            var controller = new MateriasController(todoService.Object);

            /// Act
            var result = await controller.PostMateria(materia);

            /// Assert
            todoService.Verify(_ => _.SaveMateria(materia), Times.Exactly(1));
        }

        [Fact]
        public async Task PutMateria()
        {
            /// Arrange
            var todoService = new Mock<IMateriaService>();
            var materia = TodoAppMateriaMockData.GetMateriaId();
            var controller = new MateriasController(todoService.Object);

            /// Act
            var result = await controller.PutMateria(materia.MateriaId, materia);

            /// Assert
            todoService.Verify(_ => _.PutMateria(materia.MateriaId, materia), Times.Exactly(1));
        }

        [Fact]
        public async Task DeleteMateria()
        {
            /// Arrange
            var todoService = new Mock<IMateriaService>();
            var materia = TodoAppMateriaMockData.GetMateriaId();
            todoService.Setup(_ => _.GetMateria(1)).ReturnsAsync(materia);
            var controller = new MateriasController(todoService.Object);

            /// Act
            var result = await controller.DeleteMateria(materia.MateriaId);

            /// Assert
            todoService.Verify(_ => _.DeleteMateria(materia.MateriaId), Times.Exactly(1));
        }
    }
}
