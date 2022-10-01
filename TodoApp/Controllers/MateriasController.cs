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
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriasController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<IActionResult> GetMaterias()
        {
            try
            {
                return Ok(await _materiaService.GetAllMateriasAsync());
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
        public async Task<IActionResult> GetMateria(long id)
        {
            try
            {
                var materia = await _materiaService.GetMateria(id);

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

                return Ok(materia);
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

            try
            {
                await _materiaService.PutMateria(id, materia);
                return Ok("Ok");
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

        // POST: api/Materias
        [HttpPost]
        public async Task<IActionResult> PostMateria(Materia materia)
        {
            try
            {
                await _materiaService.SaveMateria(materia);

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
                var materia = await _materiaService.GetMateria(id);
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

                await _materiaService.DeleteMateria(id);

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
    }
}
