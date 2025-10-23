using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Entities;

namespace StudentPortal.Repositories
{
    public class StudentRepository(SchoolContext db) : IStudentRepository
    {
        public SchoolContext _db = db;

        public async Task<IReadOnlyList<Student>> GetAllAsync(CancellationToken ct = default)
        =>await _db.Students.AsNoTracking().OrderBy(s => s.Age).ToListAsync(ct);
        
        public Task<Student?> GetByIdAsync(int id, CancellationToken ct = default)
        => _db.Students.FindAsync([id], ct).AsTask();
        public async Task AddAsync(Student entity,CancellationToken ct=default)
        {
            await _db.Students.AddAsync(entity, ct);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Student entity,CancellationToken ct = default)
        {
            _db.Students.Update(entity);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id,CancellationToken ct = default)
        {
            var delete = await _db.Students.FindAsync([id], ct);
            if (delete == null)
                return;
            await _db.SaveChangesAsync();
        }

        
    }
}
