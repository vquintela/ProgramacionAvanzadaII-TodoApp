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
    public class MateriasController : ControllerBase
    {
        private readonly TodoAppContext _context;

        public MateriasController(TodoAppContext context)
        {
            _context = context;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> Getmateria()
        {
            try
            {
                return await _context.materias.Include(d => d.Tareas).ToListAsync();
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

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMateria(long id)
        {
            try
            {
                var materia = await _context.materias.Include(d => d.Tareas).FirstOrDefaultAsync(p => p.MateriaId == id);

                if (materia == null)
                {
                    return NotFound(
                        new ErrorResponse()
                        {
                            Codigo = 404,
                            Error = "Materia no encontrada"
                        }
                    );
                }

                return materia;
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

        // PUT: api/Materias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(long id, Materia materia)
        {
            if (id != materia.MateriaId)
            {
                return BadRequest(
                    new ErrorResponse()
                    {
                        Codigo = 400,
                        Error = "Id de la materia no corresponde con el id enviado"
                    }
                );
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Ok");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(id))
                {
                    return NotFound(
                        new ErrorResponse()
                        {
                            Codigo = 404,
                            Error = "Materia no encontrada"
                        }
                    );
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Materias
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMateria(Materia materia)
        {
            try
            {
                _context.materias.Add(materia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMateria", new { id = materia.MateriaId }, materia);
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

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            try
            {
                var materia = await _context.materias.FindAsync(id);
                if (materia == null)
                {
                    return NotFound(
                       new ErrorResponse()
                       {
                           Codigo = 404,
                           Error = "Materia no encontrada"
                       }
                    );
                }

                _context.materias.Remove(materia);
                await _context.SaveChangesAsync();

                return Ok(id);
            } 
            catch(Exception e)
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

        private bool MateriaExists(long id)
        {
            return _context.materias.Any(e => e.MateriaId == id);
        }
    }
}
