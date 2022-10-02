using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Controllers;
using TodoApp.Models;
using TodoApp.Services;
using TodoApp.Test.MocksData;

namespace TodoApp.Test.Controller
{
    public class TodoAppTareaControllerTestCase
    {
        [Fact]
        public async Task GetTareas()
        {
            /// Arrange
            var todoService = new Mock<ITareaService>();
            todoService.Setup(_ => _.GetAllTareasAsync()).ReturnsAsync(TodoAppTareaMockData.GetAllTareas());
            var controller = new TareasController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await controller.GetTareas();

            /// Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetTarea()
        {
            /// Arrange
            var todoService = new Mock<ITareaService>();
            todoService.Setup(_ => _.GetTarea(1)).ReturnsAsync(TodoAppTareaMockData.GetTareaId());
            var controller = new TareasController(todoService.Object);

            /// Act
            var result = (OkObjectResult)await controller.GetTareas();

            /// Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AddTarea()
        {
            /// Arrange
            var todoService = new Mock<ITareaService>();
            var tarea = TodoAppTareaMockData.GetTarea();
            var controller = new TareasController(todoService.Object);

            /// Act
            var result = await controller.PostTarea(tarea);

            /// Assert
            todoService.Verify(_ => _.SaveTarea(tarea), Times.Exactly(1));
        }

        [Fact]
        public async Task PutTarea()
        {
            /// Arrange
            var todoService = new Mock<ITareaService>();
            var tarea = TodoAppTareaMockData.GetTareaId();
            var controller = new TareasController(todoService.Object);

            /// Act
            var result = await controller.PutTarea(tarea.TareaId, tarea);

            /// Assert
            todoService.Verify(_ => _.PutTarea(tarea.TareaId, tarea), Times.Exactly(1));
        }

        [Fact]
        public async Task DeleteTarea()
        {
            /// Arrange
            var todoService = new Mock<ITareaService>();
            var tarea = TodoAppTareaMockData.GetTareaId();
            todoService.Setup(_ => _.GetTarea(1)).ReturnsAsync(tarea);
            var controller = new TareasController(todoService.Object);

            /// Act
            var result = await controller.DeleteTarea(tarea.TareaId);

            /// Assert
            todoService.Verify(_ => _.DeleteTarea(tarea.TareaId), Times.Exactly(1));
        }
    }
}
