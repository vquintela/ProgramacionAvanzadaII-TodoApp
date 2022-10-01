using Microsoft.EntityFrameworkCore;
using System.Threading;
using TodoApp.Context;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TareaService : ITareaService
    {
        private readonly TodoAppContext _context;

        public TareaService(TodoAppContext context)
        {
            _context = context;
        }

        public async Task DeleteTarea(int id)
        {
            var tarea = await _context.tareas.FindAsync(id);
            _context.tareas.Remove(tarea);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tarea>> GetAllTareasAsync()
        {
            return await _context.tareas.Include(d => d.Materia).ToListAsync();
        }

        public async Task<Tarea?> GetTarea(long id)
        {
            return await _context.tareas.Include(d => d.Materia).FirstOrDefaultAsync(p => p.TareaId == id);
        }

        public async Task PutTarea(long id, Tarea tarea)
        {
            _context.Entry(tarea).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveTarea(Tarea tarea)
        {
            _context.tareas.Add(tarea);
            await _context.SaveChangesAsync();
        }
    }
}
