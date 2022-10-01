using TodoApp.Models;

namespace TodoApp.Services
{
    public interface IMateriaService
    {
        Task<List<Materia>> GetAllMateriasAsync();

        Task SaveMateria(Materia materia);

        Task<Materia?> GetMateria(long id);

        Task PutMateria(long id, Materia materia);

        Task DeleteMateria(int id);
    }
}
