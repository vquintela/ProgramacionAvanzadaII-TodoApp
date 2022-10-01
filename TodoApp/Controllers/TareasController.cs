using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Context;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            try
            {
                return Ok(await _tareaService.GetAllTareasAsync());
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ErrorResponse()
                    {
                        Codigo = 400,
                        Error = e.Message
                    }
                );
            }
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTarea(long id)
        {
            try
            {
                var tarea = await _tareaService.GetTarea(id);

                if (tarea == null)
                {
                    return NotFound(
                       new ErrorResponse()
                       {
                           Codigo = 404,
                           Error = "Tarea no encontrada"
                       }
                   );
                }

                return Ok(tarea);
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ErrorResponse()
                    {
                        Codigo = 400,
                        Error = e.Message
                    }
                );
            }
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(long id, Tarea tarea)
        {
            if (id != tarea.TareaId)
            {
                return BadRequest(
                    new ErrorResponse()
                    {
                        Codigo = 400,
                        Error = "Id de la tarea no corresponde con el id enviado"
                    }
                );
            }

            try
            {
                await _tareaService.PutTarea(id, tarea);
                return Ok("OK");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(
                    new ErrorResponse()
                    {
                        Codigo = 500,
                        Error = "Error al actualizar la base de datos"
                    }
                );
            }
        }

        // POST: api/Tareas
        [HttpPost]
        public async Task<IActionResult> PostTarea(Tarea tarea)
        {
            try
            {
                await _tareaService.SaveTarea(tarea);

                return CreatedAtAction("GetTarea", new { id = tarea.TareaId }, tarea);
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ErrorResponse()
                    {
                        Codigo = 400,
                        Error = e.Message
                    }
                );
            }
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            try
            {
                var tarea = await _tareaService.GetTarea(id);
                if (tarea == null)
                {
                    return NotFound(
                       new ErrorResponse()
                       {
                           Codigo = 404,
                           Error = "Tarea no encontrada"
                       }
                   );
                }

                await _tareaService.DeleteTarea(id);

                return Ok("OK");
            }
            catch (Exception e)
            {
                return BadRequest(
                    new ErrorResponse()
                    {
                        Codigo = 400,
                        Error = e.Message
                    }
                );
            }
        }
    }
}
