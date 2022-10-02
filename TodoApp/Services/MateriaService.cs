using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;
using TodoApp.Context;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly TodoAppContext _context;

        public MateriaService(TodoAppContext context)
        {
            _context = context;
        }

        public async Task DeleteMateria(int id)
        {
            var materia = await _context.materias.FindAsync(id);
            _context.materias.Remove(materia);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Materia>> GetAllMateriasAsync()
        {
            return await _context.materias.Include(d => d.Tareas).ToListAsync();
        }

        public async Task<Materia?> GetMateria(long id)
        {
            return await _context.materias.Include(d => d.Tareas).FirstOrDefaultAsync(p => p.MateriaId == id);
        }

        public async Task PutMateria(int id, Materia materia)
        {
            _context.Entry(materia).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveMateria(Materia materia)
        {
            _context.materias.Add(materia);
            await _context.SaveChangesAsync();
        }
    }
}
