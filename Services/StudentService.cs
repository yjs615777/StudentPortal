using AutoMapper;
using StudentPortal.Common;
using StudentPortal.Contracts.Requests;
using StudentPortal.Contracts.Responses;
using StudentPortal.Entities;
using StudentPortal.Repositories;

namespace StudentPortal.Services
{
    public class StudentService(IStudentRepository repo, IMapper mapper) : IStudentService
    {
        private readonly IStudentRepository _repo = repo;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<IReadOnlyList<StudentResponse>>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _repo.GetAllAsync(ct);
            var dto = _mapper.Map<IReadOnlyList<StudentResponse>>(list);
            return Result<IReadOnlyList<StudentResponse>>.Ok(dto);
        }

        public async Task<Result<StudentResponse>> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<StudentResponse>.Fail("NotFound");

            var dto = _mapper.Map<StudentResponse>(entity);
            return Result<StudentResponse>.Ok(dto);
        }

        public async Task<Result<int>> CreateAsync(StudentCreateRequest req, CancellationToken ct = default)
        {
            var entity = _mapper.Map<Student>(req);
            await _repo.AddAsync(entity, ct);
            return Result<int>.Ok(entity.Id);
        }

        public async Task<Result<StudentResponse>> UpdateAsync(StudentUpdateRequest req,int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result<StudentResponse>.Fail("NotFound");

            _mapper.Map(req, entity);               // 변경 필드 매핑
            await _repo.UpdateAsync(entity, ct);

            var dto = _mapper.Map<StudentResponse>(entity);
            return Result<StudentResponse>.Ok(dto);
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity is null) return Result.Fail("NotFound");

            await _repo.DeleteAsync(id, ct);
            return Result.Ok();
        }
    }
}
