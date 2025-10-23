using StudentPortal.Entities;

namespace StudentPortal.Repositories
{
    public interface IStudentRepository
    {
        Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken ct = default);
        Task<Student?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Student entity,CancellationToken ct = default);
        Task UpdateAsync(Student entity,CancellationToken ct = default);
        Task DeleteAsync(int id,CancellationToken ct = default);
    }
}
