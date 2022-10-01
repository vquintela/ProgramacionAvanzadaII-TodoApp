using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITareaService
    {
        Task<List<Tarea>> GetAllTareasAsync();

        Task SaveTarea(Tarea tarea);

        Task<Tarea?> GetTarea(long id);

        Task PutTarea(long id, Tarea tarea);

        Task DeleteTarea(int id);
    }
}
