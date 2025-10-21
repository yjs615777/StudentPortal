using StudentPortal.Common;
using StudentPortal.Contracts.Requests;
using StudentPortal.Contracts.Responses;

namespace StudentPortal.Services
{
    public interface IStudentService
    {
        Task<Result<IReadOnlyList<StudentResponse>>> GetAllAsync(CancellationToken ct = default);
        Task<Result<StudentResponse>> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Result<int>> CreateAsync(StudentCreateRequest req, CancellationToken ct = default);
        Task<Result<StudentResponse>> UpdateAsync(StudentUpdateRequest req, int id, CancellationToken ct = default);
        Task<Result> DeleteAsync(int id, CancellationToken ct = default);
    }
}
