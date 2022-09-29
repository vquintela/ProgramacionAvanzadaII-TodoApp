using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TodoApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TodoApp.Context
{
    public class TodoAppContext : DbContext
    {
        public TodoAppContext(DbContextOptions<TodoAppContext> options) : base(options)
        {
        }

        public DbSet<Tarea> tareas { get; set; }
        public DbSet<Materia> materias { get; set; }
    }
}
