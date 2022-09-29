using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Context;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly TodoAppContext _context;

        public TareasController(TodoAppContext context)
        {
            _context = context;
        }

        // GET: api/Tareas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> Gettarea()
        {
            try
            {
                return await _context.tareas.Include(d => d.Materia).ToListAsync();
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
        public async Task<ActionResult<Tarea>> GetTarea(long id)
        {
            try
            {
                var tarea = await _context.tareas.Include(d => d.Materia).FirstOrDefaultAsync(p => p.TareaId == id);

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

                return tarea;
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

            _context.Entry(tarea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("OK");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    return NotFound(
                        new ErrorResponse() 
                        { 
                            Codigo = 404, 
                            Error = "Tarea no encontrada" 
                        }
                    );
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            try
            {
                _context.tareas.Add(tarea);
                await _context.SaveChangesAsync();

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
                var tarea = await _context.tareas.FindAsync(id);
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

                _context.tareas.Remove(tarea);
                await _context.SaveChangesAsync();

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

        private bool TareaExists(long id)
        {
            return _context.tareas.Any(e => e.TareaId == id);
        }
    }
}
